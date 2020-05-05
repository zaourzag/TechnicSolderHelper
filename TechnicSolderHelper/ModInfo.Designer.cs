using System.ComponentModel;
using System.Windows.Forms;

namespace TechnicSolderHelper
{
    partial class ModInfo
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
            this.components = new System.ComponentModel.Container();
            this.modListBox = new System.Windows.Forms.ListBox();
            this.modDataGroupBox = new System.Windows.Forms.GroupBox();
            this.fileNameLabel = new System.Windows.Forms.Label();
            this.authorLabel = new System.Windows.Forms.Label();
            this.modVersionLabel = new System.Windows.Forms.Label();
            this.modIdLabel = new System.Windows.Forms.Label();
            this.modNameLabel = new System.Windows.Forms.Label();
            this.fileNameTextBox = new System.Windows.Forms.TextBox();
            this.authorTextBox = new System.Windows.Forms.TextBox();
            this.modVersionTextBox = new System.Windows.Forms.TextBox();
            this.modIdTextBox = new System.Windows.Forms.TextBox();
            this.modNameTextBox = new System.Windows.Forms.TextBox();
            this.technicPermissionsGroupBox = new System.Windows.Forms.GroupBox();
            this.technicLicenseLinkLabel = new System.Windows.Forms.Label();
            this.technicModLinkLabel = new System.Windows.Forms.Label();
            this.technicPermisisonLinkLabel = new System.Windows.Forms.Label();
            this.technicPermissionUnknownRadioButton = new TechnicSolderHelper.ReadOnlyRadioButton();
            this.technicPermissionRequestRadioButton = new TechnicSolderHelper.ReadOnlyRadioButton();
            this.technicPermissionNotifyRadioButton = new TechnicSolderHelper.ReadOnlyRadioButton();
            this.technicPermissionClosedRadioButton = new TechnicSolderHelper.ReadOnlyRadioButton();
            this.technicLicenseLinkTextBox = new System.Windows.Forms.TextBox();
            this.technicModLinkTextBox = new System.Windows.Forms.TextBox();
            this.technicPermissionLinkTextBox = new System.Windows.Forms.TextBox();
            this.technicPermissionFtbExclusiveRadioButton = new TechnicSolderHelper.ReadOnlyRadioButton();
            this.technicPermissionOpenRadioButton = new TechnicSolderHelper.ReadOnlyRadioButton();
            this.selectModLabel = new System.Windows.Forms.Label();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.showDoneCheckBox = new System.Windows.Forms.CheckBox();
            this.ftbPermissionsGroupBox = new System.Windows.Forms.GroupBox();
            this.ftbLicenseLinkLabel = new System.Windows.Forms.Label();
            this.ftbModLinkLabel = new System.Windows.Forms.Label();
            this.ftbPermissionLinkLabel = new System.Windows.Forms.Label();
            this.ftbPermissionUnknownRadioButton = new TechnicSolderHelper.ReadOnlyRadioButton();
            this.ftbPermissionRequestRadioButton = new TechnicSolderHelper.ReadOnlyRadioButton();
            this.ftbPermissionNotifyRadioButton = new TechnicSolderHelper.ReadOnlyRadioButton();
            this.ftbPermissionClosedRadioButton = new TechnicSolderHelper.ReadOnlyRadioButton();
            this.ftbLicenseLinkTextBox = new System.Windows.Forms.TextBox();
            this.ftbModLinkTextBox = new System.Windows.Forms.TextBox();
            this.ftbPermissionLinkTextBox = new System.Windows.Forms.TextBox();
            this.ftbPermissionFtbExclusiveRadioButton = new TechnicSolderHelper.ReadOnlyRadioButton();
            this.ftbPermissionOpenRadioButton = new TechnicSolderHelper.ReadOnlyRadioButton();
            this.getPermissionsButton = new System.Windows.Forms.Button();
            this.doneButton = new System.Windows.Forms.Button();
            this.skipModCheckBox = new System.Windows.Forms.CheckBox();
            this.modDataGroupBox.SuspendLayout();
            this.technicPermissionsGroupBox.SuspendLayout();
            this.ftbPermissionsGroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // modListBox
            // 
            this.modListBox.FormattingEnabled = true;
            this.modListBox.Location = new System.Drawing.Point(12, 30);
            this.modListBox.Name = "modListBox";
            this.modListBox.Size = new System.Drawing.Size(163, 381);
            this.modListBox.TabIndex = 0;
            this.modListBox.SelectedIndexChanged += new System.EventHandler(this.modListBox_SelectedIndexChanged);
            // 
            // modDataGroupBox
            // 
            this.modDataGroupBox.Controls.Add(this.fileNameLabel);
            this.modDataGroupBox.Controls.Add(this.authorLabel);
            this.modDataGroupBox.Controls.Add(this.modVersionLabel);
            this.modDataGroupBox.Controls.Add(this.modIdLabel);
            this.modDataGroupBox.Controls.Add(this.modNameLabel);
            this.modDataGroupBox.Controls.Add(this.fileNameTextBox);
            this.modDataGroupBox.Controls.Add(this.authorTextBox);
            this.modDataGroupBox.Controls.Add(this.modVersionTextBox);
            this.modDataGroupBox.Controls.Add(this.modIdTextBox);
            this.modDataGroupBox.Controls.Add(this.modNameTextBox);
            this.modDataGroupBox.Location = new System.Drawing.Point(181, 13);
            this.modDataGroupBox.Name = "modDataGroupBox";
            this.modDataGroupBox.Size = new System.Drawing.Size(253, 251);
            this.modDataGroupBox.TabIndex = 1;
            this.modDataGroupBox.TabStop = false;
            this.modDataGroupBox.Text = "Mod Data";
            // 
            // fileNameLabel
            // 
            this.fileNameLabel.AutoSize = true;
            this.fileNameLabel.Location = new System.Drawing.Point(21, 195);
            this.fileNameLabel.Name = "fileNameLabel";
            this.fileNameLabel.Size = new System.Drawing.Size(54, 13);
            this.fileNameLabel.TabIndex = 1;
            this.fileNameLabel.Text = "File Name";
            // 
            // authorLabel
            // 
            this.authorLabel.AutoSize = true;
            this.authorLabel.Location = new System.Drawing.Point(21, 151);
            this.authorLabel.Name = "authorLabel";
            this.authorLabel.Size = new System.Drawing.Size(38, 13);
            this.authorLabel.TabIndex = 1;
            this.authorLabel.Text = "Author";
            // 
            // modVersionLabel
            // 
            this.modVersionLabel.AutoSize = true;
            this.modVersionLabel.Location = new System.Drawing.Point(21, 108);
            this.modVersionLabel.Name = "modVersionLabel";
            this.modVersionLabel.Size = new System.Drawing.Size(66, 13);
            this.modVersionLabel.TabIndex = 1;
            this.modVersionLabel.Text = "Mod Version";
            // 
            // modIdLabel
            // 
            this.modIdLabel.AutoSize = true;
            this.modIdLabel.Location = new System.Drawing.Point(21, 64);
            this.modIdLabel.Name = "modIdLabel";
            this.modIdLabel.Size = new System.Drawing.Size(42, 13);
            this.modIdLabel.TabIndex = 1;
            this.modIdLabel.Text = "Mod ID";
            // 
            // modNameLabel
            // 
            this.modNameLabel.AutoSize = true;
            this.modNameLabel.Location = new System.Drawing.Point(21, 20);
            this.modNameLabel.Name = "modNameLabel";
            this.modNameLabel.Size = new System.Drawing.Size(59, 13);
            this.modNameLabel.TabIndex = 1;
            this.modNameLabel.Text = "Mod Name";
            // 
            // fileNameTextBox
            // 
            this.fileNameTextBox.Location = new System.Drawing.Point(24, 211);
            this.fileNameTextBox.Name = "fileNameTextBox";
            this.fileNameTextBox.ReadOnly = true;
            this.fileNameTextBox.Size = new System.Drawing.Size(205, 20);
            this.fileNameTextBox.TabIndex = 5;
            // 
            // authorTextBox
            // 
            this.authorTextBox.Location = new System.Drawing.Point(24, 167);
            this.authorTextBox.Name = "authorTextBox";
            this.authorTextBox.Size = new System.Drawing.Size(205, 20);
            this.authorTextBox.TabIndex = 4;
            this.authorTextBox.TextChanged += new System.EventHandler(this.authorTextBox_TextChanged);
            // 
            // modVersionTextBox
            // 
            this.modVersionTextBox.Location = new System.Drawing.Point(24, 124);
            this.modVersionTextBox.Name = "modVersionTextBox";
            this.modVersionTextBox.Size = new System.Drawing.Size(205, 20);
            this.modVersionTextBox.TabIndex = 3;
            this.modVersionTextBox.TextChanged += new System.EventHandler(this.modVersionTextBox_TextChanged);
            // 
            // modIdTextBox
            // 
            this.modIdTextBox.Location = new System.Drawing.Point(24, 80);
            this.modIdTextBox.Name = "modIdTextBox";
            this.modIdTextBox.Size = new System.Drawing.Size(205, 20);
            this.modIdTextBox.TabIndex = 2;
            this.modIdTextBox.TextChanged += new System.EventHandler(this.modIdTextBox_TextChanged);
            // 
            // modNameTextBox
            // 
            this.modNameTextBox.Location = new System.Drawing.Point(24, 36);
            this.modNameTextBox.Name = "modNameTextBox";
            this.modNameTextBox.Size = new System.Drawing.Size(205, 20);
            this.modNameTextBox.TabIndex = 1;
            this.modNameTextBox.TextChanged += new System.EventHandler(this.modNameTextBox_TextChanged);
            // 
            // technicPermissionsGroupBox
            // 
            this.technicPermissionsGroupBox.Controls.Add(this.technicLicenseLinkLabel);
            this.technicPermissionsGroupBox.Controls.Add(this.technicModLinkLabel);
            this.technicPermissionsGroupBox.Controls.Add(this.technicPermisisonLinkLabel);
            this.technicPermissionsGroupBox.Controls.Add(this.technicPermissionUnknownRadioButton);
            this.technicPermissionsGroupBox.Controls.Add(this.technicPermissionRequestRadioButton);
            this.technicPermissionsGroupBox.Controls.Add(this.technicPermissionNotifyRadioButton);
            this.technicPermissionsGroupBox.Controls.Add(this.technicPermissionClosedRadioButton);
            this.technicPermissionsGroupBox.Controls.Add(this.technicLicenseLinkTextBox);
            this.technicPermissionsGroupBox.Controls.Add(this.technicModLinkTextBox);
            this.technicPermissionsGroupBox.Controls.Add(this.technicPermissionLinkTextBox);
            this.technicPermissionsGroupBox.Controls.Add(this.technicPermissionFtbExclusiveRadioButton);
            this.technicPermissionsGroupBox.Controls.Add(this.technicPermissionOpenRadioButton);
            this.technicPermissionsGroupBox.Location = new System.Drawing.Point(182, 270);
            this.technicPermissionsGroupBox.Name = "technicPermissionsGroupBox";
            this.technicPermissionsGroupBox.Size = new System.Drawing.Size(252, 245);
            this.technicPermissionsGroupBox.TabIndex = 2;
            this.technicPermissionsGroupBox.TabStop = false;
            this.technicPermissionsGroupBox.Text = "Technic Permissions";
            // 
            // technicLicenseLinkLabel
            // 
            this.technicLicenseLinkLabel.AutoSize = true;
            this.technicLicenseLinkLabel.Location = new System.Drawing.Point(20, 185);
            this.technicLicenseLinkLabel.Name = "technicLicenseLinkLabel";
            this.technicLicenseLinkLabel.Size = new System.Drawing.Size(67, 13);
            this.technicLicenseLinkLabel.TabIndex = 1;
            this.technicLicenseLinkLabel.Text = "License Link";
            // 
            // technicModLinkLabel
            // 
            this.technicModLinkLabel.AutoSize = true;
            this.technicModLinkLabel.Location = new System.Drawing.Point(20, 138);
            this.technicModLinkLabel.Name = "technicModLinkLabel";
            this.technicModLinkLabel.Size = new System.Drawing.Size(51, 13);
            this.technicModLinkLabel.TabIndex = 1;
            this.technicModLinkLabel.Text = "Mod Link";
            // 
            // technicPermisisonLinkLabel
            // 
            this.technicPermisisonLinkLabel.AutoSize = true;
            this.technicPermisisonLinkLabel.Location = new System.Drawing.Point(20, 96);
            this.technicPermisisonLinkLabel.Name = "technicPermisisonLinkLabel";
            this.technicPermisisonLinkLabel.Size = new System.Drawing.Size(80, 13);
            this.technicPermisisonLinkLabel.TabIndex = 1;
            this.technicPermisisonLinkLabel.Text = "Permission Link";
            // 
            // technicPermissionUnknownRadioButton
            // 
            this.technicPermissionUnknownRadioButton.AutoSize = true;
            this.technicPermissionUnknownRadioButton.Location = new System.Drawing.Point(161, 66);
            this.technicPermissionUnknownRadioButton.Name = "technicPermissionUnknownRadioButton";
            this.technicPermissionUnknownRadioButton.ReadOnly = true;
            this.technicPermissionUnknownRadioButton.Size = new System.Drawing.Size(71, 17);
            this.technicPermissionUnknownRadioButton.TabIndex = 0;
            this.technicPermissionUnknownRadioButton.Text = "Unknown";
            this.technicPermissionUnknownRadioButton.UseVisualStyleBackColor = true;
            // 
            // technicPermissionRequestRadioButton
            // 
            this.technicPermissionRequestRadioButton.AutoSize = true;
            this.technicPermissionRequestRadioButton.Location = new System.Drawing.Point(161, 43);
            this.technicPermissionRequestRadioButton.Name = "technicPermissionRequestRadioButton";
            this.technicPermissionRequestRadioButton.ReadOnly = true;
            this.technicPermissionRequestRadioButton.Size = new System.Drawing.Size(65, 17);
            this.technicPermissionRequestRadioButton.TabIndex = 0;
            this.technicPermissionRequestRadioButton.Text = "Request";
            this.technicPermissionRequestRadioButton.UseVisualStyleBackColor = true;
            // 
            // technicPermissionNotifyRadioButton
            // 
            this.technicPermissionNotifyRadioButton.AutoSize = true;
            this.technicPermissionNotifyRadioButton.Location = new System.Drawing.Point(23, 66);
            this.technicPermissionNotifyRadioButton.Name = "technicPermissionNotifyRadioButton";
            this.technicPermissionNotifyRadioButton.ReadOnly = true;
            this.technicPermissionNotifyRadioButton.Size = new System.Drawing.Size(52, 17);
            this.technicPermissionNotifyRadioButton.TabIndex = 0;
            this.technicPermissionNotifyRadioButton.Text = "Notify";
            this.technicPermissionNotifyRadioButton.UseVisualStyleBackColor = true;
            // 
            // technicPermissionClosedRadioButton
            // 
            this.technicPermissionClosedRadioButton.AutoSize = true;
            this.technicPermissionClosedRadioButton.Location = new System.Drawing.Point(161, 20);
            this.technicPermissionClosedRadioButton.Name = "technicPermissionClosedRadioButton";
            this.technicPermissionClosedRadioButton.ReadOnly = true;
            this.technicPermissionClosedRadioButton.Size = new System.Drawing.Size(57, 17);
            this.technicPermissionClosedRadioButton.TabIndex = 0;
            this.technicPermissionClosedRadioButton.Text = "Closed";
            this.technicPermissionClosedRadioButton.UseVisualStyleBackColor = true;
            // 
            // technicLicenseLinkTextBox
            // 
            this.technicLicenseLinkTextBox.Location = new System.Drawing.Point(23, 201);
            this.technicLicenseLinkTextBox.Name = "technicLicenseLinkTextBox";
            this.technicLicenseLinkTextBox.Size = new System.Drawing.Size(205, 20);
            this.technicLicenseLinkTextBox.TabIndex = 8;
            this.technicLicenseLinkTextBox.TextChanged += new System.EventHandler(this.technicLicenseLinkTextBox_TextChanged);
            // 
            // technicModLinkTextBox
            // 
            this.technicModLinkTextBox.Location = new System.Drawing.Point(23, 154);
            this.technicModLinkTextBox.Name = "technicModLinkTextBox";
            this.technicModLinkTextBox.Size = new System.Drawing.Size(205, 20);
            this.technicModLinkTextBox.TabIndex = 7;
            this.technicModLinkTextBox.TextChanged += new System.EventHandler(this.technicModLinkTextBox_TextChanged);
            // 
            // technicPermissionLinkTextBox
            // 
            this.technicPermissionLinkTextBox.Location = new System.Drawing.Point(23, 112);
            this.technicPermissionLinkTextBox.Name = "technicPermissionLinkTextBox";
            this.technicPermissionLinkTextBox.Size = new System.Drawing.Size(205, 20);
            this.technicPermissionLinkTextBox.TabIndex = 6;
            this.technicPermissionLinkTextBox.TextChanged += new System.EventHandler(this.technicPermissionLinkTextBox_TextChanged);
            // 
            // technicPermissionFtbExclusiveRadioButton
            // 
            this.technicPermissionFtbExclusiveRadioButton.AutoSize = true;
            this.technicPermissionFtbExclusiveRadioButton.Location = new System.Drawing.Point(23, 43);
            this.technicPermissionFtbExclusiveRadioButton.Name = "technicPermissionFtbExclusiveRadioButton";
            this.technicPermissionFtbExclusiveRadioButton.ReadOnly = true;
            this.technicPermissionFtbExclusiveRadioButton.Size = new System.Drawing.Size(93, 17);
            this.technicPermissionFtbExclusiveRadioButton.TabIndex = 0;
            this.technicPermissionFtbExclusiveRadioButton.Text = "FTB Exclusive";
            this.technicPermissionFtbExclusiveRadioButton.UseVisualStyleBackColor = true;
            // 
            // technicPermissionOpenRadioButton
            // 
            this.technicPermissionOpenRadioButton.AutoSize = true;
            this.technicPermissionOpenRadioButton.Location = new System.Drawing.Point(23, 20);
            this.technicPermissionOpenRadioButton.Name = "technicPermissionOpenRadioButton";
            this.technicPermissionOpenRadioButton.ReadOnly = true;
            this.technicPermissionOpenRadioButton.Size = new System.Drawing.Size(51, 17);
            this.technicPermissionOpenRadioButton.TabIndex = 0;
            this.technicPermissionOpenRadioButton.Text = "Open";
            this.technicPermissionOpenRadioButton.UseVisualStyleBackColor = true;
            // 
            // selectModLabel
            // 
            this.selectModLabel.AutoSize = true;
            this.selectModLabel.Location = new System.Drawing.Point(9, 13);
            this.selectModLabel.Name = "selectModLabel";
            this.selectModLabel.Size = new System.Drawing.Size(61, 13);
            this.selectModLabel.TabIndex = 3;
            this.selectModLabel.Text = "Select Mod";
            // 
            // toolTip1
            // 
            this.toolTip1.ShowAlways = true;
            // 
            // showDoneCheckBox
            // 
            this.showDoneCheckBox.AutoSize = true;
            this.showDoneCheckBox.Location = new System.Drawing.Point(12, 417);
            this.showDoneCheckBox.Name = "showDoneCheckBox";
            this.showDoneCheckBox.Size = new System.Drawing.Size(82, 17);
            this.showDoneCheckBox.TabIndex = 15;
            this.showDoneCheckBox.Text = "Show Done";
            this.toolTip1.SetToolTip(this.showDoneCheckBox, "Show all items, even the once without any issues.\r\nCurrently only showing files w" +
        "ith issues.");
            this.showDoneCheckBox.UseVisualStyleBackColor = true;
            this.showDoneCheckBox.CheckedChanged += new System.EventHandler(this.showDoneCheckBox_CheckedChanged);
            // 
            // ftbPermissionsGroupBox
            // 
            this.ftbPermissionsGroupBox.Controls.Add(this.ftbLicenseLinkLabel);
            this.ftbPermissionsGroupBox.Controls.Add(this.ftbModLinkLabel);
            this.ftbPermissionsGroupBox.Controls.Add(this.ftbPermissionLinkLabel);
            this.ftbPermissionsGroupBox.Controls.Add(this.ftbPermissionUnknownRadioButton);
            this.ftbPermissionsGroupBox.Controls.Add(this.ftbPermissionRequestRadioButton);
            this.ftbPermissionsGroupBox.Controls.Add(this.ftbPermissionNotifyRadioButton);
            this.ftbPermissionsGroupBox.Controls.Add(this.ftbPermissionClosedRadioButton);
            this.ftbPermissionsGroupBox.Controls.Add(this.ftbLicenseLinkTextBox);
            this.ftbPermissionsGroupBox.Controls.Add(this.ftbModLinkTextBox);
            this.ftbPermissionsGroupBox.Controls.Add(this.ftbPermissionLinkTextBox);
            this.ftbPermissionsGroupBox.Controls.Add(this.ftbPermissionFtbExclusiveRadioButton);
            this.ftbPermissionsGroupBox.Controls.Add(this.ftbPermissionOpenRadioButton);
            this.ftbPermissionsGroupBox.Location = new System.Drawing.Point(440, 270);
            this.ftbPermissionsGroupBox.Name = "ftbPermissionsGroupBox";
            this.ftbPermissionsGroupBox.Size = new System.Drawing.Size(252, 245);
            this.ftbPermissionsGroupBox.TabIndex = 2;
            this.ftbPermissionsGroupBox.TabStop = false;
            this.ftbPermissionsGroupBox.Text = "FTB Permissions";
            // 
            // ftbLicenseLinkLabel
            // 
            this.ftbLicenseLinkLabel.AutoSize = true;
            this.ftbLicenseLinkLabel.Location = new System.Drawing.Point(20, 185);
            this.ftbLicenseLinkLabel.Name = "ftbLicenseLinkLabel";
            this.ftbLicenseLinkLabel.Size = new System.Drawing.Size(67, 13);
            this.ftbLicenseLinkLabel.TabIndex = 1;
            this.ftbLicenseLinkLabel.Text = "License Link";
            // 
            // ftbModLinkLabel
            // 
            this.ftbModLinkLabel.AutoSize = true;
            this.ftbModLinkLabel.Location = new System.Drawing.Point(20, 138);
            this.ftbModLinkLabel.Name = "ftbModLinkLabel";
            this.ftbModLinkLabel.Size = new System.Drawing.Size(51, 13);
            this.ftbModLinkLabel.TabIndex = 1;
            this.ftbModLinkLabel.Text = "Mod Link";
            // 
            // ftbPermissionLinkLabel
            // 
            this.ftbPermissionLinkLabel.AutoSize = true;
            this.ftbPermissionLinkLabel.Location = new System.Drawing.Point(20, 96);
            this.ftbPermissionLinkLabel.Name = "ftbPermissionLinkLabel";
            this.ftbPermissionLinkLabel.Size = new System.Drawing.Size(80, 13);
            this.ftbPermissionLinkLabel.TabIndex = 1;
            this.ftbPermissionLinkLabel.Text = "Permission Link";
            // 
            // ftbPermissionUnknownRadioButton
            // 
            this.ftbPermissionUnknownRadioButton.AutoSize = true;
            this.ftbPermissionUnknownRadioButton.Location = new System.Drawing.Point(161, 66);
            this.ftbPermissionUnknownRadioButton.Name = "ftbPermissionUnknownRadioButton";
            this.ftbPermissionUnknownRadioButton.ReadOnly = true;
            this.ftbPermissionUnknownRadioButton.Size = new System.Drawing.Size(71, 17);
            this.ftbPermissionUnknownRadioButton.TabIndex = 0;
            this.ftbPermissionUnknownRadioButton.Text = "Unknown";
            this.ftbPermissionUnknownRadioButton.UseVisualStyleBackColor = true;
            // 
            // ftbPermissionRequestRadioButton
            // 
            this.ftbPermissionRequestRadioButton.AutoSize = true;
            this.ftbPermissionRequestRadioButton.Location = new System.Drawing.Point(161, 43);
            this.ftbPermissionRequestRadioButton.Name = "ftbPermissionRequestRadioButton";
            this.ftbPermissionRequestRadioButton.ReadOnly = true;
            this.ftbPermissionRequestRadioButton.Size = new System.Drawing.Size(65, 17);
            this.ftbPermissionRequestRadioButton.TabIndex = 0;
            this.ftbPermissionRequestRadioButton.Text = "Request";
            this.ftbPermissionRequestRadioButton.UseVisualStyleBackColor = true;
            // 
            // ftbPermissionNotifyRadioButton
            // 
            this.ftbPermissionNotifyRadioButton.AutoSize = true;
            this.ftbPermissionNotifyRadioButton.Location = new System.Drawing.Point(23, 66);
            this.ftbPermissionNotifyRadioButton.Name = "ftbPermissionNotifyRadioButton";
            this.ftbPermissionNotifyRadioButton.ReadOnly = true;
            this.ftbPermissionNotifyRadioButton.Size = new System.Drawing.Size(52, 17);
            this.ftbPermissionNotifyRadioButton.TabIndex = 0;
            this.ftbPermissionNotifyRadioButton.Text = "Notify";
            this.ftbPermissionNotifyRadioButton.UseVisualStyleBackColor = true;
            // 
            // ftbPermissionClosedRadioButton
            // 
            this.ftbPermissionClosedRadioButton.AutoSize = true;
            this.ftbPermissionClosedRadioButton.Location = new System.Drawing.Point(161, 20);
            this.ftbPermissionClosedRadioButton.Name = "ftbPermissionClosedRadioButton";
            this.ftbPermissionClosedRadioButton.ReadOnly = true;
            this.ftbPermissionClosedRadioButton.Size = new System.Drawing.Size(57, 17);
            this.ftbPermissionClosedRadioButton.TabIndex = 0;
            this.ftbPermissionClosedRadioButton.Text = "Closed";
            this.ftbPermissionClosedRadioButton.UseVisualStyleBackColor = true;
            // 
            // ftbLicenseLinkTextBox
            // 
            this.ftbLicenseLinkTextBox.Location = new System.Drawing.Point(23, 201);
            this.ftbLicenseLinkTextBox.Name = "ftbLicenseLinkTextBox";
            this.ftbLicenseLinkTextBox.Size = new System.Drawing.Size(205, 20);
            this.ftbLicenseLinkTextBox.TabIndex = 11;
            this.ftbLicenseLinkTextBox.TextChanged += new System.EventHandler(this.technicLicenseLinkTextBox_TextChanged);
            // 
            // ftbModLinkTextBox
            // 
            this.ftbModLinkTextBox.Location = new System.Drawing.Point(23, 154);
            this.ftbModLinkTextBox.Name = "ftbModLinkTextBox";
            this.ftbModLinkTextBox.Size = new System.Drawing.Size(205, 20);
            this.ftbModLinkTextBox.TabIndex = 10;
            this.ftbModLinkTextBox.TextChanged += new System.EventHandler(this.technicModLinkTextBox_TextChanged);
            // 
            // ftbPermissionLinkTextBox
            // 
            this.ftbPermissionLinkTextBox.Location = new System.Drawing.Point(23, 112);
            this.ftbPermissionLinkTextBox.Name = "ftbPermissionLinkTextBox";
            this.ftbPermissionLinkTextBox.Size = new System.Drawing.Size(205, 20);
            this.ftbPermissionLinkTextBox.TabIndex = 9;
            this.ftbPermissionLinkTextBox.TextChanged += new System.EventHandler(this.technicPermissionLinkTextBox_TextChanged);
            // 
            // ftbPermissionFtbExclusiveRadioButton
            // 
            this.ftbPermissionFtbExclusiveRadioButton.AutoSize = true;
            this.ftbPermissionFtbExclusiveRadioButton.Location = new System.Drawing.Point(23, 43);
            this.ftbPermissionFtbExclusiveRadioButton.Name = "ftbPermissionFtbExclusiveRadioButton";
            this.ftbPermissionFtbExclusiveRadioButton.ReadOnly = true;
            this.ftbPermissionFtbExclusiveRadioButton.Size = new System.Drawing.Size(93, 17);
            this.ftbPermissionFtbExclusiveRadioButton.TabIndex = 0;
            this.ftbPermissionFtbExclusiveRadioButton.Text = "FTB Exclusive";
            this.ftbPermissionFtbExclusiveRadioButton.UseVisualStyleBackColor = true;
            // 
            // ftbPermissionOpenRadioButton
            // 
            this.ftbPermissionOpenRadioButton.AutoSize = true;
            this.ftbPermissionOpenRadioButton.Location = new System.Drawing.Point(23, 20);
            this.ftbPermissionOpenRadioButton.Name = "ftbPermissionOpenRadioButton";
            this.ftbPermissionOpenRadioButton.ReadOnly = true;
            this.ftbPermissionOpenRadioButton.Size = new System.Drawing.Size(51, 17);
            this.ftbPermissionOpenRadioButton.TabIndex = 0;
            this.ftbPermissionOpenRadioButton.Text = "Open";
            this.ftbPermissionOpenRadioButton.UseVisualStyleBackColor = true;
            // 
            // getPermissionsButton
            // 
            this.getPermissionsButton.Location = new System.Drawing.Point(100, 417);
            this.getPermissionsButton.Name = "getPermissionsButton";
            this.getPermissionsButton.Size = new System.Drawing.Size(75, 44);
            this.getPermissionsButton.TabIndex = 13;
            this.getPermissionsButton.Text = "Get Permissions";
            this.getPermissionsButton.UseVisualStyleBackColor = true;
            this.getPermissionsButton.Click += new System.EventHandler(this.getPermissionsButton_Click);
            // 
            // doneButton
            // 
            this.doneButton.Location = new System.Drawing.Point(100, 492);
            this.doneButton.Name = "doneButton";
            this.doneButton.Size = new System.Drawing.Size(75, 23);
            this.doneButton.TabIndex = 14;
            this.doneButton.Text = "Done";
            this.doneButton.UseVisualStyleBackColor = true;
            this.doneButton.Click += new System.EventHandler(this.doneButton_Clicked);
            // 
            // skipModCheckBox
            // 
            this.skipModCheckBox.AutoSize = true;
            this.skipModCheckBox.Location = new System.Drawing.Point(12, 496);
            this.skipModCheckBox.Name = "skipModCheckBox";
            this.skipModCheckBox.Size = new System.Drawing.Size(71, 17);
            this.skipModCheckBox.TabIndex = 12;
            this.skipModCheckBox.Text = "Skip Mod";
            this.skipModCheckBox.UseVisualStyleBackColor = true;
            this.skipModCheckBox.CheckedChanged += new System.EventHandler(this.skipModCheckBox_CheckedChanged);
            // 
            // ModInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(706, 527);
            this.Controls.Add(this.skipModCheckBox);
            this.Controls.Add(this.doneButton);
            this.Controls.Add(this.getPermissionsButton);
            this.Controls.Add(this.showDoneCheckBox);
            this.Controls.Add(this.selectModLabel);
            this.Controls.Add(this.ftbPermissionsGroupBox);
            this.Controls.Add(this.technicPermissionsGroupBox);
            this.Controls.Add(this.modDataGroupBox);
            this.Controls.Add(this.modListBox);
            this.Name = "ModInfo";
            this.ShowIcon = false;
            this.Text = "Mod Info";
            this.Closing += new System.ComponentModel.CancelEventHandler(this.ModInfo_Closing);
            this.Load += new System.EventHandler(this.ModInfo_Load);
            this.modDataGroupBox.ResumeLayout(false);
            this.modDataGroupBox.PerformLayout();
            this.technicPermissionsGroupBox.ResumeLayout(false);
            this.technicPermissionsGroupBox.PerformLayout();
            this.ftbPermissionsGroupBox.ResumeLayout(false);
            this.ftbPermissionsGroupBox.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ListBox modListBox;
        private GroupBox modDataGroupBox;
        private Label modVersionLabel;
        private Label modIdLabel;
        private Label modNameLabel;
        private TextBox modVersionTextBox;
        private TextBox modIdTextBox;
        private TextBox modNameTextBox;
        private GroupBox technicPermissionsGroupBox;
        private ReadOnlyRadioButton technicPermissionUnknownRadioButton;
        private ReadOnlyRadioButton technicPermissionRequestRadioButton;
        private ReadOnlyRadioButton technicPermissionNotifyRadioButton;
        private ReadOnlyRadioButton technicPermissionClosedRadioButton;
        private ReadOnlyRadioButton technicPermissionFtbExclusiveRadioButton;
        private ReadOnlyRadioButton technicPermissionOpenRadioButton;
        private Label authorLabel;
        private TextBox authorTextBox;
        private Label technicModLinkLabel;
        private Label technicPermisisonLinkLabel;
        private TextBox technicModLinkTextBox;
        private TextBox technicPermissionLinkTextBox;
        private Label technicLicenseLinkLabel;
        private TextBox technicLicenseLinkTextBox;
        private Label fileNameLabel;
        private TextBox fileNameTextBox;
        private Label selectModLabel;
        private ToolTip toolTip1;
        private CheckBox showDoneCheckBox;
        private GroupBox ftbPermissionsGroupBox;
        private Label ftbLicenseLinkLabel;
        private Label ftbModLinkLabel;
        private Label ftbPermissionLinkLabel;
        private ReadOnlyRadioButton ftbPermissionUnknownRadioButton;
        private ReadOnlyRadioButton ftbPermissionRequestRadioButton;
        private ReadOnlyRadioButton ftbPermissionNotifyRadioButton;
        private ReadOnlyRadioButton ftbPermissionClosedRadioButton;
        private TextBox ftbLicenseLinkTextBox;
        private TextBox ftbModLinkTextBox;
        private TextBox ftbPermissionLinkTextBox;
        private ReadOnlyRadioButton ftbPermissionFtbExclusiveRadioButton;
        private ReadOnlyRadioButton ftbPermissionOpenRadioButton;
        private Button getPermissionsButton;
        private Button doneButton;
        private CheckBox skipModCheckBox;
    }
}