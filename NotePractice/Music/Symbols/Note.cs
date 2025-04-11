using NotePractice.Music;
using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotePractice.Music.Symbols
{
    public class Note : Symbol
    {
        public NoteLetter NoteLetter { get; set; }
        public int Octave { get; set; }
        private bool _sharp, _flat;
        public bool Flat
        {
            get
            {
                return _flat;
            }
            set
            {
                if (NoteLetter != NoteLetter.C && NoteLetter != NoteLetter.F) _flat = value;
            }
        }
        public bool Sharp
        {
            get
            {
                return _sharp;
            }
            set
            {
                if (NoteLetter != NoteLetter.E && NoteLetter != NoteLetter.B) _sharp = value;
            }
        }
        public int NumVal { get => Octave * 7 + (int)NoteLetter; }
        public int Duration { get; set; }
        public int XPosShift { get; set; }
        public bool DrawStem { get; set; }
        public bool DrawFlag { get; set; }

        public SymbolType Type { get => SymbolType.Note; }
        public Note(NoteLetter noteLetter, int octave, bool sharp = false, bool flat = false, int duration = 1)
        {
            NoteLetter = noteLetter;
            Octave = octave;
            Sharp = sharp;
            Flat = flat;
            Duration = duration;
            DrawStem = true;
            DrawFlag = true;
            XPosShift = 0;
        }

        public override string ToString()
        {
            string sharpFlat = "";
            if (Sharp) sharpFlat = "#";
            if (Flat) sharpFlat = "b";
            return Enum.GetName(typeof(NoteLetter), NoteLetter) + Octave.ToString() + sharpFlat;
        }
        public override bool Equals(object? obj)
        {
            if (obj == null || obj is not Note) return false;
            Note n = (Note)obj;
            bool letterCond = n.NoteLetter == NoteLetter;
            if(n.Sharp && Flat)
            {
                letterCond = (NoteLetter)((int)n.NoteLetter + 1) == NoteLetter;
            } else if(n.Flat && Sharp)
            {
                letterCond = (NoteLetter)((int)NoteLetter + 1) == n.NoteLetter;
            }
            if ((n.Flat || n.Sharp) && !Flat && !Sharp) letterCond = false;
            if ((Flat || Sharp) && !n.Flat && !n.Sharp) letterCond = false;
            return letterCond && n.Octave == Octave;
        }
        public int Distance(Note note)
        {
            return Math.Abs(note.NumVal - NumVal) + 1;
        }
        public Note ShiftedNote(int shiftValue)
        {
            int newNlVal = (int)NoteLetter + shiftValue;
            int newOctave = Octave;
            if(newNlVal > 6)
            {
                newNlVal -= 7;
                newOctave++;
            }
            if(newNlVal < 0)
            {
                newNlVal += 7;
                newOctave--;
            }
            return new Note((NoteLetter)newNlVal, newOctave);
        }
        public void Draw(Graphics g, int xPos, Clef clef)
        {
            int noteInt = (int)NoteLetter;
            int octave = Octave;
            int yPos;
            xPos += XPosShift;
            // Y position calculation
            if (clef == Clef.Treble)
            {
                yPos = (int)(MusicDrawer.LineSpacing * 4 + MusicDrawer.LineSpacing * 5 - noteInt * MusicDrawer.NoteShift + 28 * MusicDrawer.NoteShift - octave * 7 * MusicDrawer.NoteShift);
            }
            else
            {
                yPos = (int)(MusicDrawer.LineSpacing * 3 - noteInt * MusicDrawer.NoteShift + 28 * MusicDrawer.NoteShift - octave * 7 * MusicDrawer.NoteShift);
            }
            // Draw ellipse based on note duration
            OVector notePosition = new OVector(xPos, yPos);
            int noteHeight = (int)(MusicDrawer.LineSpacing * 0.9);
            int noteWidth = (int)(noteHeight * 1.7);
            if (Duration == 1)
            {
                g.DrawEllipse(MusicDrawer.NotePen, xPos - noteWidth / 2, yPos - noteHeight / 2, noteWidth, noteHeight);
            }
            else
            {
                noteHeight = (int)(MusicDrawer.LineSpacing * 0.7);
                noteWidth = (int)(noteHeight * 1.6);
                GraphicsState savedG = g.Save();
                g.TranslateTransform(xPos, yPos);
                g.RotateTransform(-15);
                Rectangle noteRectangle = new Rectangle(-noteWidth / 2, -noteHeight / 2, noteWidth, noteHeight);
                g.DrawEllipse(MusicDrawer.NotePen, noteRectangle);
                if (Duration > 2)
                {
                    g.FillEllipse(Brushes.Black, noteRectangle);
                }
                g.Restore(savedG);
                // Draw stem
                int stemXPos = (int)(xPos + noteWidth / 2 * 1.2);
                int stemLength = MusicDrawer.LineSpacing * 3;
                if (yPos <= MusicDrawer.TopLinePosition + MusicDrawer.LineSpacing * 2)
                {
                    stemXPos = (int)(xPos - noteWidth / 2 * 1.15);
                    stemLength *= -1;
                }
                if (DrawStem)
                {
                    g.DrawLine(MusicDrawer.LinePen, stemXPos, yPos, stemXPos, yPos - stemLength);
                }
                // Draw flag
                if (DrawFlag && Duration > 4)
                {
                    int flagCount = (int)(Math.Log2(Duration));
                    int flagWidth = MusicDrawer.LineSpacing;
                    int flagHeight = stemLength / 2;
                    for(int i = 0; i < flagCount; i++)
                    {
                        int x1 = stemXPos;
                        int y1 = yPos - stemLength;
                        int x2 = stemXPos + flagWidth;
                        int y2 = yPos - stemLength + flagHeight;
                        if (yPos <= MusicDrawer.TopLinePosition + MusicDrawer.LineSpacing * 2)
                        {
                            flagHeight *= -1;
                        }
                        GraphicsPath flagPath = MyGraphics.ArcPath(new OVector(x1, y1), new OVector(x2, y2), stemLength, 20);
                        //g.DrawPath(MusicDrawer.LinePen, flagPath);
                        MyGraphics.DrawPathInterpolatedWidths(g, flagPath, MusicDrawer.Unit * 0.2f, MusicDrawer.Unit * 0.05f);

                    }
                }
            }
            // Draw Sharp and Flat signs
            if (Sharp || Flat)
            {
                string sharpFlat = Sharp ? "#" : "b";
                float fontSize = Sharp ? noteHeight * 1.3f : noteHeight;
                using Font f = new Font("Arial", fontSize, FontStyle.Bold);
                Point pos;
                if (Sharp)
                {
                    pos = new Point(notePosition.Xint - (int)(noteWidth * 1.4), notePosition.Yint - (int)(noteHeight * 0.95));
                }
                else
                {
                    pos = new Point(notePosition.Xint - (int)(noteWidth * 1.3), notePosition.Yint - (int)(noteHeight * 0.8));
                }
                g.DrawString(sharpFlat, f, Brushes.Black, pos);
            }
            // Draw helper lines when notes are above or below the staff
            int halfHelperLength = (int)(MusicDrawer.Unit * 1.5);
            if (yPos < MusicDrawer.TopLinePosition)
            {
                for (int i = MusicDrawer.TopLinePosition; i >= yPos; i -= MusicDrawer.LineSpacing)
                {
                    g.DrawLine(MusicDrawer.LinePen, new Point(xPos - halfHelperLength, i), new Point(xPos + halfHelperLength, i));
                }
            }
            if (yPos > MusicDrawer.BottomLinePosition)
            {
                for (int i = MusicDrawer.BottomLinePosition; i <= yPos; i += MusicDrawer.LineSpacing)
                {
                    g.DrawLine(MusicDrawer.LinePen, new Point(xPos - halfHelperLength, i), new Point(xPos + halfHelperLength, i));
                }
            }
        }

    }

    public enum NoteLetter
    {
        C,
        D,
        E,
        F,
        G,
        A,
        B
    }
    public enum Clef
    {
        Treble,
        Bass
    }
}
