namespace NotePractice.Piano
{
    partial class PianoForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            mainPictureBox = new MyPictureBox();
            ((System.ComponentModel.ISupportInitialize)mainPictureBox).BeginInit();
            SuspendLayout();
            // 
            // mainPictureBox
            // 
            mainPictureBox.Dock = DockStyle.Fill;
            mainPictureBox.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
            mainPictureBox.IsLMBDown = false;
            mainPictureBox.IsMMBDown = false;
            mainPictureBox.IsRMBDown = false;
            mainPictureBox.Location = new Point(0, 0);
            mainPictureBox.Name = "mainPictureBox";
            mainPictureBox.ScaledDragDifference = new Point(0, 0);
            mainPictureBox.Size = new Size(823, 111);
            mainPictureBox.SizeMode = PictureBoxSizeMode.Zoom;
            mainPictureBox.TabIndex = 0;
            mainPictureBox.TabStop = false;
            // 
            // PianoForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(823, 111);
            Controls.Add(mainPictureBox);
            Name = "PianoForm";
            Text = "PianoForm";
            ((System.ComponentModel.ISupportInitialize)mainPictureBox).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private MyPictureBox mainPictureBox;
    }
}