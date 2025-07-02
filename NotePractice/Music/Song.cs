using NotePractice.Music.Symbols;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Security;
using System.Text;
using System.Threading.Tasks;

namespace NotePractice.Music
{
    public class Song
    {
        public List<GrandStaff> GrandStaves { get; set; }
        private static Color StaffHighlightColor = Color.FromArgb(100, 50, 0, 255);
        public Song() 
        { 
            GrandStaves = new List<GrandStaff>();
        }
        public Bitmap Bitmap(bool drawCursor, int highlightedStaffIndex = -1, Clef? highlightedClef = null)
        {
            List<Bitmap> staffBitmaps = GrandStaves.Select(staff => staff.Bitmap(drawCursor)).ToList();
            int width = staffBitmaps.Max(x => x.Width);
            int height = staffBitmaps.Sum(x => x.Height);
            Bitmap result = new Bitmap(width, height);
            using Graphics g = Graphics.FromImage(result);
            int yPos = 0;
            for(int i = 0; i < staffBitmaps.Count; i++)
            {
                Bitmap bitmap = staffBitmaps[i];
                g.DrawImage(bitmap, 0, yPos);
                if (i == highlightedStaffIndex)
                {
                    using Brush brush = new SolidBrush(StaffHighlightColor);
                    g.FillRectangle(brush, 0, yPos, bitmap.Width, bitmap.Height);
                }
                yPos += bitmap.Height;
                bitmap.Dispose();
            }
            return result;
        }
        public void AddGrandStaff()
        {
            GrandStaves.Add(new GrandStaff());
        }
        public void RemoveGrandStaff(int index = -1)
        {
            if (GrandStaves.Count == 0) return;
            if(index == -1)
            {
                GrandStaves.RemoveAt(GrandStaves.Count - 1);
            } else
            {
                GrandStaves.RemoveAt(index);
            }
        }
        public void AddSymbol(Symbol symbol, int staffIndex, Clef clef)
        {
            GrandStaves[staffIndex].AddSymbol(symbol, clef);
        }
        public void RemoveSymbol(int staffIndex, Clef clef)
        {
            GrandStaves[staffIndex].RemoveLastSymbol(clef);
        }
        public Bitmap GrandStaffBitmap(int staffIndex)
        {
            return GrandStaves[staffIndex].Bitmap(true);
        }
    }
    public enum SymbolType
    {
        Note,
        Rest,
        BarLine,
        Shift,
        Clef
    }
}
