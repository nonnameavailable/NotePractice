using NotePractice.Music.Symbols;
using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotePractice.Music
{
    public class NoteDrawer
    {
        private int XPosShift { get => _note.XPosShift; }
        private int Duration { get => _note.Duration; }
        //private bool StemAlwaysRight { get => _note.StemAlwaysRight; }
        private bool DrawStem { get => _note.DrawStem; }
        private int StemLength { get => _note.StemLength; }
        private bool DrawFlag { get => _note.DrawFlag; }
        private bool IsSharp { get => _note.IsSharp; }
        private bool IsFlat { get => _note.IsFlat; }
        private NoteLetter NoteLetter { get => _note.NoteLetter; }
        private int Octave { get =>  _note.Octave; }
        public Direction StemSide { get; set; }
        public Direction StemDirection { get; set; }

        private Note _note;
        public NoteDrawer(Note note)
        {
            _note = note;
            StemSide = Direction.Right;
            StemDirection = Direction.Up;
        }
        public void DrawNote(Graphics g, int xPos, Clef clef, Color color)
        {
            int yPos = YPos(clef);
            xPos += XPosShift;
            // Draw ellipse based on note duration
            OVector notePosition = new OVector(xPos, yPos);
            int fnw = MusicDrawer.FullNoteWidth;
            int fnh = MusicDrawer.FullNoteHeight;
            int snw = MusicDrawer.SmallNoteWidth;
            int snh = MusicDrawer.SmallNoteHeight;
            OVector stemStart = StemStart(xPos, clef);
            OVector stemEnd = StemEnd(xPos, clef);
            using Brush noteBrush = new SolidBrush(color);
            using Pen notePen = new Pen(noteBrush, MusicDrawer.Unit * 0.2f);
            using Pen stemPen = new Pen(noteBrush, MusicDrawer.Unit * 0.1f);
            if (Duration == 1)
            {
                g.DrawEllipse(notePen, xPos - fnw / 2, yPos - fnh / 2, fnw, fnh);
            }
            else
            {
                GraphicsState savedG = g.Save();
                g.TranslateTransform(xPos, yPos);
                g.RotateTransform(-15);
                Rectangle noteRectangle = new Rectangle(-snw / 2, -snh / 2, snw, snh);
                g.DrawEllipse(notePen, noteRectangle);
                if (Duration > 2)
                {
                    g.FillEllipse(noteBrush, noteRectangle);
                }
                g.Restore(savedG);
                // Draw stem
                if (DrawStem)
                {
                    g.DrawLine(stemPen, stemStart.Xint, stemStart.Yint, stemEnd.Xint, stemEnd.Yint);
                }
                // Draw flag
                if (DrawFlag && Duration > 4)
                {
                    int flagCount = (int)(Math.Log2(Duration)) - 2;
                    int flagWidth = (int)(MusicDrawer.LineSpacing * 0.8);
                    int flagHeight = (int)(MusicDrawer.DefaultStemLength * 0.6);
                    int stemXPos = stemStart.Xint;
                    //Direction stemDirection = GetStemDirection(clef);
                    Direction stemDirection = StemDirection;
                    for (int i = 0; i < flagCount; i++)
                    {
                        int iShift = (int)(i * MusicDrawer.DefaultStemLength * 0.15);
                        int x1 = (int)(stemXPos + MusicDrawer.Unit * 0.06);
                        int y1 = yPos - StemLength + iShift;
                        int x2 = stemXPos + flagWidth;
                        int y2 = yPos - StemLength + flagHeight + iShift;
                        //if (stemDirection == Direction.Down && !StemAlwaysRight)
                        //{
                        //    x1 = (int)(stemXPos - MusicDrawer.Unit * 0.06);
                        //    y1 = yPos + StemLength - iShift;
                        //    x2 = stemXPos - flagWidth;
                        //    y2 = yPos + StemLength - flagHeight - iShift;
                        //}
                        if (stemDirection == Direction.Down)
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
                        MyGraphics.DrawPathInterpolatedWidths(g, flagPath, MusicDrawer.Unit * 0.2f, MusicDrawer.Unit * 0.06f, color);

                    }
                }
            }
            // Draw IsSharp and Flat signs
            if (IsSharp || IsFlat)
            {
                string sharpFlat = IsSharp ? "#" : "♭";
                float fontSize = IsSharp ? fnh * 1.3f : fnh;
                using Font f = new Font("Arial", fontSize, FontStyle.Bold);
                Point pos;
                if (IsSharp)
                {
                    pos = new Point(notePosition.Xint - (int)(fnw * 1.4), notePosition.Yint - (int)(fnh * 0.95));
                }
                else
                {
                    pos = new Point(notePosition.Xint - (int)(fnw * 1.3), notePosition.Yint - (int)(fnh * 0.8));
                }
                g.DrawString(sharpFlat, f, noteBrush, pos);
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
        private int StemXPos(int xPos, Clef clef)
        {
            int stemXPos = (int)(xPos + MusicDrawer.SmallNoteWidth / 2 * 1.2 - MusicDrawer.LinePen.Width * 0.5);
            Direction stemDirection = GetStemDirection(clef);
            //if (stemDirection == Direction.Down && !StemAlwaysRight)
            //{
            //    stemXPos = (int)(xPos - MusicDrawer.SmallNoteWidth / 2 * 1.13 + MusicDrawer.LinePen.Width * 0.5);
            //}
            if (_note.StemDirection == Direction.Down)
            {
                stemXPos = (int)(xPos - MusicDrawer.SmallNoteWidth / 2 * 1.13 + MusicDrawer.LinePen.Width * 0.5);
            }
            return stemXPos;
        }
        public OVector StemStart(int xPos, Clef clef)
        {

            return new OVector(StemXPos(xPos, clef), YPos(clef));
        }
        public OVector StemEnd(int xPos, Clef clef)
        {
            int stemXPos = StemXPos(xPos, clef);
            //Direction stemDirection = GetStemDirection(clef);
            int yPos = YPos(clef);
            //if (stemDirection == Direction.Down && !StemAlwaysRight)
            //{
            //    return new OVector(stemXPos, yPos + StemLength);
            //}
            if (_note.StemDirection == Direction.Down)
            {
                return new OVector(stemXPos, yPos + StemLength);
            }
            else
            {
                return new OVector(stemXPos, yPos - StemLength);
            }
        }
        public Direction GetStemDirection(Clef clef)
        {
            return YPos(clef) <= MusicDrawer.TopLinePosition + MusicDrawer.LineSpacing * 2 ? Direction.Down : Direction.Up;
        }
        public int YPos(Clef clef)
        {
            int noteInt = (int)NoteLetter;
            int yPos = (int)(MusicDrawer.TopLinePosition + MusicDrawer.LineSpacing * 5 - noteInt * MusicDrawer.NoteShift + 28 * MusicDrawer.NoteShift - Octave * 7 * MusicDrawer.NoteShift);
            if (clef == Clef.Bass)
            {
                yPos = (int)(MusicDrawer.TopLinePosition - MusicDrawer.LineSpacing - noteInt * MusicDrawer.NoteShift + 28 * MusicDrawer.NoteShift - Octave * 7 * MusicDrawer.NoteShift);
            }
            return yPos;
        }
    }

}
