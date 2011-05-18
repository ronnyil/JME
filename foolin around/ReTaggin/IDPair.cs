using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IdSharp.Tagging.ID3v2;

namespace ReTaggin
{
    public class IDPair
    {
        public string FullPath { get; set; }
        public string FileName { get; set; }
        public IID3v2 ID;
        public override string ToString()
        {
            return FileName + "    T: " + ID.Title + "     A: "+ID.Album+"    Ar: "+ ID.Artist;
        }
    }
}
