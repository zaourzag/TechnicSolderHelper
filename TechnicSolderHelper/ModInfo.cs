using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;
using TechnicSolderHelper.SQL;
using TechnicSolderHelper.SQL.WorkTogether;

namespace TechnicSolderHelper
{
    public partial class ModInfo : Form
    {
        private readonly SolderHelper _solderHelper;
        private readonly List<McMod> _mods;
        private readonly List<McMod> _nonFinishedMods;
        private readonly FtbPermissionsSqlHelper _ftbPermissionsSqlHelper = new FtbPermissionsSqlHelper();

        public ModInfo(SolderHelper solderHelper)
        {
            _solderHelper = solderHelper;
            InitializeComponent();
        }

        public ModInfo(List<McMod> modsList, SolderHelper solderHelper)
        {
            _nonFinishedMods = new List<McMod>();
            _solderHelper = solderHelper;
            var tmp = from mcmod1 in modsList
                      where mcmod1.Name != null
                      orderby mcmod1.Name
                      select mcmod1;
            _mods = new List<McMod>();
            _mods.AddRange(tmp.ToList());
            tmp = from mcmod1 in modsList
                  where mcmod1.Name == null
                  orderby mcmod1.Filename
                  select mcmod1;
            _mods.AddRange(tmp.ToList());
            InitializeComponent();
            foreach (McMod mcmod in _mods)
            {
                if (string.IsNullOrWhiteSpace(mcmod.McVersion))
                    mcmod.McVersion = solderHelper._currentMcVersion;
                mcmod.IsDone = IsModDone(mcmod);
                if (!mcmod.IsDone)
                {
                    ModListSqlHelper modListSqlHelper = new ModListSqlHelper();
                    McMod m = modListSqlHelper.GetModInfo(SqlHelper.CalculateMd5(mcmod.Path));
                    if (m == null)
                    {
                        if (mcmod.Authors == null || mcmod.AuthorList == null)
                        {
                            string a = _solderHelper.GetAuthors(mcmod, true);
                            if (!string.IsNullOrWhiteSpace(a))
                            {
                                List<string> s = a.Replace(" ", "").Split(',').ToList();
                                mcmod.Authors = s;
                            }
                        }
                        if (!IsValidModInfoString(mcmod.McVersion))
                        {
                            mcmod.McVersion = _solderHelper._currentMcVersion;
                        }
                        if (IsModDone(mcmod))
                        {
                            mcmod.IsDone = true;
                        }
                        else
                        {
                            DataSuggest ds = new DataSuggest();
                            m = ds.GetMcmod(SqlHelper.CalculateMd5(mcmod.Path));
                            if (m != null)
                            {
                                if (!IsValidModInfoString(mcmod.Name))
                                {
                                    mcmod.Name = m.Name;
                                }
                                if (!IsValidModInfoString(mcmod.modId))
                                {
                                    mcmod.modId = m.modId;
                                }
                                if (!IsValidModInfoString(mcmod.Version))
                                {
                                    mcmod.Version = m.Version;
                                }
                                mcmod.FromSuggestion = true;
                            }
                            if (IsModDone(mcmod))
                            {
                                mcmod.IsDone = true;
                            }
                            else
                            {
                                _nonFinishedMods.Add(mcmod);
                                modListBox.Items.Add(string.IsNullOrWhiteSpace(mcmod.Name) ? mcmod.Filename : mcmod.Name);
                            }
                        }
                    }

                    if (!mcmod.IsDone)
                    {

                        if (m != null)
                        {
                            if (!IsValidModInfoString(mcmod.Name) &&
                                !string.IsNullOrWhiteSpace(m.Name))
                            {
                                mcmod.Name = m.Name;
                            }
                            if (!IsValidModInfoString(mcmod.modId) && !string.IsNullOrWhiteSpace(m.modId))
                            {
                                mcmod.modId = m.modId;
                            }
                            if (!IsValidModInfoString(mcmod.Version) && !string.IsNullOrWhiteSpace(m.Version))
                            {
                                mcmod.Version = m.Version;
                            }
                        }
                        if (!IsValidModInfoString(mcmod.McVersion))
                        {
                            mcmod.McVersion = _solderHelper._currentMcVersion;
                        }
                        if (mcmod.Authors == null || mcmod.AuthorList == null)
                        {
                            string a = _solderHelper.GetAuthors(mcmod, true);
                            if (a != null)
                            {
                                List<string> s = a.Replace(" ", "").Split(',').ToList();
                                mcmod.Authors = s;
                            }
                        }
                        if (IsModDone(mcmod))
                        {
                            mcmod.IsDone = true;
                        }
                        else
                        {
                            DataSuggest ds = new DataSuggest();
                            m = ds.GetMcmod(SqlHelper.CalculateMd5(mcmod.Path));
                            if (m != null)
                            {
                                if (!IsValidModInfoString(mcmod.Name))
                                {
                                    mcmod.Name = m.Name;
                                }
                                if (!IsValidModInfoString(mcmod.modId))
                                {
                                    mcmod.modId = m.modId;
                                }
                                if (!IsValidModInfoString(mcmod.Version))
                                {
                                    mcmod.Version = m.Version;
                                }
                                mcmod.FromSuggestion = true;
                            }
                            if (IsModDone(mcmod))
                            {
                                mcmod.IsDone = true;
                            }
                            else
                            {
                                if (!_nonFinishedMods.Contains(mcmod))
                                {
                                    _nonFinishedMods.Add(mcmod);
                                    modListBox.Items.Add(string.IsNullOrWhiteSpace(mcmod.Name) ? mcmod.Filename : mcmod.Name);
                                }
                            }
                        }
                    }
                }
            }
            if (modListBox.Items.Count <= 0)
            {
            }
            else
            {
                modListBox.SelectedIndex = 0;
            }
        }

        private bool IsValidModInfoString(string s)
        {
            return !(string.IsNullOrWhiteSpace(s) || s.Contains("@") || s.Contains("${") || s.ToLower().Contains("example"));
        }

        private static bool IsFullyInformed(McMod mod)
        {
            if (string.IsNullOrWhiteSpace(mod.Name) || string.IsNullOrWhiteSpace(mod.Version) ||
                string.IsNullOrWhiteSpace(mod.McVersion) || string.IsNullOrWhiteSpace(mod.modId) || (mod.AuthorList == null && mod.Authors == null))
            {
                return false;
            }

            //Debug.WriteLine(mod.Version);
            if (mod.Name.Contains("${") || mod.Version.Contains("${") || mod.McVersion.Contains("${") || mod.modId.Contains("${") || mod.Version.ToLower().Contains("@version@") || mod.modId.ToLower().Contains("example") || mod.Version.ToLower().Contains("example") || mod.Name.ToLower().Contains("example"))
            {
                return false;
            }

            return true;
        }

        private bool IsModDone(McMod mod)
        {
            if (!IsFullyInformed(mod)) return false;
            OwnPermissionsSqlHelper ownPermissionsSqlHelper = new OwnPermissionsSqlHelper();
            if (ownPermissionsSqlHelper.DoUserHavePermission(mod.modId).HasPermission)
            {
                return true;
            }

            if (_solderHelper.createTechnicPackCheckBox.Checked && _solderHelper.checkTechnicPermissionsCheckBox.Checked)
            {
                if (_ftbPermissionsSqlHelper.FindPermissionPolicy(mod.modId,
                    _solderHelper.technicPublicPackRadioButton.Checked) != PermissionPolicy.Open)
                {
                    return false;
                }
            }

            if (!_solderHelper.createFTBPackCheckBox.Checked) return true;
            PermissionPolicy permissionPolicy = _ftbPermissionsSqlHelper.FindPermissionPolicy(mod.modId,
                _solderHelper.ftbPublicPackRadioButton.Checked);
            return permissionPolicy == PermissionPolicy.Open || permissionPolicy == PermissionPolicy.FTB;
        }

        private void modListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            int index = modListBox.SelectedIndex;
            if (index < 0) return;
            McMod m = showDoneCheckBox.Checked ? _mods[index] : _nonFinishedMods[index];
            skipModCheckBox.Checked = m.IsSkipping;
            fileNameTextBox.Text = m.Filename;
            authorTextBox.Text = m.Authors != null ? _solderHelper.GetAuthors(m) : string.Empty;
            modNameTextBox.Text = m.Name ?? string.Empty;

            modIdTextBox.Text = m.modId ?? string.Empty;
            //_updatingModID = String.IsNullOrWhiteSpace(textBoxModID.Text);
            //textBoxModID.ReadOnly = !String.IsNullOrWhiteSpace(textBoxModID.Text);

            modVersionTextBox.Text = m.Version ?? string.Empty;

            ShowPermissions();
        }

        private void modNameTextBox_TextChanged(object sender, EventArgs e)
        {
            int index = modListBox.SelectedIndex;
            var mod = showDoneCheckBox.Checked ? _mods[index] : _nonFinishedMods[index];
            mod.FromUserInput = true;
            if (string.IsNullOrWhiteSpace(modNameTextBox.Text))
            {
                mod.Name = string.Empty;
                modListBox.Items[index] = mod.Filename;
            }
            else
            {
                mod.Name = modNameTextBox.Text;
                modListBox.Items[index] = mod.Name;
            }
        }

        private void showDoneCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (showDoneCheckBox.Checked)
            {
                modListBox.Items.Clear();
                foreach (McMod mcmod in _mods)
                    modListBox.Items.Add(mcmod.Name ?? mcmod.Filename);
                toolTip1.SetToolTip(showDoneCheckBox,
                    "Only show items that are missing information or have other issues." + Environment.NewLine + "Currently showing all files.");
            }
            else
            {
                modListBox.Items.Clear();
                foreach (McMod mod in _nonFinishedMods)
                    modListBox.Items.Add(mod.Name ?? mod.Filename);
                toolTip1.SetToolTip(showDoneCheckBox, "Show all items, even the once without any issues." + Environment.NewLine + "Currently only showing files with issues.");
            }
        }

        private void ModInfo_Load(object sender, EventArgs e)
        {
            ftbPermissionsGroupBox.Visible = _solderHelper.createFTBPackCheckBox.Checked;
            technicPermissionsGroupBox.Visible = _solderHelper.checkTechnicPermissionsCheckBox.Checked;

            if (!technicPermissionsGroupBox.Visible)
            {
                ftbPermissionsGroupBox.Location = technicPermissionsGroupBox.Location;
                Width -= technicPermissionsGroupBox.Width;
            }
            else
            {
                if (!ftbPermissionsGroupBox.Visible)
                {
                    Width -= ftbPermissionsGroupBox.Width;
                }
            }
        }

        private void ShowPermissions()
        {
            technicLicenseLinkTextBox.Text = string.Empty;
            technicModLinkTextBox.Text = string.Empty;
            technicPermissionLinkTextBox.Text = string.Empty;
            ftbLicenseLinkTextBox.Text = string.Empty;
            ftbModLinkTextBox.Text = string.Empty;
            ftbPermissionLinkTextBox.Text = string.Empty;
            if (technicPermissionsGroupBox.Visible)
            {
                PermissionPolicy technicPermissionLevel =
                    _ftbPermissionsSqlHelper.FindPermissionPolicy(modIdTextBox.Text,
                        _solderHelper.technicPublicPackRadioButton.Checked);
                Debug.WriteLine(technicPermissionLevel);
                switch (technicPermissionLevel)
                {
                    case PermissionPolicy.Open:
                        technicPermissionOpenRadioButton.Checked = true;
                        break;
                    case PermissionPolicy.Notify:
                        technicPermissionNotifyRadioButton.Checked = true;
                        break;
                    case PermissionPolicy.FTB:
                        technicPermissionFtbExclusiveRadioButton.Checked = true;
                        break;
                    case PermissionPolicy.Request:
                        technicPermissionRequestRadioButton.Checked = true;
                        break;
                    case PermissionPolicy.Closed:
                        technicPermissionClosedRadioButton.Checked = true;
                        break;
                    case PermissionPolicy.Unknown:
                        technicPermissionUnknownRadioButton.Checked = true;
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
            if (ftbPermissionsGroupBox.Visible)
            {
                PermissionPolicy ftbPermissionLevel = _ftbPermissionsSqlHelper.FindPermissionPolicy(modIdTextBox.Text,
                    _solderHelper.ftbPublicPackRadioButton.Checked);
                Debug.WriteLine(ftbPermissionLevel);
                switch (ftbPermissionLevel)
                {
                    case PermissionPolicy.Open:
                        ftbPermissionOpenRadioButton.Checked = true;
                        break;
                    case PermissionPolicy.Notify:
                        ftbPermissionNotifyRadioButton.Checked = true;
                        break;
                    case PermissionPolicy.FTB:
                        ftbPermissionFtbExclusiveRadioButton.Checked = true;
                        break;
                    case PermissionPolicy.Request:
                        ftbPermissionRequestRadioButton.Checked = true;
                        break;
                    case PermissionPolicy.Closed:
                        ftbPermissionClosedRadioButton.Checked = true;
                        break;
                    case PermissionPolicy.Unknown:
                        ftbPermissionUnknownRadioButton.Checked = true;
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
            Permission modLinkPerm = _ftbPermissionsSqlHelper.GetPermissionFromModId(modIdTextBox.Text);
            if (modLinkPerm == null)
            {
                if (technicPermissionsGroupBox.Visible)
                {
                    technicModLinkTextBox.Text = string.Empty;
                }
                if (ftbPermissionsGroupBox.Visible)
                {
                    ftbModLinkTextBox.Text = string.Empty;
                }
            }
            else
            {
                if (technicPermissionsGroupBox.Visible)
                {
                    technicModLinkTextBox.Text = modLinkPerm.modLink;
                }
                if (ftbPermissionsGroupBox.Visible)
                {
                    ftbModLinkTextBox.Text = modLinkPerm.modLink;
                }
            }
            Permission licenseLinkPerm = _ftbPermissionsSqlHelper.GetPermissionFromModId(modIdTextBox.Text);
            if (licenseLinkPerm == null)
            {
                if (technicPermissionsGroupBox.Visible)
                {
                    technicLicenseLinkTextBox.Text = string.Empty;
                }
                if (ftbPermissionsGroupBox.Visible)
                {
                    ftbLicenseLinkTextBox.Text = string.Empty;
                }
            }
            else
            {
                if (technicPermissionsGroupBox.Visible)
                {
                    technicLicenseLinkTextBox.Text = licenseLinkPerm.licenseLink;
                }
                if (ftbPermissionsGroupBox.Visible)
                {
                    ftbLicenseLinkTextBox.Text = licenseLinkPerm.licenseLink;
                }
            }
            OwnPermissionsSqlHelper ownPermissionsSqlHelper = new OwnPermissionsSqlHelper();
            OwnPermissions permissions = ownPermissionsSqlHelper.DoUserHavePermission(modIdTextBox.Text);
            if (!permissions.HasPermission)
            {
                technicPermissionLinkTextBox.Text = string.Empty;
                ftbPermissionLinkTextBox.Text = string.Empty;
            }
            else
            {
                if (!string.IsNullOrWhiteSpace(permissions.PermissionLink))
                {
                    technicPermissionLinkTextBox.Text = permissions.PermissionLink;
                    ftbPermissionLinkTextBox.Text = permissions.PermissionLink;
                }
                if (!string.IsNullOrWhiteSpace(permissions.ModLink))
                {
                    technicModLinkTextBox.Text = permissions.ModLink;
                    ftbModLinkTextBox.Text = permissions.ModLink;
                }
                if (!string.IsNullOrWhiteSpace(permissions.LicenseLink))
                {
                    technicLicenseLinkTextBox.Text = permissions.LicenseLink;
                    ftbLicenseLinkTextBox.Text = permissions.LicenseLink;
                }
            }
        }

        private void getPermissionsButton_Click(object sender, EventArgs e)
        {
            ShowPermissions();
        }

        private void modVersionTextBox_TextChanged(object sender, EventArgs e)
        {
            int index = modListBox.SelectedIndex;
            McMod mod = showDoneCheckBox.Checked ? _mods[index] : _nonFinishedMods[index];
            mod.Version = !string.IsNullOrWhiteSpace(modVersionTextBox.Text) ? modVersionTextBox.Text : string.Empty;
            mod.FromUserInput = true;
        }

        private void authorTextBox_TextChanged(object sender, EventArgs e)
        {
            int index = modListBox.SelectedIndex;
            McMod mod = showDoneCheckBox.Checked ? _mods[index] : _nonFinishedMods[index];
            if (string.IsNullOrWhiteSpace(authorTextBox.Text))
            {
                mod.Authors = null;
            }
            else
            {
                string a = authorTextBox.Text;
                List<string> s = a.Replace(" ", "").Split(',').ToList();
                mod.Authors = s;
                if (!string.IsNullOrWhiteSpace(modIdTextBox.Text))
                {
                    OwnPermissionsSqlHelper ownPermissionsSqlHelper = new OwnPermissionsSqlHelper();
                    ownPermissionsSqlHelper.AddAuthor(modIdTextBox.Text, a);
                }
            }
            mod.FromUserInput = true;
        }

        private void ModInfo_Closing(object sender, CancelEventArgs e)
        {
            Console.WriteLine("Closing modinfo");
        }

        private void modIdTextBox_TextChanged(object sender, EventArgs e)
        {
            int index = modListBox.SelectedIndex;
            McMod mod = showDoneCheckBox.Checked ? _mods[index] : _nonFinishedMods[index];
            mod.modId = !string.IsNullOrWhiteSpace(modIdTextBox.Text) ? modIdTextBox.Text : string.Empty;
            mod.FromUserInput = true;

        }

        private void doneButton_Clicked(object sender, EventArgs e)
        {
            foreach (McMod mcmod in _mods)
            {
                if (mcmod.IsSkipping)
                    continue;
                if (string.IsNullOrWhiteSpace(mcmod.Name))
                {
                    MessageBox.Show("Please check all mods and make sure the info is filled in." +
                                    Environment.NewLine + "Issue with mod: " + mcmod.Filename);
                    return;
                }
                if (string.IsNullOrWhiteSpace(mcmod.modId))
                {
                    //TODO: get this by looking at mcmod.info
                    mcmod.modId = mcmod.Name.Replace(" ", "").ToLower();
                }
                if (!IsModDone(mcmod))
                {
                    MessageBox.Show("Please check all mods and make sure the info is filled in." +
                                    Environment.NewLine + "Issue with mod: " + mcmod.Filename);
                    return;
                }
                mcmod.IsDone = true;
            }
            foreach (McMod mcmod in _mods)
            {
                if (mcmod.FromUserInput && !mcmod.FromSuggestion)
                {
                    Debug.WriteLine(mcmod.modId);
                    DataSuggest ds = new DataSuggest();
                    string a = _solderHelper.GetAuthors(mcmod, true);
                    ds.Suggest(mcmod.Filename, mcmod.McVersion, mcmod.Version,
                        SqlHelper.CalculateMd5(mcmod.Path), mcmod.modId, mcmod.Name, a);
                }
                if (_solderHelper.createFTBPackCheckBox.Checked)
                {
                    _solderHelper.CreateFtbPackZip(mcmod, mcmod.Path);
                }
                if (_solderHelper.createTechnicPackCheckBox.Checked)
                {
                    _solderHelper.CreateTechnicModZip(mcmod, mcmod.Path);
                }
            }
        }

        private void technicPermissionLinkTextBox_TextChanged(object sender, EventArgs e)
        {
            Debug.WriteLine(e);
            var box = sender as TextBox;
            if (box != null)
            {
                if (!string.IsNullOrWhiteSpace(box.Text))
                {
                    if (Uri.IsWellFormedUriString(box.Text, UriKind.Absolute))
                    {
                        string modid = modIdTextBox.Text;
                        string modname = modNameTextBox.Text;
                        OwnPermissionsSqlHelper ownPermissionsSqlHelper = new OwnPermissionsSqlHelper();
                        ownPermissionsSqlHelper.AddOwnModPerm(modname, modid, box.Text);

                    }
                }
            }
        }

        private void technicModLinkTextBox_TextChanged(object sender, EventArgs e)
        {
            Debug.WriteLine(e);
            var box = sender as TextBox;
            if (box != null)
            {
                if (!string.IsNullOrWhiteSpace(box.Text))
                {
                    if (Uri.IsWellFormedUriString(box.Text, UriKind.Absolute))
                    {
                        string modid = modIdTextBox.Text;
                        OwnPermissionsSqlHelper ownPermissionsSqlHelper = new OwnPermissionsSqlHelper();
                        string modname = modNameTextBox.Text;
                        ownPermissionsSqlHelper.AddOwnModLink(modname, modid, box.Text);
                    }
                }
            }
        }

        private void technicLicenseLinkTextBox_TextChanged(object sender, EventArgs e)
        {
            Debug.WriteLine(e);
            var box = sender as TextBox;
            if (box != null)
            {
                if (!string.IsNullOrWhiteSpace(box.Text))
                {
                    if (Uri.IsWellFormedUriString(box.Text, UriKind.Absolute))
                    {
                        string modid = modIdTextBox.Text;
                        string modname = modNameTextBox.Text;
                        OwnPermissionsSqlHelper ownPermissionsSqlHelper = new OwnPermissionsSqlHelper();
                        ownPermissionsSqlHelper.AddOwnModLicense(modname, modid, box.Text);

                    }
                }
            }
        }

        private void skipModCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            int index = modListBox.SelectedIndex;
            McMod mod = showDoneCheckBox.Checked ? _mods[index] : _nonFinishedMods[index];
            mod.IsSkipping = skipModCheckBox.Checked;
        }
    }
}
