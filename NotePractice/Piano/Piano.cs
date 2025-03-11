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
        private const int WhiteKeyWidth = 20;
        private const int BlackKeyWidth = 12;
        private const int WhiteKeyHeight = 100;
        private const int BlackKeyHeight = 65;

        public Piano()
        {
        }
        public Bitmap PianoBitmap(Point mousePoint, out Note playedNote)
        {
            Bitmap helpBitmap = HelpBitmap();
            Bitmap result = new Bitmap(WhiteKeyWidth * 52, WhiteKeyHeight + helpBitmap.Height);
            using Graphics g = Graphics.FromImage(result);
            g.DrawImage(helpBitmap, 0, 0);
            using Pen p = new Pen(Brushes.Black, WhiteKeyWidth * 0.1f);
            playedNote = null;
            for(int i = 0; i < 52; i++)
            {
                Rectangle whiteKey = new Rectangle(i * WhiteKeyWidth, helpBitmap.Height, WhiteKeyWidth, WhiteKeyHeight);
                Rectangle blackKey = new Rectangle((int)(i * WhiteKeyWidth - BlackKeyWidth * 0.5), helpBitmap.Height, BlackKeyWidth, BlackKeyHeight);
                Rectangle nextBlackKey = new Rectangle((int)((i + 1) * WhiteKeyWidth - BlackKeyWidth * 0.5), helpBitmap.Height, BlackKeyWidth, BlackKeyHeight);
                bool whiteContains = whiteKey.Contains(mousePoint);
                bool blackContains = blackKey.Contains(mousePoint);
                bool nextBlackContains = nextBlackKey.Contains(mousePoint);
                bool skipBlack = (i + 5) % 7 == 3 || (i + 5) % 7 == 0 || i == 0;
                bool skipNextBlack = (i + 6) % 7 == 3 || (i + 6) % 7 == 0 || i == 51;
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
        private Bitmap HelpBitmap()
        {
            Bitmap result = new Bitmap(WhiteKeyWidth * 52, WhiteKeyHeight);
            Graphics g = Graphics.FromImage(result);
            for(int i = 0; i < 52; i++)
            {
                int octaveNo = (i + 12) / 7 - 1;
                int cVal = octaveNo * 30;
                Color c = ColorConvertor.HSBtoRGB(octaveNo / 8f, 1, 1); 
                using Brush brush = new SolidBrush(c);
                g.FillRectangle(brush, i * WhiteKeyWidth, 0, WhiteKeyWidth, WhiteKeyHeight);
            }
            return result;
        }
        public Note NoteUnderCursor(Point mousePoint)
        {
            int i = (int)(mousePoint.X / (WhiteKeyWidth * 52d) * 52);
            if (i > 51) return null;
            Rectangle whiteKey = new Rectangle(i * WhiteKeyWidth, WhiteKeyHeight, WhiteKeyWidth, WhiteKeyHeight);
            Rectangle blackKey = new Rectangle((int)(i * WhiteKeyWidth - BlackKeyWidth * 0.5), WhiteKeyHeight, BlackKeyWidth, BlackKeyHeight);
            Rectangle nextBlackKey = new Rectangle((int)((i + 1) * WhiteKeyWidth - BlackKeyWidth * 0.5), WhiteKeyHeight, BlackKeyWidth, BlackKeyHeight);
            bool whiteContains = whiteKey.Contains(mousePoint);
            bool blackContains = blackKey.Contains(mousePoint);
            bool nextBlackContains = nextBlackKey.Contains(mousePoint);
            bool skipBlack = (i + 5) % 7 == 3 || (i + 5) % 7 == 0 || i == 0;
            bool skipNextBlack = (i + 6) % 7 == 3 || (i + 6) % 7 == 0 || i == 51;
            Note result = null;
            if ((whiteContains && !blackContains && !nextBlackContains) || (whiteContains && skipBlack && !nextBlackContains) || (whiteContains && skipNextBlack && nextBlackContains))
            {
                result = new Note((NoteLetter)((i + 5) % 7), (i + 5) / 7);
            }
            if (skipBlack && skipNextBlack) return result;
            if (blackContains && !skipBlack) 
            {
                result = new Note((NoteLetter)((i + 4) % 7), (i + 5) / 7, sharp: true);
            }
            if (nextBlackContains && !skipNextBlack)
            {
                result =  new Note((NoteLetter)((i + 5) % 7), (i + 5) / 7, sharp: true);
            }
            return result;
        }
    }
}
