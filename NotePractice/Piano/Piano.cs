using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotePractice.Piano
{
    public class Piano
    {
        public int WhiteKeyWidth { get; set; }
        public int BlackKeyWidth { get; set; }
        public int WhiteKeyHeight { get; set; }
        public int BlackKeyHeight { get; set; }

        public Piano()
        {
            WhiteKeyWidth = 20;
            WhiteKeyHeight = 100;
            BlackKeyWidth = 12;
            BlackKeyHeight = 65;
        }
        public Bitmap PianoBitmap(Point mousePoint, out Note playedNote)
        {
            Bitmap result = new Bitmap(WhiteKeyWidth * 52, WhiteKeyHeight);
            using Graphics g = Graphics.FromImage(result);
            using Pen p = new Pen(Brushes.Black, WhiteKeyWidth * 0.1f);
            playedNote = null;
            for(int i = 0; i < 52; i++)
            {
                Rectangle whiteKey = new Rectangle(i * WhiteKeyWidth, 0, WhiteKeyWidth, WhiteKeyHeight);
                Rectangle blackKey = new Rectangle((int)(i * WhiteKeyWidth - BlackKeyWidth * 0.5), 0, BlackKeyWidth, BlackKeyHeight);
                Rectangle nextBlackKey = new Rectangle((int)((i + 1) * WhiteKeyWidth - BlackKeyWidth * 0.5), 0, BlackKeyWidth, BlackKeyHeight);
                bool whiteContains = whiteKey.Contains(mousePoint);
                bool blackContains = blackKey.Contains(mousePoint);
                bool nextBlackContains = nextBlackKey.Contains(mousePoint);
                bool skipBlack = (i + 5) % 7 == 3 || (i + 5) % 7 == 0 || i == 0;
                bool skipNextBlack = (i + 6) % 7 == 3 || (i + 6) % 7 == 0;
                if ((whiteContains && !blackContains && !nextBlackContains) || (whiteContains && skipBlack && !nextBlackContains) || (whiteContains && skipNextBlack && nextBlackContains))
                {
                    g.FillRectangle(Brushes.Purple, whiteKey);
                    playedNote = new Note((NoteLetter)((i + 5) % 7), (i + 5) / 7);
                }
                else
                {
                    g.FillRectangle(Brushes.White, whiteKey);
                }
                g.DrawRectangle(p, whiteKey);
                if (skipBlack) continue;
                if (blackContains)
                {
                    g.FillRectangle(Brushes.Purple, blackKey);
                    playedNote = new Note((NoteLetter)((i + 4) % 7), (i + 5) / 7, sharp: true);
                }
                else
                {
                    g.FillRectangle(Brushes.Black, blackKey);
                }
                g.DrawRectangle(p, blackKey);
            }
            return result;
        }

    }
}
