
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using NotePractice.Music;
using NotePractice.Music.Symbols;
using NotePractice.Piano;

namespace NotePractice
{
    public partial class MainForm : Form
    {
        public Note Note { get; set; }
        public List<Note> Notes { get; set; }
        public List<Symbol> WrittenSymbols { get; set; }
        public bool IsShiftDown { get; set; }
        public bool IsCtrlDown { get; set; }
        public int WritingOctave { get => controlClefOctave.WritingOctave; set => controlClefOctave.WritingOctave = value; }
        public int WritingDuration { get => controlClefOctave.WritingDuration; set => controlClefOctave.WritingDuration = value; }
        public ControlClefOctave Cco { get => controlClefOctave; }
        private PianoForm _pianoForm;
        public MainForm()
        {
            InitializeComponent();
            AutoScaleMode = AutoScaleMode.None;
            Note = new Note(NoteLetter.C, 4, sharp:true);

            mainPictureBox.Image = Noter.NoteImage([Note], Clef.Treble);
            mainPictureBox.Click += (sender, args) => mainPictureBox.Focus();
            ShowPianoBTN_Click(null, EventArgs.Empty);
            showPianoBTN.Click += ShowPianoBTN_Click;
            Notes = [new Note(NoteLetter.C, 4), new Note(NoteLetter.D, 4)];
            practiceModeCBB.SelectedIndex = 0;
            WrittenSymbols = new();
        }

        private void ShowPianoBTN_Click(object? sender, EventArgs e)
        {
            if (_pianoForm == null || _pianoForm.IsDisposed)
            {
                _pianoForm = new PianoForm();
                _pianoForm.NotePlayed += _pianoForm_NotePlayed;
                _pianoForm.Show();
            }
        }

        private void _pianoForm_NotePlayed(object? sender, NoteEventArgs e)
        {
            EvaluateNoteFromPiano(e.Note);
        }

        protected override bool ProcessKeyPreview(ref Message m)
        {
            if (!mainPictureBox.Focused) return false;
            const int WM_KEYDOWN = 0x100;
            const int WM_KEYUP = 0x101;
            Keys keyData = (Keys)m.WParam.ToInt32();
            if (m.Msg == WM_KEYDOWN)
            {
                if(keyData == Keys.ShiftKey)
                {
                    IsShiftDown = true;
                }
                else if (keyData == Keys.ControlKey)
                {
                    IsCtrlDown = true;
                }
                else if (keyData == Keys.Oemplus)
                {
                    WritingDuration *= 2;
                }
                else if (keyData == Keys.OemMinus)
                {
                    WritingDuration /= 2;
                }
                else if(keyData == Keys.Oemcomma)
                {
                    WritingOctave--;
                }
                else if(keyData == Keys.OemPeriod)
                {
                    WritingOctave++;
                }
                if (practiceRB.Checked)
                {
                    //PRACTICE
                    if (practiceModeCBB.Text == "Notes")
                    {
                        EvaluateNoteFromKey(keyData);
                    }
                    else
                    {
                        EvaluateIntervalFromKey(keyData);
                    }
                }
                else
                {
                    //WRITING
                    if (keyData == Keys.Back)
                    {
                        if (WrittenSymbols.Count > 0) WrittenSymbols.RemoveAt(WrittenSymbols.Count - 1);
                    }
                    else
                    {
                        Symbol s = WrittenSymbol(keyData);
                        if (s != null) WrittenSymbols.Add(s);
                    }
                    mainPictureBox.Image = MusicDrawer.MusicBitmap(WrittenSymbols);
                }
                return true;
            } else if (m.Msg == WM_KEYUP)
            {
                if(keyData == Keys.ControlKey)
                {
                    IsCtrlDown = false;
                }
                else if(keyData == Keys.ShiftKey)
                {
                    IsShiftDown = false;
                }
            }
                return base.ProcessKeyPreview(ref m);
        }
        private void EvaluateNoteFromPiano(Note note)
        {
            Clef nextClef = Cco.NextClef;

            int minOctave = nextClef == Clef.Treble ? Cco.TrebleMin : Cco.BassMin;
            int maxOctave = nextClef == Clef.Treble ? Cco.TrebleMax : Cco.BassMax;

            extraPictureBox.Image?.Dispose();
            extraPictureBox.Image = Noter.NoteImageWithLetter(Note, Cco.PreviousClef, note);
            mainPictureBox.Image?.Dispose();
            Note = Noter.RandomNote(minOctave, maxOctave, controlClefOctave.IncludeSharpFlat);
            mainPictureBox.Image = Noter.NoteImage([Note], nextClef);
            Cco.PreviousClef = nextClef;
        }
        private void EvaluateNoteFromKey(Keys keyData)
        {
            Clef nextClef = Cco.NextClef;

            int minOctave = nextClef == Clef.Treble ? Cco.TrebleMin : Cco.BassMin;
            int maxOctave = nextClef == Clef.Treble ? Cco.TrebleMax : Cco.BassMax;

            extraPictureBox.Image?.Dispose();
            extraPictureBox.Image = Noter.NoteImageWithLetter(Note, Cco.PreviousClef, keyData);
            mainPictureBox.Image?.Dispose();
            Note = Noter.RandomNote(minOctave, maxOctave, controlClefOctave.IncludeSharpFlat);
            mainPictureBox.Image = Noter.NoteImage([Note], nextClef);
            Cco.PreviousClef = nextClef;
        }
        private void EvaluateIntervalFromKey(Keys keyData)
        {
            Clef nextClef = Cco.NextClef;
            extraPictureBox.Image?.Dispose();
            extraPictureBox.Image = Noter.IntervalImageWithNumber(Notes, Cco.PreviousClef, keyData);
            mainPictureBox.Image?.Dispose();
            int minOctave = nextClef == Clef.Treble ? Cco.TrebleMin : Cco.BassMin;
            int maxOctave = nextClef == Clef.Treble ? Cco.TrebleMax : Cco.BassMax;
            Note = Noter.RandomNote(minOctave, maxOctave, false);
            int shift = new Random().Next(-7, 8);
            Notes = [Note, Note.ShiftedNote(shift)];
            mainPictureBox.Image?.Dispose();
            mainPictureBox.Image = Noter.NoteImage(Notes, nextClef);
            Cco.PreviousClef = nextClef;
        }
        private Symbol WrittenSymbol(Keys keyData)
        {
            Keys[] noteKeys = { Keys.C, Keys.D, Keys.E, Keys.F, Keys.G, Keys.A, Keys.B };
            int noteIndex = Array.IndexOf(noteKeys, keyData);
            if (noteIndex >= 0)
            {
                return new Note((NoteLetter)noteIndex, WritingOctave, IsShiftDown, IsCtrlDown, WritingDuration);
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
                return new Rest(WritingDuration);
            }
            else return null;
        }
    }
}
