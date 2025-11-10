using NotePractice.Music.Symbols;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotePractice.Music
{
    public interface Symbol
    {
        void Draw(Graphics g, int xPos, Clef clef = Clef.Treble, Color? color = null);
        string StringForFileExport();
    }
}
