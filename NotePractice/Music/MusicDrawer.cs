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
using System.Diagnostics;

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
        public const int DefaultStemLength = (int)(LineSpacing * 3.5);
        public static Pen LinePen = new Pen(Brushes.Black, Unit * 0.1f);
        public static Pen NotePen = new Pen(Brushes.Black, Unit * 0.2f);

        public static int FullNoteHeight = (int)(LineSpacing * 0.9);
        public static int FullNoteWidth = (int)(FullNoteHeight * 1.7);
        public static int SmallNoteHeight = (int)(LineSpacing* 0.8);
        public static int SmallNoteWidth = (int)(SmallNoteHeight* 1.6);
        public static Bitmap MusicBitmap(List<Symbol> symbols, bool drawCursor = true)
        {
            AdjustNotes(symbols);
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
        private static void AdjustNotes(List<Symbol> symbols)
        {
            if (symbols.Count == 0) return;
            Clef clef = symbols[0] is ClefSymbol ? ((ClefSymbol)symbols[0]).ClefType : Clef.Treble;
            List<Note> notes = new List<Note>();
            for (int i = 0; i < symbols.Count; i++)
            {
                Symbol s = symbols[i];
                if (s is Note note)
                {
                    note.DrawFlag = false;
                    note.DrawStem = true;
                    note.XPosShift = 0;
                    note.StemAlwaysRight = false;
                    note.StemLength = (int)(LineSpacing * 3.5);
                    notes.Add(note);
                }
                if((s is not Note && notes.Count > 0) || (i == symbols.Count - 1 && notes.Count > 0))
                {
                    AdjustNoteList(notes, clef);
                    notes.Clear();
                }
            }
            //Note? firstNote = null;
            //for (int i = 0; i < symbols.Count; i++)
            //{
            //    Note? note = symbols[i] as Note;
            //    Note? prevNote = null, nextNote = null;
            //    if (i > 0) prevNote = symbols[i - 1] as Note;
            //    if (i < symbols.Count - 1) nextNote = symbols[i + 1] as Note;
            //    bool isNote = note != null;
            //    bool prevIsNote = prevNote != null;
            //    bool nextIsNote = nextNote != null;
            //    bool isFirstNote = !prevIsNote;
            //    bool isLastNote = !nextIsNote;
            //    if (isFirstNote) firstNote = note;
            //    if (isNote)
            //    {
            //        if (prevIsNote)
            //        {
            //            int dist = note.Distance(prevNote);
            //            if (dist == 2)
            //            {
            //                note.XPosShift = (int)(Unit * 1.3);
            //                note.DrawStem = false;
            //                note.DrawFlag = false;
            //                prevNote.DrawFlag = true;
            //            }
            //        }
            //        if (isLastNote && !firstNote.StemShouldBeLeft(clef))
            //        {
            //            note.DrawFlag = true;
            //            if (prevIsNote && prevNote.DrawFlag) note.DrawFlag = false;
            //        }
            //        else if (isLastNote && firstNote.StemShouldBeLeft(clef))
            //        {
            //            firstNote.DrawFlag = true;
            //        }
            //        note.StemAlwaysRight = !firstNote.StemShouldBeLeft(clef);
            //    }
        //}
        }
        public static void AdjustNoteList(List<Note> notes, Clef clef)
        {
            notes = notes.OrderBy(note => note.Octave).ThenBy(note => (int)note.NoteLetter).ToList();
            Note firstNote = notes[0];
            Note? firstInSequence = null;
            int prevDist = 0;
            int shiftCounter = 0;
            int xShiftValue = (int)(Unit * 1.3);
            bool stemIsRight = !firstNote.StemShouldBeLeft(clef);
            for (int i = 0; i < notes.Count; i++)
            {
                Note note = notes[i];
                bool isFirstNote = i == 0;
                bool isLastNote = i == notes.Count - 1;
                bool isOnlyNote = notes.Count == 1;
                note.StemAlwaysRight = stemIsRight;
                if (isOnlyNote)
                {
                    note.DrawFlag = true;
                    return;
                }
                if (!isFirstNote)
                {
                    Note prevNote = notes[i - 1];
                    int dist = note.Distance(prevNote);
                    if(dist == 2)
                    {
                        if (prevDist != 2)
                        {
                            if (firstInSequence == null)
                            {
                                firstInSequence = prevNote;
                                firstInSequence.DrawFlag = true;
                            } else
                            {
                                firstInSequence.DrawFlag = false;
                                firstInSequence = prevNote;
                                firstInSequence.DrawFlag = true;
                            }
                            shiftCounter = 0;
                        }
                        if(stemIsRight)firstInSequence.StemLength += LineSpacing / 2;
                        shiftCounter++;
                        note.XPosShift = xShiftValue * ((shiftCounter) % 2);
                        if (firstNote.StemShouldBeLeft(clef))
                        {
                            note.XPosShift *= -1;
                        }
                        note.DrawStem = false;
                        prevDist = dist;
                        continue;
                    }
                    prevDist = dist;
                }
                if (isLastNote)
                {
                    if(firstNote.StemShouldBeLeft(clef))
                    {
                        firstNote.DrawFlag = true;
                    } else
                    {
                        note.DrawFlag = true;
                        firstNote.DrawFlag = false;
                    }
                }
            }
        }
    }
}
