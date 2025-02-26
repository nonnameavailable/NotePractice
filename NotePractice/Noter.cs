using System;
using System.Collections.Generic;
using System.Diagnostics;
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
        public static Bitmap NoteImage(List<Note> notes, Clef clef)
        {
            Bitmap trebleC = Resources.treble_clef;
            Bitmap BassC = Resources.bass_clef;
            using Bitmap noteBitmap = new Bitmap(trebleC.Width, trebleC.Height * 3);
            using Graphics g = Graphics.FromImage(noteBitmap);
            g.Clear(Color.White);
            int offset = 64 + trebleC.Height;
            int spacing = 30;
            int topLinePosition = offset;
            int bottomLinePosition = offset + spacing * 4;

            int noteShift = spacing / 2;
            using Pen p = new Pen(Brushes.Black, 3);
            foreach (Note note in notes)
            {
                int noteInt = (int)note.NoteLetter;
                int octave = note.Octave;

                int xNp = noteBitmap.Width / 2;
                int yNp;
                if (clef == Clef.Treble)
                {
                    yNp = (int)(offset + spacing * 5 - noteInt * noteShift + 28 * noteShift - octave * 7 * noteShift);
                }
                else
                {
                    yNp = (int)(offset - spacing - noteInt * noteShift + 28 * noteShift - octave * 7 * noteShift);
                }

                OVector notePosition = new OVector(xNp, yNp);

                DrawNote(g, notePosition, noteBitmap.Width / 5);
                if (yNp < topLinePosition)
                {
                    for (int i = topLinePosition; i >= yNp; i -= spacing)
                    {
                        g.DrawLine(p, new Point(xNp - 50, i), new Point(xNp + 50, i));
                    }
                }
                if (yNp > bottomLinePosition)
                {
                    for (int i = bottomLinePosition; i <= yNp; i += spacing)
                    {
                        g.DrawLine(p, new Point(xNp - 50, i), new Point(xNp + 50, i));
                    }
                }
            }


            Bitmap result = new Bitmap(noteBitmap.Width + trebleC.Width, noteBitmap.Height);
            using Graphics rg = Graphics.FromImage(result);
            rg.Clear(Color.White);

            if (clef == Clef.Treble)
            {
                rg.DrawImage(trebleC, 0, trebleC.Height);
            } else
            {
                rg.DrawImage(BassC, 0, trebleC.Height);
            }
            rg.DrawImage(noteBitmap, trebleC.Width, 0);
            for (int i = 0; i < 5; i++)
            {
                Point startPoint = new Point(40, offset + i * spacing);
                Point endPoint = new Point(result.Width, offset + i * spacing);

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

            using Font font = new Font("Arial", 100);
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

            using Font font = new Font("Arial", 100);
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
        private static void DrawNote(Graphics g, OVector position, int size)
        {
            OVector start = new OVector(position.X - size / 2d, position.Y);
            OVector end = new OVector(position.X + size / 2d, position.Y);
            GraphicsPath upperPath = ArcPath(start, end, size * 0.65, 10);
            GraphicsPath lowerPath = ArcPath(end, start, size * 0.65, 10);
            float noteThickness = size / 8;
            using Pen pen = new Pen(Brushes.Black, noteThickness);
            pen.StartCap = LineCap.Round;
            pen.EndCap = LineCap.Round;
            g.DrawPath(pen, upperPath);
            g.DrawPath(pen, lowerPath);
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
        public static Note RandomNote(int minOctave, int maxOctave)
        {
            Random r = new Random();            
            int octave = r.Next(minOctave, maxOctave + 1);

            int minNl = 0;
            int maxNl = 7;
            if (octave == 0) minNl = 5;
            if (octave == 8) maxNl = 1;
            NoteLetter nl = (NoteLetter)r.Next(minNl, maxNl);
            Note result = new Note(nl, octave);
            return result;
        }
    }
}
