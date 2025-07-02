using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotePractice.Music.Symbols
{
    public class BarLine : Symbol
    {
        public SymbolType Type { get => SymbolType.BarLine; }
        public void Draw(Graphics g, int xPos, Clef clef, Color? color)
        {
            g.DrawLine(MusicDrawer.LinePen, xPos, MusicDrawer.TopLinePosition, xPos, MusicDrawer.BottomLinePosition);
        }

        string Symbol.StringForFileExport()
        {
            throw new NotImplementedException();
        }
    }
}
