using System;
using System.Windows.Forms;
using TechnicSolderHelper.SQL.Forge;

namespace TechnicSolderHelper.SmallInterfaces
{
    public partial class McSelector : Form
    {
        private readonly SolderHelper _solderHelper;
        public McSelector(SolderHelper solderHelper)
        {
            _solderHelper = solderHelper;
            InitializeComponent();
            ForgeSqlHelper forgeSqlHelper = new ForgeSqlHelper();
            mcVersionDropdown.Items.AddRange(forgeSqlHelper.GetMcVersions().ToArray());
        }

        private void acceptButton_Click(object sender, EventArgs e)
        {
            int index = mcVersionDropdown.SelectedIndex;
            if (index == -1)
            {
                MessageBox.Show("You need to select a Minecraft version to continue.");
                return;
            }
            string mcVersion = mcVersionDropdown.SelectedItem.ToString();
            _solderHelper._currentMcVersion = mcVersion;
            Close();
        }
    }
}
