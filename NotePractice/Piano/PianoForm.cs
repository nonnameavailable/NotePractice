using NotePractice.Music;
using NotePractice.Music.Symbols;
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
            _noteUnderCursor = new Note(NoteLetter.C, 4);
            _piano = new Piano();
            mainPictureBox.Image = _piano.PianoBitmap(new Point(-1, -1), out Note playedNote);
            helpPictureBox.Image = Noter.NoteImageWithLetter(_noteUnderCursor, _helpClef, _noteUnderCursor);
            UpdateHelpPictureBox();
            mainPictureBox.MouseMove += MainPictureBox_MouseMove;
            mainPictureBox.Click += MainPictureBox_Click;
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
            using Graphics g = Graphics.FromImage(newHelpImage);
            HelpBitmapColorOverlay(g);
            helpPictureBox.Image?.Dispose();
            helpPictureBox.Image = newHelpImage;
        }
        private void HelpBitmapColorOverlay(Graphics g)
        {
            int width = helpPictureBox.Image.Width;
            int octaveHeight = MusicDrawer.NoteShift * 7;
            for(int i = 0; i < 8; i++)
            {
                Color c = ColorConvertor.HSBtoRGB(i / 8f, 1, 1);
                Color ca = Color.FromArgb(120, c.R, c.G, c.B);
                int y = MusicDrawer.BottomLinePosition + MusicDrawer.NoteShift * 2 + octaveHeight * 3 - i * octaveHeight;
                if(_helpClef == Clef.Bass)
                {
                    y -= MusicDrawer.LineSpacing * 6;
                }
                Rectangle colorRectangle = new Rectangle(0, y, width, octaveHeight);
                using Brush brush = new SolidBrush(ca);
                g.FillRectangle(brush, colorRectangle);
            }
        }
        private void MainPictureBox_Click(object? sender, EventArgs e)
        {
            mainPictureBox.Focus();
            Image newImage = _piano.PianoBitmap(mainPictureBox.MousePositionOnImage, out Note playedNote);
            mainPictureBox.Image?.Dispose();
            if(playedNote != null) NotePlayed?.Invoke(this, new NoteEventArgs(playedNote));
            mainPictureBox.Image = newImage;
        }
        protected override bool ProcessCmdKey(ref Message m, Keys keyData)
        {
            if (!mainPictureBox.Focused) return false;
            const int WM_KEYDOWN = 0x100;
            const int WM_KEYUP = 0x101;
            //Keys keyData = (Keys)m.WParam.ToInt32();
            if (m.Msg == WM_KEYDOWN)
            {
                if (keyData == Keys.T)
                {
                    _helpClef = Clef.Treble;
                    UpdateHelpPictureBox();
                    return true;
                }
                else if (keyData == Keys.B)
                {
                    _helpClef = Clef.Bass;
                    UpdateHelpPictureBox();
                    return true;
                }
                else if (keyData == Keys.H)
                {
                    _piano.HelpMode = !_piano.HelpMode;
                    if (_piano.HelpMode)
                    {
                        helpPictureBox.Visible = true;
                    }
                    else
                    {
                        helpPictureBox.Visible = false;
                    }
                    UpdateHelpPictureBox();
                    Image newImage = _piano.PianoBitmap(mainPictureBox.MousePositionOnImage, out Note playedNote);
                    mainPictureBox.Image?.Dispose();
                    mainPictureBox.Image = newImage;
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
