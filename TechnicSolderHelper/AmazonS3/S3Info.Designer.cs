using System.ComponentModel;
using System.Windows.Forms;

namespace TechnicSolderHelper.AmazonS3
{
    partial class S3Info
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
            this.serviceUrlTextBox = new System.Windows.Forms.TextBox();
            this.serviceUrlLabel = new System.Windows.Forms.Label();
            this.accessKeyTextBox = new System.Windows.Forms.TextBox();
            this.accessKeyLabel = new System.Windows.Forms.Label();
            this.secretKeyTextBox = new System.Windows.Forms.TextBox();
            this.secretKeyLabel = new System.Windows.Forms.Label();
            this.saveButton = new System.Windows.Forms.Button();
            this.testButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.bucketsListBox = new System.Windows.Forms.ListBox();
            this.createNewBucketButton = new System.Windows.Forms.Button();
            this.newBucketNameTextBox = new System.Windows.Forms.TextBox();
            this.newBucketNameLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // serviceUrlTextBox
            // 
            this.serviceUrlTextBox.Location = new System.Drawing.Point(16, 29);
            this.serviceUrlTextBox.Name = "serviceUrlTextBox";
            this.serviceUrlTextBox.Size = new System.Drawing.Size(156, 20);
            this.serviceUrlTextBox.TabIndex = 0;
            this.serviceUrlTextBox.Text = "http://s3.amazonaws.com/";
            // 
            // serviceUrlLabel
            // 
            this.serviceUrlLabel.AutoSize = true;
            this.serviceUrlLabel.Location = new System.Drawing.Point(13, 13);
            this.serviceUrlLabel.Name = "serviceUrlLabel";
            this.serviceUrlLabel.Size = new System.Drawing.Size(68, 13);
            this.serviceUrlLabel.TabIndex = 10;
            this.serviceUrlLabel.Text = "Service URL";
            // 
            // accessKeyTextBox
            // 
            this.accessKeyTextBox.Location = new System.Drawing.Point(16, 68);
            this.accessKeyTextBox.Name = "accessKeyTextBox";
            this.accessKeyTextBox.Size = new System.Drawing.Size(156, 20);
            this.accessKeyTextBox.TabIndex = 1;
            // 
            // accessKeyLabel
            // 
            this.accessKeyLabel.AutoSize = true;
            this.accessKeyLabel.Location = new System.Drawing.Point(13, 52);
            this.accessKeyLabel.Name = "accessKeyLabel";
            this.accessKeyLabel.Size = new System.Drawing.Size(63, 13);
            this.accessKeyLabel.TabIndex = 10;
            this.accessKeyLabel.Text = "Access Key";
            // 
            // secretKeyTextBox
            // 
            this.secretKeyTextBox.Location = new System.Drawing.Point(16, 107);
            this.secretKeyTextBox.Name = "secretKeyTextBox";
            this.secretKeyTextBox.Size = new System.Drawing.Size(156, 20);
            this.secretKeyTextBox.TabIndex = 2;
            // 
            // secretKeyLabel
            // 
            this.secretKeyLabel.AutoSize = true;
            this.secretKeyLabel.Location = new System.Drawing.Point(13, 91);
            this.secretKeyLabel.Name = "secretKeyLabel";
            this.secretKeyLabel.Size = new System.Drawing.Size(59, 13);
            this.secretKeyLabel.TabIndex = 10;
            this.secretKeyLabel.Text = "Secret Key";
            // 
            // saveButton
            // 
            this.saveButton.Location = new System.Drawing.Point(13, 134);
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size(45, 23);
            this.saveButton.TabIndex = 3;
            this.saveButton.Text = "Save";
            this.saveButton.UseVisualStyleBackColor = true;
            this.saveButton.Click += new System.EventHandler(this.Save_Click);
            // 
            // testButton
            // 
            this.testButton.Location = new System.Drawing.Point(64, 134);
            this.testButton.Name = "testButton";
            this.testButton.Size = new System.Drawing.Size(51, 23);
            this.testButton.TabIndex = 4;
            this.testButton.Text = "Test";
            this.testButton.UseVisualStyleBackColor = true;
            this.testButton.Click += new System.EventHandler(this.test_Click);
            // 
            // cancelButton
            // 
            this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelButton.Location = new System.Drawing.Point(122, 134);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(50, 23);
            this.cancelButton.TabIndex = 4;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // bucketsListBox
            // 
            this.bucketsListBox.FormattingEnabled = true;
            this.bucketsListBox.Location = new System.Drawing.Point(178, 29);
            this.bucketsListBox.Name = "bucketsListBox";
            this.bucketsListBox.Size = new System.Drawing.Size(159, 95);
            this.bucketsListBox.TabIndex = 5;
            // 
            // createNewBucketButton
            // 
            this.createNewBucketButton.Location = new System.Drawing.Point(178, 173);
            this.createNewBucketButton.Name = "createNewBucketButton";
            this.createNewBucketButton.Size = new System.Drawing.Size(159, 23);
            this.createNewBucketButton.TabIndex = 11;
            this.createNewBucketButton.Text = "Create new bucket";
            this.createNewBucketButton.UseVisualStyleBackColor = true;
            this.createNewBucketButton.Click += new System.EventHandler(this.createNewBucketButton_Click);
            // 
            // newBucketNameTextBox
            // 
            this.newBucketNameTextBox.Location = new System.Drawing.Point(178, 147);
            this.newBucketNameTextBox.Name = "newBucketNameTextBox";
            this.newBucketNameTextBox.Size = new System.Drawing.Size(159, 20);
            this.newBucketNameTextBox.TabIndex = 12;
            // 
            // newBucketNameLabel
            // 
            this.newBucketNameLabel.AutoSize = true;
            this.newBucketNameLabel.Location = new System.Drawing.Point(179, 131);
            this.newBucketNameLabel.Name = "newBucketNameLabel";
            this.newBucketNameLabel.Size = new System.Drawing.Size(94, 13);
            this.newBucketNameLabel.TabIndex = 13;
            this.newBucketNameLabel.Text = "New bucket name";
            // 
            // S3Info
            // 
            this.AcceptButton = this.saveButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cancelButton;
            this.ClientSize = new System.Drawing.Size(349, 207);
            this.Controls.Add(this.newBucketNameLabel);
            this.Controls.Add(this.newBucketNameTextBox);
            this.Controls.Add(this.createNewBucketButton);
            this.Controls.Add(this.bucketsListBox);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.testButton);
            this.Controls.Add(this.saveButton);
            this.Controls.Add(this.secretKeyLabel);
            this.Controls.Add(this.secretKeyTextBox);
            this.Controls.Add(this.accessKeyLabel);
            this.Controls.Add(this.accessKeyTextBox);
            this.Controls.Add(this.serviceUrlLabel);
            this.Controls.Add(this.serviceUrlTextBox);
            this.Name = "S3Info";
            this.ShowIcon = false;
            this.Text = "Amazon S3 Config";
            this.Load += new System.EventHandler(this.S3Info_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private TextBox serviceUrlTextBox;
        private Label serviceUrlLabel;
        private TextBox accessKeyTextBox;
        private Label accessKeyLabel;
        private TextBox secretKeyTextBox;
        private Label secretKeyLabel;
        private Button saveButton;
        private Button testButton;
        private Button cancelButton;
        private ListBox bucketsListBox;
        private Button createNewBucketButton;
        private TextBox newBucketNameTextBox;
        private Label newBucketNameLabel;
    }
}