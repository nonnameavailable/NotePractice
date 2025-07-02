using Melanchall.DryWetMidi.MusicTheory;
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
        public int WhiteKeyValue { get => Octave * 7 + (int)NoteLetter; }
        public int Duration { get; set; }
        public int XPosShift { get; set; }
        public bool DrawStem { get; set; }
        public bool DrawFlag { get; set; }
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
        public Note(int midiValue, int duration = 1)
        {
            Octave = (midiValue / 12) - 1;
            int noteIndex = midiValue % 12;
            switch (noteIndex)
            {
                case 0: NoteLetter = NoteLetter.C; break;
                case 1: NoteLetter = NoteLetter.C; Accidental = Accidental.Sharp; break;
                case 2: NoteLetter = NoteLetter.D; break;
                case 3: NoteLetter = NoteLetter.D; Accidental = Accidental.Sharp; break;
                case 4: NoteLetter = NoteLetter.E; break;
                case 5: NoteLetter = NoteLetter.F; break;
                case 6: NoteLetter = NoteLetter.F; Accidental = Accidental.Sharp; break;
                case 7: NoteLetter = NoteLetter.G; break;
                case 8: NoteLetter = NoteLetter.G; Accidental = Accidental.Sharp; break;
                case 9: NoteLetter = NoteLetter.A; break;
                case 10: NoteLetter = NoteLetter.A; Accidental = Accidental.Sharp; break;
                case 11: NoteLetter = NoteLetter.B; break;
                default: throw new Exception("Invalid MIDI note.");
            }
            Duration = duration;
            DrawStem = true;
            DrawFlag = false;
            XPosShift = 0;
            StemLength = MusicDrawer.DefaultStemLength;
            Velocity = 100;
            _noteDrawer = new NoteDrawer(this);
        }
        public override string ToString()
        {
            string sharpFlat = "";
            if (Accidental == Accidental.Sharp) sharpFlat = "#";
            if (Accidental == Accidental.Flat) sharpFlat = "♭";
            return Enum.GetName(typeof(NoteLetter), NoteLetter) + Octave.ToString() + sharpFlat;
        }
        public int ToMidiNumber()
        {
            int[] letterMap = [0, 2, 4, 5, 7, 9, 11];
            int accidentalAdjustment = 0;
            if (Accidental == Accidental.Sharp) accidentalAdjustment = 1;
            if (Accidental == Accidental.Flat) accidentalAdjustment = -1;
            return (Octave + 1) * 12 + letterMap[(int)NoteLetter] + accidentalAdjustment;
        }
        public override bool Equals(object? obj)
        {
            if (obj == null || obj is not Note) return false;
            Note n = (Note)obj;
            return ToMidiNumber() == n.ToMidiNumber();
        }
        public int WhiteKeyDistance(Note note)
        {
            return Math.Abs(note.WhiteKeyValue - WhiteKeyValue) + 1;
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
            return new Note((NoteLetter)newNlVal, newOctave, duration: Duration);
        }
        public void Draw(Graphics g, int xPos, Clef clef, Color? color = null)
        {
            Color resultColor;
            if (color == null)
            {
                resultColor = Color.Black;
            } else
            {
                resultColor = (Color)color;
            }
                _noteDrawer.DrawNote(g, xPos, clef, resultColor);
        }

        string Symbol.StringForFileExport()
        {
            throw new NotImplementedException();
        }
        public OVector StemEnd(int xPos, Clef clef) => _noteDrawer.StemEnd(xPos, clef);
        public int YPos(Clef clef) => _noteDrawer.YPos(clef);
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
