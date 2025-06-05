using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NotePractice.UI
{
    public partial class TopControlWriting : UserControl
    {
        public int WritingOctave { get => (int)(writingOctaveNUD.Value); set => writingOctaveNUD.Value = Math.Clamp(value, 0, 8); }
        public int WritingDuration { get => (int)durationNUD.Value; set => durationNUD.Value = Math.Clamp(value, 1, 64); }
        public TopControlWriting()
        {
            InitializeComponent();
        }
    }
}
