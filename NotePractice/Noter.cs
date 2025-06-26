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
using NotePractice.Music;
using NotePractice.Music.Symbols;
using NotePractice.Properties;

namespace NotePractice
{
    public class Noter
    {
        public static Bitmap NoteImageWithLetter(Note note, Clef clef, Keys key)
        {
            Keys[] noteKeys = { Keys.C, Keys.D, Keys.E, Keys.F, Keys.G, Keys.A, Keys.B };
            NoteLetter pressedNL = (NoteLetter)Array.IndexOf(noteKeys, key);
            Bitmap result = MusicDrawer.MusicBitmap(MusicDrawer.StartSymbols(clef, [note]), false);
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
            Bitmap result = MusicDrawer.MusicBitmap(MusicDrawer.StartSymbols(clef, [correctNote]), false);
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
        public static Bitmap IntervalImageWithNumber(List<Note> notes, Clef clef, Keys inputKey, int hDistance)
        {
            notes = notes.OrderBy(n => n.NumVal).ToList();
            List<Symbol> symbols = [new ClefSymbol(clef), new Shift(), new Shift()];
            symbols.Add(notes[0]);
            for(int i = 0; i < hDistance; i++)
            {
                symbols.Add(new Shift());
            }
            symbols.Add(notes[1]);
            //symbols = symbols.Concat(notes).ToList();
            Bitmap result = MusicDrawer.MusicBitmap(symbols, false);
            using Graphics g = Graphics.FromImage(result);
            using Font font = ScaledLetterFont(60); 
            Keys[] numPadKeys = [Keys.NumPad2, Keys.NumPad3, Keys.NumPad4, Keys.NumPad5, Keys.NumPad6, Keys.NumPad7, Keys.NumPad8];
            Keys[] numRowKeys = [Keys.D2, Keys.D3, Keys.D4, Keys.D5, Keys.D6, Keys.D7, Keys.D8];
            int pressedNo = Array.IndexOf(numPadKeys, inputKey) + 2;
            if (pressedNo == 1) pressedNo = Array.IndexOf(numRowKeys, inputKey) + 2;
            int distance = notes[0].Distance(notes[1]);
            if(pressedNo == distance)
            {
                g.DrawString(distance.ToString() + $"({notes[0].ToString()} - {notes[1].ToString()})", font, Brushes.Green, 20, 0);
            }
            else
            {
                g.DrawString(pressedNo.ToString(), font, Brushes.Red, 20, 0);
                g.DrawString(distance.ToString() + $"({notes[0].ToString()} - {notes[1].ToString()})", font, Brushes.Green, 20, 80);
            }
            return result;
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
        public static Note RandomNote(int minOctave, int maxOctave, bool includeSharpFlat, int duration)
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
                    result.Accidental = Accidental.Sharp;
                    if(nl == NoteLetter.E || nl == NoteLetter.B)
                    {
                        result.NoteLetter = (NoteLetter)((int)nl - 1);
                    }
                }
                else if(rnd < 6)
                {
                    result.Accidental = Accidental.Flat;
                    if (nl == NoteLetter.F || nl == NoteLetter.C)
                    {
                        result.NoteLetter = (NoteLetter)((int)nl + 1);
                    }
                }
            }
            result.Duration = duration;
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
