using StockManageBLL.Company;
using StockManageDTO.Company;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace StockManageUI.Company
{
    public partial class FrmCompany : Form
    {
        CompanyBLL _companyBLL;
        CompanyDTO _companyDTO;

        public FrmCompany()
        {
            InitializeComponent();
            _companyBLL = new CompanyBLL();
            _companyDTO = new CompanyDTO();
        }

        private void FrmCompany_Load(object sender, EventArgs e)
        {
            try
            {
                if (_companyBLL.CheckExist())
                {
                    _companyDTO = _companyBLL.GetCompanyData();

                    txtID.Text = _companyDTO.ID;
                    txtComName.Text = _companyDTO.ComName;
                    txtAddress.Text = _companyDTO.Address;
                    txtTelNo.Text = _companyDTO.Telephone;
                    txtFax.Text = _companyDTO.Fax;
                    txtEmail.Text = _companyDTO.Email;
                    txtWeb.Text = _companyDTO.Web;

                    btnSave.Text = "Update";
                }
            }
            catch (Exception ex)
            {
                Clipboard.SetText(string.Format("Error Message : {0} \r\nTrace Message : {1}", ex.Message, ex.StackTrace));
                MessageBox.Show("Company Data Load Error");
                this.Close();
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtComName.Text))
            {
                if (_companyBLL.CheckExist())
                {
                    UpdateCompany();
                }
                else
                {
                    SaveCompany();
                }
            }
            else
            {
                MessageBox.Show("Enter Company Name", "Company Name", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void SaveCompany()
        {
            try
            {
                CompanyDTO _companyDTO = new CompanyDTO()
                {
                    ComName = txtComName.Text,
                    Address = txtAddress.Text,
                    Telephone = txtTelNo.Text,
                    Fax = txtFax.Text,
                    Email = txtEmail.Text,
                    Web = txtWeb.Text
                };

                bool Save = _companyBLL.Save(_companyDTO);

                if (Save)
                {
                    MessageBox.Show("Successfully Saved", "Company Data", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Could not be saved", "Company Data", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                Clipboard.SetText(string.Format("Error Message : {0} /r/nTrace Message : {1}", ex.Message, ex.StackTrace));
                MessageBox.Show("Company Data Save Error");
                this.Close();
            }
        }

        private void UpdateCompany()
        {
            try
            {
                CompanyDTO _companyDTO = new CompanyDTO()
                {
                    ID = txtID.Text,
                    ComName = txtComName.Text,
                    Address = txtAddress.Text,
                    Telephone = txtTelNo.Text,
                    Fax = txtFax.Text,
                    Email = txtEmail.Text,
                    Web = txtWeb.Text
                };

                bool Save = _companyBLL.Update(_companyDTO);

                if (Save)
                {
                    MessageBox.Show("Successfully Updated", "Company Data", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Could not be saved", "Company Data", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                Clipboard.SetText(string.Format("Error Message : {0} /r/nTrace Message : {1}", ex.Message, ex.StackTrace));
                MessageBox.Show("Company Data Update Error");
                this.Close();
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }


    }
}
