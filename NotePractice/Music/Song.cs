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
        public Song() 
        { 
            GrandStaves = new List<GrandStaff>();
        }
        public Bitmap Bitmap(bool drawCursor)
        {
            List<Bitmap> staffBitmaps = GrandStaves.Select(staff => staff.Bitmap(drawCursor)).ToList();
            int width = staffBitmaps.Max(x => x.Width);
            int height = staffBitmaps.Max(x => x.Height);
            Bitmap result = new Bitmap(width, height);
            using Graphics g = Graphics.FromImage(result);
            int yPos = 0;
            foreach(Bitmap bitmap in staffBitmaps)
            {
                g.DrawImage(bitmap, 0, yPos);
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
            List<Symbol> listToRemoveFrom = clef == Clef.Treble ? GrandStaves[staffIndex].TrebleSymbols : GrandStaves[staffIndex].BassSymbols;
            if (listToRemoveFrom.Count == 0) return;
            listToRemoveFrom.RemoveAt(listToRemoveFrom.Count - 1);
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
    public interface Symbol
    {
        void Draw(Graphics g, int xPos, Clef clef = Clef.Treble);
    }
}
