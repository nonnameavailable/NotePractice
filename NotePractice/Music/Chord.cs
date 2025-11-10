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
