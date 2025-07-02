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

        public void Draw(Graphics g, int xPos, Clef clef, Color? color)
        {
            Color cursorColor = Color.FromArgb(120, 0, 255, 0);
            using Pen p = new Pen(cursorColor, MusicDrawer.Unit / 10);
            g.DrawLine(p, xPos, MusicDrawer.TopLinePosition, xPos, MusicDrawer.BottomLinePosition);
        }

        string Symbol.StringForFileExport()
        {
            throw new NotImplementedException();
        }
    }
}
