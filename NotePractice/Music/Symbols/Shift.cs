using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotePractice.Music.Symbols
{
    public class Shift : Symbol
    {
        public SymbolType Type { get => SymbolType.Shift; }

        public void Draw(Graphics g, int xPos, Clef clef = Clef.Treble)
        {
            //nothing for now
        }
    }
}
