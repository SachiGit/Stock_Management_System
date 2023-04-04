using StockManageBLL.Vendor;
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

namespace StockManageUI.Vendor
{
    public partial class FrmVendorList : Form
    {
        VendorBLL _vendorBLLObject;

        public FrmVendorList()
        {
            InitializeComponent();
            _vendorBLLObject = new VendorBLL();
        }

        private void FrmVendorList_Load(object sender, EventArgs e)
        {
            try
            {
                DataGridStyles.styleGridList_2014(dataGridView1);
                LoadVendorList();
            }
            catch (Exception ex)
            {
                Clipboard.SetText(string.Format("Error Message : {0} \r\nTrace Message : {1}", ex.Message, ex.StackTrace));
                MessageBox.Show("Vendor List Load Error");
            }
        }

        private void LoadVendorList()
        {
            dataGridView1.DataSource = _vendorBLLObject.LoadVendorList();
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            FrmEditVendorList _vendor = new FrmEditVendorList();
            _vendor.ShowDialog();
            btnRefresh.PerformClick();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            string ID = dataGridView1.SelectedCells[0].Value.ToString();

            FrmEditVendorList _vendor = new FrmEditVendorList();
            _vendor.ID = ID;
            _vendor.btnSaveUpdate.Text = "Update";
            _vendor.ShowDialog();
            btnRefresh.PerformClick();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            bool _isDelete = false;

            try
            {
                if (MessageBox.Show(string.Format("Do you want to delete Vendor {0} ? ", dataGridView1.SelectedCells[1].Value.ToString()), "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    _isDelete = _vendorBLLObject.DeleteVendorDetails(int.Parse(dataGridView1.SelectedCells[0].Value.ToString()));
                }

                if (_isDelete)
                {
                    MessageBox.Show("Successfully Deleted.", "Vendor Data", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    btnRefresh.PerformClick();
                }
            }
            catch (Exception ex)
            {
                Clipboard.SetText(string.Format("Error Message : {0} \r\nTrace Message : {1}", ex.Message, ex.StackTrace));
                MessageBox.Show("Vendor Data Delete Error");
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            try
            {
                LoadVendorList();
            }
            catch (Exception ex)
            {
                Clipboard.SetText(string.Format("Error Message : {0} \r\nTrace Message : {1}", ex.Message, ex.StackTrace));
                MessageBox.Show("Vendor List Load Error");
            }
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            string ID = dataGridView1.SelectedCells[0].Value.ToString();

            FrmEditVendorList _vendor = new FrmEditVendorList();
            _vendor.ID = ID;
            _vendor.btnSaveUpdate.Text = "Update";
            _vendor.ShowDialog();
            btnRefresh.PerformClick();
        }
    }
}
