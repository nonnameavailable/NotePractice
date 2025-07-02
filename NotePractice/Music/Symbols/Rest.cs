using NotePractice.Properties;
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
        public void Draw(Graphics g, int xPos, Clef clef, Color? color)
        {
            if (Duration == 1 || Duration == 2)
            {
                int width = MusicDrawer.LineSpacing * 3 / 2;
                int x = xPos - width / 2;
                int height = MusicDrawer.LineSpacing / 2;
                if (Duration == 1)
                {
                    int y = MusicDrawer.LineSpacing * 5;
                    g.FillRectangle(Brushes.Black, x, y, width, height);
                }
                else
                {
                    int y = MusicDrawer.LineSpacing * 6 - height;
                    g.FillRectangle(Brushes.Black, x, y, width, height);
                }
            }
            else
            {
                int width = MusicDrawer.LineSpacing;
                int height = MusicDrawer.LineSpacing * 3;
                int x = xPos - width / 2;
                int y = MusicDrawer.LineSpacing * 9 / 2;
                g.DrawImage(Resources.squiggle, x, y, width, height);
            }
        }

        string Symbol.StringForFileExport()
        {
            throw new NotImplementedException();
        }
    }
}
