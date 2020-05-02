namespace TechnicSolderHelper.FileUpload.Sftp
{
    partial class SftpInfo
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
            this.hostLabel = new System.Windows.Forms.Label();
            this.userLabel = new System.Windows.Forms.Label();
            this.portLabel = new System.Windows.Forms.Label();
            this.passwordLabel = new System.Windows.Forms.Label();
            this.destinationLabel = new System.Windows.Forms.Label();
            this.portInput = new System.Windows.Forms.NumericUpDown();
            this.hostTextBox = new System.Windows.Forms.TextBox();
            this.userTextBox = new System.Windows.Forms.TextBox();
            this.destinationTextBox = new System.Windows.Forms.TextBox();
            this.passwordTextBox = new System.Windows.Forms.MaskedTextBox();
            this.acceptButton = new System.Windows.Forms.Button();
            this.uploadTestFileButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.portInput)).BeginInit();
            this.SuspendLayout();
            // 
            // hostLabel
            // 
            this.hostLabel.AutoSize = true;
            this.hostLabel.Location = new System.Drawing.Point(6, 10);
            this.hostLabel.Name = "hostLabel";
            this.hostLabel.Size = new System.Drawing.Size(29, 13);
            this.hostLabel.TabIndex = 0;
            this.hostLabel.Text = "Host";
            // 
            // userLabel
            // 
            this.userLabel.AutoSize = true;
            this.userLabel.Location = new System.Drawing.Point(6, 54);
            this.userLabel.Name = "userLabel";
            this.userLabel.Size = new System.Drawing.Size(29, 13);
            this.userLabel.TabIndex = 1;
            this.userLabel.Text = "User";
            // 
            // portLabel
            // 
            this.portLabel.AutoSize = true;
            this.portLabel.Location = new System.Drawing.Point(146, 10);
            this.portLabel.Name = "portLabel";
            this.portLabel.Size = new System.Drawing.Size(26, 13);
            this.portLabel.TabIndex = 2;
            this.portLabel.Text = "Port";
            // 
            // passwordLabel
            // 
            this.passwordLabel.AutoSize = true;
            this.passwordLabel.Location = new System.Drawing.Point(8, 96);
            this.passwordLabel.Name = "passwordLabel";
            this.passwordLabel.Size = new System.Drawing.Size(53, 13);
            this.passwordLabel.TabIndex = 3;
            this.passwordLabel.Text = "Password";
            // 
            // destinationLabel
            // 
            this.destinationLabel.AutoSize = true;
            this.destinationLabel.Location = new System.Drawing.Point(8, 139);
            this.destinationLabel.Name = "destinationLabel";
            this.destinationLabel.Size = new System.Drawing.Size(60, 13);
            this.destinationLabel.TabIndex = 4;
            this.destinationLabel.Text = "Destination";
            // 
            // portInput
            // 
            this.portInput.Location = new System.Drawing.Point(149, 27);
            this.portInput.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
            this.portInput.Name = "portInput";
            this.portInput.Size = new System.Drawing.Size(60, 20);
            this.portInput.TabIndex = 5;
            this.portInput.Value = new decimal(new int[] {
            22,
            0,
            0,
            0});
            // 
            // hostTextBox
            // 
            this.hostTextBox.Location = new System.Drawing.Point(9, 26);
            this.hostTextBox.Name = "hostTextBox";
            this.hostTextBox.Size = new System.Drawing.Size(125, 20);
            this.hostTextBox.TabIndex = 6;
            // 
            // userTextBox
            // 
            this.userTextBox.Location = new System.Drawing.Point(9, 70);
            this.userTextBox.Name = "userTextBox";
            this.userTextBox.Size = new System.Drawing.Size(125, 20);
            this.userTextBox.TabIndex = 7;
            // 
            // destinationTextBox
            // 
            this.destinationTextBox.Location = new System.Drawing.Point(9, 155);
            this.destinationTextBox.Name = "destinationTextBox";
            this.destinationTextBox.Size = new System.Drawing.Size(125, 20);
            this.destinationTextBox.TabIndex = 8;
            // 
            // passwordTextBox
            // 
            this.passwordTextBox.Location = new System.Drawing.Point(9, 112);
            this.passwordTextBox.Name = "passwordTextBox";
            this.passwordTextBox.Size = new System.Drawing.Size(125, 20);
            this.passwordTextBox.TabIndex = 9;
            // 
            // acceptButton
            // 
            this.acceptButton.Location = new System.Drawing.Point(12, 186);
            this.acceptButton.Name = "acceptButton";
            this.acceptButton.Size = new System.Drawing.Size(75, 23);
            this.acceptButton.TabIndex = 10;
            this.acceptButton.Text = "Accept";
            this.acceptButton.UseVisualStyleBackColor = true;
            this.acceptButton.Click += new System.EventHandler(this.acceptButton_Click);
            // 
            // uploadTestFileButton
            // 
            this.uploadTestFileButton.Location = new System.Drawing.Point(93, 186);
            this.uploadTestFileButton.Name = "uploadTestFileButton";
            this.uploadTestFileButton.Size = new System.Drawing.Size(116, 23);
            this.uploadTestFileButton.TabIndex = 11;
            this.uploadTestFileButton.Text = "Upload Test File";
            this.uploadTestFileButton.UseVisualStyleBackColor = true;
            this.uploadTestFileButton.Click += new System.EventHandler(this.uploadTestFileButton_Click);
            // 
            // cancelButton
            // 
            this.cancelButton.Location = new System.Drawing.Point(215, 186);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 23);
            this.cancelButton.TabIndex = 12;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // SftpInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(302, 218);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.uploadTestFileButton);
            this.Controls.Add(this.acceptButton);
            this.Controls.Add(this.passwordTextBox);
            this.Controls.Add(this.destinationTextBox);
            this.Controls.Add(this.userTextBox);
            this.Controls.Add(this.hostTextBox);
            this.Controls.Add(this.portInput);
            this.Controls.Add(this.destinationLabel);
            this.Controls.Add(this.passwordLabel);
            this.Controls.Add(this.portLabel);
            this.Controls.Add(this.userLabel);
            this.Controls.Add(this.hostLabel);
            this.Name = "SftpInfo";
            this.ShowIcon = false;
            this.Text = "SFTP Config";
            this.Load += new System.EventHandler(this.SftpInfo_Load);
            ((System.ComponentModel.ISupportInitialize)(this.portInput)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label hostLabel;
        private System.Windows.Forms.Label userLabel;
        private System.Windows.Forms.Label portLabel;
        private System.Windows.Forms.Label passwordLabel;
        private System.Windows.Forms.Label destinationLabel;
        private System.Windows.Forms.NumericUpDown portInput;
        private System.Windows.Forms.TextBox hostTextBox;
        private System.Windows.Forms.TextBox userTextBox;
        private System.Windows.Forms.TextBox destinationTextBox;
        private System.Windows.Forms.MaskedTextBox passwordTextBox;
        private System.Windows.Forms.Button acceptButton;
        private System.Windows.Forms.Button uploadTestFileButton;
        private System.Windows.Forms.Button cancelButton;
    }
}