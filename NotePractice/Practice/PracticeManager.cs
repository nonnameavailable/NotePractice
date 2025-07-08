using NotePractice.Music;
using NotePractice.Music.Symbols;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Media;
using NotePractice.Properties;

namespace NotePractice.Practice
{
    public class PracticeManager
    {
        public event EventHandler NoteCountReached;
        public List<Note> PracticeNotes { get; set; }
        public List<Note> InputNotes { get; }
        private SoundPlayer _metronomeClicker;
        System.Threading.Timer _metronomeTimer;
        public PracticeManager()
        {
            InputNotes = new();
            PracticeNotes = new();
            _metronomeClicker = new SoundPlayer(Resources.metronome_click);
        }

        public void AddPracticeNote(Note note) => PracticeNotes.Add(note);
        public void AddInputNote(Note note)
        {
            InputNotes.Add(note);
            if(InputNotes.Count >= PracticeNotes.Count)
            {
                NoteCountReached?.Invoke(this, EventArgs.Empty);
            }
        }
        public bool EvaluateNotes()
        {
            if (PracticeNotes.Count != InputNotes.Count) return false;
            bool result = true;
            for(int i = 0; i < InputNotes.Count; i++)
            {
                if (!PracticeNotes[i].Equals(InputNotes[i])) result = false;
            }
            return result;
        }
        public bool EvaluateInterval(int inputNo)
        {
            if (PracticeNotes.Count != 2) return false;
            return inputNo == PracticeNotes[0].WhiteKeyDistance(PracticeNotes[1]);
        }
        public int FirstIntervalDistance()
        {
            if (PracticeNotes.Count != 2) return -1;
            return PracticeNotes[0].WhiteKeyDistance(PracticeNotes[1]);
        }
        public void GenerateNotePractice(int noteCount, int minOctave, int maxOctave, bool includeSharpFlat, int noteLength)
        {
            PracticeNotes.Clear();
            InputNotes.Clear();

            int minMidiValue = (minOctave + 1) * 12;
            int maxMidiValue = (maxOctave + 2) * 12;

            var rnd = new Random();
            int firstMidiValue = rnd.Next(minMidiValue, maxMidiValue);
            for (int i = 0; i < noteCount; i++)
            {
                Note note = new Note(rnd.Next(minMidiValue, maxMidiValue), noteLength);
                if (!includeSharpFlat)
                {
                    note.Accidental = Accidental.None;
                } else
                {
                    int rand = rnd.Next(0, 100);
                    if(rand < 33)
                    {
                        note.Accidental = Accidental.None;
                    } else if(rand < 66 && note.NoteLetter != NoteLetter.E && note.NoteLetter != NoteLetter.B)
                    {
                        note.Accidental = Accidental.Sharp;
                    }
                    else if (note.NoteLetter != NoteLetter.F && note.NoteLetter != NoteLetter.C)
                    {
                        note.Accidental = Accidental.Flat;
                    }
                }
                AddPracticeNote(note);
            }
        }
        public string PracticeNotesString()
        {
            return String.Join(", ", PracticeNotes.Select(note => note.ToString()));
        }
        public string InputNotesString()
        {
            return String.Join(", ", InputNotes.Select(note => note.ToString()));
        }

        public static List<Symbol> SpacedSymbolList(List<Symbol> symbols, int spacing)
        {
            List<Symbol> result;
            if (spacing > 0 && symbols.Count > 1)
            {
                result = new();
                result.Add(symbols[0]);
                for (int i = 1; i < symbols.Count; i++)
                {
                    for (int j = 0; j < spacing; j++)
                    {
                        result.Add(new Shift());
                    }
                    result.Add(symbols[i]);
                }
            }
            else
            {
                result = new(symbols);
            }
            return result;
        }
        public static Note RandomNote(int minOctave, int maxOctave, bool includeSharpFlat, int duration)
        {
            Random r = new Random();
            int octave = r.Next(minOctave, maxOctave + 1);

            int minNl = 0;
            int maxNl = 7;
            if (octave == 0) minNl = 5;
            if (octave == 8) maxNl = 1;
            NoteLetter nl = (NoteLetter)r.Next(minNl, maxNl);
            Note result = new Note(nl, octave);
            if (includeSharpFlat)
            {
                int rnd = r.Next(0, 10);
                if (rnd < 3)
                {
                    result.Accidental = Accidental.Sharp;
                    if (nl == NoteLetter.E || nl == NoteLetter.B)
                    {
                        result.NoteLetter = (NoteLetter)((int)nl - 1);
                    }
                }
                else if (rnd < 6)
                {
                    result.Accidental = Accidental.Flat;
                    if (nl == NoteLetter.F || nl == NoteLetter.C)
                    {
                        result.NoteLetter = (NoteLetter)((int)nl + 1);
                    }
                }
            }
            result.Duration = duration;
            return result;
        }
        public void StartMetronome(int delay)
        {
            _metronomeTimer = new(_ => _metronomeClicker.Play(), null, 0, delay);
        }
        public void StopMetronome()
        {
            _metronomeTimer?.Dispose();
        }
    }
}
