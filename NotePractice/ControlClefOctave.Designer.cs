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
            ((System.ComponentModel.ISupportInitialize)bassMaxNUD).BeginInit();
            ((System.ComponentModel.ISupportInitialize)bassMinNUD).BeginInit();
            ((System.ComponentModel.ISupportInitialize)trebleMaxNUD).BeginInit();
            ((System.ComponentModel.ISupportInitialize)trebleMinNUD).BeginInit();
            SuspendLayout();
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(191, 33);
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
            clefCBB.Location = new Point(230, 29);
            clefCBB.Name = "clefCBB";
            clefCBB.Size = new Size(89, 23);
            clefCBB.TabIndex = 20;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(137, 9);
            label4.Name = "label4";
            label4.Size = new Size(19, 15);
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
            label1.Size = new Size(81, 15);
            label1.TabIndex = 12;
            label1.Text = "Treble octaves";
            // 
            // ControlClefOctave
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
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
            Size = new Size(326, 99);
            ((System.ComponentModel.ISupportInitialize)bassMaxNUD).EndInit();
            ((System.ComponentModel.ISupportInitialize)bassMinNUD).EndInit();
            ((System.ComponentModel.ISupportInitialize)trebleMaxNUD).EndInit();
            ((System.ComponentModel.ISupportInitialize)trebleMinNUD).EndInit();
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
    }
}
