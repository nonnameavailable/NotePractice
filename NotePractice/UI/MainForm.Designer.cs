using NotePractice.Music.Symbols;

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
            mainPictureBox = new PictureBox();
            showPianoBTN = new Button();
            panel1 = new Panel();
            modeGroupBox = new GroupBox();
            writingRB = new RadioButton();
            practiceRB = new RadioButton();
            topPanel = new Panel();
            rightPanel = new Panel();
            panel2 = new Panel();
            outputMidiBTN = new Button();
            inputMidiBTN = new Button();
            label2 = new Label();
            label1 = new Label();
            tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)mainPictureBox).BeginInit();
            panel1.SuspendLayout();
            modeGroupBox.SuspendLayout();
            panel2.SuspendLayout();
            SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 3;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 100F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 65F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 35F));
            tableLayoutPanel1.Controls.Add(mainPictureBox, 1, 1);
            tableLayoutPanel1.Controls.Add(showPianoBTN, 0, 0);
            tableLayoutPanel1.Controls.Add(panel1, 0, 1);
            tableLayoutPanel1.Controls.Add(topPanel, 1, 0);
            tableLayoutPanel1.Controls.Add(rightPanel, 2, 1);
            tableLayoutPanel1.Controls.Add(panel2, 2, 0);
            tableLayoutPanel1.Dock = DockStyle.Fill;
            tableLayoutPanel1.Location = new Point(0, 0);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 2;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 120F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tableLayoutPanel1.Size = new Size(669, 428);
            tableLayoutPanel1.TabIndex = 0;
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
            // showPianoBTN
            // 
            showPianoBTN.Dock = DockStyle.Fill;
            showPianoBTN.Location = new Point(3, 3);
            showPianoBTN.Name = "showPianoBTN";
            showPianoBTN.Size = new Size(94, 114);
            showPianoBTN.TabIndex = 2;
            showPianoBTN.Text = "Show piano";
            showPianoBTN.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            panel1.Controls.Add(modeGroupBox);
            panel1.Dock = DockStyle.Fill;
            panel1.Location = new Point(3, 123);
            panel1.Name = "panel1";
            panel1.Size = new Size(94, 302);
            panel1.TabIndex = 4;
            // 
            // modeGroupBox
            // 
            modeGroupBox.Controls.Add(writingRB);
            modeGroupBox.Controls.Add(practiceRB);
            modeGroupBox.Location = new Point(3, 3);
            modeGroupBox.Name = "modeGroupBox";
            modeGroupBox.Size = new Size(88, 82);
            modeGroupBox.TabIndex = 0;
            modeGroupBox.TabStop = false;
            modeGroupBox.Text = "Mode";
            // 
            // writingRB
            // 
            writingRB.AutoSize = true;
            writingRB.Location = new Point(6, 47);
            writingRB.Name = "writingRB";
            writingRB.Size = new Size(64, 19);
            writingRB.TabIndex = 1;
            writingRB.TabStop = true;
            writingRB.Text = "Writing";
            writingRB.UseVisualStyleBackColor = true;
            // 
            // practiceRB
            // 
            practiceRB.AutoSize = true;
            practiceRB.Checked = true;
            practiceRB.Location = new Point(6, 22);
            practiceRB.Name = "practiceRB";
            practiceRB.Size = new Size(67, 19);
            practiceRB.TabIndex = 0;
            practiceRB.TabStop = true;
            practiceRB.Text = "Practice";
            practiceRB.UseVisualStyleBackColor = true;
            // 
            // topPanel
            // 
            topPanel.Dock = DockStyle.Fill;
            topPanel.Location = new Point(103, 3);
            topPanel.Name = "topPanel";
            topPanel.Size = new Size(363, 114);
            topPanel.TabIndex = 5;
            // 
            // rightPanel
            // 
            rightPanel.Dock = DockStyle.Fill;
            rightPanel.Location = new Point(472, 123);
            rightPanel.Name = "rightPanel";
            rightPanel.Size = new Size(194, 302);
            rightPanel.TabIndex = 6;
            // 
            // panel2
            // 
            panel2.Controls.Add(outputMidiBTN);
            panel2.Controls.Add(inputMidiBTN);
            panel2.Controls.Add(label2);
            panel2.Controls.Add(label1);
            panel2.Dock = DockStyle.Fill;
            panel2.Location = new Point(472, 3);
            panel2.Name = "panel2";
            panel2.Size = new Size(194, 114);
            panel2.TabIndex = 7;
            // 
            // outputMidiBTN
            // 
            outputMidiBTN.Location = new Point(103, 36);
            outputMidiBTN.Name = "outputMidiBTN";
            outputMidiBTN.Size = new Size(88, 43);
            outputMidiBTN.TabIndex = 3;
            outputMidiBTN.Text = "None";
            outputMidiBTN.UseVisualStyleBackColor = true;
            // 
            // inputMidiBTN
            // 
            inputMidiBTN.Location = new Point(3, 36);
            inputMidiBTN.Name = "inputMidiBTN";
            inputMidiBTN.Size = new Size(88, 43);
            inputMidiBTN.TabIndex = 2;
            inputMidiBTN.Text = "None";
            inputMidiBTN.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(112, 16);
            label2.Name = "label2";
            label2.Size = new Size(73, 15);
            label2.TabIndex = 1;
            label2.Text = "Output MIDI";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(15, 16);
            label1.Name = "label1";
            label1.Size = new Size(63, 15);
            label1.TabIndex = 0;
            label1.Text = "Input MIDI";
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
            ((System.ComponentModel.ISupportInitialize)mainPictureBox).EndInit();
            panel1.ResumeLayout(false);
            modeGroupBox.ResumeLayout(false);
            modeGroupBox.PerformLayout();
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private TableLayoutPanel tableLayoutPanel1;
        private PictureBox mainPictureBox;
        private NumericUpDown numericUpDown2;
        private Button showPianoBTN;
        private Panel panel1;
        private GroupBox modeGroupBox;
        private RadioButton writingRB;
        private RadioButton practiceRB;
        private Panel topPanel;
        private Panel rightPanel;
        private Panel panel2;
        private Button outputMidiBTN;
        private Button inputMidiBTN;
        private Label label2;
        private Label label1;
    }
}
