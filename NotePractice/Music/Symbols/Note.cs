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
        public bool StemAlwaysRight { get; set; }
        public int StemLength { get; set; }

        public SymbolType Type { get => SymbolType.Note; }
        public Note(NoteLetter noteLetter, int octave, bool sharp = false, bool flat = false, int duration = 1)
        {
            NoteLetter = noteLetter;
            Octave = octave;
            Sharp = sharp;
            Flat = flat;
            Duration = duration;
            DrawStem = true;
            DrawFlag = false;
            XPosShift = 0;
            StemLength = MusicDrawer.DefaultStemLength;
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
        public bool StemShouldBeLeft(Clef clef)
        {
            return YPos(clef) <= MusicDrawer.TopLinePosition + MusicDrawer.LineSpacing * 2;
        }
        private int YPos(Clef clef)
        {
            int noteInt = (int)NoteLetter;
            int yPos = (int)(MusicDrawer.LineSpacing * 4 + MusicDrawer.LineSpacing * 5 - noteInt * MusicDrawer.NoteShift + 28 * MusicDrawer.NoteShift - Octave * 7 * MusicDrawer.NoteShift);
            if (clef == Clef.Bass)
            {
                yPos = (int)(MusicDrawer.LineSpacing * 3 - noteInt * MusicDrawer.NoteShift + 28 * MusicDrawer.NoteShift - Octave * 7 * MusicDrawer.NoteShift);
            }
            return yPos;
        }
        public void Draw(Graphics g, int xPos, Clef clef)
        {
            int yPos = YPos(clef);
            xPos += XPosShift;
            // Draw ellipse based on note duration
            OVector notePosition = new OVector(xPos, yPos);
            int fnw = MusicDrawer.FullNoteWidth;
            int fnh = MusicDrawer.FullNoteHeight;
            int snw = MusicDrawer.SmallNoteWidth;
            int snh = MusicDrawer.SmallNoteHeight;
            if (Duration == 1)
            {
                g.DrawEllipse(MusicDrawer.NotePen, xPos - fnw / 2, yPos - fnh / 2, fnw, fnh);
            }
            else
            {
                GraphicsState savedG = g.Save();
                g.TranslateTransform(xPos, yPos);
                g.RotateTransform(-15);
                Rectangle noteRectangle = new Rectangle(-snw / 2, -snh / 2, snw, snh);
                g.DrawEllipse(MusicDrawer.NotePen, noteRectangle);
                if (Duration > 2)
                {
                    g.FillEllipse(Brushes.Black, noteRectangle);
                }
                g.Restore(savedG);
                // Draw stem
                int stemXPos = (int)(xPos + snw / 2 * 1.2 - MusicDrawer.LinePen.Width * 0.5);
                bool stemDown = StemShouldBeLeft(clef);
                if (stemDown && !StemAlwaysRight)
                {
                    stemXPos = (int)(xPos - snw / 2 * 1.13 + MusicDrawer.LinePen.Width * 0.5);
                }
                if (DrawStem)
                {
                    if (stemDown && !StemAlwaysRight)
                    {
                        g.DrawLine(MusicDrawer.LinePen, stemXPos, yPos, stemXPos, yPos + StemLength);
                    } else
                    {
                        g.DrawLine(MusicDrawer.LinePen, stemXPos, yPos, stemXPos, yPos - StemLength);
                    }
                }
                // Draw flag
                if (DrawFlag && Duration > 4)
                {
                    int flagCount = (int)(Math.Log2(Duration)) - 2;
                    int flagWidth = (int)(MusicDrawer.LineSpacing * 0.8);
                    int flagHeight = (int)(MusicDrawer.DefaultStemLength * 0.6);
                    for (int i = 0; i < flagCount; i++)
                    {
                        int iShift = (int)(i * MusicDrawer.DefaultStemLength * 0.15);
                        int x1 = (int)(stemXPos + MusicDrawer.Unit * 0.06);
                        int y1 = yPos - StemLength + iShift;
                        int x2 = stemXPos + flagWidth;
                        int y2 = yPos - StemLength + flagHeight + iShift;
                        if (stemDown && !StemAlwaysRight)
                        {
                            x1 = (int)(stemXPos - MusicDrawer.Unit * 0.06);
                            y1 = yPos + StemLength - iShift;
                            x2 = stemXPos - flagWidth;
                            y2 = yPos + StemLength - flagHeight - iShift;
                        }
                        OVector v1 = new OVector(x1, y1);
                        OVector v2 = new OVector(x2, y2);
                        //using GraphicsPath flagPath = MyGraphics.ArcPath(v1, v2, MusicDrawer.DefaultStemLength, 20);
                        using GraphicsPath flagPath = MyGraphics.FlagPath(v1, v2);
                        MyGraphics.DrawPathInterpolatedWidths(g, flagPath, MusicDrawer.Unit * 0.2f, MusicDrawer.Unit * 0.02f);

                    }
                }
            }
            // Draw Sharp and Flat signs
            if (Sharp || Flat)
            {
                string sharpFlat = Sharp ? "#" : "b";
                float fontSize = Sharp ? fnh * 1.3f : fnh;
                using Font f = new Font("Arial", fontSize, FontStyle.Bold);
                Point pos;
                if (Sharp)
                {
                    pos = new Point(notePosition.Xint - (int)(fnw * 1.4), notePosition.Yint - (int)(fnh * 0.95));
                }
                else
                {
                    pos = new Point(notePosition.Xint - (int)(fnw * 1.3), notePosition.Yint - (int)(fnh * 0.8));
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
