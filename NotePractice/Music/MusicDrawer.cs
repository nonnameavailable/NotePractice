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
        public const int Unit = 60;
        public const int LineSpacing = Unit;
        public const int XSymbolShift = Unit * 3;
        public const int NoteShift = LineSpacing / 2;
        public const int TopLinePosition = LineSpacing * 4;
        public const int BottomLinePosition = LineSpacing * 8;
        public static Pen LinePen = new Pen(Brushes.Black, 4);
        public static Pen NotePen = new Pen(Brushes.Black, Unit * 0.2f);
        public static Bitmap MusicBitmap(List<Symbol> symbols, bool drawCursor = true)
        {
            int width = XSymbolShift;
            int height = LineSpacing * 12;
            foreach (Symbol symbol in symbols)
            {
                if (symbol is Shift) width += XSymbolShift; 
            }
            width += XSymbolShift;
            Bitmap result = new Bitmap(width, height);
            if(symbols.Count == 0) return result;
            using Graphics g = Graphics.FromImage(result);
            g.Clear(Color.White);
            for(int i = 0; i < 5; i++)
            {
                g.DrawLine(LinePen, new Point(0, Unit * (i + 4)), new Point(width, Unit * (i + 4)));
            }
            int xPos = XSymbolShift;
            Clef clef = Clef.Treble;
            if (symbols[0] is ClefSymbol cs)
            {
                clef = cs.ClefType;
            }
            for(int i = 0; i < symbols.Count; i++)
            {
                Symbol s = symbols[i];
                if (s is Shift)
                {
                    xPos += XSymbolShift;
                    continue;
                }
                else s.Draw(g, xPos, clef);
            }
            if (drawCursor)
            {
                Color cursorColor = Color.FromArgb(120, 255, 0, 0);
                using Pen p = new Pen(cursorColor, Unit / 10);
                g.DrawLine(p, xPos, TopLinePosition, xPos, BottomLinePosition);
            }
            return result;
        }
    }
}
