using NotePractice.Music.Symbols;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotePractice.Music
{
    public class GrandStaff
    {
        public List<Symbol> TrebleSymbols { get; set; }
        public List<Symbol> BassSymbols { get; set; }
        public GrandStaff()
        {
            TrebleSymbols = new List<Symbol>();
            BassSymbols = new List<Symbol>();
            TrebleSymbols.Add(new ClefSymbol(Clef.Treble));
            BassSymbols.Add(new ClefSymbol(Clef.Bass));
        }
        public Bitmap Bitmap(bool drawCursor)
        {
            using Bitmap trebleBitmap = MusicDrawer.MusicBitmap(TrebleSymbols, drawCursor);
            using Bitmap bassBitmap = MusicDrawer.MusicBitmap(BassSymbols, drawCursor);
            Bitmap result = new Bitmap(Math.Max(trebleBitmap.Width, bassBitmap.Width), trebleBitmap.Height + bassBitmap.Height);
            using Graphics g = Graphics.FromImage(result);
            g.DrawImage(trebleBitmap, 0, 0);
            g.DrawImage(bassBitmap, 0, trebleBitmap.Height);
            return result;
        }
        public void AddSymbol(Symbol symbol, Clef clef)
        {
            if(clef == Clef.Treble)
            {
                TrebleSymbols.Add(symbol);
            } else
            {
                BassSymbols.Add(symbol);
            }
        }
    }
}
