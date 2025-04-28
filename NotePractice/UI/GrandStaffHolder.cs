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
        public event EventHandler<ClefEventArgs> ClefButtonPressed;
        public GrandStaffHolder()
        {
            InitializeComponent();
            trebleBTN.Click += (sender, args) => ClefButtonPressed?.Invoke(this, new ClefEventArgs(Clef.Treble));
            bassBTN.Click += (sender, args) => ClefButtonPressed?.Invoke(this, new ClefEventArgs(Clef.Bass));
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
