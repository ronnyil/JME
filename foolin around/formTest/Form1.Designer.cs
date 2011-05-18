namespace formTest
{
    partial class Form1
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
            this.button1 = new System.Windows.Forms.Button();
            this.albumBox = new System.Windows.Forms.ComboBox();
            this.foundlabel = new System.Windows.Forms.Label();
            this.goButton = new System.Windows.Forms.Button();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.BW = new System.ComponentModel.BackgroundWorker();
            this.waitLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(33, 25);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(223, 38);
            this.button1.TabIndex = 1;
            this.button1.Text = "Choose a folder...";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // albumBox
            // 
            this.albumBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.albumBox.FormattingEnabled = true;
            this.albumBox.Location = new System.Drawing.Point(8, 110);
            this.albumBox.Name = "albumBox";
            this.albumBox.Size = new System.Drawing.Size(159, 21);
            this.albumBox.TabIndex = 2;
            this.albumBox.Visible = false;
            // 
            // foundlabel
            // 
            this.foundlabel.AutoSize = true;
            this.foundlabel.Location = new System.Drawing.Point(22, 78);
            this.foundlabel.Name = "foundlabel";
            this.foundlabel.Size = new System.Drawing.Size(0, 13);
            this.foundlabel.TabIndex = 3;
            // 
            // goButton
            // 
            this.goButton.Location = new System.Drawing.Point(229, 137);
            this.goButton.Name = "goButton";
            this.goButton.Size = new System.Drawing.Size(51, 53);
            this.goButton.TabIndex = 4;
            this.goButton.Text = "GO!";
            this.goButton.UseVisualStyleBackColor = true;
            this.goButton.Visible = false;
            this.goButton.Click += new System.EventHandler(this.goButton_Click);
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(91, 157);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(100, 23);
            this.progressBar1.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            this.progressBar1.TabIndex = 5;
            this.progressBar1.Visible = false;
            // 
            // BW
            // 
            this.BW.WorkerReportsProgress = true;
            this.BW.DoWork += new System.ComponentModel.DoWorkEventHandler(this.BW_DoWork);
            this.BW.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.BW_RunWorkerCompleted);
            // 
            // waitLabel
            // 
            this.waitLabel.AutoSize = true;
            this.waitLabel.Location = new System.Drawing.Point(7, 137);
            this.waitLabel.Name = "waitLabel";
            this.waitLabel.Size = new System.Drawing.Size(216, 13);
            this.waitLabel.TabIndex = 6;
            this.waitLabel.Text = "Please wait while I fetch the search results...";
            this.waitLabel.Visible = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoValidate = System.Windows.Forms.AutoValidate.EnablePreventFocusChange;
            this.ClientSize = new System.Drawing.Size(287, 192);
            this.Controls.Add(this.waitLabel);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.goButton);
            this.Controls.Add(this.foundlabel);
            this.Controls.Add(this.albumBox);
            this.Controls.Add(this.button1);
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "JME";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ComboBox albumBox;
        private System.Windows.Forms.Label foundlabel;
        private System.Windows.Forms.Button goButton;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.ComponentModel.BackgroundWorker BW;
        private System.Windows.Forms.Label waitLabel;
    }
}

