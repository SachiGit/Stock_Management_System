using StockManageBLL.User;
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

namespace StockManageUI.User
{
    public partial class FrmUserList : Form
    {
        UserBLL _userBLLObject;

        public FrmUserList()
        {
            InitializeComponent();
            _userBLLObject = new UserBLL();
        }

        private void FrmUserList_Load(object sender, EventArgs e)
        {
            try
            {
                DataGridStyles.styleGridList_2014(dataGridView1);
                LoadUserList();
            }
            catch (Exception ex)
            {
                Clipboard.SetText(string.Format("Error Message : {0} \r\nTrace Message : {1}", ex.Message, ex.StackTrace));
                MessageBox.Show("User List Load Error");
            }
        }

        private void LoadUserList()
        {
            dataGridView1.DataSource = _userBLLObject.LoadUserList();
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            FrmEditUserList _eul = new FrmEditUserList();
            _eul.ShowDialog();
            btnRefresh.PerformClick(); 
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            string ID = dataGridView1.SelectedCells[0].Value.ToString();

            FrmEditUserList _eul = new FrmEditUserList();
            _eul.ID = ID;
            _eul.btnSaveUpdate.Text = "Update";
            _eul.ShowDialog();
            btnRefresh.PerformClick();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            bool _isDelete = false;

            try
            {
                if (MessageBox.Show(string.Format("Do you want to delete User {0} ? ", dataGridView1.SelectedCells[1].Value.ToString()), "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    _isDelete = _userBLLObject.DeleteUserDetails(int.Parse(dataGridView1.SelectedCells[0].Value.ToString()));
                }

                if (_isDelete)
                {
                    MessageBox.Show("Successfully Deleted.", "User Data", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    btnRefresh.PerformClick();
                }
            }
            catch (Exception ex)
            {
                Clipboard.SetText(string.Format("Error Message : {0} \r\nTrace Message : {1}", ex.Message, ex.StackTrace));
                MessageBox.Show("User Data Delete Error");
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            try
            {
                LoadUserList();
            }
            catch (Exception ex)
            {
                Clipboard.SetText(string.Format("Error Message : {0} \r\nTrace Message : {1}", ex.Message, ex.StackTrace));
                MessageBox.Show("User List Load Error");
            }
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            string ID = dataGridView1.SelectedCells[0].Value.ToString();

            FrmEditUserList _eul = new FrmEditUserList();
            _eul.ID = ID;
            _eul.btnSaveUpdate.Text = "Update";
            _eul.ShowDialog();
            btnRefresh.PerformClick();
        }


    }
}
