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
            panel2 = new Panel();
            extraPictureBox = new PictureBox();
            mainPictureBox = new PictureBox();
            tableLayoutPanel1.SuspendLayout();
            panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)extraPictureBox).BeginInit();
            ((System.ComponentModel.ISupportInitialize)mainPictureBox).BeginInit();
            SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 2;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 100F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.Controls.Add(controlClefOctave, 1, 0);
            tableLayoutPanel1.Controls.Add(panel2, 1, 1);
            tableLayoutPanel1.Dock = DockStyle.Fill;
            tableLayoutPanel1.Location = new Point(0, 0);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 2;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 120F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tableLayoutPanel1.Size = new Size(584, 363);
            tableLayoutPanel1.TabIndex = 0;
            // 
            // controlClefOctave
            // 
            controlClefOctave.Dock = DockStyle.Fill;
            controlClefOctave.Location = new Point(103, 3);
            controlClefOctave.Name = "controlClefOctave";
            controlClefOctave.Size = new Size(478, 114);
            controlClefOctave.TabIndex = 0;
            // 
            // panel2
            // 
            panel2.Controls.Add(extraPictureBox);
            panel2.Controls.Add(mainPictureBox);
            panel2.Dock = DockStyle.Fill;
            panel2.Location = new Point(103, 123);
            panel2.Name = "panel2";
            panel2.Size = new Size(478, 237);
            panel2.TabIndex = 2;
            // 
            // extraPictureBox
            // 
            extraPictureBox.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right;
            extraPictureBox.Location = new Point(315, 3);
            extraPictureBox.Name = "extraPictureBox";
            extraPictureBox.Size = new Size(160, 231);
            extraPictureBox.SizeMode = PictureBoxSizeMode.Zoom;
            extraPictureBox.TabIndex = 1;
            extraPictureBox.TabStop = false;
            // 
            // mainPictureBox
            // 
            mainPictureBox.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            mainPictureBox.Location = new Point(3, 3);
            mainPictureBox.Name = "mainPictureBox";
            mainPictureBox.Size = new Size(306, 231);
            mainPictureBox.SizeMode = PictureBoxSizeMode.Zoom;
            mainPictureBox.TabIndex = 0;
            mainPictureBox.TabStop = false;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(584, 363);
            Controls.Add(tableLayoutPanel1);
            Name = "MainForm";
            Text = "Form1";
            tableLayoutPanel1.ResumeLayout(false);
            panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)extraPictureBox).EndInit();
            ((System.ComponentModel.ISupportInitialize)mainPictureBox).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private TableLayoutPanel tableLayoutPanel1;
        private PictureBox mainPictureBox;
        private Panel panel2;
        private PictureBox extraPictureBox;
        private NumericUpDown numericUpDown2;
        private ControlClefOctave controlClefOctave;
    }
}
