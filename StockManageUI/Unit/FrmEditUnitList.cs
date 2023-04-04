using StockManageBLL.Unit;
using StockManageDTO.Unit;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StockManageUI.Unit
{
    public partial class FrmEditUnitList : Form
    {
        UnitBLL _unitBLLObject;
        UnitDTO _unitDTOObject;

        public string ID;

        public FrmEditUnitList()
        {
            InitializeComponent();
            _unitBLLObject = new UnitBLL();
            _unitDTOObject = new UnitDTO();
        }

        private void FrmEditUnitList_Load(object sender, EventArgs e)
        {
            try
            {
                if (ID != null)
                {
                    _unitDTOObject = _unitBLLObject.LoadUnitDetails(int.Parse(ID));

                    txtID.Text = _unitDTOObject.ID;
                    txtUnit.Text = _unitDTOObject.Unit;
                    txtDes.Text = _unitDTOObject.Description;
                }
            }
            catch (Exception ex)
            {
                Clipboard.SetText(string.Format("Error Message : {0} \r\nTrace Message : {1}", ex.Message, ex.StackTrace));
                MessageBox.Show("Unit Data Load Error");
                this.Close();
            }
        }

        private bool Validation()
        {
            if (string.IsNullOrWhiteSpace(txtUnit.Text))
            {
                MessageBox.Show("Enter Unit", "Unit", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
                UnitDTO _unitDTOObject = new UnitDTO()
                {
                    Unit  = txtUnit.Text,
                    Description  = txtDes.Text
                };

                int _result = _unitBLLObject.Save(_unitDTOObject);

                if (_result == 1)
                {
                    MessageBox.Show("Successfully Saved", "Unit Data", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }
                else if (_result == 2)
                {
                    MessageBox.Show(string.Format("Unit Name : {0} is already exists !", txtUnit.Text), "Unit Exists", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    MessageBox.Show("Could not be saved", "Unit Data", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                Clipboard.SetText(string.Format("Error Message : {0} \r\nTrace Message : {1}", ex.Message, ex.StackTrace));
                MessageBox.Show("Unit Data Save Error");
                this.Close();
            }
        }

        private void Update()
        {
            try
            {
                UnitDTO _unitDTOObject = new UnitDTO()
                {
                    ID = txtID.Text,
                    Unit  = txtUnit.Text,
                    Description  = txtDes.Text
                };

                bool _result = _unitBLLObject.Update(_unitDTOObject);

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
    }
}
