using NotePractice.Music.Symbols;
using NotePractice.Music;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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
