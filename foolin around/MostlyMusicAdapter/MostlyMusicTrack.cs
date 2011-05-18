using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MostlyMusic
{
    public class MostlyMusicTrack
    {
        public int Number { get; set; }
        public string Title { get; set; }
        public override string ToString()
        {
            return Number+ " -- " + Title;
        }
    }
}
