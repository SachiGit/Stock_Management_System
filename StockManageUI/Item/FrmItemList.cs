using StockManageBLL.Item;
using StockManageUI.Class;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StockManageUI.Item
{
    public partial class FrmItemList : Form
    {
        ItemBLL _itemBLLObject;

        public FrmItemList()
        {
            InitializeComponent();
            _itemBLLObject = new ItemBLL();
        }

        private void FrmItemList_Load(object sender, EventArgs e)
        {
            try
            {
                DataGridStyles.styleGrid(dataGridView1);
                LoadItemList();
            }
            catch (Exception ex)
            {
                Clipboard.SetText(string.Format("Error Message : {0} \r\nTrace Message : {1}", ex.Message, ex.StackTrace));
                MessageBox.Show("Item List Load Error");
            }
        }

        private void LoadItemList()
        {
            dataGridView1.DataSource = _itemBLLObject.LoadItemList();
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            FrmEditItemList _eil = new FrmEditItemList();
            _eil.ShowDialog();
            btnRefresh.PerformClick();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            string ID = dataGridView1.SelectedCells[0].Value.ToString();

            FrmEditItemList _eil = new FrmEditItemList();
            _eil.ID = ID;
            _eil.btnSaveUpdate.Text = "Update";
            _eil.ShowDialog();
            btnRefresh.PerformClick();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            bool _isDelete = false;

            try
            {
                if (MessageBox.Show(string.Format("Do you want to delete Item {0} ? ", dataGridView1.SelectedCells[1].Value.ToString()), "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    _isDelete = _itemBLLObject.DeleteItemDetails(int.Parse(dataGridView1.SelectedCells[0].Value.ToString()));
                }

                if (_isDelete)
                {
                    MessageBox.Show("Successfully Deleted.", "Item Data", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    btnRefresh.PerformClick();
                }
            }
            catch (Exception ex)
            {
                Clipboard.SetText(string.Format("Error Message : {0} \r\nTrace Message : {1}", ex.Message, ex.StackTrace));
                MessageBox.Show("Item Data Delete Error");
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            try
            {
                LoadItemList();
            }
            catch (Exception ex)
            {
                Clipboard.SetText(string.Format("Error Message : {0} \r\nTrace Message : {1}", ex.Message, ex.StackTrace));
                MessageBox.Show("Item List Load Error");
            }
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            string ID = dataGridView1.SelectedCells[0].Value.ToString();

            FrmEditItemList _eil = new FrmEditItemList();
            _eil.ID = ID;
            _eil.btnSaveUpdate.Text = "Update";
            _eil.ShowDialog();
            btnRefresh.PerformClick();
        }

        private void txtSearch_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((Keys)e.KeyChar == Keys.Enter)
            {
                backgroundWorker1.RunWorkerAsync();
            }
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            findWithBackgroundworker();
        }

        private void findWithBackgroundworker()
        {
            try
            {
                bool _canFoundResult = false;
                string _searchText = txtSearch.Text.ToLower();

                if (dataGridView1.InvokeRequired)
                {
                    dataGridView1.Invoke(new MethodInvoker(delegate
                    {
                        dataGridView1.ClearSelection();

                        if (!string.IsNullOrWhiteSpace(_searchText))
                        {
                            for (int _count = 0; _count < dataGridView1.Rows.Count; _count++)
                            {
                                if (dataGridView1.Rows[_count].Cells["icode"].Value != null)
                                {
                                    if (dataGridView1.Rows[_count].Cells["icode"].Value.ToString().ToLower().Contains(_searchText))
                                    {
                                        dataGridView1.Rows[_count].Cells["icode"].Selected = true;
                                        dataGridView1.CurrentCell = dataGridView1.Rows[_count].Cells[1];
                                        _canFoundResult = true;
                                        txtSearch.ResetText();
                                        //if (MessageBox.Show("Do you want to continue search?","Item List Search", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                                        //{
                                        //    dataGridView1.ClearSelection();
                                        //    _canFoundResult = false;
                                        //    continue;
                                        //}
                                        //else
                                        //{
                                        //    break;
                                        //}
                                    }
                                }
                            }
                        }

                        if (!_canFoundResult)
                        {
                            MessageBox.Show("No records match your search", "Item List Search", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }

                    }));
                }
            }
            catch (Exception ex)
            {
                Clipboard.SetText(ex.Message);
            }
        }

        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            if (lbl_Searching.Visible == false)
            {
                lbl_Searching.Visible = true;
            }
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            lbl_Searching.Visible = false;
            txtSearch.ReadOnly = false;
        }

        private void backgroundWorker2_DoWork(object sender, DoWorkEventArgs e)
        {
            FindItem();
        }

        private void FindItem()
        {
            try
            {
                string _searchText = txtSearch.Text.ToLower();

                if (dataGridView1.InvokeRequired)
                {
                    dataGridView1.Invoke(new MethodInvoker(delegate
                    {

                        dataGridView1.ClearSelection();

                        if (!string.IsNullOrWhiteSpace(_searchText))
                        {
                            for (int _count = 0; _count < dataGridView1.Rows.Count; _count++)
                            {
                                if (dataGridView1.Rows[_count].Cells["icode"].Value != null)
                                {
                                    if (dataGridView1.Rows[_count].Cells["icode"].Value.ToString().ToLower().Contains(_searchText))
                                    {
                                        dataGridView1.Rows[_count].Cells["icode"].Selected = true;
                                        dataGridView1.CurrentCell = dataGridView1.Rows[_count].Cells[1];
                                        break;
                                    }
                                }
                            }
                        }

                    }));
                }

            }
            catch (Exception ex)
            {
                Clipboard.SetText(ex.Message);
            }
        }
    }
}
