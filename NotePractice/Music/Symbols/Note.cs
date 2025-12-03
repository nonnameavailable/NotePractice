using Melanchall.DryWetMidi.MusicTheory;
using NotePractice.Music.Drawing;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
        private NoteDrawer _noteDrawer;
        public string TopNote { get; set; }
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
        /// <summary>
        /// Returns a new note with the NoteLetter shifted by the shiftValue
        /// </summary>
        /// <param name="shiftValue"></param>
        /// <returns></returns>
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
            return new Note((NoteLetter)newNlVal, newOctave, duration: Duration, accidental: Accidental);
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
        public Note IntervalNote(IntervalType it)
        {
            int[] shiftMap = [0, 1, 1, 2, 2, 3, 4, 4, 5, 5, 6, 6, 7];
            int resultMidiNumber = ToMidiNumber() + (int)it;
            Note shiftedNote = ShiftedNote(shiftMap[(int)it]);
            if(shiftedNote.ToMidiNumber() > resultMidiNumber)
            {
                if(shiftedNote.Accidental == Accidental.Sharp)
                {
                    shiftedNote.Accidental = Accidental.None;
                }
                else if(shiftedNote.Accidental == Accidental.None)
                {
                    shiftedNote.Accidental = Accidental.Flat;
                }
            }
            else if(shiftedNote.ToMidiNumber() < resultMidiNumber)
            {
                if(shiftedNote.Accidental == Accidental.Flat)
                {
                    shiftedNote.Accidental = Accidental.None;
                }
                else if(shiftedNote.Accidental == Accidental.None)
                {
                    shiftedNote.Accidental = Accidental.Sharp;
                }
            }
            Debug.Print(resultMidiNumber.ToString() + " x " + shiftedNote.ToMidiNumber().ToString());
            return shiftedNote;
        }
        public Note ShiftedByOctave(int octaveShift)
        {
            return new Note(NoteLetter, Octave + octaveShift, Accidental, Duration);
        }
        //public Note IntervalNote(IntervalQuality intervalQuality, int intervalValue)
        //{
        //    Note result = ShiftedNote(intervalValue - 1);
        //    int qualityShiftModifier = intervalQuality switch
        //    {
        //        IntervalQuality.Major => 1,
        //        _ => 0
        //    };
        //    int semitoneShift = (intervalValue - 2)
        //    int midiDif = result.ToMidiNumber() - this.ToMidiNumber();
        //}
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
    public enum IntervalType
    {
        PerfectUnison,
        MinorSecond,
        MajorSecond,
        MinorThird,
        MajorThird,
        PerfectFourth,
        AugmentedFourthOrDiminishedFifth,
        PerfectFifth,
        MinorSixth,
        MajorSixth,
        MinorSeventh,
        MajorSeventh,
        PerfectOctave
    }
    public enum IntervalQuality
    {
        Perfect,
        Minor,
        Major,
        Diminished,
        Augmented
    }
}
