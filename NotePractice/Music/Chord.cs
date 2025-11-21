using NotePractice.Music.Symbols;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotePractice.Music
{
    public class Chord
    {
        public Note Root { get; private set; }
        public ChordQuality Quality { get; private set; }
        public List<Note> Notes { get; set; }
        public List<Symbol> NotesS { get => Notes.Cast<Symbol>().ToList(); }
        public List<Note> FirstInversion
        {
            get
            {
                return [Notes[1], Notes[2], Notes[0].ShiftedByOctave(1)];
            }
        }
        public List<Note> SecondInversion
        {
            get
            {
                return [Notes[2], Notes[0].ShiftedByOctave(1), Notes[1].ShiftedByOctave(1)];
            }
        }
        public List<Symbol> FirstInversionS { get => FirstInversion.Cast<Symbol>().ToList(); }
        public List<Symbol> SecondInversionS { get => SecondInversion.Cast<Symbol>().ToList(); }
        public Chord(Note root, ChordQuality quality)
        {
            Quality = quality;
            Root = root;
            Notes = new();
            Note firstNote = quality switch
            {
                ChordQuality.Major => root,
                ChordQuality.Minor => root,
                ChordQuality.Seventh => root,
                _ => root
            };
            Note secondNote = quality switch
            {
                ChordQuality.Major => root.IntervalNote(IntervalType.MajorThird),
                ChordQuality.Minor => root.IntervalNote(IntervalType.MinorThird),
                ChordQuality.Seventh => root.IntervalNote(IntervalType.MajorThird),
                _ => root
            };
            Note thirdNote = quality switch
            {
                ChordQuality.Major =>root.IntervalNote(IntervalType.PerfectFifth),
                ChordQuality.Minor => root.IntervalNote(IntervalType.PerfectFifth),
                ChordQuality.Seventh => root.IntervalNote(IntervalType.MinorSeventh),
                _ => root
            };
            Notes.Add(firstNote);
            Notes.Add(secondNote);
            Notes.Add(thirdNote);
            SetTopNote();
        }
        private void SetTopNote()
        {
            Notes[Notes.Count - 1].TopNote = Quality switch
            {
                ChordQuality.Major => Root.NoteLetter.ToString(),
                ChordQuality.Minor => Root.NoteLetter.ToString().ToLower(),
                ChordQuality.Seventh => Root.NoteLetter.ToString() + "7",
                _ => ""
            };
        }
        public Chord Invert(int inversion = 1)
        {
            Notes.ForEach(n => n.TopNote = "");
            for(int i = 0; i < inversion; i++)
            {
                Note n = Notes[0];
                Notes.RemoveAt(0);
                Notes.Add(n.ShiftedByOctave(1));
            }
            SetTopNote();
            return this;
        }
        public static List<Symbol> Nskp()
        {
            Chord D = new Chord(new Note(NoteLetter.D, 3), ChordQuality.Major);
            Chord E = new Chord(new Note(NoteLetter.E, 3), ChordQuality.Major);
            Chord A = new Chord(new Note(NoteLetter.A, 2), ChordQuality.Major).Invert(2);
            Chord G = new Chord(new Note(NoteLetter.G, 3), ChordQuality.Major);
            Chord A7 = new Chord(new Note(NoteLetter.A, 2), ChordQuality.Seventh).Invert();
            List <Symbol> result = new();
            result.Add(new ClefSymbol(Clef.Bass));
            result.Add(new Shift());
            result.Add(new Shift());
            result.AddRange(D.NotesS);
            result.Add(new Shift());
            result.AddRange(E.NotesS);
            result.Add(new Shift());
            result.AddRange(A.NotesS);
            result.Add(new Shift());
            result.AddRange(D.NotesS);
            result.Add(new Shift());
            result.AddRange(A.NotesS);
            result.Add(new Shift());
            result.AddRange(E.NotesS);
            result.Add(new Shift());
            result.AddRange(A.NotesS);
            result.Add(new Shift());
            result.AddRange(A7.NotesS);
            result.Add(new Shift());
            result.Add(new Shift());
            result.AddRange(D.NotesS);
            result.Add(new Shift());
            result.AddRange(A.NotesS);
            result.Add(new Shift());
            result.AddRange(D.NotesS);
            result.Add(new Shift());
            result.AddRange(G.NotesS);
            result.Add(new Shift());
            result.AddRange(D.NotesS);
            result.Add(new Shift());
            result.AddRange(A7.NotesS);
            result.Add(new Shift());
            result.Add(new Shift());
            result.AddRange(D.NotesS);
            return result;
        }
    }
    public enum ChordQuality
    {
        Major,
        Minor,
        Diminished,
        Augmented,
        Seventh
    }
}
