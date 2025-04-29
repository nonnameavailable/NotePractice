namespace NotePractice.UI
{
    partial class GrandStaffHolder
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
            trebleBTN = new Button();
            bassBTN = new Button();
            SuspendLayout();
            // 
            // trebleBTN
            // 
            trebleBTN.Location = new Point(3, 3);
            trebleBTN.Name = "trebleBTN";
            trebleBTN.Size = new Size(63, 23);
            trebleBTN.TabIndex = 0;
            trebleBTN.Text = "Treble";
            trebleBTN.UseVisualStyleBackColor = true;
            // 
            // bassBTN
            // 
            bassBTN.Location = new Point(72, 3);
            bassBTN.Name = "bassBTN";
            bassBTN.Size = new Size(63, 23);
            bassBTN.TabIndex = 1;
            bassBTN.Text = "Bass";
            bassBTN.UseVisualStyleBackColor = true;
            // 
            // GrandStaffHolder
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BorderStyle = BorderStyle.FixedSingle;
            Controls.Add(bassBTN);
            Controls.Add(trebleBTN);
            Name = "GrandStaffHolder";
            Size = new Size(137, 30);
            ResumeLayout(false);
        }

        #endregion

        private Button trebleBTN;
        private Button bassBTN;
    }
}
