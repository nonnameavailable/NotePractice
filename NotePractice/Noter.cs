using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using NotePractice.Properties;

namespace NotePractice
{
    public class Noter
    {
        public const int ClefWidth = 293;
        public const int ClefHeight = 242;
        public const int Offset = 65 + ClefHeight;
        public const int Spacing = 30;
        public const int TopLinePosition = Offset;
        public const int BottomLinePosition = Offset + Spacing;
        public const int NoteShift = Spacing / 2;
        public static Bitmap NoteImage(List<Note> notes, Clef clef)
        {
            Bitmap clefC = clef == Clef.Treble ? Resources.treble_clef : Resources.bass_clef;
            using Bitmap noteBitmap = new Bitmap(clefC.Width, clefC.Height * 3);
            using Graphics g = Graphics.FromImage(noteBitmap);
            g.Clear(Color.White);
            using Pen p = new Pen(Brushes.Black, 4);
            int counter = 0;
            Random rand = new Random();
            foreach (Note note in notes)
            {
                int noteInt = (int)note.NoteLetter;
                int octave = note.Octave;
                int xNp = noteBitmap.Width / 2;
                if(rand.Next(11) > 4 && counter > 0)
                {
                    xNp = (int)(noteBitmap.Width * 0.8);
                }
                int yNp;
                if (clef == Clef.Treble)
                {
                    yNp = (int)(Offset + Spacing * 5 - noteInt * NoteShift + 28 * NoteShift - octave * 7 * NoteShift);
                }
                else
                {
                    yNp = (int)(Offset - Spacing - noteInt * NoteShift + 28 * NoteShift - octave * 7 * NoteShift);
                }

                OVector notePosition = new OVector(xNp, yNp);
                int noteSize = noteBitmap.Width / 5;
                DrawNote(g, notePosition, noteSize);
                if(note.Sharp || note.Flat)
                {
                    string symbol = note.Sharp ? "#" : "b";
                    float fontSize = note.Sharp ? noteSize * 0.7f : noteSize * 0.5f;
                    using Font f = new Font("Arial", fontSize);
                    Point pos;
                    if (note.Sharp)
                    {
                        pos = new Point(notePosition.Xint - 80, notePosition.Yint - 37);
                    } else
                    {
                        pos = new Point(notePosition.Xint - 65, notePosition.Yint - 32);
                    }
                    g.DrawString(symbol, f, Brushes.Black, pos);
                }
                if (yNp < TopLinePosition)
                {
                    for (int i = TopLinePosition; i >= yNp; i -= Spacing)
                    {
                        g.DrawLine(p, new Point(xNp - 50, i), new Point(xNp + 50, i));
                    }
                }
                if (yNp > BottomLinePosition)
                {
                    for (int i = BottomLinePosition; i <= yNp; i += Spacing)
                    {
                        g.DrawLine(p, new Point(xNp - 50, i), new Point(xNp + 50, i));
                    }
                }
                counter++;
            }
            Bitmap result = new Bitmap(noteBitmap.Width + clefC.Width, noteBitmap.Height);
            using Graphics rg = Graphics.FromImage(result);
            rg.Clear(Color.White);
            rg.DrawImage(clefC, 0, clefC.Height, clefC.Width, clefC.Height);
            rg.DrawImage(noteBitmap, clefC.Width, 0);
            for (int i = 0; i < 5; i++)
            {
                Point startPoint = new Point(40, Offset + i * Spacing);
                Point endPoint = new Point(result.Width, Offset + i * Spacing);
                rg.DrawLine(p, startPoint, endPoint);
            }
            return result;
        }
        public static Bitmap NoteImageWithLetter(Note note, Clef clef, Keys key)
        {
            Keys[] noteKeys = { Keys.C, Keys.D, Keys.E, Keys.F, Keys.G, Keys.A, Keys.B };
            NoteLetter pressedNL = (NoteLetter)Array.IndexOf(noteKeys, key);
            Bitmap result = NoteImage([note], clef);
            using Graphics g = Graphics.FromImage(result);

            using Font font = ScaledLetterFont(100); 
            if(pressedNL == note.NoteLetter)
            {
                g.DrawString(key.ToString() + note.Octave.ToString(), font, Brushes.Green, 20, 0);
            } else
            {
                g.DrawString(key.ToString() + note.Octave.ToString(), font, Brushes.Red, 20, 0);
                g.DrawString(noteKeys[(int)note.NoteLetter].ToString() + note.Octave.ToString(), font, Brushes.Green, 20, 120);
            }
            return result;
        }
        public static Bitmap NoteImageWithLetter(Note correctNote, Clef clef, Note inputNote)
        {
            Bitmap result = NoteImage([correctNote], clef);
            using Graphics g = Graphics.FromImage(result);

            using Font font = ScaledLetterFont(100); 
            if (correctNote.Equals(inputNote))
            {
                g.DrawString(correctNote.ToString(), font, Brushes.Green, 20, 0);
            }
            else
            {
                g.DrawString(inputNote.ToString(), font, Brushes.Red, 20, 0);
                g.DrawString(correctNote.ToString(), font, Brushes.Green, 20, 120);
            }
            return result;
        }
        public static Bitmap IntervalImageWithNumber(List<Note> notes, Clef clef, Keys inputKey)
        {
            notes = notes.OrderBy(n => n.NumVal).ToList();
            Bitmap result = NoteImage(notes, clef);
            using Graphics g = Graphics.FromImage(result);
            using Font font = ScaledLetterFont(80); 
            Keys[] numkeys = [Keys.NumPad2, Keys.NumPad3, Keys.NumPad4, Keys.NumPad5, Keys.NumPad6, Keys.NumPad7, Keys.NumPad8];
            int pressedNo = Array.IndexOf(numkeys, inputKey) + 2;
            int distance = notes[0].Distance(notes[1]);
            if(pressedNo == distance)
            {
                g.DrawString(distance.ToString() + $"({notes[0].ToString()} - {notes[1].ToString()})", font, Brushes.Green, 20, 0);
            }
            else
            {
                g.DrawString(pressedNo.ToString(), font, Brushes.Red, 20, 0);
                g.DrawString(distance.ToString() + $"({notes[0].ToString()} - {notes[1].ToString()})", font, Brushes.Green, 20, 120);
            }
            return result;
        }
        private static void DrawNote(Graphics g, OVector position, int size)
        {
            using Pen p = new Pen(Brushes.Black, size / 8f);
            g.DrawEllipse(p, (float)(position.X - size / 2), (float)(position.Y - size / 4), size, size / 2);
        //    OVector start = new OVector(position.X - size / 2d, position.Y);
        //    OVector end = new OVector(position.X + size / 2d, position.Y);
        //    GraphicsPath upperPath = ArcPath(start, end, size * 0.65, 10);
        //    GraphicsPath lowerPath = ArcPath(end, start, size * 0.65, 10);
        //    float noteThickness = size / 8;
        //    using Pen pen = new Pen(Brushes.Black, noteThickness);
        //    pen.StartCap = LineCap.Round;
        //    pen.EndCap = LineCap.Round;
        //    g.DrawPath(pen, upperPath);
        //    g.DrawPath(pen, lowerPath);
        }

        public static GraphicsPath ArcPath(OVector start, OVector end, double radius, int steps)
        {
            GraphicsPath path = new GraphicsPath();
            double distance = start.Distance(end);
            double sweepAngle = Math.Asin(distance / 2 / radius) * 360 / Math.PI;
            OVector halfway = end.Copy().Subtract(start).Divide(2d);
            double triDist = Math.Sqrt(radius * radius - halfway.Magnitude * halfway.Magnitude);
            OVector center = halfway.Copy().Rotate(90).Normalize().Multiply(triDist).Add(start).Add(halfway);
            OVector arm = start.Copy().Subtract(center);
            double angleIncrement = sweepAngle / steps;
            for (int i = 0; i < steps; i++)
            {

                OVector lp1 = center.Copy().Add(arm);
                arm.Rotate(angleIncrement);
                OVector lp2 = center.Copy().Add(arm);
                path.AddLine(lp1.ToPoint(), lp2.ToPoint());
            }
            return path;
        }
        public static Note RandomNote(int minOctave, int maxOctave, bool includeSharpFlat)
        {
            Random r = new Random();            
            int octave = r.Next(minOctave, maxOctave + 1);

            int minNl = 0;
            int maxNl = 7;
            if (octave == 0) minNl = 5;
            if (octave == 8) maxNl = 1;
            NoteLetter nl = (NoteLetter)r.Next(minNl, maxNl);
            Note result = new Note(nl, octave);
            if (includeSharpFlat)
            {
                int rnd = r.Next(0, 10);
                if(rnd < 3)
                {
                    result.Sharp = true;
                    if(nl == NoteLetter.E || nl == NoteLetter.B)
                    {
                        result.NoteLetter = (NoteLetter)((int)nl - 1);
                    }
                }
                else if(rnd < 6)
                {
                    result.Flat = true;
                    if (nl == NoteLetter.F || nl == NoteLetter.C)
                    {
                        result.NoteLetter = (NoteLetter)((int)nl + 1);
                    }
                }
            }
            return result;
        }
        private static float DpiMult()
        {
            using (Graphics graphics = Graphics.FromHwnd(IntPtr.Zero))
            {
                float dpiX = graphics.DpiX; // Horizontal DPI
                return 96 / dpiX;
            }
        }
        private static Font ScaledLetterFont(float baseSize)
        {
            return new Font("Arial", baseSize * DpiMult());
        }
    }
}
