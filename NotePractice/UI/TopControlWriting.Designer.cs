namespace NotePractice.UI
{
    partial class TopControlWriting
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
            label7 = new Label();
            durationNUD = new NumericUpDown();
            writingOctaveNUD = new NumericUpDown();
            label6 = new Label();
            ((System.ComponentModel.ISupportInitialize)durationNUD).BeginInit();
            ((System.ComponentModel.ISupportInitialize)writingOctaveNUD).BeginInit();
            SuspendLayout();
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(133, 5);
            label7.Name = "label7";
            label7.Size = new Size(53, 15);
            label7.TabIndex = 31;
            label7.Text = "Duration";
            // 
            // durationNUD
            // 
            durationNUD.Location = new Point(192, 3);
            durationNUD.Maximum = new decimal(new int[] { 64, 0, 0, 0 });
            durationNUD.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            durationNUD.Name = "durationNUD";
            durationNUD.Size = new Size(36, 23);
            durationNUD.TabIndex = 30;
            durationNUD.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // writingOctaveNUD
            // 
            writingOctaveNUD.Location = new Point(91, 3);
            writingOctaveNUD.Maximum = new decimal(new int[] { 8, 0, 0, 0 });
            writingOctaveNUD.Name = "writingOctaveNUD";
            writingOctaveNUD.Size = new Size(36, 23);
            writingOctaveNUD.TabIndex = 29;
            writingOctaveNUD.Value = new decimal(new int[] { 4, 0, 0, 0 });
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(4, 5);
            label6.Name = "label6";
            label6.Size = new Size(84, 15);
            label6.TabIndex = 28;
            label6.Text = "Writing octave";
            // 
            // TopControlWriting
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(label7);
            Controls.Add(durationNUD);
            Controls.Add(writingOctaveNUD);
            Controls.Add(label6);
            Name = "TopControlWriting";
            Size = new Size(348, 92);
            ((System.ComponentModel.ISupportInitialize)durationNUD).EndInit();
            ((System.ComponentModel.ISupportInitialize)writingOctaveNUD).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label7;
        private NumericUpDown durationNUD;
        private NumericUpDown writingOctaveNUD;
        private Label label6;
    }
}
