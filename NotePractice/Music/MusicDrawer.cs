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
        public static Pen BarPen = new Pen(Brushes.Black, Unit * 0.5f);

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
            DrawBeams(symbols, g);
            for (int i = 0; i < 5; i++)
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
                s.Draw(g, xPos, clef);
                if (s is Shift)
                {
                    xPos += XSymbolShift;
                }
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
            List<Note> chord = new List<Note>();
            for (int i = 0; i < symbols.Count; i++)
            {
                Symbol s = symbols[i];
                if (s is Note note)
                {
                    note.DrawFlag = false;
                    note.DrawStem = true;
                    note.XPosShift = 0;
                    //note.StemAlwaysRight = false;
                    note.StemDirection = Direction.Up;
                    note.StemSide = Direction.Right;
                    note.StemLength = (int)(LineSpacing * 3.5);
                    chord.Add(note);
                }
                if((s is not Note && chord.Count > 0) || (i == symbols.Count - 1 && chord.Count > 0))
                {
                    AdjustFLagsAndStemsOfChord(chord, clef);
                    chord.Clear();
                }
            }
        }
        private static void AdjustFLagsAndStemsOfChord(List<Note> notes, Clef clef)
        {
            notes = notes.OrderBy(note => note.Octave).ThenBy(note => (int)note.NoteLetter).ToList();
            Note firstNote = notes[0];
            Note? firstInSequence = null;
            int prevDist = 0;
            int shiftCounter = 0;
            int xShiftValue = (int)(Unit * 1.3);
            Direction stemDirection = firstNote.GetStemDirection(clef);
            for (int i = 0; i < notes.Count; i++)
            {
                Note note = notes[i];
                bool isFirstNote = i == 0;
                bool isLastNote = i == notes.Count - 1;
                bool isOnlyNote = notes.Count == 1;
                bool stemIsRight = stemDirection == Direction.Up;
                note.StemDirection = stemDirection;
                note.StemSide = stemDirection == Direction.Down ? Direction.Left : Direction.Right;
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
                        note.XPosShift = xShiftValue * (shiftCounter % 2);
                        if (!stemIsRight)
                        {
                            note.XPosShift *= -1;
                        }
                        //note.DrawStem = false;
                        note.DrawStem = !stemIsRight && shiftCounter % 2 == 0;
                        prevDist = dist;
                        continue;
                    }
                    prevDist = dist;
                }
                if (isLastNote)
                {
                    if(firstNote.GetStemDirection(clef) == Direction.Down)
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
        private static void DrawBeams(List<Symbol> symbols, Graphics g)
        {
            if(symbols.Count < 2) return;
            double beatCount = 0;
            int xPos = XSymbolShift;
            Clef clef = symbols[0] is ClefSymbol cleff ? cleff.ClefType : Clef.Treble;
            int barStartIndex = -1;
            int barStartXPos = 0;
            for(int i = 1; i < symbols.Count; i++)
            {
                Symbol symbol = symbols[i];
                Symbol prevSymbol = symbols[i - 1];
                bool isBarStart = beatCount == 0 && symbol is Note;
                if (symbol is Shift shift)
                {
                    xPos += XSymbolShift;
                    continue;
                }
                if(symbol is Note note && prevSymbol is Shift)
                {
                    beatCount += 1d / note.Duration;
                    if (beatCount > 0.25)
                    {
                        beatCount = 1d / note.Duration;
                        barStartIndex = i;
                        barStartXPos = xPos;
                        continue;
                    }
                } else if (symbol is Rest rest && prevSymbol is Shift)
                {
                    beatCount += 1d / rest.Duration;
                }
                bool isBarEnd = beatCount == 0.25 && symbol is Note;
                if (isBarStart)
                {
                    barStartIndex = i;
                    barStartXPos = xPos;
                }
                if (isBarEnd)
                {
                    beatCount = 0;
                    if (barStartIndex == -1) continue;
                    Note firstNote = (Note)symbols[barStartIndex];
                    for (int j = barStartIndex; j <= i; j++)
                    {
                        if (symbols[j] is Note notee)
                        {
                            notee.DrawFlag = false;
                            if (j > barStartIndex)
                            {
                                notee.StemSide = firstNote.StemSide;
                                notee.StemDirection = firstNote.StemDirection;
                            }
                        }
                    }
                    OVector barStart = ((Note)symbols[barStartIndex]).StemEnd(barStartXPos, clef);
                    OVector barEnd = ((Note)symbol).StemEnd(xPos, clef);
                    Debug.Print(firstNote.StemDirection.ToString() + " x " + ((Note)symbol).StemDirection.ToString());
                    if (!barStart.Equals(barEnd))
                    {
                        g.DrawLine(BarPen, barStart.ToPoint(), barEnd.ToPoint());
                    };
                }
            }
        }
        public static List<Symbol> StartSymbols(Clef clef, List<Note> notes)
        {
            return new List<Symbol>() { new ClefSymbol(clef), new Shift(), new Shift() }.Concat(notes).ToList();
        }
    }
}
