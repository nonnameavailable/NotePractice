namespace NotePractice.UI
{
    partial class MusicHolder
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
            mainFLP = new FlowLayoutPanel();
            tableLayoutPanel1 = new TableLayoutPanel();
            panel1 = new Panel();
            addBTN = new Button();
            removeBTN = new Button();
            tableLayoutPanel1.SuspendLayout();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // mainFLP
            // 
            mainFLP.AutoScroll = true;
            mainFLP.Dock = DockStyle.Fill;
            mainFLP.Location = new Point(3, 43);
            mainFLP.Name = "mainFLP";
            mainFLP.Size = new Size(144, 104);
            mainFLP.TabIndex = 0;
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 1;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.Controls.Add(mainFLP, 0, 1);
            tableLayoutPanel1.Controls.Add(panel1, 0, 0);
            tableLayoutPanel1.Dock = DockStyle.Fill;
            tableLayoutPanel1.Location = new Point(0, 0);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 2;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.Size = new Size(150, 150);
            tableLayoutPanel1.TabIndex = 1;
            // 
            // panel1
            // 
            panel1.Controls.Add(removeBTN);
            panel1.Controls.Add(addBTN);
            panel1.Dock = DockStyle.Fill;
            panel1.Location = new Point(3, 3);
            panel1.Name = "panel1";
            panel1.Size = new Size(144, 34);
            panel1.TabIndex = 1;
            // 
            // addBTN
            // 
            addBTN.Location = new Point(3, 3);
            addBTN.Name = "addBTN";
            addBTN.Size = new Size(67, 28);
            addBTN.TabIndex = 0;
            addBTN.Text = "Add";
            addBTN.UseVisualStyleBackColor = true;
            // 
            // removeBTN
            // 
            removeBTN.Location = new Point(74, 3);
            removeBTN.Name = "removeBTN";
            removeBTN.Size = new Size(67, 28);
            removeBTN.TabIndex = 1;
            removeBTN.Text = "Remove";
            removeBTN.UseVisualStyleBackColor = true;
            // 
            // MusicHolder
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(tableLayoutPanel1);
            Name = "MusicHolder";
            tableLayoutPanel1.ResumeLayout(false);
            panel1.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private FlowLayoutPanel mainFLP;
        private TableLayoutPanel tableLayoutPanel1;
        private Panel panel1;
        private Button removeBTN;
        private Button addBTN;
    }
}
