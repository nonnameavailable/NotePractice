using NotePractice.Music.Symbols;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotePractice.Music.Drawing
{
    public class GrandStaff
    {
        public List<Symbol> TrebleSymbols { get; set; }
        public List<Symbol> BassSymbols { get; set; }
        private List<Note> _notesHereTreble, _notesHereBass;
        public GrandStaff()
        {
            TrebleSymbols = new List<Symbol>();
            BassSymbols = new List<Symbol>();
            TrebleSymbols.Add(new ClefSymbol(Clef.Treble));
            BassSymbols.Add(new ClefSymbol(Clef.Bass));
            _notesHereBass = new();
            _notesHereTreble = new();
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
                if (symbol is Note note)
                {
                    if (ListContainsNote(note, _notesHereTreble)) return;
                    _notesHereTreble.Add(note);
                    TrebleSymbols.Add(symbol);
                }
                else
                {
                    _notesHereTreble.Clear();
                    TrebleSymbols.Add(symbol);
                }
            } else
            {
                if (symbol is Note note)
                {
                    if (ListContainsNote(note, _notesHereBass)) return;
                    _notesHereBass.Add(note);
                    BassSymbols.Add(symbol);
                }
                else
                {
                    _notesHereBass.Clear();
                    BassSymbols.Add(symbol);
                }

            }
        }
        public void RemoveLastSymbol(Clef clef)
        {
            List<Symbol> listToRemoveFrom = clef == Clef.Treble ? TrebleSymbols : BassSymbols;
            if (listToRemoveFrom.Count == 0) return;
            listToRemoveFrom.RemoveAt(listToRemoveFrom.Count - 1);
            if(clef == Clef.Treble)_notesHereTreble.Clear();
            if(clef == Clef.Bass)_notesHereTreble.Clear();
        }
        private bool ListContainsNote(Note note, List<Note> notes)
        {
            foreach(Note note2 in notes)
            {
                if (note2.Equals(note)) return true;
            }
            return false;
        }
    }
}
