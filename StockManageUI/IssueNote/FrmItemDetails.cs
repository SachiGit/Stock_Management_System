using StockManageBLL.Item;
using StockManageUI.Class;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace StockManageUI.IssueNote
{
    public partial class FrmItemDetails : Form
    {
        private ItemBLL _itemBLLObject;
        DataTable dt;

        public FrmItemDetails()
        {
            InitializeComponent();
            _itemBLLObject = new ItemBLL();
        }

        private void FrmItemDetails_Load(object sender, EventArgs e)
        {
            try
            {
                CreateDataTable();
                DataGridStyles.styleGrid(dataGridView1);
                LoadGridData();
                //this.dataGridView1.DefaultCellStyle.Font = new Font("Tahoma", 15);
            }
            catch (Exception ex)
            {
                Clipboard.SetText("Error Message : " + ex.Message + "/r/nTrace Message : " + ex.StackTrace.ToString());
                MessageBox.Show("Data Load Error");
            }
        }

        private void LoadGridData()
        {
            DataTable dt = _itemBLLObject.GetOnHandWithItemDetails();
            dataGridView1.DataSource = dt;
        }

        private void CreateDataTable()
        {
            dt = new DataTable();

            dt.Columns.Add("ItemCode");
            dt.Columns.Add("ItemName");
            dt.Columns.Add("Description");
            dt.Columns.Add("Onhand");
            dt.Columns.Add("Unit");
            dt.Columns.Add("Rate");
        }

        private void txtItemCode_TextChanged(object sender, EventArgs e)
        {
            (dataGridView1.DataSource as DataTable).DefaultView.RowFilter = string.Format("itemcode LIKE '%{0}%'", txtItemCode.Text);
        }

        private void txtItemName_TextChanged(object sender, EventArgs e)
        {
            (dataGridView1.DataSource as DataTable).DefaultView.RowFilter = string.Format("itemName LIKE '%{0}%'", txtItemName.Text);
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                dt.Rows.Clear();
                int _row = dataGridView1.CurrentRow.Index;

                string Onhand = dataGridView1.Rows[_row].Cells[0].Value == null ? "0" : dataGridView1.Rows[_row].Cells[0].Value.ToString();
                string ItemCode = dataGridView1.Rows[_row].Cells[1].Value == null ? "" : dataGridView1.Rows[_row].Cells[1].Value.ToString();
                string ItemName = dataGridView1.Rows[_row].Cells[2].Value == null ? "" : dataGridView1.Rows[_row].Cells[2].Value.ToString();
                string Description = dataGridView1.Rows[_row].Cells[3].Value == null ? "" : dataGridView1.Rows[_row].Cells[3].Value.ToString();
                string Unit = dataGridView1.Rows[_row].Cells[4].Value == null ? "" : dataGridView1.Rows[_row].Cells[4].Value.ToString();
                string Rate = dataGridView1.Rows[_row].Cells[5].Value == null ? "0.00" : dataGridView1.Rows[_row].Cells[5].Value.ToString();

                //string Onhand = dataGridView1.SelectedCells[1].Value.Equals(null) ? "0" : dataGridView1.SelectedCells[1].Value.ToString();
                //string ItemCode = dataGridView1.SelectedCells[2].Value.Equals(null) ? "" : dataGridView1.SelectedCells[2].Value.ToString();
                //string ItemName = dataGridView1.SelectedCells[3].Value.Equals(null) ? "" : dataGridView1.SelectedCells[3].Value.ToString();
                //string Description = dataGridView1.SelectedCells[0].Value.Equals(null) ? "" : dataGridView1.SelectedCells[0].Value.ToString();
                //string Unit = dataGridView1.SelectedCells[4].Value.Equals(null) ? "" : dataGridView1.SelectedCells[4].Value.ToString();
                //string Rate = dataGridView1.SelectedCells[5].Value.Equals(null) ? "0.00" : dataGridView1.SelectedCells[5].Value.ToString();

                dt.Rows.Add(ItemCode, ItemName, Description, Onhand, Unit, Rate);

                FrmIssueNote _iss = (FrmIssueNote)Application.OpenForms["FrmIssueNote"];
                _iss.GetItemDetails(dt);
            }
            catch (Exception ex)
            {
                 Clipboard.SetText("Error Message : " + ex.Message + "/r/nTrace Message : " + ex.StackTrace.ToString());
                MessageBox.Show("Item Details Data Export Error");
            }
        }
    }
}
