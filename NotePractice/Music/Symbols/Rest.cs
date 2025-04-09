using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotePractice.Music.Symbols
{
    public class Rest : Symbol
    {
        public SymbolType Type { get => SymbolType.Rest; }
        public int Duration { get; set; }
        public Rest(int duration)
        {
            Duration = duration;
        }
    }
}
