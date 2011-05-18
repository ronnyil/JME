using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MostlyMusic
{
    public class MostlyMusicQueryResponse
    {
        public string AlbumTitle { get; set; }
        public string AlbumLink { get; set; }
        public override string ToString()
        {
            return AlbumTitle;
        }
    }
}
