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
            tableLayoutPanel1 = new TableLayoutPanel();
            helpPictureBox = new MyPictureBox();
            ((System.ComponentModel.ISupportInitialize)mainPictureBox).BeginInit();
            tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)helpPictureBox).BeginInit();
            SuspendLayout();
            // 
            // mainPictureBox
            // 
            mainPictureBox.Dock = DockStyle.Fill;
            mainPictureBox.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
            mainPictureBox.IsLMBDown = false;
            mainPictureBox.IsMMBDown = false;
            mainPictureBox.IsRMBDown = false;
            mainPictureBox.Location = new Point(194, 3);
            mainPictureBox.Name = "mainPictureBox";
            mainPictureBox.ScaledDragDifference = new Point(0, 0);
            mainPictureBox.Size = new Size(759, 130);
            mainPictureBox.SizeMode = PictureBoxSizeMode.Zoom;
            mainPictureBox.TabIndex = 0;
            mainPictureBox.TabStop = false;
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 2;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 80F));
            tableLayoutPanel1.Controls.Add(helpPictureBox, 0, 0);
            tableLayoutPanel1.Controls.Add(mainPictureBox, 1, 0);
            tableLayoutPanel1.Dock = DockStyle.Fill;
            tableLayoutPanel1.Location = new Point(0, 0);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 1;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.Size = new Size(956, 136);
            tableLayoutPanel1.TabIndex = 1;
            // 
            // helpPictureBox
            // 
            helpPictureBox.Dock = DockStyle.Fill;
            helpPictureBox.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
            helpPictureBox.IsLMBDown = false;
            helpPictureBox.IsMMBDown = false;
            helpPictureBox.IsRMBDown = false;
            helpPictureBox.Location = new Point(3, 3);
            helpPictureBox.Name = "helpPictureBox";
            helpPictureBox.ScaledDragDifference = new Point(0, 0);
            helpPictureBox.Size = new Size(185, 130);
            helpPictureBox.SizeMode = PictureBoxSizeMode.Zoom;
            helpPictureBox.TabIndex = 2;
            helpPictureBox.TabStop = false;
            // 
            // PianoForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(956, 136);
            Controls.Add(tableLayoutPanel1);
            Name = "PianoForm";
            Text = "PianoForm";
            ((System.ComponentModel.ISupportInitialize)mainPictureBox).EndInit();
            tableLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)helpPictureBox).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private MyPictureBox mainPictureBox;
        private TableLayoutPanel tableLayoutPanel1;
        private MyPictureBox helpPictureBox;
    }
}