using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MostlyMusic
{
    public enum MMAdapterExceptions { AlbumNotSet };
    public class MMAdapterException : Exception
    {
        public MMAdapterExceptions problem { get; set; }
    }
}
