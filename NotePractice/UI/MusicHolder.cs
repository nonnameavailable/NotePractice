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
        public event EventHandler<ClefEventArgs> StaffUnderCursorChanged;
        public int CurrentStaffUnderCursor { get; set; }
        public MusicHolder()
        {
            InitializeComponent();
            addBTN.Click += (sender, args) => AddNewGrandStaff();
            removeBTN.Click += RemoveBTN_Click;
            CurrentStaffUnderCursor = -1;
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
            gsh.MouseEnter += Gsh_MouseEnter;
            gsh.ClefButtonPressed += Gsh_ClefButtonPressed;
            gsh.ClefUnderCursorChanged += Gsh_ClefUnderCursorChanged;
            mainFLP.Controls.Add(gsh);
            GrandStaffAdded?.Invoke(this, EventArgs.Empty);
        }

        private void Gsh_ClefUnderCursorChanged(object? sender, ClefEventArgs e)
        {
            GrandStaffHolder gsh = (GrandStaffHolder)sender;
            int index = mainFLP.Controls.IndexOf(gsh);
            StaffUnderCursorChanged?.Invoke(sender, new ClefEventArgs(gsh.CurrentClefUnderCursor, index));
        }

        private void Gsh_MouseEnter(object? sender, EventArgs e)
        {
            GrandStaffHolder gsh = (GrandStaffHolder)sender;
            int index = mainFLP.Controls.IndexOf(gsh);
            if (index != CurrentStaffUnderCursor)
            {
                CurrentStaffUnderCursor = index;
                StaffUnderCursorChanged?.Invoke(sender, new ClefEventArgs(gsh.CurrentClefUnderCursor, index));
            }
        }

        private void Gsh_ClefButtonPressed(object? sender, ClefEventArgs e)
        {
            e.Index = mainFLP.Controls.IndexOf((Control)sender);
            ClefButtonPressed?.Invoke(this, e);
        }
    }
}
