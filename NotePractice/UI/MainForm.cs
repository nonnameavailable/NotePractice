
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using NotePractice.MIDIHandling;
using NotePractice.Music;
using NotePractice.Music.Symbols;
using NotePractice.Piano;
using NotePractice.UI;

namespace NotePractice
{
    public partial class MainForm : Form
    {
        public Note Note { get; set; }
        public List<Note> Notes { get; set; }
        public List<Symbol> WrittenSymbols { get; set; }
        public int WritingOctave { get => TCW.WritingOctave; set => TCW.WritingOctave = value; }
        public int WritingDuration { get => TCW.WritingDuration; set => TCW.WritingDuration = value; }
        private TopControlPractice _tcp;
        public TopControlPractice TCP { get => _tcp; }
        private TopControlWriting _tcw;
        public TopControlWriting TCW { get => _tcw; }
        private PianoForm _pianoForm;
        public bool IsInPracticeMode { get => practiceRB.Checked; }
        public string PracticeMode { get => TCP.PracticeMode; }
        public PictureBox MainPictureBox { get => mainPictureBox; }
        private PictureBox _extraPictureBox;
        private MusicHolder _musicHolder;
        public PictureBox ExtraPictureBox { get => _extraPictureBox; }
        private Song _song;
        public Song Song { get => _song; set => _song = value; }
        public int SelectedStaffIndex { get; set; }
        public Clef SelectedStaffClef { get; set; }
        private bool _showingWholeSong;
        private MidiListener _midiListener;
        private MidiSender _midiSender;
        public MidiSender MidiSender { get => _midiSender; }
        private KeyProcessor _keyProcessor;
        public MainForm()
        {
            InitializeComponent();
            AutoScaleMode = AutoScaleMode.None;
            Note = new Note(NoteLetter.C, 4, duration: 64);

            mainPictureBox.Image = MusicDrawer.MusicBitmap(MusicDrawer.StartSymbols(Clef.Treble, [Note]), false);
            mainPictureBox.Click += (sender, args) => mainPictureBox.Focus();
            ShowPianoBTN_Click(null, EventArgs.Empty);
            showPianoBTN.Click += ShowPianoBTN_Click;
            Notes = [new Note(NoteLetter.C, 4), new Note(NoteLetter.D, 4)];
            WrittenSymbols = new();
            _showingWholeSong = false;

            _extraPictureBox = new PictureBox();
            _extraPictureBox.SizeMode = PictureBoxSizeMode.Zoom;
            _extraPictureBox.Dock = DockStyle.Fill;

            _musicHolder = new MusicHolder();
            _musicHolder.Dock = DockStyle.Fill;

            practiceRB.Click += PracticeRB_Click;
            writingRB.Click += WritingRB_Click;
            rightPanel.Controls.Add(_extraPictureBox);

            _song = new Song();
            _song.AddGrandStaff();
            _musicHolder.AddNewGrandStaff();

            _musicHolder.ClefButtonPressed += _musicHolder_ClefButtonPressed;
            _musicHolder.GrandStaffAdded += _musicHolder_GrandStaffAdded;
            _musicHolder.GrandStaffRemoved += _musicHolder_GrandStaffRemoved;
            _musicHolder.StaffUnderCursorChanged += _musicHolder_StaffUnderCursorChanged;
            _musicHolder.ShowSongClicked += _musicHolder_ShowSongClicked;

            SelectedStaffClef = Clef.Treble;
            SelectedStaffIndex = 0;

            _midiListener = new MidiListener();
            _midiSender = new MidiSender();
            //_midiSender.SendNoteToMidi(new Note(NoteLetter.C, 4));

            _keyProcessor = new KeyProcessor();

            inputMidiBTN.Click += InputMidiBTN_Click;
            outputMidiBTN.Click += OutputMidiBTN_Click;

            _tcp = new TopControlPractice();
            topPanel.Controls.Add(_tcp);
            _tcw = new TopControlWriting();
        }

        private void OutputMidiBTN_Click(object? sender, EventArgs e)
        {
            FindMidiDeviceForOutput();
            if(_midiListener.DeviceName != "")
            {
                outputMidiBTN.Text = _midiListener.DeviceName;
            } else
            {
                outputMidiBTN.Text = "None";
            }
        }

        private void InputMidiBTN_Click(object? sender, EventArgs e)
        {
            FindMidiDeviceForInput();
            if(_midiSender.DeviceName != "")
            {
                inputMidiBTN.Text = _midiSender.DeviceName;
            } else
            {
                inputMidiBTN.Text = "None";
            }
        }

        public void FindMidiDeviceForInput()
        {
            _midiListener.FindDevice();
            _midiListener.NoteReceived += _midiListener_NoteReceived;
            try
            {
                _midiListener.StartListening();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public void FindMidiDeviceForOutput()
        {
            _midiSender.FindDevice();
        }

        private void _midiListener_NoteReceived(object? sender, NoteEventArgs e)
        {
            if (InvokeRequired)
            {
                Invoke(new Action(() => EvaluateNoteFromPiano(e.Note)));
            }
            else
            {
                EvaluateNoteFromPiano(e.Note);
            }
            //EvaluateNoteFromPiano(e.Note);
        }
        private void _musicHolder_GrandStaffRemoved(object? sender, EventArgs e)
        {
            Song.RemoveGrandStaff();
            ShowDefaultSongBitmap();
        }

        private void _musicHolder_GrandStaffAdded(object? sender, EventArgs e)
        {
            Song.AddGrandStaff();
            ShowDefaultSongBitmap();

        }

        private void _musicHolder_ShowSongClicked(object? sender, EventArgs e)
        {
            _showingWholeSong = true;
            ShowDefaultSongBitmap();
        }

        private void _musicHolder_StaffUnderCursorChanged(object? sender, ClefEventArgs e)
        {
            if (_showingWholeSong) SetNewMainBitmap(Song.Bitmap(false, e.Index, e.Clef));
        }

        private void _musicHolder_ClefButtonPressed(object? sender, ClefEventArgs e)
        {
            _showingWholeSong = false;
            SelectedStaffClef = e.Clef;
            SelectedStaffIndex = e.Index;
            UpdatePictureBoxAfterWrite();
        }

        private void WritingRB_Click(object? sender, EventArgs e)
        {
            rightPanel.Controls.Clear();
            rightPanel.Controls.Add(_musicHolder);
            topPanel.Controls.Clear();
            topPanel.Controls.Add(TCW);
        }

        private void PracticeRB_Click(object? sender, EventArgs e)
        {
            rightPanel.Controls.Clear();
            rightPanel.Controls.Add(_extraPictureBox);
            topPanel.Controls.Clear();
            topPanel.Controls.Add(TCP);
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
        //protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        //{
        //    Debug.Print("processing key");
        //    return KeyProcessor.ProcessKey(msg, keyData, this);
        //    //return base.ProcessCmdKey(ref msg, keyData);
        //}
        protected override bool ProcessKeyPreview(ref Message m)
        {
            return _keyProcessor.ProcessKey(m, (Keys)m.WParam, this);
        }
        private void EvaluateNoteFromPiano(Note note)
        {
            Clef nextClef = TCP.NextClef;

            int minOctave = nextClef == Clef.Treble ? TCP.TrebleMin : TCP.BassMin;
            int maxOctave = nextClef == Clef.Treble ? TCP.TrebleMax : TCP.BassMax;

            ExtraPictureBox.Image?.Dispose();
            ExtraPictureBox.Image = Noter.NoteImageWithLetter(Note, TCP.PreviousClef, note);
            MainPictureBox.Image?.Dispose();
            Note = Noter.RandomNote(minOctave, maxOctave, TCP.IncludeSharpFlat);
            MainPictureBox.Image = MusicDrawer.MusicBitmap(MusicDrawer.StartSymbols(nextClef, [Note]), false);
            TCP.PreviousClef = nextClef;
        }
        public void SetNewMainBitmap(Bitmap bitmap)
        {
            MainPictureBox.Image?.Dispose();
            MainPictureBox.Image = bitmap;
        }
        public void UpdatePictureBoxAfterWrite()
        {
            mainPictureBox.Image?.Dispose();
            MainPictureBox.Image = Song.GrandStaffBitmap(SelectedStaffIndex);
        }
        public void ShowDefaultSongBitmap()
        {
            SetNewMainBitmap(Song.Bitmap(false, -1, null));
        }
    }
}
