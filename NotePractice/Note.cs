using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotePractice
{
    public class Note
    {
        public Note(NoteLetter noteLetter, int octave, bool sharp = false, bool flat = false)
        {
            NoteLetter = noteLetter;
            Octave = octave;
            Sharp = sharp;
            Flat = flat;
        }
        public NoteLetter NoteLetter { get; }
        public int Octave { get; }
        public bool Flat { get; }
        public bool Sharp { get; }
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
            return letterCond && n.Octave == Octave;
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
