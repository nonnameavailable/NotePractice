using NotePractice.Music.Symbols;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NotePractice
{
    public partial class ControlClefOctave : UserControl
    {
        public int TrebleMin { get => (int)trebleMinNUD.Value; }
        public int TrebleMax { get => (int)trebleMaxNUD.Value; }
        public int BassMin { get => (int)bassMinNUD.Value; }
        public int BassMax { get => (int)bassMaxNUD.Value; }
        public bool IncludeSharpFlat { get => includeSharpFlatCB.Checked; }
        public int WritingOctave { get => (int)(writingOctaveNUD.Value); set => writingOctaveNUD.Value = Math.Clamp(value, 0, 8); }
        public int WritingDuration { get => (int)durationNUD.Value; set => durationNUD.Value = Math.Clamp(value, 1, 64); }
        public string PracticeMode { get => practiceModeCBB.Text; }
        public Clef NextClef
        {
            get
            {
                string sc = clefCBB.SelectedItem.ToString();
                if (sc == "Treble")
                {
                    return Clef.Treble;
                }
                else if (sc == "Bass")
                {
                    return Clef.Bass;
                }
                else
                {
                    if (new Random().Next(0, 2) == 0)
                    {
                        return Clef.Treble;
                    }
                    else return Clef.Bass;
                }
            }
        }
        public Clef PreviousClef { get; set; }
        public ControlClefOctave()
        {
            InitializeComponent();
            clefCBB.SelectedIndex = 0;
            PreviousClef = NextClef;

            trebleMinNUD.ValueChanged += TrebleMinNUD_ValueChanged;
            trebleMaxNUD.ValueChanged += TrebleMaxNUD_ValueChanged;
            bassMinNUD.ValueChanged += BassMinNUD_ValueChanged;
            bassMaxNUD.ValueChanged += BassMaxNUD_ValueChanged;
            
            practiceModeCBB.SelectedIndex = 0;
        }

        private void BassMaxNUD_ValueChanged(object? sender, EventArgs e)
        {
            if (BassMax < BassMin) bassMaxNUD.Value = BassMin;
        }

        private void BassMinNUD_ValueChanged(object? sender, EventArgs e)
        {
            if (BassMin > BassMax) bassMinNUD.Value = BassMax;
        }

        private void TrebleMaxNUD_ValueChanged(object? sender, EventArgs e)
        {
            if (TrebleMax < TrebleMin) trebleMaxNUD.Value = TrebleMin;
        }

        private void TrebleMinNUD_ValueChanged(object? sender, EventArgs e)
        {
            if (TrebleMin > TrebleMax) trebleMinNUD.Value = TrebleMax;
        }

        private void clefCBB_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
