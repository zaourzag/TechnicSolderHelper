using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace TechnicSolderHelper.SQL.Forge
{
    public partial class ForgeVersionSelector : Form
    {
        private readonly SolderHelper _solderHelper;
        public ForgeVersionSelector(SolderHelper solderHelper)
        {
            _solderHelper = solderHelper;
            InitializeComponent();
            ForgeSqlHelper helper = new ForgeSqlHelper();
            List<string> forgeVersions = helper.GetForgeBuilds(solderHelper._currentMcVersion);
            forgeVersionDropdown.Items.AddRange(forgeVersions.ToArray());
            forgeVersionDropdown.SelectedIndex = forgeVersionDropdown.Items.Count - 1;
        }

        private void confirmButton_Click(object sender, EventArgs e)
        {
            string version = forgeVersionDropdown.SelectedItem.ToString();
            _solderHelper.PackForge(version);
            Close();
        }
    }
}
