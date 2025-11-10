using NotePractice.Music.Drawing;
using NotePractice.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotePractice.Music.Symbols
{
    public class ClefSymbol : Symbol
    {
        public Clef ClefType { get; set; }
        public ClefSymbol(Clef clef)
        {
            ClefType = clef;
        }
        public void Draw(Graphics g, int xPos, Clef clef, Color? color)
        {
            Bitmap treble = Resources.treble_clef_nolines;
            Bitmap bass = Resources.bass_clef_nolines;
            if (ClefType == Clef.Treble)
            {
                int y = (int)(MusicDrawer.TopLinePosition - MusicDrawer.LineSpacing * 1.4);
                int width = (int)(MusicDrawer.LineSpacing * 2.5);
                int height = MusicDrawer.LineSpacing * 7;
                g.DrawImage(treble, xPos, y, width, height);
            }
            else if (ClefType == Clef.Bass)
            {
                int y = MusicDrawer.TopLinePosition;
                int width = (int)(MusicDrawer.LineSpacing * 2.7);
                int height = MusicDrawer.LineSpacing * 7 / 2;
                g.DrawImage(bass, xPos, y, width, height);
            }
        }

        string Symbol.StringForFileExport()
        {
            throw new NotImplementedException();
        }
    }
}
