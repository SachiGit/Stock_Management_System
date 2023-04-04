using StockManageBLL.Department;
using StockManageDTO.Department;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StockManageUI.Department
{
    public partial class FrmEditDepartmentList : Form
    {
        DepartmentBLL _departmentBLLObject;
        DepartmentDTO _departmentDTOObject;

        public string ID;

        public FrmEditDepartmentList()
        {
            InitializeComponent();
            _departmentBLLObject = new DepartmentBLL();
            _departmentDTOObject = new DepartmentDTO();
        }

        private void FrmEditDepartmentList_Load(object sender, EventArgs e)
        {
            try
            {
                if (ID != null)
                {
                    _departmentDTOObject = _departmentBLLObject.LoadDepartmentDetails(int.Parse(ID));

                    txtID.Text = _departmentDTOObject.ID;
                    txtDepartment.Text = _departmentDTOObject.Department;
                    txtDes.Text = _departmentDTOObject.Description;
                    txtVoteNo.Text = _departmentDTOObject.Vote;
                    txtBudget.Text = _departmentDTOObject.Budget.ToString("N2");
                }
            }
            catch (Exception ex)
            {
                Clipboard.SetText(string.Format("Error Message : {0} \r\nTrace Message : {1}", ex.Message, ex.StackTrace));
                MessageBox.Show("Department Data Load Error");
                this.Close();
            }
        }

        private bool Validation()
        {
            if (string.IsNullOrWhiteSpace(txtDepartment.Text))
            {
                MessageBox.Show("Enter Department", "Department", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
                DepartmentDTO _unitDTOObject = new DepartmentDTO()
                {
                    Department = txtDepartment.Text,
                    Description = txtDes.Text,
                    Vote = txtVoteNo.Text,
                    Budget = Convert.ToDouble(txtBudget.Text)
                };

                int _result = _departmentBLLObject.Save(_unitDTOObject);

                if (_result == 1)
                {
                    MessageBox.Show("Successfully Saved", "Department Data", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }
                else if (_result == 2)
                {
                    MessageBox.Show(string.Format("Unit Name : {0} is already exists !", txtDepartment.Text), "Department Exists", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    MessageBox.Show("Could not be saved", "Department Data", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                Clipboard.SetText(string.Format("Error Message : {0} \r\nTrace Message : {1}", ex.Message, ex.StackTrace));
                MessageBox.Show("Department Data Save Error");
                this.Close();
            }
        }

        private void Update()
        {
            try
            {
                DepartmentDTO _unitDTOObject = new DepartmentDTO()
                {
                    ID = txtID.Text,
                    Department = txtDepartment.Text,
                    Description = txtDes.Text,
                    Vote = txtVoteNo.Text,
                    Budget = Convert.ToDouble(txtBudget.Text)
                };

                bool _result = _departmentBLLObject.Update(_unitDTOObject);

                if (_result)
                {
                    MessageBox.Show("Successfully Updated", "Department Data", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Could not be Updated", "Department Data", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                Clipboard.SetText(string.Format("Error Message : {0} \r\nTrace Message : {1}", ex.Message, ex.StackTrace));
                MessageBox.Show("Department Data Update Error");
                this.Close();
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtBudget_Leave(object sender, EventArgs e)
        {
            double amount = Convert.ToDouble(txtBudget.Text);
            txtBudget.Text = amount.ToString("N2");
        }

    }
}
