using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotePractice.UI
{
    public class SaveButton : Button
    {
        public event EventHandler? ShouldStartDragDrop;
        private OVector _mousePressed;
        public bool IsLmbDown { get; set; }
        public SaveButton() : base()
        {
            _mousePressed = new OVector(0, 0);
            MouseMove += SaveButton_MouseMove;
            MouseDown += SaveButton_MouseDown;
            MouseUp += SaveButton_MouseUp;
        }

        private void SaveButton_MouseUp(object? sender, MouseEventArgs e)
        {
            IsLmbDown = false;
        }

        private void SaveButton_MouseDown(object? sender, MouseEventArgs e)
        {
            _mousePressed = new OVector(e.X, e.Y);
            IsLmbDown = true;
        }

        private void SaveButton_MouseMove(object? sender, MouseEventArgs e)
        {
            if (!IsLmbDown) return;
            double distance = new OVector(e.X, e.Y).Subtract(_mousePressed).Magnitude;
            if(distance > 5)
            {
                ShouldStartDragDrop?.Invoke(this, EventArgs.Empty);
            }
        }
    }
}
