namespace NotePractice
{
    partial class ControlClefOctave
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            label5 = new Label();
            clefCBB = new ComboBox();
            label4 = new Label();
            label3 = new Label();
            bassMaxNUD = new NumericUpDown();
            bassMinNUD = new NumericUpDown();
            trebleMaxNUD = new NumericUpDown();
            trebleMinNUD = new NumericUpDown();
            label2 = new Label();
            label1 = new Label();
            includeSharpFlatCB = new CheckBox();
            writingOctaveNUD = new NumericUpDown();
            label6 = new Label();
            durationNUD = new NumericUpDown();
            label7 = new Label();
            practiceModeCBB = new ComboBox();
            ((System.ComponentModel.ISupportInitialize)bassMaxNUD).BeginInit();
            ((System.ComponentModel.ISupportInitialize)bassMinNUD).BeginInit();
            ((System.ComponentModel.ISupportInitialize)trebleMaxNUD).BeginInit();
            ((System.ComponentModel.ISupportInitialize)trebleMinNUD).BeginInit();
            ((System.ComponentModel.ISupportInitialize)writingOctaveNUD).BeginInit();
            ((System.ComponentModel.ISupportInitialize)durationNUD).BeginInit();
            SuspendLayout();
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(201, 13);
            label5.Name = "label5";
            label5.Size = new Size(28, 15);
            label5.TabIndex = 21;
            label5.Text = "Clef";
            // 
            // clefCBB
            // 
            clefCBB.DropDownStyle = ComboBoxStyle.DropDownList;
            clefCBB.FormattingEnabled = true;
            clefCBB.Items.AddRange(new object[] { "Treble", "Bass", "Both" });
            clefCBB.Location = new Point(241, 9);
            clefCBB.Name = "clefCBB";
            clefCBB.Size = new Size(89, 23);
            clefCBB.TabIndex = 20;
            clefCBB.SelectedIndexChanged += clefCBB_SelectedIndexChanged;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(137, 9);
            label4.Name = "label4";
            label4.Size = new Size(20, 15);
            label4.TabIndex = 19;
            label4.Text = "To";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(93, 9);
            label3.Name = "label3";
            label3.Size = new Size(35, 15);
            label3.TabIndex = 18;
            label3.Text = "From";
            // 
            // bassMaxNUD
            // 
            bassMaxNUD.Location = new Point(137, 56);
            bassMaxNUD.Maximum = new decimal(new int[] { 8, 0, 0, 0 });
            bassMaxNUD.Name = "bassMaxNUD";
            bassMaxNUD.Size = new Size(36, 23);
            bassMaxNUD.TabIndex = 17;
            bassMaxNUD.Value = new decimal(new int[] { 3, 0, 0, 0 });
            // 
            // bassMinNUD
            // 
            bassMinNUD.Location = new Point(95, 56);
            bassMinNUD.Maximum = new decimal(new int[] { 8, 0, 0, 0 });
            bassMinNUD.Name = "bassMinNUD";
            bassMinNUD.Size = new Size(36, 23);
            bassMinNUD.TabIndex = 16;
            bassMinNUD.Value = new decimal(new int[] { 3, 0, 0, 0 });
            // 
            // trebleMaxNUD
            // 
            trebleMaxNUD.Location = new Point(137, 27);
            trebleMaxNUD.Maximum = new decimal(new int[] { 8, 0, 0, 0 });
            trebleMaxNUD.Name = "trebleMaxNUD";
            trebleMaxNUD.Size = new Size(36, 23);
            trebleMaxNUD.TabIndex = 15;
            trebleMaxNUD.Value = new decimal(new int[] { 4, 0, 0, 0 });
            // 
            // trebleMinNUD
            // 
            trebleMinNUD.Location = new Point(95, 27);
            trebleMinNUD.Maximum = new decimal(new int[] { 8, 0, 0, 0 });
            trebleMinNUD.Name = "trebleMinNUD";
            trebleMinNUD.Size = new Size(36, 23);
            trebleMinNUD.TabIndex = 14;
            trebleMinNUD.Value = new decimal(new int[] { 4, 0, 0, 0 });
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(8, 58);
            label2.Name = "label2";
            label2.Size = new Size(73, 15);
            label2.TabIndex = 13;
            label2.Text = "Bass octaves";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(8, 29);
            label1.Name = "label1";
            label1.Size = new Size(82, 15);
            label1.TabIndex = 12;
            label1.Text = "Treble octaves";
            // 
            // includeSharpFlatCB
            // 
            includeSharpFlatCB.AutoSize = true;
            includeSharpFlatCB.Location = new Point(212, 36);
            includeSharpFlatCB.Margin = new Padding(3, 2, 3, 2);
            includeSharpFlatCB.Name = "includeSharpFlatCB";
            includeSharpFlatCB.Size = new Size(82, 19);
            includeSharpFlatCB.TabIndex = 23;
            includeSharpFlatCB.Text = "include #b";
            includeSharpFlatCB.UseVisualStyleBackColor = true;
            // 
            // writingOctaveNUD
            // 
            writingOctaveNUD.Location = new Point(95, 89);
            writingOctaveNUD.Maximum = new decimal(new int[] { 8, 0, 0, 0 });
            writingOctaveNUD.Name = "writingOctaveNUD";
            writingOctaveNUD.Size = new Size(36, 23);
            writingOctaveNUD.TabIndex = 25;
            writingOctaveNUD.Value = new decimal(new int[] { 4, 0, 0, 0 });
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(8, 91);
            label6.Name = "label6";
            label6.Size = new Size(84, 15);
            label6.TabIndex = 24;
            label6.Text = "Writing octave";
            // 
            // durationNUD
            // 
            durationNUD.Location = new Point(196, 89);
            durationNUD.Maximum = new decimal(new int[] { 64, 0, 0, 0 });
            durationNUD.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            durationNUD.Name = "durationNUD";
            durationNUD.Size = new Size(36, 23);
            durationNUD.TabIndex = 26;
            durationNUD.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(137, 91);
            label7.Name = "label7";
            label7.Size = new Size(53, 15);
            label7.TabIndex = 27;
            label7.Text = "Duration";
            // 
            // practiceModeCBB
            // 
            practiceModeCBB.DropDownStyle = ComboBoxStyle.DropDownList;
            practiceModeCBB.FormattingEnabled = true;
            practiceModeCBB.Items.AddRange(new object[] { "Notes", "Intervals" });
            practiceModeCBB.Location = new Point(201, 56);
            practiceModeCBB.Name = "practiceModeCBB";
            practiceModeCBB.Size = new Size(121, 23);
            practiceModeCBB.TabIndex = 28;
            // 
            // ControlClefOctave
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(practiceModeCBB);
            Controls.Add(label7);
            Controls.Add(durationNUD);
            Controls.Add(writingOctaveNUD);
            Controls.Add(label6);
            Controls.Add(includeSharpFlatCB);
            Controls.Add(label5);
            Controls.Add(clefCBB);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(bassMaxNUD);
            Controls.Add(bassMinNUD);
            Controls.Add(trebleMaxNUD);
            Controls.Add(trebleMinNUD);
            Controls.Add(label2);
            Controls.Add(label1);
            Name = "ControlClefOctave";
            Size = new Size(351, 117);
            ((System.ComponentModel.ISupportInitialize)bassMaxNUD).EndInit();
            ((System.ComponentModel.ISupportInitialize)bassMinNUD).EndInit();
            ((System.ComponentModel.ISupportInitialize)trebleMaxNUD).EndInit();
            ((System.ComponentModel.ISupportInitialize)trebleMinNUD).EndInit();
            ((System.ComponentModel.ISupportInitialize)writingOctaveNUD).EndInit();
            ((System.ComponentModel.ISupportInitialize)durationNUD).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label5;
        private ComboBox clefCBB;
        private Label label4;
        private Label label3;
        private NumericUpDown bassMaxNUD;
        private NumericUpDown bassMinNUD;
        private NumericUpDown trebleMaxNUD;
        private NumericUpDown trebleMinNUD;
        private Label label2;
        private Label label1;
        private CheckBox includeSharpFlatCB;
        private NumericUpDown writingOctaveNUD;
        private Label label6;
        private NumericUpDown durationNUD;
        private Label label7;
        private ComboBox practiceModeCBB;
    }
}
