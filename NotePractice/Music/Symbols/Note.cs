using NotePractice.Music;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotePractice.Music.Symbols
{
    public class Note : Symbol
    {
        public Note(NoteLetter noteLetter, int octave, bool sharp = false, bool flat = false, int duration = 1)
        {
            NoteLetter = noteLetter;
            Octave = octave;
            Sharp = sharp;
            Flat = flat;
            Duration = duration;
        }
        public NoteLetter NoteLetter { get; set; }
        public int Octave { get; set; }
        public bool Flat { get; set; }
        public bool Sharp { get; set; }
        public int NumVal { get => Octave * 7 + (int)NoteLetter; }
        public int Duration { get; set; }

        public SymbolType Type { get => SymbolType.Note; }

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
