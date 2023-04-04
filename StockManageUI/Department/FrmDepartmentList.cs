using StockManageBLL.Department;
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

namespace StockManageUI.Department
{
    public partial class FrmDepartmentList : Form
    {
        DepartmentBLL _departmentBLLObject;

        public FrmDepartmentList()
        {
            InitializeComponent();
            _departmentBLLObject = new DepartmentBLL();
        }

        private void FrmDepartmentList_Load(object sender, EventArgs e)
        {
            try
            {
                DataGridStyles.styleGridList_2014(dataGridView1);
                LoadDepartmentList();
            }
            catch (Exception ex)
            {
                Clipboard.SetText(string.Format("Error Message : {0} \r\nTrace Message : {1}", ex.Message, ex.StackTrace));
                MessageBox.Show("Department List Load Error");
            }
        }

        private void LoadDepartmentList()
        {
            dataGridView1.DataSource = _departmentBLLObject.LoadDepartmentList();
            dataGridView1.Columns["budget"].DefaultCellStyle.Format = "N2";
            dataGridView1.Columns["budget"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            FrmEditDepartmentList _department = new FrmEditDepartmentList();
            _department.ShowDialog();
            btnRefresh.PerformClick(); 
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            string ID = dataGridView1.SelectedCells[0].Value.ToString();

            FrmEditDepartmentList _department = new FrmEditDepartmentList();
            _department.ID = ID;
            _department.btnSaveUpdate.Text = "Update";
            _department.ShowDialog();
            btnRefresh.PerformClick();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            bool _isDelete = false;

            try
            {
                if (MessageBox.Show(string.Format("Do you want to delete Department {0} ? ", dataGridView1.SelectedCells[1].Value.ToString()), "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    _isDelete = _departmentBLLObject.DeleteDepartmentDetails(int.Parse(dataGridView1.SelectedCells[0].Value.ToString()));
                }

                if (_isDelete)
                {
                    MessageBox.Show("Successfully Deleted.", "Department Data", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    btnRefresh.PerformClick();
                }
            }
            catch (Exception ex)
            {
                Clipboard.SetText(string.Format("Error Message : {0} \r\nTrace Message : {1}", ex.Message, ex.StackTrace));
                MessageBox.Show("Department Data Delete Error");
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            try
            {
                LoadDepartmentList();
            }
            catch (Exception ex)
            {
                Clipboard.SetText(string.Format("Error Message : {0} \r\nTrace Message : {1}", ex.Message, ex.StackTrace));
                MessageBox.Show("Department List Load Error");
            }
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            string ID = dataGridView1.SelectedCells[0].Value.ToString();

            FrmEditDepartmentList _department = new FrmEditDepartmentList();
            _department.ID = ID;
            _department.btnSaveUpdate.Text = "Update";
            _department.ShowDialog();
            btnRefresh.PerformClick();
        }


    }
}
