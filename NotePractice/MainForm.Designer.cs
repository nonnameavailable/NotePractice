namespace NotePractice
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            tableLayoutPanel1 = new TableLayoutPanel();
            controlClefOctave = new ControlClefOctave();
            extraPictureBox = new PictureBox();
            mainPictureBox = new PictureBox();
            tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)extraPictureBox).BeginInit();
            ((System.ComponentModel.ISupportInitialize)mainPictureBox).BeginInit();
            SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 3;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 100F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 65F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 35F));
            tableLayoutPanel1.Controls.Add(extraPictureBox, 2, 1);
            tableLayoutPanel1.Controls.Add(controlClefOctave, 1, 0);
            tableLayoutPanel1.Controls.Add(mainPictureBox, 1, 1);
            tableLayoutPanel1.Dock = DockStyle.Fill;
            tableLayoutPanel1.Location = new Point(0, 0);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 2;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 120F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.Size = new Size(669, 428);
            tableLayoutPanel1.TabIndex = 0;
            // 
            // controlClefOctave
            // 
            tableLayoutPanel1.SetColumnSpan(controlClefOctave, 2);
            controlClefOctave.Dock = DockStyle.Fill;
            controlClefOctave.Location = new Point(103, 3);
            controlClefOctave.Name = "controlClefOctave";
            controlClefOctave.PreviousClef = Clef.Treble;
            controlClefOctave.Size = new Size(563, 114);
            controlClefOctave.TabIndex = 0;
            // 
            // extraPictureBox
            // 
            extraPictureBox.Dock = DockStyle.Fill;
            extraPictureBox.Location = new Point(472, 123);
            extraPictureBox.Name = "extraPictureBox";
            extraPictureBox.Size = new Size(194, 302);
            extraPictureBox.SizeMode = PictureBoxSizeMode.Zoom;
            extraPictureBox.TabIndex = 1;
            extraPictureBox.TabStop = false;
            // 
            // mainPictureBox
            // 
            mainPictureBox.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            mainPictureBox.Location = new Point(103, 123);
            mainPictureBox.Name = "mainPictureBox";
            mainPictureBox.Size = new Size(363, 302);
            mainPictureBox.SizeMode = PictureBoxSizeMode.Zoom;
            mainPictureBox.TabIndex = 0;
            mainPictureBox.TabStop = false;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(669, 428);
            Controls.Add(tableLayoutPanel1);
            Name = "MainForm";
            Text = "Form1";
            tableLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)extraPictureBox).EndInit();
            ((System.ComponentModel.ISupportInitialize)mainPictureBox).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private TableLayoutPanel tableLayoutPanel1;
        private PictureBox mainPictureBox;
        private PictureBox extraPictureBox;
        private NumericUpDown numericUpDown2;
        private ControlClefOctave controlClefOctave;
    }
}
