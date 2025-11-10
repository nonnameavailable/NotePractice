using NotePractice.Music.Symbols;
using NotePractice.Music;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using NotePractice.Music.Drawing;

namespace NotePractice.Practice
{
    public class PracticeDrawer
    {
        public static Bitmap EvaluatedNotePractice(PracticeManager pm, Clef clef, int noteSpacing)
        {
            List<Symbol> practiceSymbolList = PracticeManager.SpacedSymbolList(pm.PracticeNotes.Cast<Symbol>().ToList(), noteSpacing);
            Bitmap result = MusicDrawer.MusicBitmap(MusicDrawer.StartSymbols(clef, practiceSymbolList), false);
            using Graphics g = Graphics.FromImage(result);
            using Font font = ScaledLetterFont(100);
            if (pm.EvaluateNotes())
            {
                g.DrawString(pm.PracticeNotesString(), font, Brushes.Green, 20, 0);
            }
            else
            {
                g.DrawString(pm.InputNotesString(), font, Brushes.Red, 20, 0);
                g.DrawString(pm.PracticeNotesString(), font, Brushes.Green, 20, 120);
            }
            return result;
        }
        public static Bitmap EvaluatedIntervalPractice(PracticeManager pm, Clef clef, int noteSpacing, Keys keyData)
        {
            List<Symbol> practiceSymbolList = PracticeManager.SpacedSymbolList(pm.PracticeNotes.Cast<Symbol>().ToList(), noteSpacing);
            Bitmap result = MusicDrawer.MusicBitmap(MusicDrawer.StartSymbols(clef, practiceSymbolList), false);
            using Graphics g = Graphics.FromImage(result);
            using Font font = ScaledLetterFont(100);

            Keys[] numPadKeys = [Keys.NumPad2, Keys.NumPad3, Keys.NumPad4, Keys.NumPad5, Keys.NumPad6, Keys.NumPad7, Keys.NumPad8];
            Keys[] numRowKeys = [Keys.D2, Keys.D3, Keys.D4, Keys.D5, Keys.D6, Keys.D7, Keys.D8];
            int pressedNo = Array.IndexOf(numPadKeys, keyData) + 2;
            string correctString = pm.FirstIntervalDistance() + "(" + pm.PracticeNotes[0].ToString() + "-" + pm.PracticeNotes[1].ToString() + ")";
            if (pm.EvaluateInterval(pressedNo))
            {
                g.DrawString(correctString, font, Brushes.Green, 20, 0);
            }
            else
            {
                g.DrawString(pressedNo.ToString(), font, Brushes.Red, 20, 0);
                g.DrawString(correctString, font, Brushes.Green, 20, 120);
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
