using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;
using System.Threading;
using System.Windows.Forms;
using System.ComponentModel;
using System.Drawing;
using System.Net;


 namespace MostlyMusic
{
    public class MostlyMusicAdapter
    {
        public event EventHandler DownloadFinished;
        public event EventHandler TrackDownloadFinished;
        private List<MostlyMusicQueryResponse> _responses;
        private string _query;
        private string _imageLink;
        private Image _albumArt;
        public Image AlbumArt
        {
            get { return _albumArt; }
        }
        

        public string ImageLink
        {
            get { return _imageLink; }
        }


        public string Query
        {
            get { return _query; }
            set
            {
                _query = value;
                searchForAlbum();
            }
        }
        public List<MostlyMusicQueryResponse> Responses
        {
            get { return _responses; }
            set { _responses = value; }
        }
        private MostlyMusicQueryResponse _album;

        public MostlyMusicQueryResponse Album
        {
            get { return _album; }
            set
            { 
                _album = value;
                downloadTracks();            
            }
        }
        
        private List<MostlyMusicTrack> _trackList;
        public static List<MostlyMusicTrack>  downloadTracksStat(MostlyMusicQueryResponse m)
        {
            HtmlWeb webHandler = new HtmlWeb();
            HtmlAgilityPack.HtmlDocument doc = webHandler.Load(m.AlbumLink);
            HtmlNode node = doc.DocumentNode;
            var s = node.SelectNodes("//tr[@class='info']");
            List<MostlyMusicTrack>  _trackList = (from p in s
                          select new MostlyMusicTrack
                          {
                              Number = (from g in p.DescendantNodes()
                                        where g.Attributes.FirstOrDefault(t => t.Value == "number") != null
                                        select int.Parse(g.InnerText.Replace(".", ""))).First(),
                              Title = p.DescendantNodes().Where(g => g.Name == "label").First().InnerText
                          }).ToList();
            return _trackList;

        }
        public List<MostlyMusicTrack> TrackList
        {
            get { return _trackList; }
        }

        public MostlyMusicAdapter()
        {
            
        }
        

        public bool InitializeComponent(string query)
        {
            _query = query;
            searchForAlbum();
            return true;
        }
        private void searchForAlbum()
        {
            HtmlNodeCollection s = null;
            List<string> strList = new List<string> (){ _query};
            strList.AddRange(_query.Split(' ').ToList());
            foreach (var item in strList)
            {
                HtmlWeb webHandler = new HtmlWeb();
                HtmlAgilityPack.HtmlDocument doc = webHandler.Load("http://www.mostlymusic.com/catalogsearch/result/?q=" + item);
                HtmlNode node = doc.DocumentNode;
                s = doc.DocumentNode.SelectNodes("//div[@class='product-name']");
                if (s != null)
                    break;
            }
            if (s == null)
                return;
            var d = (from p in s
                    select new MostlyMusicQueryResponse
                    {
                        AlbumTitle = p.FirstChild.Attributes.Where(c => c.Name == "title").First().Value,
                        AlbumLink = p.FirstChild.Attributes.Where(c => c.Name == "href").First().Value
                    }).ToList();
            _responses = d;
            //this.DownloadFinished(this, new EventArgs());            
        }
        private void downloadTracks()
        {
            if (_album == null)
                throw new MMAdapterException { problem = MMAdapterExceptions.AlbumNotSet };
            BackgroundWorker BW = new BackgroundWorker();
            BW.DoWork += new DoWorkEventHandler(BW_DoWork);
            BW.RunWorkerCompleted += new RunWorkerCompletedEventHandler(BW_RunWorkerCompleted);
            BW.RunWorkerAsync(_album);           
        }

        void BW_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            tmpCont t = e.Result as tmpCont;
            _trackList = t.tl;
            _imageLink = t.imgL;
            _albumArt = t.img;
            if (TrackDownloadFinished != null)
                TrackDownloadFinished(this, new EventArgs());         

        }

        void BW_DoWork(object sender, DoWorkEventArgs e)
        {
            HtmlWeb webHandler = new HtmlWeb();
            MostlyMusicQueryResponse arg = e.Argument as MostlyMusicQueryResponse;
            HtmlAgilityPack.HtmlDocument doc = webHandler.Load(arg.AlbumLink);
            HtmlNode node = doc.DocumentNode;
            var s = node.SelectNodes("//tr[@class='info']");
            var tt = node.SelectNodes("//img[@alt='" + arg.AlbumTitle + "']").FirstOrDefault();
            string f = (from c in tt.Attributes
                       where c.Name == "src"
                       select c.Value).FirstOrDefault();
            WebRequest h = WebRequest.Create(f);
            WebResponse wresp = h.GetResponse();
            Image tmp = Image.FromStream(wresp.GetResponseStream());           
            List<MostlyMusicTrack> trackList = (from p in s
                          select new MostlyMusicTrack
                          {
                              Number = (from g in p.DescendantNodes()
                                        where g.Attributes.FirstOrDefault(t => t.Value == "number") != null
                                        select int.Parse(g.InnerText.Replace(".", ""))).First(),
                              Title = p.DescendantNodes().Where(g => g.Name == "label").First().InnerText
                          }).ToList();
            e.Result = new tmpCont() { tl = trackList, imgL = f, img = tmp };
        }

    }
    class tmpCont
    {
        public List<MostlyMusicTrack> tl;
        public string imgL;
        public Image img;
    }
}
