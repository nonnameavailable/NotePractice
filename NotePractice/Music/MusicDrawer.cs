using NotePractice.Music.Symbols;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using NotePractice.Properties;
using System.Drawing.Drawing2D;

namespace NotePractice.Music
{
    public class MusicDrawer
    {
        private const int Unit = 30;
        private const int LineSpacing = Unit;
        private const int XSymbolShift = Unit * 3;
        private const int NoteShift = LineSpacing / 2;
        private const int TopLinePosition = LineSpacing * 4;
        private const int BottomLinePosition = LineSpacing * 8;
        private static Pen LinePen = new Pen(Brushes.Black, 4);
        private static Pen NotePen = new Pen(Brushes.Black, Unit * 0.2f);
        public static Bitmap MusicBitmap(List<Symbol> symbols)
        {
            int width = 0;
            int height = LineSpacing * 12;
            foreach (Symbol symbol in symbols)
            {
                if (symbol.Type == SymbolType.Shift) width += XSymbolShift; 
            }
            width += XSymbolShift;
            Bitmap result = new Bitmap(width, height);
            using Graphics g = Graphics.FromImage(result);
            g.Clear(Color.White);
            for(int i = 0; i < 5; i++)
            {
                g.DrawLine(LinePen, new Point(0, Unit * (i + 4)), new Point(width, Unit * (i + 4)));
            }
            int xPos = 0;
            for(int i = 0; i < symbols.Count; i++)
            {
                Symbol s = symbols[i];
                if (s is Shift)
                {
                    xPos += XSymbolShift;
                    continue;
                }
                else DrawSymbol(g, Clef.Treble, s, xPos);
            }
            return result;
        }

        private static void DrawSymbol(Graphics g, Clef clef, Symbol symbol, int xPos)
        {
            if(symbol is Note note)
            {
                DrawNote(g, clef, note, xPos);
            }
            else if(symbol is Rest rest)
            {
                DrawRest(g, rest, xPos);
            }
            else if (symbol is BarLine barLine)
            {
                DrawBarLine(g, xPos);
            }
        }
        private static void DrawBarLine(Graphics g, int xPos)
        {
            g.DrawLine(LinePen, xPos, TopLinePosition, xPos, BottomLinePosition);
        }
        private static void DrawRest(Graphics g, Rest rest, int xPos)
        {
            if (rest.Duration == 1 || rest.Duration == 2)
            {
                int width = LineSpacing * 3 / 2;
                int x = xPos - width / 2;
                int height = LineSpacing / 2;
                if(rest.Duration == 1)
                {
                    int y = LineSpacing * 5;
                    g.FillRectangle(Brushes.Black, x, y, width, height);
                } else
                {
                    int y = LineSpacing * 6 - height;
                    g.FillRectangle(Brushes.Black, x, y, width, height);
                }
            } else
            {
                int width = LineSpacing;
                int height = LineSpacing * 3;
                int x = xPos - width / 2;
                int y = LineSpacing * 9 / 2;
                g.DrawImage(Resources.squiggle, x, y, width, height);
            }
        }
        private static void DrawNote(Graphics g, Clef clef, Note note, int xPos)
        {
            int noteInt = (int)note.NoteLetter;
            int octave = note.Octave;
            int yPos;
            if (clef == Clef.Treble)
            {
                yPos = (int)(LineSpacing * 4 + LineSpacing * 5 - noteInt * NoteShift + 28 * NoteShift - octave * 7 * NoteShift);
            }
            else
            {
                yPos = (int)(LineSpacing * 3 - noteInt * NoteShift + 28 * NoteShift - octave * 7 * NoteShift);
            }

            OVector notePosition = new OVector(xPos, yPos);
            int noteHeight = (int)(LineSpacing * 0.9);
            int noteWidth = (int)(noteHeight * 1.7);
            if(note.Duration == 1)
            {
                g.DrawEllipse(NotePen, xPos - noteWidth / 2, yPos - noteHeight / 2, noteWidth, noteHeight);
            } else
            {
                noteHeight = (int)(LineSpacing * 0.7);
                noteWidth = (int)(noteHeight *  1.6);
                GraphicsState savedG = g.Save();
                g.TranslateTransform(xPos, yPos);
                g.RotateTransform(-15);
                Rectangle noteRectangle = new Rectangle(-noteWidth / 2, -noteHeight / 2, noteWidth, noteHeight);
                g.DrawEllipse(NotePen, noteRectangle);
                if (note.Duration > 2)
                {
                    g.FillEllipse(Brushes.Black, noteRectangle);
                }
                g.Restore(savedG);
                int lineXPos = (int)(xPos + noteWidth / 2 * 1.2);
                g.DrawLine(LinePen, lineXPos, yPos, lineXPos, yPos - LineSpacing * 3);
            }
            if (note.Sharp || note.Flat)
            {
                string sharpFlat = note.Sharp ? "#" : "b";
                float fontSize = note.Sharp ? noteHeight * 1.3f : noteHeight;
                using Font f = new Font("Arial", fontSize, FontStyle.Bold);
                Point pos;
                if (note.Sharp)
                {
                    pos = new Point(notePosition.Xint - (int)(noteWidth * 1.4), notePosition.Yint - (int)(noteHeight * 0.95));
                }
                else
                {
                    pos = new Point(notePosition.Xint - (int)(noteWidth * 1.3), notePosition.Yint - (int)(noteHeight * 0.8));
                }
                g.DrawString(sharpFlat, f, Brushes.Black, pos);
            }
            if (yPos < TopLinePosition)
            {
                for (int i = TopLinePosition; i >= yPos; i -= LineSpacing)
                {
                    g.DrawLine(LinePen, new Point(xPos - 50, i), new Point(xPos + 50, i));
                }
            }
            if (yPos > BottomLinePosition)
            {
                for (int i = BottomLinePosition; i <= yPos; i += LineSpacing)
                {
                    g.DrawLine(LinePen, new Point(xPos - 50, i), new Point(xPos + 50, i));
                }
            }
        }
    }
}
