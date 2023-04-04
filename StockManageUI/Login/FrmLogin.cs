using MySql.Data.MySqlClient;
using StockManageBLL.Login;
using StockManageDTO.Login;
using StockManageUI.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StockManageUI.Login
{
    public partial class FrmLogin : Form
    {
        LoginBLL _loginBLLObject = new LoginBLL();
        LoginDTO _loginDTOObject = new LoginDTO();

        ConfigBLL _configurationBLLObject = new ConfigBLL();
        ConfigDTO _configurationDTOObject = new ConfigDTO();

        public bool IsMainPageClose;
        public bool _canLogin = false;

        public FrmLogin()
        {
            InitializeComponent();
        }

        private void FrmLogin_Load(object sender, EventArgs e)
        {
            this.Size = new System.Drawing.Size(516, 226);
            Rectangle resolution = Screen.PrimaryScreen.Bounds;
            int height = resolution.Size.Height;
            int width = resolution.Size.Width;
            int x = (width - 516) / 2;
            int y = (height - 205) / 2;
            this.Location = new Point(x, y);
            //this.Refresh();     

            this.ActiveControl = txtName;

            if (IsMainPageClose)
            {
                Application.Exit();
            }
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            LoginApplication();
        }

        private void LoginApplication()
        {
            lblMessage.Visible = false;

            try
            {
                _loginDTOObject.UserName = txtName.Text;
                _loginDTOObject.Password = txtPassword.Text;

                bool _canAuthenticate = _loginBLLObject.CheckUserAuthentication(_loginDTOObject);
                string UserType = _loginBLLObject.GetUserType(_loginDTOObject);

                if (_canAuthenticate)
                {
                    FrmMain _main = new FrmMain();
                    _main.UserName = txtName.Text;
                    _main.UserType = UserType;
                    _main.Show();

                    this.Hide();
                }
                else
                {
                    lblMessage.Text = "Please Enter Correct User Name & Password";
                    lblMessage.Visible = true;
                }
            }
            catch (MySqlException SQLEx)
            {
                lblMessage.Text = "Please Check your Connection";
                lblMessage.Visible = true;
                Clipboard.SetText(SQLEx.Message);
            }
            catch (Exception exe)
            {
                lblMessage.Text = "Cannot access application. Please contact customer care.";
                lblMessage.Visible = true;
                Clipboard.SetText(exe.Message);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void pbSetting_Click(object sender, EventArgs e)
        {
            if (this.Size != new Size(760, 226))
            {
                this.Size = new Size(760, 226);
                Rectangle resolution = Screen.PrimaryScreen.Bounds;
                int height = resolution.Size.Height;
                int width = resolution.Size.Width;
                int x = (width - 760) / 2;
                int y = (height - 226) / 2;
                this.Location = new Point(x, y);
                txtServer.Focus();
                try
                {
                    _configurationDTOObject = _configurationBLLObject.GetConfigurations();

                    txtServer.Text = _configurationDTOObject.Server;
                    txtPort.Text = _configurationDTOObject.Port.ToString();
                    txtRoot.Text = _configurationDTOObject.Root;
                    txtpw.Text = _configurationDTOObject.Password;
                    txt_DB.Text = _configurationDTOObject.DataBase;
                }
                catch (Exception ex)
                {
                    Clipboard.SetText(ex.Message);
                    MessageBox.Show("Cannot load Connection file", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
            {
                this.Size = new Size(516, 226);
                Rectangle resolution = Screen.PrimaryScreen.Bounds;
                int height = resolution.Size.Height;
                int width = resolution.Size.Width;
                int x = (width - 516) / 2;
                int y = (height - 226) / 2;
                this.Location = new Point(x, y);
                txtName.Focus();
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                _configurationDTOObject.DataBase = txt_DB.Text;
                _configurationDTOObject.Password = txtpw.Text;
                _configurationDTOObject.Root = txtRoot.Text;
                _configurationDTOObject.Server = txtServer.Text;
                _configurationDTOObject.Port = Convert.ToUInt16(txtPort.Text.Length < 1 ? "0" : txtPort.Text);

                bool _canLog = _configurationBLLObject.SetConfigurations(_configurationDTOObject);

                if (_canLog)
                {
                    MessageBox.Show("Connection String Changed", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    this.Size = new Size(516, 226);
                    Rectangle resolution = Screen.PrimaryScreen.Bounds;
                    int height = resolution.Size.Height;
                    int width = resolution.Size.Width;
                    int x = (width - 516) / 2;
                    int y = (height - 226) / 2;
                    this.Location = new Point(x, y);
                    txtName.Focus();
                }
                else
                {
                    MessageBox.Show("Cannot change connection", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                Clipboard.SetText(ex.Message);
                MessageBox.Show("Could not be saved configuration details", "Configuration", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void txtName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnLogin.PerformClick();
            }
        }

        private void txtPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnLogin.PerformClick();
            }
        }

    }
}
