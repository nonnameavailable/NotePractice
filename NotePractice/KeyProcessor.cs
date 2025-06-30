using NotePractice.Music;
using NotePractice.Music.Symbols;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotePractice
{
    public class KeyProcessor
    {
        private bool _isShiftDown, _isCtrlDown;
        private HashSet<Keys> _heldKeys;
        public KeyProcessor()
        {
            _isShiftDown = false;
            _isCtrlDown = false;
            _heldKeys = new();
        }
        public bool ProcessKey(Message msg, Keys keyData, MainForm mf)
        {
            mf.MainPictureBox.Focus();
            const int WM_KEYDOWN = 0x100;
            const int WM_KEYUP = 0x101;
            Symbol? s = WrittenSymbol(keyData, mf);
            //Keys keyData = (Keys)m.WParam.ToInt32();
            if (msg.Msg == WM_KEYDOWN)
            {
                if (_heldKeys.Contains(keyData)) return false;
                _heldKeys.Add(keyData);
                if (keyData == Keys.ShiftKey)
                {
                    _isShiftDown = true;
                    return true;
                } else if (keyData == Keys.ControlKey)
                {
                    _isCtrlDown = true;
                    return true;
                }
                if (mf.IsInPracticeMode)
                {
                    //PRACTICE
                    if (mf.PracticeMode == "Notes")
                    {
                        EvaluateNoteFromKey(keyData, mf);
                    }
                    else
                    {
                        EvaluateIntervalFromKey(keyData, mf);
                    }
                    return true;
                }
                else
                {
                    //WRITING
                    if (keyData == Keys.Back)
                    {
                        //if (mf.WrittenSymbols.Count > 0) mf.WrittenSymbols.RemoveAt(mf.WrittenSymbols.Count - 1);
                        mf.Song.RemoveSymbol(mf.SelectedStaffIndex, mf.SelectedStaffClef);
                    }
                    else
                    {
                        //if (s != null) mf.WrittenSymbols.Add(s);
                        if (s != null)
                        {
                            mf.Song.AddSymbol(s, mf.SelectedStaffIndex, mf.SelectedStaffClef);
                            //Debug.Print(mf.MidiSender.CallCount.ToString());
                            if (s is Note note) mf.MidiSender.SendNotesToMidiAsyncON([note]);
                        }
                    }
                    mf.UpdatePictureBoxAfterWrite();
                    //mf.MainPictureBox.Image = MusicDrawer.MusicBitmap(mf.WrittenSymbols);
                    return true;
                }
            } else if (msg.Msg == WM_KEYUP)
            {
                _heldKeys.Remove(keyData);
                if (keyData == Keys.ShiftKey)
                {
                    _isShiftDown = false;
                    return true;
                }
                else if (keyData == Keys.ControlKey)
                {
                    _isCtrlDown = false;
                    return true;
                }
                else if (keyData == Keys.Oemplus || keyData == Keys.Right)
                {
                    mf.WritingDuration *= 2;
                    return true;
                }
                else if (keyData == Keys.OemMinus || keyData == Keys.Left)
                {
                    mf.WritingDuration /= 2;
                    return true;
                }
                else if (keyData == Keys.Oemcomma || keyData == Keys.Down)
                {
                    mf.WritingOctave--;
                    return true;
                }
                else if (keyData == Keys.OemPeriod || keyData == Keys.Up)
                {
                    mf.WritingOctave++;
                    return true;
                }
                if (s is Note note) mf.MidiSender.SendNotesToMidiAsyncOFF([note]);
                return true;
            }
            return false;
        }
        private Note? NoteFromKey(Keys keyData, MainForm mf)
        {
            Keys[] noteKeys = { Keys.C, Keys.D, Keys.E, Keys.F, Keys.G, Keys.A, Keys.B };
            Keys actualKey = keyData & Keys.KeyCode;
            int noteIndex = Array.IndexOf(noteKeys, actualKey);
            //int noteIndex = Array.IndexOf(noteKeys, keyData);
            //bool isShiftDown = (keyData & Keys.Shift) == Keys.Shift;
            //bool isCtrlDown = (keyData & Keys.Control) == Keys.Control;
            if (noteIndex >= 0)
            {
                Accidental accidental = Accidental.None;
                if (_heldKeys.Contains(Keys.ShiftKey)) accidental = Accidental.Sharp;
                if (_heldKeys.Contains(Keys.ControlKey)) accidental = Accidental.Flat;
                return new Note((NoteLetter)noteIndex, mf.WritingOctave, accidental, mf.WritingDuration);
            }
            else return null;
        }
        private Symbol? WrittenSymbol(Keys keyData, MainForm mf)
        {
            Note note = NoteFromKey(keyData, mf);
            if (note != null) return note;
            if (keyData == Keys.Space)
            {
                return new Shift();
            }
            else if (keyData == Keys.OemSemicolon)
            {
                return new BarLine();
            }
            else if (keyData == Keys.R)
            {
                return new Rest(mf.WritingDuration);
            }
            else if ((keyData & Keys.L) == Keys.L)
            {
                if (_isShiftDown)
                {
                    return new ClefSymbol(Clef.Bass);
                }
                else
                {
                    return new ClefSymbol(Clef.Treble);
                }
            }
            else return null;
        }
        private static void EvaluateNoteFromKey(Keys keyData, MainForm mf)
        {
            Clef nextClef = mf.TCP.NextClef;

            int minOctave = nextClef == Clef.Treble ? mf.TCP.TrebleMin : mf.TCP.BassMin;
            int maxOctave = nextClef == Clef.Treble ? mf.TCP.TrebleMax : mf.TCP.BassMax;

            mf.ExtraPictureBox.Image?.Dispose();
            mf.ExtraPictureBox.Image = Noter.NoteImageWithLetter(mf.Note, mf.TCP.PreviousClef, keyData);
            mf.MainPictureBox.Image?.Dispose();
            mf.Note = Noter.RandomNote(minOctave, maxOctave, mf.TCP.IncludeSharpFlat, mf.PracticeNoteLength);
            mf.MainPictureBox.Image = MusicDrawer.MusicBitmap(MusicDrawer.StartSymbols(nextClef, [mf.Note]), false);
            mf.TCP.PreviousClef = nextClef;
            //mf.MidiSender.SendNoteToMidiAsync(mf.Note);
        }
        private static void EvaluateIntervalFromKey(Keys keyData, MainForm mf)
        {
            Clef nextClef = mf.TCP.NextClef;
            mf.ExtraPictureBox.Image?.Dispose();
            mf.ExtraPictureBox.Image = Noter.IntervalImageWithNumber(mf.Notes, mf.TCP.PreviousClef, keyData, mf.NoteSpacing);
            mf.MainPictureBox.Image?.Dispose();
            int minOctave = nextClef == Clef.Treble ? mf.TCP.TrebleMin : mf.TCP.BassMin;
            int maxOctave = nextClef == Clef.Treble ? mf.TCP.TrebleMax : mf.TCP.BassMax;
            mf.Note = Noter.RandomNote(minOctave, maxOctave, false, mf.PracticeNoteLength);
            int shift = new Random().Next(-7, 8);
            mf.MainPictureBox.Image?.Dispose();

            List<Symbol> shiftedNotes = new List<Symbol>();
            shiftedNotes.Add(mf.Note);
            for(int i = 0; i < mf.NoteSpacing; i++)
            {
                shiftedNotes.Add(new Shift());
            }
            shiftedNotes.Add(mf.Note.ShiftedNote(shift));
            mf.MainPictureBox.Image = MusicDrawer.MusicBitmap(MusicDrawer.StartSymbols(nextClef, shiftedNotes), false);
            mf.TCP.PreviousClef = nextClef;
            List<Symbol> shifts = new List<Symbol>();
            for(int i = 0; i < mf.NoteSpacing; i++)
            {
                shifts.Add(new Shift());
            }
            mf.Notes = [mf.Note, mf.Note.ShiftedNote(shift)];
        }
    }
}
