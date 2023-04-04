using StockManageBLL.User;
using StockManageDTO.User;
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
    public partial class FrmEditUserList : Form
    {
        UserBLL _userBLLObject;
        UserDTO _userDTOObject;

        public string ID;

        public FrmEditUserList()
        {
            InitializeComponent();
            _userBLLObject = new UserBLL();
            _userDTOObject = new UserDTO();
        }

        private void FrmEditUserList_Load(object sender, EventArgs e)
        {
            try
            {
                if (ID != null)
                {
                    _userDTOObject = _userBLLObject.LoadUserDetails(int.Parse(ID));

                    txtID.Text = _userDTOObject.ID;
                    txtUserName.Text = _userDTOObject.UserName;
                    cmbUserType.Text = _userDTOObject.UserType;
                    txtPassword.Text = _userDTOObject.Password;
                    txtRePassword.Text = _userDTOObject.Password;
                    txtFullName.Text = _userDTOObject.FullName;
                }
            }
            catch (Exception ex)
            {
                Clipboard.SetText(string.Format("Error Message : {0} \r\nTrace Message : {1}", ex.Message, ex.StackTrace));
                MessageBox.Show("User Data Load Error");
                this.Close();
            }
        }

        private bool Validation()
        {
            if (string.IsNullOrWhiteSpace(txtUserName.Text))
            {
                MessageBox.Show("Enter username", "User Name", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            else if (string.IsNullOrWhiteSpace(txtPassword.Text))
            {
                MessageBox.Show("Enter password", "User Password", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            else if (string.IsNullOrWhiteSpace(txtRePassword.Text))
            {
                MessageBox.Show("Enter confirm password", "User Confirm Password", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            else if (txtPassword.Text != txtRePassword.Text)
            {
                MessageBox.Show("Password mismatch", "Re-Enter Password", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtPassword.ResetText();
                txtRePassword.ResetText();
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
                    UpdateUser();
                }
            }
            else
            {
                if (Validation())
                {
                    SaveUser();
                }
            }
        }

        private void SaveUser()
        {
            try
            {
                UserDTO _userDTOObject = new UserDTO()
                {
                    UserName = txtUserName.Text,
                    UserType = cmbUserType.Text,
                    Password = txtPassword.Text,
                    FullName = txtFullName.Text
                };

                int _result = _userBLLObject.Save(_userDTOObject);

                if (_result == 1)
                {
                    MessageBox.Show("Successfully Saved", "User Data", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }
                else if (_result == 2)
                {
                    MessageBox.Show(string.Format("User Name : {0} is already exists !", txtUserName.Text), "User Exists", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    MessageBox.Show("Could not be saved", "User Data", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                Clipboard.SetText(string.Format("Error Message : {0} \r\nTrace Message : {1}", ex.Message, ex.StackTrace));
                MessageBox.Show("User Data Save Error");
                this.Close();
            }
        }

        private void UpdateUser()
        {
            try
            {
                UserDTO _userDTOObject = new UserDTO()
                {
                    ID = txtID.Text,
                    UserName = txtUserName.Text,
                    UserType = cmbUserType.Text,
                    Password = txtPassword.Text,
                    FullName = txtFullName.Text
                };

                bool _result = _userBLLObject.Update(_userDTOObject);

                if (_result)
                {
                    MessageBox.Show("Successfully Updated", "User Data", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Could not be Updated", "User Data", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                Clipboard.SetText(string.Format("Error Message : {0} \r\nTrace Message : {1}", ex.Message, ex.StackTrace));
                MessageBox.Show("User Data Update Error");
                this.Close();
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
