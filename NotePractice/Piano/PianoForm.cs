using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TextBox;

namespace NotePractice.Piano
{
    public partial class PianoForm: Form
    {
        private Piano _piano;
        private Note _noteUnderCursor;
        private Clef _helpClef;
        public event EventHandler<NoteEventArgs> NotePlayed;
        public PianoForm()
        {
            InitializeComponent();
            _helpClef = Clef.Treble;
            _noteUnderCursor = null;
            _piano = new Piano();
            mainPictureBox.Image = _piano.PianoBitmap(new Point(-1, -1), out Note playedNote);
            mainPictureBox.Click += MainPictureBox_Click;
            mainPictureBox.MouseMove += MainPictureBox_MouseMove;
        }

        private void MainPictureBox_MouseMove(object? sender, MouseEventArgs e)
        {
            Note newNote = _piano.NoteUnderCursor(mainPictureBox.MousePositionOnImage);
            if (newNote == null) return;
            //Debug.Print(newNote.ToString());
            if (_noteUnderCursor == null || (!_noteUnderCursor.Equals(newNote)))
            {
                //Debug.Print(newNote.ToString());
                _noteUnderCursor = newNote;
                UpdateHelpPictureBox();
            }
        }
        private void UpdateHelpPictureBox()
        {
            Bitmap newHelpImage = Noter.NoteImageWithLetter(_noteUnderCursor, _helpClef, _noteUnderCursor);
            helpPictureBox.Image?.Dispose();
            helpPictureBox.Image = newHelpImage;
        }
        private void MainPictureBox_Click(object? sender, EventArgs e)
        {
            mainPictureBox.Focus();
            Image newImage = _piano.PianoBitmap(mainPictureBox.MousePositionOnImage, out Note playedNote);
            mainPictureBox.Image?.Dispose();
            if(playedNote != null) NotePlayed?.Invoke(this, new NoteEventArgs(playedNote));
            mainPictureBox.Image = newImage;
        }
        protected override bool ProcessKeyPreview(ref Message m)
        {
            if (!mainPictureBox.Focused) return false;
            const int WM_KEYDOWN = 0x100;
            const int WM_KEYUP = 0x101;
            Keys keyData = (Keys)m.WParam.ToInt32();
            if (m.Msg == WM_KEYDOWN)
            {
                if(keyData == Keys.T)
                {
                    _helpClef = Clef.Treble;
                    UpdateHelpPictureBox();
                    return true;
                }
                else if(keyData == Keys.B)
                {
                    _helpClef = Clef.Bass;
                    UpdateHelpPictureBox();
                    return true;
                }
            }
            return base.ProcessKeyPreview(ref m);
        }
    }
    public class NoteEventArgs : EventArgs
    {
        public Note Note { get; }
        public NoteEventArgs(Note note) : base()
        {
            Note = note;
        }
    }
}
