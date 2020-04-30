using System.ComponentModel;
using System.Windows.Forms;

namespace TechnicSolderHelper.FileUpload
{
    partial class UploadProgression
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private IContainer components = null;

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
            this.fileProgressBar = new System.Windows.Forms.ProgressBar();
            this.totalProgressBar = new System.Windows.Forms.ProgressBar();
            this.fileProgressLabel = new System.Windows.Forms.Label();
            this.totalProgressLabel = new System.Windows.Forms.Label();
            this.currentFileTextLabel = new System.Windows.Forms.Label();
            this.currentFileNameLabel = new System.Windows.Forms.Label();
            this.progressPercentageLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // fileProgressBar
            // 
            this.fileProgressBar.Location = new System.Drawing.Point(12, 76);
            this.fileProgressBar.Name = "fileProgressBar";
            this.fileProgressBar.Size = new System.Drawing.Size(260, 23);
            this.fileProgressBar.TabIndex = 0;
            // 
            // totalProgressBar
            // 
            this.totalProgressBar.Location = new System.Drawing.Point(12, 25);
            this.totalProgressBar.Name = "totalProgressBar";
            this.totalProgressBar.Size = new System.Drawing.Size(260, 23);
            this.totalProgressBar.TabIndex = 1;
            // 
            // fileProgressLabel
            // 
            this.fileProgressLabel.AutoSize = true;
            this.fileProgressLabel.Location = new System.Drawing.Point(9, 57);
            this.fileProgressLabel.Name = "fileProgressLabel";
            this.fileProgressLabel.Size = new System.Drawing.Size(66, 13);
            this.fileProgressLabel.TabIndex = 2;
            this.fileProgressLabel.Text = "File progress";
            // 
            // totalProgressLabel
            // 
            this.totalProgressLabel.AutoSize = true;
            this.totalProgressLabel.Location = new System.Drawing.Point(9, 9);
            this.totalProgressLabel.Name = "totalProgressLabel";
            this.totalProgressLabel.Size = new System.Drawing.Size(74, 13);
            this.totalProgressLabel.TabIndex = 3;
            this.totalProgressLabel.Text = "Total progress";
            // 
            // currentFileTextLabel
            // 
            this.currentFileTextLabel.AutoSize = true;
            this.currentFileTextLabel.Location = new System.Drawing.Point(12, 109);
            this.currentFileTextLabel.Name = "currentFileTextLabel";
            this.currentFileTextLabel.Size = new System.Drawing.Size(63, 13);
            this.currentFileTextLabel.TabIndex = 4;
            this.currentFileTextLabel.Text = "Current File:";
            // 
            // currentFileNameLabel
            // 
            this.currentFileNameLabel.AutoSize = true;
            this.currentFileNameLabel.Location = new System.Drawing.Point(82, 109);
            this.currentFileNameLabel.Name = "currentFileNameLabel";
            this.currentFileNameLabel.Size = new System.Drawing.Size(79, 13);
            this.currentFileNameLabel.TabIndex = 5;
            this.currentFileNameLabel.Text = "Some file name";
            // 
            // progressPercentageLabel
            // 
            this.progressPercentageLabel.AutoSize = true;
            this.progressPercentageLabel.Location = new System.Drawing.Point(77, 57);
            this.progressPercentageLabel.Name = "progressPercentageLabel";
            this.progressPercentageLabel.Size = new System.Drawing.Size(21, 13);
            this.progressPercentageLabel.TabIndex = 6;
            this.progressPercentageLabel.Text = "0%";
            // 
            // UploadProgression
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 131);
            this.Controls.Add(this.progressPercentageLabel);
            this.Controls.Add(this.currentFileNameLabel);
            this.Controls.Add(this.currentFileTextLabel);
            this.Controls.Add(this.totalProgressLabel);
            this.Controls.Add(this.fileProgressLabel);
            this.Controls.Add(this.totalProgressBar);
            this.Controls.Add(this.fileProgressBar);
            this.MaximizeBox = false;
            this.Name = "UploadProgression";
            this.ShowIcon = false;
            this.Text = "Upload Progress";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ProgressBar fileProgressBar;
        private ProgressBar totalProgressBar;
        private Label fileProgressLabel;
        private Label totalProgressLabel;
        private Label currentFileTextLabel;
        private Label currentFileNameLabel;
        private Label progressPercentageLabel;
        private BackgroundWorker uploadWorker;
    }
}