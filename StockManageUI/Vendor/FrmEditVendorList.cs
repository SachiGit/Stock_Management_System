using StockManageBLL.Vendor;
using StockManageDTO.Vendor;
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
    public partial class FrmEditVendorList : Form
    {
        VendorBLL _vendorBLLObject;
        VendorDTO _vendorDTOObject;

        public string ID;

        public FrmEditVendorList()
        {
            InitializeComponent();
            _vendorBLLObject = new VendorBLL();
            _vendorDTOObject = new VendorDTO();
        }

        private void FrmEditVendorList_Load(object sender, EventArgs e)
        {
            try
            {
                if (ID != null)
                {
                    _vendorDTOObject = _vendorBLLObject.LoadVendorDetails(int.Parse(ID));

                    txtID.Text = _vendorDTOObject.ID;
                    txtVendorName.Text = _vendorDTOObject.VendorName;
                    txtAddress.Text = _vendorDTOObject.Address;
                    txtTel.Text = _vendorDTOObject.Telephone;
                    txtFax.Text = _vendorDTOObject.Fax;
                    txtEmail.Text = _vendorDTOObject.Email;
                    txtWeb.Text = _vendorDTOObject.Web;
                    txtMemo.Text = _vendorDTOObject.Memo;
                }
            }
            catch (Exception ex)
            {
                Clipboard.SetText(string.Format("Error Message : {0} \r\nTrace Message : {1}", ex.Message, ex.StackTrace));
                MessageBox.Show("Vendor Data Load Error");
                this.Close();
            }
        }

        private bool Validation()
        {
            if (string.IsNullOrWhiteSpace(txtVendorName.Text))
            {
                MessageBox.Show("Enter Vendor Name", "Vendor", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            return true;
        }

        private void btnSaveUpdate_Click(object sender, EventArgs e)
        {
            if (ID != null)
            {
                if (Validation())
                {
                    Update();
                }
            }
            else
            {
                if (Validation())
                {
                    Save();
                }
            }
        }

        private void Save()
        {
            try
            {
                VendorDTO _vendorDTOObject = new VendorDTO()
                {
                    VendorName = txtVendorName.Text,
                    Address = txtAddress.Text,
                    Telephone = txtTel.Text,
                    Fax = txtFax.Text,
                    Email = txtEmail.Text,
                    Web = txtWeb.Text,
                    Memo = txtMemo.Text
                };

                int _result = _vendorBLLObject.Save(_vendorDTOObject);

                if (_result == 1)
                {
                    MessageBox.Show("Successfully Saved", "Vendor Data", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }
                else if (_result == 2)
                {
                    MessageBox.Show(string.Format("Vendor Name : {0} is already exists !", txtVendorName.Text), "Vendor Exists", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    MessageBox.Show("Could not be saved", "Vendor Data", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                Clipboard.SetText(string.Format("Error Message : {0} \r\nTrace Message : {1}", ex.Message, ex.StackTrace));
                MessageBox.Show("Vendor Data Save Error");
                this.Close();
            }
        }

        private void Update()
        {
            try
            {
                VendorDTO _vendorDTOObject = new VendorDTO()
                {
                    ID = txtID.Text,
                    VendorName = txtVendorName.Text,
                    Address = txtAddress.Text,
                    Telephone = txtTel.Text,
                    Fax = txtFax.Text,
                    Email = txtEmail.Text,
                    Web = txtWeb.Text,
                    Memo = txtMemo.Text
                };

                bool _result = _vendorBLLObject.Update(_vendorDTOObject);

                if (_result)
                {
                    MessageBox.Show("Successfully Updated", "Unit Data", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Could not be Updated", "Unit Data", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                Clipboard.SetText(string.Format("Error Message : {0} \r\nTrace Message : {1}", ex.Message, ex.StackTrace));
                MessageBox.Show("Unit Data Update Error");
                this.Close();
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtTel_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsNumber(e.KeyChar) & (Keys)e.KeyChar != Keys.Back & e.KeyChar != '.')
            {
                e.Handled = true;
            }
        }

        private void txtFax_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsNumber(e.KeyChar) & (Keys)e.KeyChar != Keys.Back & e.KeyChar != '.')
            {
                e.Handled = true;
            }
        }


    }
}
