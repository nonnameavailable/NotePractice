using NotePractice.Music.Symbols;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotePractice.Music
{
    public class Music
    {
        public const int Unit = 30;
        public List<Symbol> SymbolList { get; set; }
        public Music() 
        { 
            SymbolList = new List<Symbol>();
        }
    }
    public enum SymbolType
    {
        Note,
        Rest,
        BarLine,
        Shift,
        Clef
    }
    public interface Symbol
    {
        void Draw(Graphics g, int xPos, Clef clef = Clef.Treble);
    }
}
