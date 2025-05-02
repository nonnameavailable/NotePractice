namespace NotePractice.MIDIHandling
{
    partial class DeviceSelectForm
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
            mainFLP = new FlowLayoutPanel();
            SuspendLayout();
            // 
            // mainFLP
            // 
            mainFLP.Dock = DockStyle.Fill;
            mainFLP.FlowDirection = FlowDirection.TopDown;
            mainFLP.Location = new Point(0, 0);
            mainFLP.Name = "mainFLP";
            mainFLP.Size = new Size(177, 205);
            mainFLP.TabIndex = 0;
            // 
            // DeviceSelectForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(177, 205);
            Controls.Add(mainFLP);
            Name = "DeviceSelectForm";
            Text = "DeviceSelectForm";
            ResumeLayout(false);
        }

        #endregion

        private FlowLayoutPanel mainFLP;
    }
}