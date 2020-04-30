using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace TechnicSolderHelper.SQL
{
    public partial class DatabaseEditor : Form
    {
        public DatabaseEditor()
        {
            InitializeComponent();
            ModListSqlHelper modListSqlHelper = new ModListSqlHelper();
            dataGridView.DataSource = modListSqlHelper.GetTableInfoForEditing();
            if (dataGridView.Columns["ID"] != null)
            {
                dataGridView.Columns["ID"].Visible = false;
            }
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            ModListSqlHelper modListSqlHelper = new ModListSqlHelper();
            modListSqlHelper.SetTableInfoAfterEditing(dataGridView.DataSource as DataTable);
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void saveAndExitButton_Click(object sender, EventArgs e)
        {
            saveButton_Click(null, null);
            Close();
        }

        private void dataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void highlightVersionsButton_Click(object sender, EventArgs e)
        {
            var dataGridViewColumn = dataGridView.Columns["ModVersion"];
            if (dataGridViewColumn == null) return;
            int modVersionIndex = dataGridViewColumn.Index;
            var gridViewColumn = dataGridView.Columns["MinecraftVersion"];
            if (gridViewColumn == null) return;
            int minecraftVersionIndex = gridViewColumn.Index;
            foreach (DataGridViewRow row in dataGridView.Rows)
            {
                //Here 2 cell is target value and 1 cell is Volume
                dataGridView.Rows[row.Index].Cells[modVersionIndex].Style.BackColor = row.Cells[modVersionIndex].Value.ToString().Contains(row.Cells[minecraftVersionIndex].Value.ToString()) ? Color.Red : Color.White;
            }
        }
    }
}
