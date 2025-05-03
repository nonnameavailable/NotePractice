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
        public static bool ProcessKey(Message msg, Keys keyData, MainForm mf)
        {
            const int WM_KEYDOWN = 0x100;
            const int WM_KEYUP = 0x101;
            //Keys keyData = (Keys)m.WParam.ToInt32();
            if (msg.Msg == WM_KEYDOWN)
            {
                if (keyData == Keys.Oemplus || keyData == Keys.Right)
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
                        Symbol s = WrittenSymbol(keyData, mf);
                        //if (s != null) mf.WrittenSymbols.Add(s);
                        if(s != null)
                        {
                            mf.Song.AddSymbol(s, mf.SelectedStaffIndex, mf.SelectedStaffClef);
                            //Debug.Print(mf.MidiSender.CallCount.ToString());
                            if(s is Note note) mf.MidiSender.SendNoteToMidiAsync(note);
                        }
                    }
                    mf.UpdatePictureBoxAfterWrite();
                    //mf.MainPictureBox.Image = MusicDrawer.MusicBitmap(mf.WrittenSymbols);
                    return true;
                }
            }
            return false;
        }
        private static Symbol WrittenSymbol(Keys keyData, MainForm mf)
        {
            Keys[] noteKeys = { Keys.C, Keys.D, Keys.E, Keys.F, Keys.G, Keys.A, Keys.B };
            Keys actualKey = keyData & Keys.KeyCode;
            int noteIndex = Array.IndexOf(noteKeys, actualKey);
            //int noteIndex = Array.IndexOf(noteKeys, keyData);
            bool isShiftDown = (keyData & Keys.Shift) == Keys.Shift;
            bool isCtrlDown = (keyData & Keys.Control) == Keys.Control;
            if (noteIndex >= 0)
            {
                Accidental accidental = Accidental.None;
                if (isShiftDown) accidental = Accidental.Sharp;
                if (isCtrlDown) accidental = Accidental.Flat;
                return new Note((NoteLetter)noteIndex, mf.WritingOctave, accidental, mf.WritingDuration);
            }
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
                if (isShiftDown)
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
            Clef nextClef = mf.Cco.NextClef;

            int minOctave = nextClef == Clef.Treble ? mf.Cco.TrebleMin : mf.Cco.BassMin;
            int maxOctave = nextClef == Clef.Treble ? mf.Cco.TrebleMax : mf.Cco.BassMax;

            mf.ExtraPictureBox.Image?.Dispose();
            mf.ExtraPictureBox.Image = Noter.NoteImageWithLetter(mf.Note, mf.Cco.PreviousClef, keyData);
            mf.MainPictureBox.Image?.Dispose();
            mf.Note = Noter.RandomNote(minOctave, maxOctave, mf.Cco.IncludeSharpFlat);
            mf.MainPictureBox.Image = MusicDrawer.MusicBitmap(MusicDrawer.StartSymbols(nextClef, [mf.Note]), false);
            mf.Cco.PreviousClef = nextClef;
            //mf.MidiSender.SendNoteToMidiAsync(mf.Note);
        }
        private static void EvaluateIntervalFromKey(Keys keyData, MainForm mf)
        {
            Clef nextClef = mf.Cco.NextClef;
            mf.ExtraPictureBox.Image?.Dispose();
            mf.ExtraPictureBox.Image = Noter.IntervalImageWithNumber(mf.Notes, mf.Cco.PreviousClef, keyData);
            mf.MainPictureBox.Image?.Dispose();
            int minOctave = nextClef == Clef.Treble ? mf.Cco.TrebleMin : mf.Cco.BassMin;
            int maxOctave = nextClef == Clef.Treble ? mf.Cco.TrebleMax : mf.Cco.BassMax;
            mf.Note = Noter.RandomNote(minOctave, maxOctave, false);
            int shift = new Random().Next(-7, 8);
            mf.MainPictureBox.Image?.Dispose();
            mf.MainPictureBox.Image = MusicDrawer.MusicBitmap(MusicDrawer.StartSymbols(nextClef, [mf.Note, mf.Note.ShiftedNote(shift)]), false);
            mf.Cco.PreviousClef = nextClef;
        }
    }
}
