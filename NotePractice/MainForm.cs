
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using NotePractice.Piano;

namespace NotePractice
{
    public partial class MainForm : Form
    {
        public Note Note { get; set; }
        public List<Note> Notes { get; set; }
        public ControlClefOctave Cco { get => controlClefOctave; }
        private PianoForm _pianoForm;
        public MainForm()
        {
            InitializeComponent();

            Note = new Note(NoteLetter.C, 4, sharp:true);
            Debug.Print(Note.Equals(new Note(NoteLetter.C, 4)).ToString());

            mainPictureBox.Image = Noter.NoteImage([Note], Clef.Treble);
            mainPictureBox.Click += (sender, args) => mainPictureBox.Focus();
            ShowPianoBTN_Click(null, EventArgs.Empty);
            showPianoBTN.Click += ShowPianoBTN_Click;
            Notes = [new Note(NoteLetter.C, 4), new Note(NoteLetter.D, 4)];
            practiceModeCBB.SelectedIndex = 0;
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
                if(practiceModeCBB.Text == "Notes")
                {
                    EvaluateNoteFromKey(keyData);
                } else
                {
                    EvaluateIntervalFromKey(keyData);
                }
                return true;
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
    }
}
