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

namespace NotePractice.Piano
{
    public partial class PianoForm: Form
    {
        private Piano _piano;
        private Note _noteUnderCursor;
        public event EventHandler<NoteEventArgs> NotePlayed;
        public PianoForm()
        {
            InitializeComponent();
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
                Debug.Print(newNote.ToString());
                _noteUnderCursor = newNote;
            }
        }

        private void MainPictureBox_Click(object? sender, EventArgs e)
        {
            Image bkup = mainPictureBox.Image;
            mainPictureBox.Image = _piano.PianoBitmap(mainPictureBox.MousePositionOnImage, out Note playedNote);
            if(playedNote != null) NotePlayed?.Invoke(this, new NoteEventArgs(playedNote));
            bkup?.Dispose();
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
