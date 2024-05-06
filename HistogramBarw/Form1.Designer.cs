namespace HistogramBarw
{
    partial class Form1
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
            this.label2 = new System.Windows.Forms.Label();
            this.trackThreads = new System.Windows.Forms.TrackBar();
            this.button1 = new System.Windows.Forms.Button();
            this.buttonCreate = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.histogramBox = new System.Windows.Forms.PictureBox();
            this.labelThreads = new System.Windows.Forms.Label();
            this.labelTime = new System.Windows.Forms.Label();
            this.radioButtonAsm = new System.Windows.Forms.RadioButton();
            this.radioButtonCs = new System.Windows.Forms.RadioButton();
            ((System.ComponentModel.ISupportInitialize)(this.trackThreads)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.histogramBox)).BeginInit();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(378, 29);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(64, 15);
            this.label2.TabIndex = 1;
            this.label2.Text = "PROGRAM";
            // 
            // trackThreads
            // 
            this.trackThreads.Location = new System.Drawing.Point(362, 93);
            this.trackThreads.Maximum = 64;
            this.trackThreads.Minimum = 1;
            this.trackThreads.Name = "trackThreads";
            this.trackThreads.Size = new System.Drawing.Size(104, 45);
            this.trackThreads.TabIndex = 2;
            this.trackThreads.Value = 1;
            this.trackThreads.Scroll += new System.EventHandler(this.trackThreads_Scroll);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(150, 133);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 45);
            this.button1.TabIndex = 5;
            this.button1.Text = "Load image";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // buttonCreate
            // 
            this.buttonCreate.Enabled = false;
            this.buttonCreate.Location = new System.Drawing.Point(391, 474);
            this.buttonCreate.Name = "buttonCreate";
            this.buttonCreate.Size = new System.Drawing.Size(126, 23);
            this.buttonCreate.TabIndex = 6;
            this.buttonCreate.Text = "Create histogram";
            this.buttonCreate.UseVisualStyleBackColor = true;
            this.buttonCreate.Click += new System.EventHandler(this.button2_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(70, 197);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(256, 256);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 7;
            this.pictureBox1.TabStop = false;
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // histogramBox
            // 
            this.histogramBox.Location = new System.Drawing.Point(535, 197);
            this.histogramBox.Name = "histogramBox";
            this.histogramBox.Size = new System.Drawing.Size(256, 256);
            this.histogramBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.histogramBox.TabIndex = 8;
            this.histogramBox.TabStop = false;
            // 
            // labelThreads
            // 
            this.labelThreads.AutoSize = true;
            this.labelThreads.Location = new System.Drawing.Point(369, 123);
            this.labelThreads.Name = "labelThreads";
            this.labelThreads.Size = new System.Drawing.Size(93, 15);
            this.labelThreads.TabIndex = 9;
            this.labelThreads.Text = "Threads number";
            // 
            // labelTime
            // 
            this.labelTime.AutoSize = true;
            this.labelTime.Location = new System.Drawing.Point(412, 352);
            this.labelTime.Name = "labelTime";
            this.labelTime.Size = new System.Drawing.Size(41, 15);
            this.labelTime.TabIndex = 10;
            this.labelTime.Text = "--- ms";
            // 
            // radioButtonAsm
            // 
            this.radioButtonAsm.AutoSize = true;
            this.radioButtonAsm.Checked = true;
            this.radioButtonAsm.Location = new System.Drawing.Point(72, 42);
            this.radioButtonAsm.Name = "radioButtonAsm";
            this.radioButtonAsm.Size = new System.Drawing.Size(49, 19);
            this.radioButtonAsm.TabIndex = 11;
            this.radioButtonAsm.TabStop = true;
            this.radioButtonAsm.Text = "Asm";
            this.radioButtonAsm.UseVisualStyleBackColor = true;
            // 
            // radioButtonCs
            // 
            this.radioButtonCs.AutoSize = true;
            this.radioButtonCs.Location = new System.Drawing.Point(72, 67);
            this.radioButtonCs.Name = "radioButtonCs";
            this.radioButtonCs.Size = new System.Drawing.Size(40, 19);
            this.radioButtonCs.TabIndex = 11;
            this.radioButtonCs.TabStop = true;
            this.radioButtonCs.Text = "C#";
            this.radioButtonCs.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(862, 533);
            this.Controls.Add(this.radioButtonCs);
            this.Controls.Add(this.radioButtonAsm);
            this.Controls.Add(this.labelTime);
            this.Controls.Add(this.labelThreads);
            this.Controls.Add(this.histogramBox);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.buttonCreate);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.trackThreads);
            this.Controls.Add(this.label2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.Text = "Histogram";
            ((System.ComponentModel.ISupportInitialize)(this.trackThreads)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.histogramBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private Label label2;
        private TrackBar trackThreads;
        private Button button1;
        private Button buttonCreate;
        private PictureBox pictureBox1;
        private OpenFileDialog openFileDialog1;
        private PictureBox histogramBox;
        private Label labelThreads;
        private Label labelTime;
        private RadioButton radioButtonAsm;
        private RadioButton radioButtonCs;
    }
}