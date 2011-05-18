using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using IdSharp.Tagging.ID3v2;
using MostlyMusic;

namespace ReTaggin
{
    public interface IReTag
    {
        List<FileInfo> getMp3Files(string dir);
        /// <summary>
        /// Returns a set (no doubles) of all the albums
        /// in a given list of FileInfo objects.
        /// </summary>
        /// <param name="fi">A list of FileInfo objects.</param>
        /// <returns>A sorted set of Album titles.</returns>
        SortedSet<string> getAllAlbums(List<FileInfo> fi);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="fi"></param>
        /// <returns></returns>
        List<IDPair> getIDObjects(List<FileInfo> fi);
        List<IDPair> getIDObjOfAlbum(List<IDPair> il, string album);
        List<IDPair> updateFilesOfAlbum(List<IDPair> il, List<MostlyMusicTrack> tl);


    }
}
