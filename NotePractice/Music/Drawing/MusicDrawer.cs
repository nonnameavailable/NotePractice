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

namespace NotePractice.Music.Drawing
{
    public class MusicDrawer
    {
        public const int Unit = 60;
        public const int LineSpacing = Unit;
        public const int TopEmptySpace = LineSpacing * 6;
        public const int BottomEmptySpace = LineSpacing * 6;
        public const int XSymbolShift = Unit * 3;
        public const int NoteShift = LineSpacing / 2;
        public const int TopLinePosition = TopEmptySpace;
        public const int BottomLinePosition = TopEmptySpace + LineSpacing * 4;
        public const int DefaultStemLength = (int)(LineSpacing * 3.5);
        public static Pen LinePen = new Pen(Brushes.Black, Unit * 0.1f);
        public static Pen BarPen = new Pen(Brushes.Black, Unit * 0.5f);

        public const int FullNoteHeight = (int)(LineSpacing * 0.9);
        public const int FullNoteWidth = (int)(FullNoteHeight * 1.7);
        public const int SmallNoteHeight = (int)(LineSpacing* 0.8);
        public const int SmallNoteWidth = (int)(SmallNoteHeight* 1.6);
        public static Bitmap MusicBitmap(List<Symbol> symbols, bool drawCursor = true, bool clearTransparent = false, Color? color = null)
        {
            AdjustNotes(symbols);
            int width = XSymbolShift;
            int height = TopEmptySpace + LineSpacing * 4 + BottomEmptySpace;
            foreach (Symbol symbol in symbols)
            {
                if (symbol is Shift) width += XSymbolShift; 
            }
            width += XSymbolShift;
            Bitmap result = new Bitmap(width, height);
            if(symbols.Count == 0) return result;
            using Graphics g = Graphics.FromImage(result);
            if (clearTransparent)
            {
                g.Clear(Color.FromArgb(0, 0, 0, 0));
            } else
            {
                g.Clear(Color.White);
            }

            Color symbolColor = Color.FromArgb(0, 0, 0);
            if (color != null)
            {
                symbolColor = (Color)color;
            }

            DrawBeams(symbols, g, symbolColor);
            for (int i = 0; i < 5; i++)
            {
                int yLinePosition = TopLinePosition + LineSpacing * i;
                g.DrawLine(LinePen, new Point(0, yLinePosition), new Point(width, yLinePosition));
            }
            int xPos = XSymbolShift;
            Clef clef = Clef.Treble;
            if (symbols[0] is ClefSymbol cs)
            {
                clef = cs.ClefType;
            }
            
            DrawSymbols(symbols, g, xPos, clef, drawCursor, symbolColor);
            if (drawCursor)
            {
                Color cursorColor = Color.FromArgb(120, 255, 0, 0);
                using Pen p = new Pen(cursorColor, Unit / 10);
                g.DrawLine(p, xPos, TopLinePosition, xPos, BottomLinePosition);
            }
            return result;
        }
        public static void DrawSymbols(List<Symbol> symbols, Graphics g, int xPos, Clef clef, bool drawCursor, Color color)
        {
            for (int i = 0; i < symbols.Count; i++)
            {
                Symbol s = symbols[i];
                if (s is not Shift || s is Shift & drawCursor) s.Draw(g, xPos, clef, color);
                if (s is Shift)
                {
                    xPos += XSymbolShift;
                }
            }

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
                if(s is not Note && chord.Count > 0 || i == symbols.Count - 1 && chord.Count > 0)
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
                    int dist = note.WhiteKeyDistance(prevNote);
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
                    if(stemDirection == Direction.Up)
                    {
                        firstNote.StemLength += firstNote.YPos(clef) - note.YPos(clef);
                    }
                }
            }
        }
        private static void DrawBeams(List<Symbol> symbols, Graphics g, Color color)
        {
            if(symbols.Count < 2) return;
            double beatCount = 0;
            int xPos = XSymbolShift;
            Clef clef = symbols[0] is ClefSymbol cleff ? cleff.ClefType : Clef.Treble;
            int beamStartIndex = -1;
            int beamStartXPos = 0;
            List<Note> beamNotes = new();
            using Brush b = new SolidBrush(color);
            using Pen colorBarPen = new Pen(b, BarPen.Width);
            for (int i = 1; i < symbols.Count; i++)
            {
                Symbol symbol = symbols[i];
                Symbol prevSymbol = symbols[i - 1];
                bool isBeamStart = beatCount == 0 && symbol is Note;
                if (isBeamStart)
                {
                    beamNotes.Add((Note)symbol);
                    beamStartIndex = i;
                    beamStartXPos = xPos;
                    beatCount = 1d / ((Note)symbol).Duration;
                    continue;
                }
                if (symbol is Shift)
                {
                    xPos += XSymbolShift;
                    continue;
                }
                if(symbol is Note note && prevSymbol is Shift)
                {
                    beatCount += 1d / note.Duration;
                    beamNotes.Add((Note)symbol);
                    if (beatCount > 0.25)
                    {
                        beatCount = 1d / note.Duration;
                        beamStartIndex = i;
                        beamStartXPos = xPos;
                        beamNotes.Clear();
                        beamNotes.Add((Note)symbol);
                        continue;
                    }
                } else if (symbol is Rest rest && prevSymbol is Shift)
                {
                    beatCount += 1d / rest.Duration;
                }
                bool isBeamEnd = beatCount == 0.25 && symbol is Note;
                if (isBeamEnd)
                {
                    beatCount = 0;
                    if (beamStartIndex == -1)
                    {
                        beamNotes.Clear();
                        continue;
                    }
                    //beamNotes.Add((Note)symbol);
                    Note firstNote = (Note)symbols[beamStartIndex];
                    for (int j = beamStartIndex; j <= i; j++)
                    {
                        if (symbols[j] is Note notee)
                        {
                            notee.DrawFlag = false;
                            if (j > beamStartIndex)
                            {
                                notee.StemSide = firstNote.StemSide;
                                notee.StemDirection = firstNote.StemDirection;
                            }
                        }
                    }
                    OVector beamStart = ((Note)symbols[beamStartIndex]).StemEnd(beamStartXPos, clef);
                    OVector beamEnd = ((Note)symbol).StemEnd(xPos, clef);
                    OVector beamDirection = beamEnd.Copy().Subtract(beamStart);
                    int beamShiftValue = Unit;
                    OVector beamShift = firstNote.StemDirection == Direction.Up ? new OVector(0, beamShiftValue) : new OVector(0, -beamShiftValue);
                    int beamShiftSpan = (xPos - beamStartXPos) / XSymbolShift;
                    OVector beamSection = beamDirection.Copy().Divide(beamShiftSpan);
                    for(int j = 0; j < beamShiftSpan; j++)
                    {
                        int beamCount = (int)Math.Log2(beamNotes[Math.Min(j+1, beamNotes.Count - 1)].Duration) - 2;
                        int beamCount2 = (int)Math.Log2(beamNotes[Math.Min(j, beamNotes.Count - 1)].Duration) - 2;
                        if (j == 0)
                        {
                            beamCount = beamCount2;
                        } else
                        {
                            if (beamCount != beamCount2) beamCount = 1;
                        }
                            OVector bs = beamStart.Copy();
                        for (int k = 0; k < beamCount; k++)
                        {
                            if (!beamStart.Equals(beamEnd))
                            {
                                g.DrawLine(colorBarPen, bs.Copy().Add(beamSection.Copy().Multiply(j)).ToPoint(), bs.Copy().Add(beamSection.Copy().Multiply(j+1)).ToPoint());
                                bs.Add(beamShift);
                                //beamEnd.Add(beamShift);
                            }
                        }
                    }
                    beamNotes.Clear();
                }
            }
        }
        //public static List<Symbol> StartSymbols(Clef clef, List<Note> notes)
        //{
        //    return new List<Symbol>() { new ClefSymbol(clef), new Shift(), new Shift() }.Concat(notes).ToList();
        //}
        public static List<Symbol> StartSymbols(Clef clef, List<Symbol> symbols)
        {
            return new List<Symbol>() { new ClefSymbol(clef), new Shift(), new Shift() }.Concat(symbols).ToList();
        }
    }
}
