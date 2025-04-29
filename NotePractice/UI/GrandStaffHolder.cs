using NotePractice.Music.Symbols;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NotePractice.UI
{
    public partial class GrandStaffHolder : UserControl
    {
        private static Color HighlightColor = Color.Blue;
        private static Color TrebleHighlightColor = Color.ForestGreen;
        private static Color BassHighlightColor = Color.RebeccaPurple;
        public event EventHandler<ClefEventArgs> ClefButtonPressed;
        public event EventHandler<ClefEventArgs> ClefUnderCursorChanged;
        public Clef CurrentClefUnderCursor { get; set; }
        public GrandStaffHolder()
        {
            InitializeComponent();
            trebleBTN.Click += (sender, args) => ClefButtonPressed?.Invoke(this, new ClefEventArgs(Clef.Treble));
            bassBTN.Click += (sender, args) => ClefButtonPressed?.Invoke(this, new ClefEventArgs(Clef.Bass));
            MouseEnter += GrandStaffHolder_MouseEnter;
            MouseLeave += GrandStaffHolder_MouseLeave;
            trebleBTN.MouseEnter += TrebleBTN_MouseEnter;
            bassBTN.MouseEnter += BassBTN_MouseEnter;
            trebleBTN.MouseLeave += (sender, args) => BackColor = HighlightColor;
            bassBTN.MouseLeave += (sender, args) => BackColor = HighlightColor;
            CurrentClefUnderCursor = Clef.Treble;
        }

        private void BassBTN_MouseEnter(object? sender, EventArgs e)
        {
            BackColor = BassHighlightColor;
            if(CurrentClefUnderCursor != Clef.Bass)
            {
                CurrentClefUnderCursor = Clef.Bass;
                ClefUnderCursorChanged?.Invoke(this, new ClefEventArgs(Clef.Bass));
            }
        }

        private void TrebleBTN_MouseEnter(object? sender, EventArgs e)
        {
            BackColor = TrebleHighlightColor;
            if (CurrentClefUnderCursor != Clef.Treble)
            {
                CurrentClefUnderCursor = Clef.Treble;
                ClefUnderCursorChanged?.Invoke(this, new ClefEventArgs(Clef.Treble));
            }
        }

        private void GrandStaffHolder_MouseLeave(object? sender, EventArgs e)
        {
            BackColor = DefaultBackColor;
        }

        private void GrandStaffHolder_MouseEnter(object? sender, EventArgs e)
        {
            BackColor = HighlightColor;
        }
    }
    public class ClefEventArgs
    {
        public Clef Clef { get; set; }
        public int Index { get; set; }
        public ClefEventArgs(Clef clef, int index = 0)
        {
            Clef = clef;
            Index = index;
        }
    }
}
