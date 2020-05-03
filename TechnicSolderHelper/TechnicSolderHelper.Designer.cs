using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using Newtonsoft.Json;

namespace TechnicSolderHelper
{
    partial class SolderHelper
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SolderHelper));
            this.inputDirectoryLabel = new System.Windows.Forms.Label();
            this.outputDirectoryLabel = new System.Windows.Forms.Label();
            this.inputDirectoryBrowseButton = new System.Windows.Forms.Button();
            this.outputDirectoryBrowseButton = new System.Windows.Forms.Button();
            this.folderBrowser = new System.Windows.Forms.FolderBrowserDialog();
            this.goButton = new System.Windows.Forms.Button();
            this.resetDatabaseButton = new System.Windows.Forms.Button();
            this.clearOutputDirectoryCheckBox = new System.Windows.Forms.CheckBox();
            this.updateStoredFTBPermissionsButton = new System.Windows.Forms.Button();
            this.createFTBPackCheckBox = new System.Windows.Forms.CheckBox();
            this.createTechnicPackCheckBox = new System.Windows.Forms.CheckBox();
            this.outputDirectoryTextBox = new System.Windows.Forms.TextBox();
            this.inputDirectoryTextBox = new System.Windows.Forms.ComboBox();
            this.uploadToFTPServerCheckBox = new System.Windows.Forms.CheckBox();
            this.getForgeAndMcVersionsButton = new System.Windows.Forms.Button();
            this.modpackNameLabel = new System.Windows.Forms.Label();
            this.modpackNameInput = new System.Windows.Forms.ComboBox();
            this.modpackVersionTextBox = new System.Windows.Forms.TextBox();
            this.modpackVersionLabel = new System.Windows.Forms.Label();
            this.getLiteLoaderVersionsButton = new System.Windows.Forms.Button();
            this.additionalFoldersGroupBox = new System.Windows.Forms.GroupBox();
            this.additionalFoldersPanel = new System.Windows.Forms.Panel();
            this.configureFtpButton = new System.Windows.Forms.Button();
            this.forgeVersionLabel = new System.Windows.Forms.Label();
            this.mcVersionLabel = new System.Windows.Forms.Label();
            this.technicDistributionLevelGroupBox = new System.Windows.Forms.GroupBox();
            this.technicPublicPackRadioButton = new System.Windows.Forms.RadioButton();
            this.technicPrivatePackRadioButton = new System.Windows.Forms.RadioButton();
            this.ftbDistributionLevelGroupBox = new System.Windows.Forms.GroupBox();
            this.ftbPublicPackRadioButton = new System.Windows.Forms.RadioButton();
            this.ftbPrivatePackRadioButton = new System.Windows.Forms.RadioButton();
            this.packTypeGroupBox = new System.Windows.Forms.GroupBox();
            this.includeForgeZipCheckBox = new System.Windows.Forms.CheckBox();
            this.checkTechnicPermissionsCheckBox = new System.Windows.Forms.CheckBox();
            this.zipPackRadioButton = new System.Windows.Forms.RadioButton();
            this.solderPackRadioButton = new System.Windows.Forms.RadioButton();
            this.includeConfigZipCheckBox = new System.Windows.Forms.CheckBox();
            this.useSolderCheckBox = new System.Windows.Forms.CheckBox();
            this.configureSolderMySQLButton = new System.Windows.Forms.Button();
            this.useS3CheckBox = new System.Windows.Forms.CheckBox();
            this.configureS3Button = new System.Windows.Forms.Button();
            this.editModDataButton = new System.Windows.Forms.Button();
            this.mcVersionDropdown = new System.Windows.Forms.ComboBox();
            this.forgeVersionDropdown = new System.Windows.Forms.ComboBox();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.toolStripProgressBar = new System.Windows.Forms.ToolStripProgressBar();
            this.toolStripCurrentlyDoingTextLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.generatePermissionsButton = new System.Windows.Forms.Button();
            this.toolTips = new System.Windows.Forms.ToolTip(this.components);
            this.minimumMemoryTextBox = new System.Windows.Forms.TextBox();
            this.minimumJavaVersionDropdown = new System.Windows.Forms.ComboBox();
            this.forceSolderUpdateCheckBox = new System.Windows.Forms.CheckBox();
            this.configureSftpButton = new System.Windows.Forms.Button();
            this.uploadToSFTPCheckBox = new System.Windows.Forms.CheckBox();
            this.doDebugCheckBox = new System.Windows.Forms.CheckBox();
            this.minimumJavaVersionLabel = new System.Windows.Forms.Label();
            this.minimumMemoryLabel = new System.Windows.Forms.Label();
            this.additionalFoldersGroupBox.SuspendLayout();
            this.technicDistributionLevelGroupBox.SuspendLayout();
            this.ftbDistributionLevelGroupBox.SuspendLayout();
            this.packTypeGroupBox.SuspendLayout();
            this.statusStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // inputDirectoryLabel
            // 
            this.inputDirectoryLabel.AutoSize = true;
            this.inputDirectoryLabel.Location = new System.Drawing.Point(9, 9);
            this.inputDirectoryLabel.Name = "inputDirectoryLabel";
            this.inputDirectoryLabel.Size = new System.Drawing.Size(99, 13);
            this.inputDirectoryLabel.TabIndex = 0;
            this.inputDirectoryLabel.Text = "Directory with mods";
            // 
            // outputDirectoryLabel
            // 
            this.outputDirectoryLabel.AutoSize = true;
            this.outputDirectoryLabel.Location = new System.Drawing.Point(12, 48);
            this.outputDirectoryLabel.Name = "outputDirectoryLabel";
            this.outputDirectoryLabel.Size = new System.Drawing.Size(82, 13);
            this.outputDirectoryLabel.TabIndex = 0;
            this.outputDirectoryLabel.Text = "Output directory";
            // 
            // inputDirectoryBrowseButton
            // 
            this.inputDirectoryBrowseButton.Location = new System.Drawing.Point(451, 25);
            this.inputDirectoryBrowseButton.Name = "inputDirectoryBrowseButton";
            this.inputDirectoryBrowseButton.Size = new System.Drawing.Size(85, 23);
            this.inputDirectoryBrowseButton.TabIndex = 1;
            this.inputDirectoryBrowseButton.Text = "Browse...";
            this.toolTips.SetToolTip(this.inputDirectoryBrowseButton, "Browse for the location of the mods folder");
            this.inputDirectoryBrowseButton.UseVisualStyleBackColor = true;
            this.inputDirectoryBrowseButton.Click += new System.EventHandler(this.inputDirectoryBrowseButton_Click);
            // 
            // outputDirectoryBrowseButton
            // 
            this.outputDirectoryBrowseButton.Location = new System.Drawing.Point(449, 61);
            this.outputDirectoryBrowseButton.Name = "outputDirectoryBrowseButton";
            this.outputDirectoryBrowseButton.Size = new System.Drawing.Size(85, 23);
            this.outputDirectoryBrowseButton.TabIndex = 1;
            this.outputDirectoryBrowseButton.Text = "Browse...";
            this.toolTips.SetToolTip(this.outputDirectoryBrowseButton, "Browse for the output location");
            this.outputDirectoryBrowseButton.UseVisualStyleBackColor = true;
            this.outputDirectoryBrowseButton.Click += new System.EventHandler(this.outputDirectoryBrowseButton_Click);
            // 
            // folderBrowser
            // 
            this.folderBrowser.Description = "Select the directory which contains the modpack\'s mods";
            this.folderBrowser.ShowNewFolderButton = false;
            // 
            // goButton
            // 
            this.goButton.Location = new System.Drawing.Point(545, 25);
            this.goButton.Name = "goButton";
            this.goButton.Size = new System.Drawing.Size(84, 59);
            this.goButton.TabIndex = 2;
            this.goButton.Text = "GO";
            this.toolTips.SetToolTip(this.goButton, "Start packing mods");
            this.goButton.UseVisualStyleBackColor = true;
            this.goButton.Click += new System.EventHandler(this.goButton_Click);
            // 
            // resetDatabaseButton
            // 
            this.resetDatabaseButton.Location = new System.Drawing.Point(497, 429);
            this.resetDatabaseButton.Name = "resetDatabaseButton";
            this.resetDatabaseButton.Size = new System.Drawing.Size(132, 23);
            this.resetDatabaseButton.TabIndex = 3;
            this.resetDatabaseButton.Text = "Reset internal database";
            this.resetDatabaseButton.UseVisualStyleBackColor = true;
            this.resetDatabaseButton.Click += new System.EventHandler(this.resetDatabaseButton_Click);
            // 
            // clearOutputDirectoryCheckBox
            // 
            this.clearOutputDirectoryCheckBox.AutoSize = true;
            this.clearOutputDirectoryCheckBox.Checked = true;
            this.clearOutputDirectoryCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.clearOutputDirectoryCheckBox.Location = new System.Drawing.Point(470, 104);
            this.clearOutputDirectoryCheckBox.Name = "clearOutputDirectoryCheckBox";
            this.clearOutputDirectoryCheckBox.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.clearOutputDirectoryCheckBox.Size = new System.Drawing.Size(159, 17);
            this.clearOutputDirectoryCheckBox.TabIndex = 4;
            this.clearOutputDirectoryCheckBox.Text = "Clear output directory on run";
            this.toolTips.SetToolTip(this.clearOutputDirectoryCheckBox, "Have Modpack Helper clear the output directory when it runs, so you only have new" +
        " files there");
            this.clearOutputDirectoryCheckBox.UseVisualStyleBackColor = true;
            // 
            // updateStoredFTBPermissionsButton
            // 
            this.updateStoredFTBPermissionsButton.Location = new System.Drawing.Point(510, 376);
            this.updateStoredFTBPermissionsButton.Name = "updateStoredFTBPermissionsButton";
            this.updateStoredFTBPermissionsButton.Size = new System.Drawing.Size(119, 47);
            this.updateStoredFTBPermissionsButton.TabIndex = 6;
            this.updateStoredFTBPermissionsButton.Text = "Update stored FTB permissions";
            this.toolTips.SetToolTip(this.updateStoredFTBPermissionsButton, "Update the stored information about mod permissions. WARNING: This will take a lo" +
        "ng time, during which Modpack Helper will be unable to pack your mods.");
            this.updateStoredFTBPermissionsButton.UseVisualStyleBackColor = true;
            this.updateStoredFTBPermissionsButton.Click += new System.EventHandler(this.updateStoredFTBPermissionsButton_Click);
            // 
            // createFTBPackCheckBox
            // 
            this.createFTBPackCheckBox.AutoSize = true;
            this.createFTBPackCheckBox.Location = new System.Drawing.Point(11, 298);
            this.createFTBPackCheckBox.Name = "createFTBPackCheckBox";
            this.createFTBPackCheckBox.Size = new System.Drawing.Size(108, 17);
            this.createFTBPackCheckBox.TabIndex = 7;
            this.createFTBPackCheckBox.Text = "Create FTB Pack";
            this.toolTips.SetToolTip(this.createFTBPackCheckBox, "Check to create an FTB modpack");
            this.createFTBPackCheckBox.UseVisualStyleBackColor = true;
            this.createFTBPackCheckBox.CheckedChanged += new System.EventHandler(this.createFTBPackCheckBox_CheckedChanged);
            // 
            // createTechnicPackCheckBox
            // 
            this.createTechnicPackCheckBox.AutoSize = true;
            this.createTechnicPackCheckBox.Checked = true;
            this.createTechnicPackCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.createTechnicPackCheckBox.Location = new System.Drawing.Point(11, 104);
            this.createTechnicPackCheckBox.Name = "createTechnicPackCheckBox";
            this.createTechnicPackCheckBox.Size = new System.Drawing.Size(127, 17);
            this.createTechnicPackCheckBox.TabIndex = 9;
            this.createTechnicPackCheckBox.Text = "Create Technic Pack";
            this.toolTips.SetToolTip(this.createTechnicPackCheckBox, "Check to create a Technic modpack");
            this.createTechnicPackCheckBox.UseVisualStyleBackColor = true;
            this.createTechnicPackCheckBox.CheckedChanged += new System.EventHandler(this.createTechnicPackCheckBox_CheckedChanged);
            // 
            // outputDirectoryTextBox
            // 
            this.outputDirectoryTextBox.Location = new System.Drawing.Point(11, 64);
            this.outputDirectoryTextBox.MaxLength = 2000;
            this.outputDirectoryTextBox.Name = "outputDirectoryTextBox";
            this.outputDirectoryTextBox.Size = new System.Drawing.Size(432, 20);
            this.outputDirectoryTextBox.TabIndex = 0;
            this.toolTips.SetToolTip(this.outputDirectoryTextBox, "The location where you want Modpack Helper to put your packed mods");
            this.outputDirectoryTextBox.TextChanged += new System.EventHandler(this.outputDirectoryTextBox_TextChanged);
            // 
            // inputDirectoryTextBox
            // 
            this.inputDirectoryTextBox.Location = new System.Drawing.Point(12, 25);
            this.inputDirectoryTextBox.Name = "inputDirectoryTextBox";
            this.inputDirectoryTextBox.Size = new System.Drawing.Size(432, 21);
            this.inputDirectoryTextBox.TabIndex = 0;
            this.inputDirectoryTextBox.Text = "C:\\Users\\User\\AppData\\Roaming\\.minecraft\\mods";
            this.toolTips.SetToolTip(this.inputDirectoryTextBox, "The location of the \"mods\" folder on your computer");
            this.inputDirectoryTextBox.TextChanged += new System.EventHandler(this.inputDirectoryTextBox_TextChanged);
            // 
            // uploadToFTPServerCheckBox
            // 
            this.uploadToFTPServerCheckBox.AutoSize = true;
            this.uploadToFTPServerCheckBox.Location = new System.Drawing.Point(12, 400);
            this.uploadToFTPServerCheckBox.Name = "uploadToFTPServerCheckBox";
            this.uploadToFTPServerCheckBox.Size = new System.Drawing.Size(127, 17);
            this.uploadToFTPServerCheckBox.TabIndex = 12;
            this.uploadToFTPServerCheckBox.Text = "Upload to FTP server";
            this.toolTips.SetToolTip(this.uploadToFTPServerCheckBox, "Automatically upload your mods to a server? (Only works for Solder packs)");
            this.uploadToFTPServerCheckBox.UseVisualStyleBackColor = true;
            this.uploadToFTPServerCheckBox.CheckedChanged += new System.EventHandler(this.uploadToFTPServerCheckBox_CheckedChanged);
            // 
            // getForgeAndMcVersionsButton
            // 
            this.getForgeAndMcVersionsButton.Location = new System.Drawing.Point(470, 343);
            this.getForgeAndMcVersionsButton.Name = "getForgeAndMcVersionsButton";
            this.getForgeAndMcVersionsButton.Size = new System.Drawing.Size(159, 27);
            this.getForgeAndMcVersionsButton.TabIndex = 14;
            this.getForgeAndMcVersionsButton.Text = "Get Forge/Minecraft versions";
            this.toolTips.SetToolTip(this.getForgeAndMcVersionsButton, "Update the list of Forge and MC versions. WARNING: This will take a while, during" +
        " which Modpack Helper will be unable to pack your mods.");
            this.getForgeAndMcVersionsButton.UseVisualStyleBackColor = true;
            this.getForgeAndMcVersionsButton.Click += new System.EventHandler(this.getForgeAndMcVersionsButton_Click);
            // 
            // modpackNameLabel
            // 
            this.modpackNameLabel.AutoSize = true;
            this.modpackNameLabel.Location = new System.Drawing.Point(507, 258);
            this.modpackNameLabel.Name = "modpackNameLabel";
            this.modpackNameLabel.Size = new System.Drawing.Size(81, 13);
            this.modpackNameLabel.TabIndex = 0;
            this.modpackNameLabel.Text = "Modpack name";
            // 
            // modpackNameInput
            // 
            this.modpackNameInput.FormattingEnabled = true;
            this.modpackNameInput.Location = new System.Drawing.Point(507, 274);
            this.modpackNameInput.Name = "modpackNameInput";
            this.modpackNameInput.Size = new System.Drawing.Size(121, 21);
            this.modpackNameInput.TabIndex = 18;
            this.toolTips.SetToolTip(this.modpackNameInput, "The name of the modpack");
            // 
            // modpackVersionTextBox
            // 
            this.modpackVersionTextBox.Location = new System.Drawing.Point(507, 317);
            this.modpackVersionTextBox.Name = "modpackVersionTextBox";
            this.modpackVersionTextBox.Size = new System.Drawing.Size(122, 20);
            this.modpackVersionTextBox.TabIndex = 19;
            this.toolTips.SetToolTip(this.modpackVersionTextBox, "The version of the modpack");
            // 
            // modpackVersionLabel
            // 
            this.modpackVersionLabel.AutoSize = true;
            this.modpackVersionLabel.Location = new System.Drawing.Point(507, 302);
            this.modpackVersionLabel.Name = "modpackVersionLabel";
            this.modpackVersionLabel.Size = new System.Drawing.Size(89, 13);
            this.modpackVersionLabel.TabIndex = 20;
            this.modpackVersionLabel.Text = "Modpack version";
            // 
            // getLiteLoaderVersionsButton
            // 
            this.getLiteLoaderVersionsButton.Location = new System.Drawing.Point(429, 376);
            this.getLiteLoaderVersionsButton.Name = "getLiteLoaderVersionsButton";
            this.getLiteLoaderVersionsButton.Size = new System.Drawing.Size(75, 52);
            this.getLiteLoaderVersionsButton.TabIndex = 21;
            this.getLiteLoaderVersionsButton.Text = "Get Liteloader versions";
            this.toolTips.SetToolTip(this.getLiteLoaderVersionsButton, "Update the list of Liteloader versions (Should only take a few seconds)");
            this.getLiteLoaderVersionsButton.UseVisualStyleBackColor = true;
            this.getLiteLoaderVersionsButton.Click += new System.EventHandler(this.getLiteLoaderVersionsButton_Click);
            // 
            // additionalFoldersGroupBox
            // 
            this.additionalFoldersGroupBox.Controls.Add(this.additionalFoldersPanel);
            this.additionalFoldersGroupBox.Location = new System.Drawing.Point(636, 25);
            this.additionalFoldersGroupBox.Name = "additionalFoldersGroupBox";
            this.additionalFoldersGroupBox.Size = new System.Drawing.Size(153, 427);
            this.additionalFoldersGroupBox.TabIndex = 23;
            this.additionalFoldersGroupBox.TabStop = false;
            this.additionalFoldersGroupBox.Text = "Additional folders";
            this.toolTips.SetToolTip(this.additionalFoldersGroupBox, "Select any additional folders you want included in the modpack");
            // 
            // additionalFoldersPanel
            // 
            this.additionalFoldersPanel.AutoScroll = true;
            this.additionalFoldersPanel.Location = new System.Drawing.Point(2, 19);
            this.additionalFoldersPanel.Name = "additionalFoldersPanel";
            this.additionalFoldersPanel.Size = new System.Drawing.Size(149, 405);
            this.additionalFoldersPanel.TabIndex = 0;
            // 
            // configureFtpButton
            // 
            this.configureFtpButton.Location = new System.Drawing.Point(12, 424);
            this.configureFtpButton.Name = "configureFtpButton";
            this.configureFtpButton.Size = new System.Drawing.Size(107, 23);
            this.configureFtpButton.TabIndex = 24;
            this.configureFtpButton.Text = "Configure FTP";
            this.toolTips.SetToolTip(this.configureFtpButton, "Configure where your files are uploaded");
            this.configureFtpButton.UseVisualStyleBackColor = true;
            this.configureFtpButton.Click += new System.EventHandler(this.configureFtpButton_Click);
            // 
            // forgeVersionLabel
            // 
            this.forgeVersionLabel.AutoSize = true;
            this.forgeVersionLabel.Location = new System.Drawing.Point(172, 171);
            this.forgeVersionLabel.Name = "forgeVersionLabel";
            this.forgeVersionLabel.Size = new System.Drawing.Size(71, 13);
            this.forgeVersionLabel.TabIndex = 16;
            this.forgeVersionLabel.Text = "Forge version";
            // 
            // mcVersionLabel
            // 
            this.mcVersionLabel.AutoSize = true;
            this.mcVersionLabel.Location = new System.Drawing.Point(507, 215);
            this.mcVersionLabel.Name = "mcVersionLabel";
            this.mcVersionLabel.Size = new System.Drawing.Size(88, 13);
            this.mcVersionLabel.TabIndex = 15;
            this.mcVersionLabel.Text = "Minecraft version";
            // 
            // technicDistributionLevelGroupBox
            // 
            this.technicDistributionLevelGroupBox.Controls.Add(this.technicPublicPackRadioButton);
            this.technicDistributionLevelGroupBox.Controls.Add(this.technicPrivatePackRadioButton);
            this.technicDistributionLevelGroupBox.Location = new System.Drawing.Point(168, 207);
            this.technicDistributionLevelGroupBox.Name = "technicDistributionLevelGroupBox";
            this.technicDistributionLevelGroupBox.Size = new System.Drawing.Size(136, 73);
            this.technicDistributionLevelGroupBox.TabIndex = 7;
            this.technicDistributionLevelGroupBox.TabStop = false;
            this.technicDistributionLevelGroupBox.Text = "Technic Distribution Lvl";
            this.toolTips.SetToolTip(this.technicDistributionLevelGroupBox, "What level the pack will be distributed on");
            // 
            // technicPublicPackRadioButton
            // 
            this.technicPublicPackRadioButton.AutoSize = true;
            this.technicPublicPackRadioButton.Location = new System.Drawing.Point(7, 43);
            this.technicPublicPackRadioButton.Name = "technicPublicPackRadioButton";
            this.technicPublicPackRadioButton.Size = new System.Drawing.Size(82, 17);
            this.technicPublicPackRadioButton.TabIndex = 1;
            this.technicPublicPackRadioButton.TabStop = true;
            this.technicPublicPackRadioButton.Text = "Public Pack";
            this.toolTips.SetToolTip(this.technicPublicPackRadioButton, "This is a public modpack intended to be used by the general populace");
            this.technicPublicPackRadioButton.UseVisualStyleBackColor = true;
            // 
            // technicPrivatePackRadioButton
            // 
            this.technicPrivatePackRadioButton.AutoSize = true;
            this.technicPrivatePackRadioButton.Location = new System.Drawing.Point(7, 20);
            this.technicPrivatePackRadioButton.Name = "technicPrivatePackRadioButton";
            this.technicPrivatePackRadioButton.Size = new System.Drawing.Size(86, 17);
            this.technicPrivatePackRadioButton.TabIndex = 0;
            this.technicPrivatePackRadioButton.TabStop = true;
            this.technicPrivatePackRadioButton.Text = "Private Pack";
            this.toolTips.SetToolTip(this.technicPrivatePackRadioButton, resources.GetString("technicPrivatePackRadioButton.ToolTip"));
            this.technicPrivatePackRadioButton.UseVisualStyleBackColor = true;
            this.technicPrivatePackRadioButton.CheckedChanged += new System.EventHandler(this.technicPrivatePackRadioButton_CheckedChanged);
            // 
            // ftbDistributionLevelGroupBox
            // 
            this.ftbDistributionLevelGroupBox.Controls.Add(this.ftbPublicPackRadioButton);
            this.ftbDistributionLevelGroupBox.Controls.Add(this.ftbPrivatePackRadioButton);
            this.ftbDistributionLevelGroupBox.Location = new System.Drawing.Point(12, 323);
            this.ftbDistributionLevelGroupBox.Name = "ftbDistributionLevelGroupBox";
            this.ftbDistributionLevelGroupBox.Size = new System.Drawing.Size(146, 70);
            this.ftbDistributionLevelGroupBox.TabIndex = 11;
            this.ftbDistributionLevelGroupBox.TabStop = false;
            this.ftbDistributionLevelGroupBox.Text = "FTB Distribution Lvl";
            this.toolTips.SetToolTip(this.ftbDistributionLevelGroupBox, "What level the pack will be distributed on");
            // 
            // ftbPublicPackRadioButton
            // 
            this.ftbPublicPackRadioButton.AutoSize = true;
            this.ftbPublicPackRadioButton.Location = new System.Drawing.Point(18, 44);
            this.ftbPublicPackRadioButton.Name = "ftbPublicPackRadioButton";
            this.ftbPublicPackRadioButton.Size = new System.Drawing.Size(105, 17);
            this.ftbPublicPackRadioButton.TabIndex = 1;
            this.ftbPublicPackRadioButton.TabStop = true;
            this.ftbPublicPackRadioButton.Text = "Public FTB Pack";
            this.toolTips.SetToolTip(this.ftbPublicPackRadioButton, "This is a public modpack intended to be used by the general populace");
            this.ftbPublicPackRadioButton.UseVisualStyleBackColor = true;
            // 
            // ftbPrivatePackRadioButton
            // 
            this.ftbPrivatePackRadioButton.AutoSize = true;
            this.ftbPrivatePackRadioButton.Location = new System.Drawing.Point(18, 20);
            this.ftbPrivatePackRadioButton.Name = "ftbPrivatePackRadioButton";
            this.ftbPrivatePackRadioButton.Size = new System.Drawing.Size(109, 17);
            this.ftbPrivatePackRadioButton.TabIndex = 0;
            this.ftbPrivatePackRadioButton.TabStop = true;
            this.ftbPrivatePackRadioButton.Text = "Private FTB Pack";
            this.toolTips.SetToolTip(this.ftbPrivatePackRadioButton, resources.GetString("ftbPrivatePackRadioButton.ToolTip"));
            this.ftbPrivatePackRadioButton.UseVisualStyleBackColor = true;
            this.ftbPrivatePackRadioButton.CheckedChanged += new System.EventHandler(this.ftbPrivatePackRadioButton_CheckedChanged);
            // 
            // packTypeGroupBox
            // 
            this.packTypeGroupBox.Controls.Add(this.includeForgeZipCheckBox);
            this.packTypeGroupBox.Controls.Add(this.checkTechnicPermissionsCheckBox);
            this.packTypeGroupBox.Controls.Add(this.zipPackRadioButton);
            this.packTypeGroupBox.Controls.Add(this.solderPackRadioButton);
            this.packTypeGroupBox.Controls.Add(this.includeConfigZipCheckBox);
            this.packTypeGroupBox.Location = new System.Drawing.Point(11, 127);
            this.packTypeGroupBox.Name = "packTypeGroupBox";
            this.packTypeGroupBox.Size = new System.Drawing.Size(148, 153);
            this.packTypeGroupBox.TabIndex = 10;
            this.packTypeGroupBox.TabStop = false;
            this.packTypeGroupBox.Text = "Pack Type";
            // 
            // includeForgeZipCheckBox
            // 
            this.includeForgeZipCheckBox.AutoSize = true;
            this.includeForgeZipCheckBox.Location = new System.Drawing.Point(20, 65);
            this.includeForgeZipCheckBox.Name = "includeForgeZipCheckBox";
            this.includeForgeZipCheckBox.Size = new System.Drawing.Size(107, 17);
            this.includeForgeZipCheckBox.TabIndex = 13;
            this.includeForgeZipCheckBox.Text = "Include Forge zip";
            this.toolTips.SetToolTip(this.includeForgeZipCheckBox, "Automatically download Forge and include it in the pack");
            this.includeForgeZipCheckBox.UseVisualStyleBackColor = true;
            this.includeForgeZipCheckBox.CheckedChanged += new System.EventHandler(this.includeForgeZipCheckBox_CheckedChanged);
            // 
            // checkTechnicPermissionsCheckBox
            // 
            this.checkTechnicPermissionsCheckBox.AutoSize = true;
            this.checkTechnicPermissionsCheckBox.Location = new System.Drawing.Point(20, 111);
            this.checkTechnicPermissionsCheckBox.Name = "checkTechnicPermissionsCheckBox";
            this.checkTechnicPermissionsCheckBox.Size = new System.Drawing.Size(114, 17);
            this.checkTechnicPermissionsCheckBox.TabIndex = 6;
            this.checkTechnicPermissionsCheckBox.Text = "Check permissions";
            this.toolTips.SetToolTip(this.checkTechnicPermissionsCheckBox, "Should Modpack Helper check to see if you have permission to distribute the mods?" +
        "");
            this.checkTechnicPermissionsCheckBox.UseVisualStyleBackColor = true;
            this.checkTechnicPermissionsCheckBox.CheckedChanged += new System.EventHandler(this.checkTechnicPermissionsCheckBox_CheckedChanged);
            // 
            // zipPackRadioButton
            // 
            this.zipPackRadioButton.AutoSize = true;
            this.zipPackRadioButton.Location = new System.Drawing.Point(20, 42);
            this.zipPackRadioButton.Name = "zipPackRadioButton";
            this.zipPackRadioButton.Size = new System.Drawing.Size(68, 17);
            this.zipPackRadioButton.TabIndex = 0;
            this.zipPackRadioButton.Text = "Zip Pack";
            this.toolTips.SetToolTip(this.zipPackRadioButton, "Create a normal zip pack");
            this.zipPackRadioButton.UseVisualStyleBackColor = true;
            // 
            // solderPackRadioButton
            // 
            this.solderPackRadioButton.AutoSize = true;
            this.solderPackRadioButton.Checked = true;
            this.solderPackRadioButton.Location = new System.Drawing.Point(20, 19);
            this.solderPackRadioButton.Name = "solderPackRadioButton";
            this.solderPackRadioButton.Size = new System.Drawing.Size(83, 17);
            this.solderPackRadioButton.TabIndex = 0;
            this.solderPackRadioButton.TabStop = true;
            this.solderPackRadioButton.Text = "Solder Pack";
            this.toolTips.SetToolTip(this.solderPackRadioButton, "Create a pack for a Solder installation");
            this.solderPackRadioButton.UseVisualStyleBackColor = true;
            this.solderPackRadioButton.CheckedChanged += new System.EventHandler(this.solderPackRadioButton_CheckedChanged);
            // 
            // includeConfigZipCheckBox
            // 
            this.includeConfigZipCheckBox.AutoSize = true;
            this.includeConfigZipCheckBox.Checked = true;
            this.includeConfigZipCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.includeConfigZipCheckBox.Location = new System.Drawing.Point(20, 88);
            this.includeConfigZipCheckBox.Name = "includeConfigZipCheckBox";
            this.includeConfigZipCheckBox.Size = new System.Drawing.Size(105, 17);
            this.includeConfigZipCheckBox.TabIndex = 5;
            this.includeConfigZipCheckBox.Text = "Create config zip";
            this.toolTips.SetToolTip(this.includeConfigZipCheckBox, "Pack the modpack\'s config files with the modpack");
            this.includeConfigZipCheckBox.UseVisualStyleBackColor = true;
            this.includeConfigZipCheckBox.CheckedChanged += new System.EventHandler(this.includeConfigZipCheckBox_CheckedChanged);
            // 
            // useSolderCheckBox
            // 
            this.useSolderCheckBox.AutoSize = true;
            this.useSolderCheckBox.Location = new System.Drawing.Point(168, 298);
            this.useSolderCheckBox.Name = "useSolderCheckBox";
            this.useSolderCheckBox.Size = new System.Drawing.Size(130, 17);
            this.useSolderCheckBox.TabIndex = 26;
            this.useSolderCheckBox.Text = "Use Solder installation";
            this.toolTips.SetToolTip(this.useSolderCheckBox, "Automatically enter modpack info into Solder");
            this.useSolderCheckBox.UseVisualStyleBackColor = true;
            this.useSolderCheckBox.CheckedChanged += new System.EventHandler(this.useSolderCheckBox_CheckedChanged);
            // 
            // configureSolderMySQLButton
            // 
            this.configureSolderMySQLButton.Location = new System.Drawing.Point(168, 321);
            this.configureSolderMySQLButton.Name = "configureSolderMySQLButton";
            this.configureSolderMySQLButton.Size = new System.Drawing.Size(156, 23);
            this.configureSolderMySQLButton.TabIndex = 27;
            this.configureSolderMySQLButton.Text = "Configure Solder MySQL";
            this.toolTips.SetToolTip(this.configureSolderMySQLButton, "Configure the login credentials for the MySQL server");
            this.configureSolderMySQLButton.UseVisualStyleBackColor = true;
            this.configureSolderMySQLButton.Click += new System.EventHandler(this.configureSolder_Click);
            // 
            // useS3CheckBox
            // 
            this.useS3CheckBox.AutoSize = true;
            this.useS3CheckBox.Location = new System.Drawing.Point(151, 400);
            this.useS3CheckBox.Name = "useS3CheckBox";
            this.useS3CheckBox.Size = new System.Drawing.Size(102, 17);
            this.useS3CheckBox.TabIndex = 29;
            this.useS3CheckBox.Text = "Use Amazon S3";
            this.toolTips.SetToolTip(this.useS3CheckBox, "Automatically upload your mods to Amazon S3 (Only works for Solder packs)");
            this.useS3CheckBox.UseVisualStyleBackColor = true;
            this.useS3CheckBox.CheckedChanged += new System.EventHandler(this.useS3CheckBox_CheckedChanged);
            // 
            // configureS3Button
            // 
            this.configureS3Button.Location = new System.Drawing.Point(151, 424);
            this.configureS3Button.Name = "configureS3Button";
            this.configureS3Button.Size = new System.Drawing.Size(103, 23);
            this.configureS3Button.TabIndex = 30;
            this.configureS3Button.Text = "Configure Amazon S3";
            this.toolTips.SetToolTip(this.configureS3Button, "Configure Amazon S3 login credentials");
            this.configureS3Button.UseVisualStyleBackColor = true;
            this.configureS3Button.Click += new System.EventHandler(this.configureS3Button_Click);
            // 
            // editModDataButton
            // 
            this.editModDataButton.Location = new System.Drawing.Point(333, 388);
            this.editModDataButton.Name = "editModDataButton";
            this.editModDataButton.Size = new System.Drawing.Size(90, 23);
            this.editModDataButton.TabIndex = 31;
            this.editModDataButton.Text = "Edit mod data";
            this.toolTips.SetToolTip(this.editModDataButton, "Edit the stored data about mods. This will only edit local data.");
            this.editModDataButton.UseVisualStyleBackColor = true;
            this.editModDataButton.Click += new System.EventHandler(this.editModDataButton_Click);
            // 
            // mcVersionDropdown
            // 
            this.mcVersionDropdown.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.mcVersionDropdown.FormattingEnabled = true;
            this.mcVersionDropdown.Location = new System.Drawing.Point(507, 234);
            this.mcVersionDropdown.Name = "mcVersionDropdown";
            this.mcVersionDropdown.Size = new System.Drawing.Size(121, 21);
            this.mcVersionDropdown.TabIndex = 32;
            this.toolTips.SetToolTip(this.mcVersionDropdown, "The Minecraft version your modpack is. Available Forge version is dependant on th" +
        "is, as is the file naming. ");
            this.mcVersionDropdown.SelectedIndexChanged += new System.EventHandler(this.mcVersionDropdown_SelectedIndexChanged);
            // 
            // forgeVersionDropdown
            // 
            this.forgeVersionDropdown.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.forgeVersionDropdown.FormattingEnabled = true;
            this.forgeVersionDropdown.Location = new System.Drawing.Point(250, 168);
            this.forgeVersionDropdown.Name = "forgeVersionDropdown";
            this.forgeVersionDropdown.Size = new System.Drawing.Size(121, 21);
            this.forgeVersionDropdown.TabIndex = 33;
            this.toolTips.SetToolTip(this.forgeVersionDropdown, "The Forge version to include in the modpack");
            this.forgeVersionDropdown.SelectedIndexChanged += new System.EventHandler(this.forgeVersionDropdown_SelectedIndexChanged);
            // 
            // statusStrip
            // 
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripProgressBar,
            this.toolStripCurrentlyDoingTextLabel,
            this.toolStripStatusLabel});
            this.statusStrip.Location = new System.Drawing.Point(0, 462);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(801, 22);
            this.statusStrip.TabIndex = 35;
            this.statusStrip.Text = "statusStrip1";
            // 
            // toolStripProgressBar
            // 
            this.toolStripProgressBar.Name = "toolStripProgressBar";
            this.toolStripProgressBar.Size = new System.Drawing.Size(100, 16);
            // 
            // toolStripCurrentlyDoingTextLabel
            // 
            this.toolStripCurrentlyDoingTextLabel.Name = "toolStripCurrentlyDoingTextLabel";
            this.toolStripCurrentlyDoingTextLabel.Size = new System.Drawing.Size(94, 17);
            this.toolStripCurrentlyDoingTextLabel.Text = "Currently Doing:";
            // 
            // toolStripStatusLabel
            // 
            this.toolStripStatusLabel.Name = "toolStripStatusLabel";
            this.toolStripStatusLabel.Size = new System.Drawing.Size(57, 17);
            this.toolStripStatusLabel.Text = "Waiting...";
            this.toolStripStatusLabel.TextChanged += new System.EventHandler(this.toolStripStatusLabel_TextChanged);
            // 
            // generatePermissionsButton
            // 
            this.generatePermissionsButton.Location = new System.Drawing.Point(333, 414);
            this.generatePermissionsButton.Name = "generatePermissionsButton";
            this.generatePermissionsButton.Size = new System.Drawing.Size(90, 37);
            this.generatePermissionsButton.TabIndex = 34;
            this.generatePermissionsButton.Text = "Generate Permissions";
            this.toolTips.SetToolTip(this.generatePermissionsButton, "Generate a text file with all current permission info for the selected location.");
            this.generatePermissionsButton.UseVisualStyleBackColor = true;
            this.generatePermissionsButton.Click += new System.EventHandler(this.generatePermissionsButton_Click);
            // 
            // toolTips
            // 
            this.toolTips.AutomaticDelay = 100;
            this.toolTips.AutoPopDelay = 10000;
            this.toolTips.InitialDelay = 100;
            this.toolTips.ReshowDelay = 20;
            // 
            // minimumMemoryTextBox
            // 
            this.minimumMemoryTextBox.Location = new System.Drawing.Point(330, 323);
            this.minimumMemoryTextBox.Name = "minimumMemoryTextBox";
            this.minimumMemoryTextBox.Size = new System.Drawing.Size(124, 20);
            this.minimumMemoryTextBox.TabIndex = 36;
            this.toolTips.SetToolTip(this.minimumMemoryTextBox, "Specify a minimum memory requirement for the pack.");
            // 
            // minimumJavaVersionDropdown
            // 
            this.minimumJavaVersionDropdown.FormattingEnabled = true;
            this.minimumJavaVersionDropdown.Items.AddRange(new object[] {
            "Java 1.8",
            "Java 1.7",
            "Java 1.6",
            "No requirement"});
            this.minimumJavaVersionDropdown.Location = new System.Drawing.Point(168, 363);
            this.minimumJavaVersionDropdown.Name = "minimumJavaVersionDropdown";
            this.minimumJavaVersionDropdown.Size = new System.Drawing.Size(121, 21);
            this.minimumJavaVersionDropdown.TabIndex = 37;
            this.toolTips.SetToolTip(this.minimumJavaVersionDropdown, "The minimum required java version to play the pack. ");
            // 
            // forceSolderUpdateCheckBox
            // 
            this.forceSolderUpdateCheckBox.AutoSize = true;
            this.forceSolderUpdateCheckBox.Location = new System.Drawing.Point(295, 363);
            this.forceSolderUpdateCheckBox.Name = "forceSolderUpdateCheckBox";
            this.forceSolderUpdateCheckBox.Size = new System.Drawing.Size(122, 17);
            this.forceSolderUpdateCheckBox.TabIndex = 40;
            this.forceSolderUpdateCheckBox.Text = "Force Solder update";
            this.toolTips.SetToolTip(this.forceSolderUpdateCheckBox, "Will force Modpack Helper to update all data on Solder and create all mods");
            this.forceSolderUpdateCheckBox.UseVisualStyleBackColor = true;
            // 
            // configureSftpButton
            // 
            this.configureSftpButton.Location = new System.Drawing.Point(315, 253);
            this.configureSftpButton.Name = "configureSftpButton";
            this.configureSftpButton.Size = new System.Drawing.Size(107, 23);
            this.configureSftpButton.TabIndex = 43;
            this.configureSftpButton.Text = "Configure SFTP";
            this.toolTips.SetToolTip(this.configureSftpButton, "Configure where your files are uploaded");
            this.configureSftpButton.UseVisualStyleBackColor = true;
            this.configureSftpButton.Click += new System.EventHandler(this.configureSftpButton_Click);
            // 
            // uploadToSFTPCheckBox
            // 
            this.uploadToSFTPCheckBox.AutoSize = true;
            this.uploadToSFTPCheckBox.Location = new System.Drawing.Point(315, 228);
            this.uploadToSFTPCheckBox.Name = "uploadToSFTPCheckBox";
            this.uploadToSFTPCheckBox.Size = new System.Drawing.Size(102, 17);
            this.uploadToSFTPCheckBox.TabIndex = 42;
            this.uploadToSFTPCheckBox.Text = "Upload to SFTP";
            this.toolTips.SetToolTip(this.uploadToSFTPCheckBox, "Automatically upload your mods to a server? (Only works for Solder packs)");
            this.uploadToSFTPCheckBox.UseVisualStyleBackColor = true;
            this.uploadToSFTPCheckBox.CheckedChanged += new System.EventHandler(this.uploadToSFTPCheckBox_CheckedChanged);
            // 
            // doDebugCheckBox
            // 
            this.doDebugCheckBox.AutoSize = true;
            this.doDebugCheckBox.Location = new System.Drawing.Point(348, 104);
            this.doDebugCheckBox.Name = "doDebugCheckBox";
            this.doDebugCheckBox.Size = new System.Drawing.Size(121, 17);
            this.doDebugCheckBox.TabIndex = 41;
            this.doDebugCheckBox.Text = "Enable debug mode";
            this.toolTips.SetToolTip(this.doDebugCheckBox, "Outputs debug info to a file on your desktop called \"DebugFromModpackHelper.txt\"");
            this.doDebugCheckBox.UseVisualStyleBackColor = true;
            this.doDebugCheckBox.CheckedChanged += new System.EventHandler(this.doDebugCheckBox_CheckedChanged);
            // 
            // minimumJavaVersionLabel
            // 
            this.minimumJavaVersionLabel.AutoSize = true;
            this.minimumJavaVersionLabel.Location = new System.Drawing.Point(165, 350);
            this.minimumJavaVersionLabel.Name = "minimumJavaVersionLabel";
            this.minimumJavaVersionLabel.Size = new System.Drawing.Size(112, 13);
            this.minimumJavaVersionLabel.TabIndex = 38;
            this.minimumJavaVersionLabel.Text = "Minimum Java Version";
            // 
            // minimumMemoryLabel
            // 
            this.minimumMemoryLabel.AutoSize = true;
            this.minimumMemoryLabel.Location = new System.Drawing.Point(330, 307);
            this.minimumMemoryLabel.Name = "minimumMemoryLabel";
            this.minimumMemoryLabel.Size = new System.Drawing.Size(123, 13);
            this.minimumMemoryLabel.TabIndex = 39;
            this.minimumMemoryLabel.Text = "Minimum memory (in MB)";
            // 
            // SolderHelper
            // 
            this.AcceptButton = this.goButton;
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(801, 484);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.configureSftpButton);
            this.Controls.Add(this.uploadToSFTPCheckBox);
            this.Controls.Add(this.doDebugCheckBox);
            this.Controls.Add(this.forceSolderUpdateCheckBox);
            this.Controls.Add(this.minimumMemoryLabel);
            this.Controls.Add(this.minimumJavaVersionLabel);
            this.Controls.Add(this.minimumJavaVersionDropdown);
            this.Controls.Add(this.minimumMemoryTextBox);
            this.Controls.Add(this.generatePermissionsButton);
            this.Controls.Add(this.forgeVersionDropdown);
            this.Controls.Add(this.mcVersionDropdown);
            this.Controls.Add(this.editModDataButton);
            this.Controls.Add(this.configureS3Button);
            this.Controls.Add(this.useS3CheckBox);
            this.Controls.Add(this.configureSolderMySQLButton);
            this.Controls.Add(this.useSolderCheckBox);
            this.Controls.Add(this.configureFtpButton);
            this.Controls.Add(this.additionalFoldersGroupBox);
            this.Controls.Add(this.getLiteLoaderVersionsButton);
            this.Controls.Add(this.modpackVersionLabel);
            this.Controls.Add(this.modpackVersionTextBox);
            this.Controls.Add(this.modpackNameInput);
            this.Controls.Add(this.modpackNameLabel);
            this.Controls.Add(this.forgeVersionLabel);
            this.Controls.Add(this.mcVersionLabel);
            this.Controls.Add(this.getForgeAndMcVersionsButton);
            this.Controls.Add(this.uploadToFTPServerCheckBox);
            this.Controls.Add(this.technicDistributionLevelGroupBox);
            this.Controls.Add(this.ftbDistributionLevelGroupBox);
            this.Controls.Add(this.packTypeGroupBox);
            this.Controls.Add(this.createTechnicPackCheckBox);
            this.Controls.Add(this.createFTBPackCheckBox);
            this.Controls.Add(this.updateStoredFTBPermissionsButton);
            this.Controls.Add(this.clearOutputDirectoryCheckBox);
            this.Controls.Add(this.resetDatabaseButton);
            this.Controls.Add(this.goButton);
            this.Controls.Add(this.outputDirectoryBrowseButton);
            this.Controls.Add(this.inputDirectoryBrowseButton);
            this.Controls.Add(this.outputDirectoryLabel);
            this.Controls.Add(this.inputDirectoryLabel);
            this.Controls.Add(this.outputDirectoryTextBox);
            this.Controls.Add(this.inputDirectoryTextBox);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimumSize = new System.Drawing.Size(800, 522);
            this.Name = "SolderHelper";
            this.Text = "Modpack Helper";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.OnApplicationClosing);
            this.Load += new System.EventHandler(this.SolderHelper_Load);
            this.Resize += new System.EventHandler(this.form_resize);
            this.additionalFoldersGroupBox.ResumeLayout(false);
            this.technicDistributionLevelGroupBox.ResumeLayout(false);
            this.technicDistributionLevelGroupBox.PerformLayout();
            this.ftbDistributionLevelGroupBox.ResumeLayout(false);
            this.ftbDistributionLevelGroupBox.PerformLayout();
            this.packTypeGroupBox.ResumeLayout(false);
            this.packTypeGroupBox.PerformLayout();
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ComboBox inputDirectoryTextBox;
        private Label inputDirectoryLabel;
        private Label outputDirectoryLabel;
        private TextBox outputDirectoryTextBox;
        private Button inputDirectoryBrowseButton;
        private Button outputDirectoryBrowseButton;
        private FolderBrowserDialog folderBrowser;
        private Button goButton;
        private Button resetDatabaseButton;
        private CheckBox clearOutputDirectoryCheckBox;
        private CheckBox includeConfigZipCheckBox;
        private Button updateStoredFTBPermissionsButton;
        private GroupBox packTypeGroupBox;
        private RadioButton zipPackRadioButton;
        private RadioButton solderPackRadioButton;
        private GroupBox ftbDistributionLevelGroupBox;
        private GroupBox technicDistributionLevelGroupBox;
        private CheckBox uploadToFTPServerCheckBox;
        private CheckBox includeForgeZipCheckBox;
        private Button getForgeAndMcVersionsButton;
        private Label mcVersionLabel;
        private Label forgeVersionLabel;
        private Label modpackNameLabel;
        private ComboBox modpackNameInput;
        private TextBox modpackVersionTextBox;
        private Label modpackVersionLabel;
        private Button getLiteLoaderVersionsButton;
        private GroupBox additionalFoldersGroupBox;
        private Panel additionalFoldersPanel;
        private Button configureFtpButton;
        private CheckBox useSolderCheckBox;
        private Button configureSolderMySQLButton;
        private CheckBox useS3CheckBox;
        private Button configureS3Button;
        private Button editModDataButton;
        private ComboBox mcVersionDropdown;
        private ComboBox forgeVersionDropdown;
        public CheckBox createFTBPackCheckBox;
        public RadioButton ftbPublicPackRadioButton;
        public RadioButton ftbPrivatePackRadioButton;
        public CheckBox checkTechnicPermissionsCheckBox;
        public RadioButton technicPublicPackRadioButton;
        public RadioButton technicPrivatePackRadioButton;
        public CheckBox createTechnicPackCheckBox;
        private StatusStrip statusStrip;
        private ToolStripProgressBar toolStripProgressBar;
        private ToolStripStatusLabel toolStripCurrentlyDoingTextLabel;
        private ToolStripStatusLabel toolStripStatusLabel;
        private Button generatePermissionsButton;
        private ToolTip toolTips;
        private TextBox minimumMemoryTextBox;
        private ComboBox minimumJavaVersionDropdown;
        private Label minimumJavaVersionLabel;
        private Label minimumMemoryLabel;
        private CheckBox forceSolderUpdateCheckBox;
        private CheckBox doDebugCheckBox;
        private Button configureSftpButton;
        private CheckBox uploadToSFTPCheckBox;

        public SolderHelper()
        {
            Globalfunctions.PathSeperator = Globalfunctions.IsUnix() ? '/' : '\\';
            InitializeComponent();
            bool firstRun = true;
            try
            {
                firstRun = Convert.ToBoolean(_confighandler.GetConfig("FirstRun"));
            }
            catch (Exception)
            {
                firstRun = true;
            }
            if (firstRun)
            {
                MessageToUser m = new MessageToUser();
                Thread startingThread = new Thread(m.FirstTimeRun);
                startingThread.Start();
                getLiteLoaderVersionsButton_Click(null, null);
                _confighandler.SetConfig("InputDirectory", Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), ".minecraft", "mods"));
                _confighandler.SetConfig("OutputDirectory", Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory), "SolderHelper"));
                _confighandler.SetConfig("FirstRun", "false");

                UpdateForgeVersions();
                UpdateFtbPermissions();

            }
            #region Reload Interface
            if (File.Exists(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "SolderHelper", "inputDirectories.json")))
            {
                _inputDirectories = JsonConvert.DeserializeObject<List<String>>(File.ReadAllText(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "SolderHelper", "inputDirectories.json")));
                inputDirectoryTextBox.Items.Clear();
                inputDirectoryTextBox.Items.AddRange(_inputDirectories.ToArray());
            }
            try
            {
                inputDirectoryTextBox.Text = _confighandler.GetConfig("InputDirectory");
            }
            catch (Exception)
            {
                inputDirectoryTextBox.Text = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), ".minecraft", "mods");
            }

            try
            {
                outputDirectoryTextBox.Text = _confighandler.GetConfig("OutputDirectory");
            }
            catch (Exception)
            {
                outputDirectoryTextBox.Text = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory), "SolderHelper");
            }

            try
            {
                createTechnicPackCheckBox.Checked = Convert.ToBoolean(_confighandler.GetConfig("CreateTechnicSolderFiles"));
                packTypeGroupBox.Visible = createTechnicPackCheckBox.Checked;
            }
            catch (Exception)
            {
                createTechnicPackCheckBox.Checked = false;
            }

            try
            {
                createFTBPackCheckBox.Checked = Convert.ToBoolean(_confighandler.GetConfig("CreateFTBPack"));
            }
            catch (Exception)
            {
                createFTBPackCheckBox.Checked = false;
            }

            Boolean createSolderHelper = true, createPrivateFtbPack = true, technicPrivatePermissionsLevel = true, includeForgeVersion = false, createTechnicConfigZip = true, checkTechnicPermissions = false, uploadToFTPServer = false, uses3 = false, createFtbPack = false, missingInfoAction = true;
            try
            {
                useSolderCheckBox.Checked = Convert.ToBoolean(_confighandler.GetConfig("useSolder"));
            }
            catch (Exception)
            {
                useSolderCheckBox.Checked = false;
            }
            if (useSolderCheckBox.Checked)
            if (useSolderCheckBox.Checked)
            {
                configureSolderMySQLButton.Show();
                minimumJavaVersionLabel.Show();
                minimumMemoryLabel.Show();
                minimumMemoryTextBox.Show();
                minimumJavaVersionDropdown.Show();
            }
            else
            {
                configureSolderMySQLButton.Hide();
                minimumJavaVersionLabel.Hide();
                minimumMemoryLabel.Hide();
                minimumMemoryTextBox.Hide();
                minimumJavaVersionDropdown.Hide();
            }
            try
            {
                createSolderHelper = Convert.ToBoolean(_confighandler.GetConfig("CreateSolderPack"));
            }
            catch (Exception)
            {
            }
            try
            {
                createFtbPack = Convert.ToBoolean(_confighandler.GetConfig("CreateFTBPack"));
            }
            catch (Exception)
            {
                
            }
            try
            {
                createPrivateFtbPack = Convert.ToBoolean(_confighandler.GetConfig("CreatePrivateFTBPack"));
            }
            catch (Exception)
            {
            }
            try
            {
                technicPrivatePermissionsLevel = Convert.ToBoolean(_confighandler.GetConfig("TechnicPrivatePermissionsLevel"));
            }
            catch (Exception)
            {
            }
            try
            {
                includeForgeVersion = Convert.ToBoolean(_confighandler.GetConfig("IncludeForgeVersion"));
            }
            catch (Exception)
            {

            }
            try
            {
                createTechnicConfigZip = Convert.ToBoolean(_confighandler.GetConfig("CreateTechnicConfigZip"));
            }
            catch (Exception)
            {
            }
            try
            {
                checkTechnicPermissions = Convert.ToBoolean(_confighandler.GetConfig("CheckTechnicPermissions"));
            }
            catch (Exception)
            {
            }
            try
            {
                uploadToFTPServer = Convert.ToBoolean(_confighandler.GetConfig("UploadToFTPServer"));
            }
            catch
            {
            }
            try
            {
                uses3 = Convert.ToBoolean(_confighandler.GetConfig("useS3"));
            }
            catch
            {
            }
            
            if (uses3)
            {
                useS3CheckBox.Checked = true;
                configureS3Button.Show();
            }
            else
            {
                useS3CheckBox.Checked = false;
                configureS3Button.Hide();
            }
            if (createSolderHelper)
            {
                zipPackRadioButton.Checked = false;
                solderPackRadioButton.Checked = true;
            }
            else
            {
                solderPackRadioButton.Checked = false;
                zipPackRadioButton.Checked = true;
            }
            if (createFtbPack)
            {
                ftbDistributionLevelGroupBox.Show();
            }
            else
            {
                ftbDistributionLevelGroupBox.Hide();
            }
            if (createPrivateFtbPack)
            {
                ftbPublicPackRadioButton.Checked = false;
                ftbPrivatePackRadioButton.Checked = true;
            }
            else
            {
                ftbPrivatePackRadioButton.Checked = false;
                ftbPublicPackRadioButton.Checked = true;
            }
            if (technicPrivatePermissionsLevel)
            {
                technicPublicPackRadioButton.Checked = false;
                technicPrivatePackRadioButton.Checked = true;
            }
            else
            {
                technicPrivatePackRadioButton.Checked = false;
                technicPublicPackRadioButton.Checked = true;
            }
            uploadToFTPServerCheckBox.Checked = uploadToFTPServer;
            if (uploadToFTPServerCheckBox.Checked)
            {
                configureFtpButton.Show();
            }
            else
            {
                configureFtpButton.Hide();
            }
            includeForgeZipCheckBox.Checked = includeForgeVersion;
            if (createTechnicPackCheckBox.Checked && includeForgeVersion)
            {
                forgeVersionLabel.Show();
                forgeVersionDropdown.Show();
            }
            else
            {
                forgeVersionLabel.Hide();
                forgeVersionDropdown.Hide();
            }
            includeConfigZipCheckBox.Checked = createTechnicConfigZip;
            checkTechnicPermissionsCheckBox.Checked = checkTechnicPermissions;
            if (checkTechnicPermissions && createTechnicPackCheckBox.Checked)
            {
                technicDistributionLevelGroupBox.Visible = true;
            }
            else
            {
                technicDistributionLevelGroupBox.Visible = false;
            }

            if (solderPackRadioButton.Checked)
            {
                includeForgeZipCheckBox.Text = "Create Forge zip";
                includeConfigZipCheckBox.Text = "Create config zip";
            }
            else
            {
                includeForgeZipCheckBox.Text = "Include Forge in zip";
                includeConfigZipCheckBox.Text = "Include configs in zip";
            }
            List<String> minecraftversions = _forgeSqlHelper.GetMcVersions();
            foreach (String mcversion in minecraftversions)
                mcVersionDropdown.Items.Add(mcversion);

            #endregion

            _startInfo.WindowStyle = ProcessWindowStyle.Hidden;
            _startInfo.FileName = Globalfunctions.IsUnix() ? "unzip" : _sevenZipLocation;

            if (File.Exists(_modpacksJsonFile))
            {
                String modpackJson = "";
                using (StreamReader reader = new StreamReader(_modpacksJsonFile))
                {
                    modpackJson = reader.ReadToEnd();
                }
                _modpacks = JsonConvert.DeserializeObject<ModPacks>(modpackJson);
                foreach (String item in _modpacks.ModPack.Keys)
                {
                    modpackNameInput.Items.Add(item);
                }
            }
            if (Globalfunctions.IsUnix())
            {
                this.MinimumSize = new Size(923, 527);
            }
            else
            {
                this.MinimumSize = new Size(800, 503);
            }
        }
    }
}
