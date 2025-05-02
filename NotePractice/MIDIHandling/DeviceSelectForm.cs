using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NotePractice.MIDIHandling
{
    public partial class DeviceSelectForm : Form
    {
        public string SelectedDeviceName { get; set; }
        public DeviceSelectForm(List<string> deviceNames)
        {
            InitializeComponent();
            deviceNames.ForEach(deviceName => AddButton(deviceName));
            SelectedDeviceName = "";
        }
        private void AddButton(string text)
        {
            Button btn = new Button();
            btn.Text = text;
            btn.DialogResult = DialogResult.OK;
            btn.Click += (sender, args) => SelectedDeviceName = btn.Text;
            mainFLP.Controls.Add(btn);
            btn.Width = mainFLP.Width - mainFLP.Margin.Size.Width;
            btn.Height = 50; 
        }
    }
}
