using NotePractice.Music;
using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotePractice.Music.Symbols
{
    public class Note : Symbol
    {
        public NoteLetter NoteLetter { get; set; }
        public int Octave { get; set; }
        public Accidental Accidental { get; set; }
        private int _velocity;
        public int Velocity
        {
            get
            {
                return _velocity;
            }
            set
            {
                _velocity = Math.Clamp(value, 0, 127);
            }
        }
        public int NumVal { get => Octave * 7 + (int)NoteLetter; }
        public int Duration { get; set; }
        public int XPosShift { get; set; }
        public bool DrawStem { get; set; }
        public bool DrawFlag { get; set; }
        //public bool StemAlwaysRight { get; set; }
        public int StemLength { get; set; }
        public bool IsSharp { get => Accidental == Accidental.Sharp; }
        public bool IsFlat { get => Accidental == Accidental.Flat; }
        public Direction StemSide { get => _noteDrawer.StemSide; set => _noteDrawer.StemSide = value; }
        public Direction StemDirection { get => _noteDrawer.StemDirection; set => _noteDrawer.StemDirection = value; }
        public SymbolType Type { get => SymbolType.Note; }
        private NoteDrawer _noteDrawer;

        public Note(NoteLetter noteLetter, int octave, Accidental accidental = Accidental.None, int duration = 1)
        {
            NoteLetter = noteLetter;
            Octave = octave;
            Duration = duration;
            DrawStem = true;
            DrawFlag = false;
            XPosShift = 0;
            Accidental = accidental;
            StemLength = MusicDrawer.DefaultStemLength;
            Velocity = 100;
            _noteDrawer = new NoteDrawer(this);
        }
        public override string ToString()
        {
            string sharpFlat = "";
            if (Accidental == Accidental.Sharp) sharpFlat = "#";
            if (Accidental == Accidental.Flat) sharpFlat = "b";
            return Enum.GetName(typeof(NoteLetter), NoteLetter) + Octave.ToString() + sharpFlat;
        }
        public override bool Equals(object? obj)
        {
            if (obj == null || obj is not Note) return false;
            Note n = (Note)obj;
            bool letterCond = n.NoteLetter == NoteLetter;
            if (n.IsSharp && IsFlat)
            {
                letterCond = (NoteLetter)((int)n.NoteLetter + 1) == NoteLetter;
            } else if (n.IsFlat && IsSharp)
            {
                letterCond = (NoteLetter)((int)NoteLetter + 1) == n.NoteLetter;
            }
            if ((n.IsFlat || n.IsSharp) && !IsFlat && !IsSharp) letterCond = false;
            if ((IsFlat || IsSharp) && !n.IsFlat && !n.IsSharp) letterCond = false;
            return letterCond && n.Octave == Octave;
        }
        public int Distance(Note note)
        {
            return Math.Abs(note.NumVal - NumVal) + 1;
        }
        public Direction GetStemDirection(Clef clef)
        {
            return _noteDrawer.GetStemDirection(clef);
        }
        public Note ShiftedNote(int shiftValue)
        {
            int newNlVal = (int)NoteLetter + shiftValue;
            int newOctave = Octave;
            if (newNlVal > 6)
            {
                newNlVal -= 7;
                newOctave++;
            }
            if (newNlVal < 0)
            {
                newNlVal += 7;
                newOctave--;
            }
            return new Note((NoteLetter)newNlVal, newOctave);
        }
        public void Draw(Graphics g, int xPos, Clef clef)
        {
            _noteDrawer.DrawNote(g, xPos, clef);
        }

        string Symbol.StringForFileExport()
        {
            throw new NotImplementedException();
        }
        public OVector StemEnd(int xPos, Clef clef) => _noteDrawer.StemEnd(xPos, clef);
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
    public enum Accidental
    {
        None,
        Sharp,
        Flat,
        Natural
    }
    public enum Direction
    {
        Up,
        Down,
        Left,
        Right
    }
}
