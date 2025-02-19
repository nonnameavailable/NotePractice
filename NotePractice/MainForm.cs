
namespace NotePractice
{
    public partial class MainForm : Form
    {
        public Note Note { get; set; }
        public ControlClefOctave Cco { get => controlClefOctave; }
        public MainForm()
        {
            InitializeComponent();

            Note = Noter.RandomNote(4,4);

            mainPictureBox.Image = Noter.NoteImage(Note, Clef.Treble);
            mainPictureBox.Click += (sender, args) => mainPictureBox.Focus();
        }

        protected override bool ProcessKeyPreview(ref Message m)
        {
            if (!mainPictureBox.Focused) return false;
            const int WM_KEYDOWN = 0x100;
            const int WM_KEYUP = 0x101;
            Keys keyData = (Keys)m.WParam.ToInt32();
            if (m.Msg == WM_KEYDOWN)
            {
                Clef nextClef = Cco.NextClef;

                int minOctave = nextClef == Clef.Treble ? Cco.TrebleMin : Cco.BassMin;
                int maxOctave = nextClef == Clef.Treble ? Cco.TrebleMax : Cco.BassMax;

                extraPictureBox.Image?.Dispose();
                extraPictureBox.Image = Noter.NoteImageWithLetter(Note, Cco.PreviousClef, keyData);
                mainPictureBox.Image?.Dispose();
                Note = Noter.RandomNote(minOctave, maxOctave);
                mainPictureBox.Image = Noter.NoteImage(Note, nextClef);
                Cco.PreviousClef = nextClef;
                return true;
            }
            return base.ProcessKeyPreview(ref m);
        }
    }
}
