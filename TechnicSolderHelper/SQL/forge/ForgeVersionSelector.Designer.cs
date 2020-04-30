using System.ComponentModel;
using System.Windows.Forms;

namespace TechnicSolderHelper.SQL.Forge
{
    partial class ForgeVersionSelector
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
            this.errorLabel = new System.Windows.Forms.Label();
            this.forgeVersionDropdown = new System.Windows.Forms.ComboBox();
            this.confirmButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // errorLabel
            // 
            this.errorLabel.AutoSize = true;
            this.errorLabel.Location = new System.Drawing.Point(12, 9);
            this.errorLabel.Name = "errorLabel";
            this.errorLabel.Size = new System.Drawing.Size(239, 26);
            this.errorLabel.TabIndex = 0;
            this.errorLabel.Text = "Selected forge version could not be downloaded.\r\nPlease select a new version.";
            // 
            // forgeVersionDropdown
            // 
            this.forgeVersionDropdown.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.forgeVersionDropdown.FormattingEnabled = true;
            this.forgeVersionDropdown.Location = new System.Drawing.Point(66, 38);
            this.forgeVersionDropdown.Name = "forgeVersionDropdown";
            this.forgeVersionDropdown.Size = new System.Drawing.Size(121, 21);
            this.forgeVersionDropdown.TabIndex = 1;
            // 
            // confirmButton
            // 
            this.confirmButton.Location = new System.Drawing.Point(101, 66);
            this.confirmButton.Name = "confirmButton";
            this.confirmButton.Size = new System.Drawing.Size(51, 23);
            this.confirmButton.TabIndex = 2;
            this.confirmButton.Text = "Confirm";
            this.confirmButton.UseVisualStyleBackColor = true;
            this.confirmButton.Click += new System.EventHandler(this.confirmButton_Click);
            // 
            // ForgeVersionSelector
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(259, 107);
            this.Controls.Add(this.confirmButton);
            this.Controls.Add(this.forgeVersionDropdown);
            this.Controls.Add(this.errorLabel);
            this.Name = "ForgeVersionSelector";
            this.ShowIcon = false;
            this.Text = "Forge Version";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Label errorLabel;
        private ComboBox forgeVersionDropdown;
        private Button confirmButton;
    }
}