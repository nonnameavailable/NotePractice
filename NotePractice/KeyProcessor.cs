using NotePractice.Music;
using NotePractice.Music.Drawing;
using NotePractice.Music.Symbols;
using NotePractice.Practice;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NotePractice
{
    public class KeyProcessor
    {
        private bool _isShiftDown, _isCtrlDown;
        private HashSet<Keys> _heldKeys;
        private List<DateTime> _keyDownTimes;
        private List<DateTime> _keyUpTimes;
        public KeyProcessor()
        {
            _isShiftDown = false;
            _isCtrlDown = false;
            _heldKeys = new();
            _keyDownTimes = new();
            _keyUpTimes = new();
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
                _keyDownTimes.Add(DateTime.Now);
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
                        //EvaluateNoteFromKey(keyData, mf);
                        AddNoteFromKey(keyData, mf);
                        if (s is Note note)
                        {
                            mf.MidiSender.SendNotesToMidiAsyncON([note]);
                            Debug.Print("sending note");
                        }
                    }
                    else
                    {
                        if (mf.PM.PracticeNotes.Count == 2)
                        {
                            EvaluateIntervalFromKey(keyData, mf);
                        }
                        else return false;
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
                _keyUpTimes.Add(DateTime.Now);
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
        private void AddNoteFromKey(Keys keyData, MainForm mf)
        {
            Keys[] noteKeys = { Keys.C, Keys.D, Keys.E, Keys.F, Keys.G, Keys.A, Keys.B };
            if (Array.IndexOf(noteKeys, keyData) == -1) return;
            NoteLetter pressedNL = (NoteLetter)Array.IndexOf(noteKeys, keyData);
            Note templateNote = mf.PM.PracticeNotes[mf.PM.InputNotes.Count];
            mf.PM.AddInputNote(new Note(pressedNL, templateNote.Octave, templateNote.Accidental, templateNote.Duration));
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
        private static void EvaluateIntervalFromKey(Keys keyData, MainForm mf)
        {
            Clef nextClef = mf.TCP.NextClef;
            mf.ExtraPictureBox.Image?.Dispose();
            //mf.ExtraPictureBox.Image = Noter.IntervalImageWithNumber(mf.Notes, mf.TCP.PreviousClef, keyData, mf.NoteSpacing);
            mf.ExtraPictureBox.Image = PracticeDrawer.EvaluatedIntervalPractice(mf.PM, mf.TCP.PreviousClef, mf.NoteSpacing, keyData);
            mf.MainPictureBox.Image?.Dispose();
            int minOctave = nextClef == Clef.Treble ? mf.TCP.TrebleMin : mf.TCP.BassMin;
            int maxOctave = nextClef == Clef.Treble ? mf.TCP.TrebleMax : mf.TCP.BassMax;
            Note firstNote = PracticeManager.RandomNote(minOctave, maxOctave, false, mf.PracticeNoteLength);
            int shift = new Random().Next(-7, 8);
            mf.MainPictureBox.Image?.Dispose();

            mf.PM.PracticeNotes.Clear();
            mf.PM.AddPracticeNote(firstNote);
            mf.PM.AddPracticeNote(firstNote.ShiftedNote(shift));
            mf.MainPictureBox.Image = MusicDrawer.MusicBitmap(MusicDrawer.StartSymbols(nextClef, PracticeManager.SpacedSymbolList(mf.PM.PracticeNotes.Cast<Symbol>().ToList(), mf.NoteSpacing)), false);
            mf.TCP.PreviousClef = nextClef;
        }
        //public List<int> NoteLengthsFromKeyPresses()
        //{
        //    List<int> result = new();
        //    if (_keyDownTimes.Count != _keyUpTimes.Count || _keyDownTimes.Count == 0 || _keyUpTimes.Count == 0) return result;
        //    for(int i = 0; i < _keyDownTimes.Count; i++)
        //    {
        //        result.Add((int)(_keyUpTimes[i] - _keyDownTimes[i]).TotalMilliseconds);
        //    }
        //    return result;
        //}
        public void ResetKeyTimes()
        {
            _keyDownTimes.Clear();
            _keyUpTimes.Clear();
        }
    }
}
