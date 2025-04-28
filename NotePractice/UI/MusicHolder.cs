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
    public partial class MusicHolder : UserControl
    {
        public event EventHandler<ClefEventArgs> ClefButtonPressed;
        public event EventHandler GrandStaffAdded;
        public event EventHandler GrandStaffRemoved;
        public MusicHolder()
        {
            InitializeComponent();
            addBTN.Click += (sender, args) => AddNewGrandStaff();
            removeBTN.Click += RemoveBTN_Click;
        }

        private void RemoveBTN_Click(object? sender, EventArgs e)
        {
            if (mainFLP.Controls.Count == 0) return;
            GrandStaffHolder gsh = (GrandStaffHolder)mainFLP.Controls[mainFLP.Controls.Count - 1];
            mainFLP.Controls.Remove(gsh);
            gsh.Dispose();
            GrandStaffRemoved?.Invoke(this, EventArgs.Empty);
        }

        public void AddNewGrandStaff()
        {
            GrandStaffHolder gsh = new GrandStaffHolder();
            gsh.ClefButtonPressed += Gsh_ClefButtonPressed;
            mainFLP.Controls.Add(gsh);
            GrandStaffAdded?.Invoke(this, EventArgs.Empty);
        }

        private void Gsh_ClefButtonPressed(object? sender, ClefEventArgs e)
        {
            e.Index = mainFLP.Controls.IndexOf((Control)sender);
            ClefButtonPressed?.Invoke(this, e);
        }
    }
}
