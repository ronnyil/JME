using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using IdSharp.Tagging.ID3v2;
using MostlyMusic;
using System.Drawing;

namespace ReTaggin
{
    public class ProgressEventArgs : EventArgs
    {
        public string File { get; set; }   
    }
    public class ReTag
    {
        private List<FileInfo> _completeFileList;
        /// <summary>
        /// A list of all the file information.
        /// </summary>
        public List<FileInfo> CompleteFileList
        {
            get { return _completeFileList; }
        }
        private SortedSet<string> _albumSet;

        public SortedSet<string> AlbumSet
        {
            get { return _albumSet; }
        }

        private List<IDPair> _completeIdList;

        public List<IDPair> CompleteIdList
        {
            get { return _completeIdList; }
        }
        private string _directory;
        public string Directory
        {
            get { return _directory; }
            set
            {
                _directory = value;
                InitializeComponent();
            }
        }
        private string _curAlbum;

        public string CurrentAlbum
        {
            get { return _curAlbum; }
            set 
            { 
                _curAlbum = value;
                setCurrentAlbumList();
            }
        }
        private List<IDPair> _curAlbumList;

        public List<IDPair> CurrentAlbumList
        {
            get { return _curAlbumList; }
        }
        public event EventHandler ProgressUpdate;
        public ReTag(string dir , int choice = 1)
        {
            _directory =  dir;
            InitializeComponent(choice);
        }
        /// <summary>
        /// Initailizes the object requiring this._directory to be set.
        /// </summary>
        private void InitializeComponent(int choice = 1)
        {
            if (_directory != null)
            {
                if(choice == 1)
                     getMp3Files();
                else
                {
                    getMp3FilesRecursive();
                }
                getIDObjects();
                getAllAlbums();
            }
        }
        /// <summary>
        /// Resets all the files with the correct album name (fetched from mostly music).
        /// </summary>
        /// <param name="alb">The new album name.</param>
        public void ResetAlbumName(string alb)
        {
            foreach (var item in _curAlbumList)
            {
                item.ID.Album = alb;
            }
            _curAlbum = alb;
        }
        private void getAllAlbums()
        {
            _albumSet = new SortedSet<string>();
            foreach (var item in _completeIdList)
            {
                
                _albumSet.Add(item.ID.Album);
            }          
        }


        private void getIDObjects()
        {
            _completeIdList = new List<IDPair>();
            foreach (var item in _completeFileList)
            {
                try
                {
                    IID3v2 tmp = ID3v2Helper.CreateID3v2(item.FullName);
                   
                    _completeIdList.Add(new IDPair { FileName = item.Name, FullPath = item.FullName, ID = tmp });
                }
                catch (Exception) { }
            }
        }


        private void setCurrentAlbumList()
        {
            _curAlbumList = (from p in _completeIdList
                     where p.ID.Album == _curAlbum
                     select p).ToList();
        }
        private bool replaceImage = false;
        private List<MostlyMusic.MostlyMusicTrack> tl;
        private Image img = null;
        public void prep(List<MostlyMusic.MostlyMusicTrack> tl1, bool replaceImage1 = false, Image img1 = null)
        {
            tl = tl1;
            replaceImage = replaceImage1;
            img = img1;
        }

        public void saveChangesByTrackList()
        {
            //resolved to be compatibale with iTunes-style track-number labeling
            foreach (var item in _curAlbumList)
            {
                if (ProgressUpdate != null)
                {
                    ProgressUpdate(this, new ProgressEventArgs { File = item.FileName });
                }
                int tmp;
                int trcknr;
                MostlyMusicTrack tmpTrack = null;

                bool res = int.TryParse(item.ID.TrackNumber, out tmp);
                if (!res)
                {
                    if (item.ID.TrackNumber.Where((c) => c == '/').FirstOrDefault() != default(char))
                    {
                        trcknr = int.Parse(item.ID.TrackNumber.Split('/')[0]);
                        tmpTrack = tl.Where(c => trcknr == c.Number).FirstOrDefault();
                    }
                    else
                    {
                        throw new Exception(item.ID.TrackNumber + "  is an invalid track number");
                    }
                }
                else
                {
                    trcknr = int.Parse(item.ID.TrackNumber);
                    tmpTrack = tl.Where(c => int.Parse(item.ID.TrackNumber) == c.Number).FirstOrDefault();
                }
                if (tmpTrack != null)
                {
                    item.ID.Title = tmpTrack.Title;
                    item.ID.TrackNumber = trcknr.ToString() + '/' + tl.Count.ToString();
                    if (replaceImage)
                    {
                        if (item.ID.PictureList.Count == 0)
                        {
                            item.ID.PictureList.AddNew();
                            item.ID.PictureList[0].Picture = img;

                        }
                        else
                        {
                            item.ID.PictureList.FirstOrDefault().Picture = img;
                        }
                    }
                    item.ID.Save(item.FullPath);
                }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        private void getMp3Files()
        {
            DirectoryInfo _di = new DirectoryInfo(Directory);
            List<FileInfo> retList = (from p in _di.EnumerateFiles()
                                 where p.Extension == ".mp3"
                                 select p).ToList();
            if (retList.Count == 0)
                throw new Exception("no mp3 files were found!");
            _completeFileList = retList;
        }
        private void getMp3FilesRecursive()
        {
            List<FileInfo> retList = new List<FileInfo>();
            Stack<DirectoryInfo> i = new Stack<DirectoryInfo>();
            i.Push(new DirectoryInfo(_directory));
            while (i.Count!=0)
            {
                DirectoryInfo d = i.Pop();
                foreach (var item in d.EnumerateDirectories())
                {
                    i.Push(item);
                }
                foreach (var item in d.EnumerateFiles())
                {
                    retList.Add(item);
                }                
            }
            _completeFileList = retList;
        }
    }
}
