using System.ComponentModel;
using System.Windows.Forms;

namespace TechnicSolderHelper.SmallInterfaces
{
    partial class McSelector
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
            this.questionLabel = new System.Windows.Forms.Label();
            this.mcVersionDropdown = new System.Windows.Forms.ComboBox();
            this.acceptButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // questionLabel
            // 
            this.questionLabel.AutoSize = true;
            this.questionLabel.Location = new System.Drawing.Point(29, 9);
            this.questionLabel.Name = "questionLabel";
            this.questionLabel.Size = new System.Drawing.Size(228, 13);
            this.questionLabel.TabIndex = 0;
            this.questionLabel.Text = "What is the Minecraft version of the modpack?";
            // 
            // mcVersionDropdown
            // 
            this.mcVersionDropdown.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.mcVersionDropdown.FormattingEnabled = true;
            this.mcVersionDropdown.Location = new System.Drawing.Point(77, 39);
            this.mcVersionDropdown.Name = "mcVersionDropdown";
            this.mcVersionDropdown.Size = new System.Drawing.Size(121, 21);
            this.mcVersionDropdown.TabIndex = 1;
            // 
            // acceptButton
            // 
            this.acceptButton.Location = new System.Drawing.Point(102, 66);
            this.acceptButton.Name = "acceptButton";
            this.acceptButton.Size = new System.Drawing.Size(75, 23);
            this.acceptButton.TabIndex = 2;
            this.acceptButton.Text = "Accept";
            this.acceptButton.UseVisualStyleBackColor = true;
            this.acceptButton.Click += new System.EventHandler(this.acceptButton_Click);
            // 
            // McSelector
            // 
            this.AcceptButton = this.acceptButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 103);
            this.Controls.Add(this.acceptButton);
            this.Controls.Add(this.mcVersionDropdown);
            this.Controls.Add(this.questionLabel);
            this.Name = "McSelector";
            this.ShowIcon = false;
            this.Text = "Minecraft Version";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Label questionLabel;
        private ComboBox mcVersionDropdown;
        private Button acceptButton;
    }
}