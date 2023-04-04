using StockManageBLL.Item;
using StockManageBLL.Unit;
using StockManageDTO.Item;
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
    public partial class FrmEditItemList : Form
    {
        ItemBLL _itemBLLObject;
        ItemDTO _itemDTOObject;
        UnitBLL _unitBLLObject;

        public string ID;

        public FrmEditItemList()
        {
            InitializeComponent();
            _itemBLLObject = new ItemBLL();
            _itemDTOObject = new ItemDTO();
            _unitBLLObject = new UnitBLL();
        }

        private void FrmEditItemList_Load(object sender, EventArgs e)
        {
            try
            {
                LoadUnitData();

                if (ID != null)
                {
                    _itemDTOObject = _itemBLLObject.LoadItemDetails(int.Parse(ID));

                    txtID.Text = _itemDTOObject.ID;
                    txtItemName.Text = _itemDTOObject.ItemName;
                    txtItemCode.Text = _itemDTOObject.ItemCode;
                    txtDes.Text = _itemDTOObject.Description;
                    cmbUnit.Text = _itemDTOObject.Unit;
                    txtPrice1.Text = _itemDTOObject.Price1.ToString("N2");
                    txtPrice2.Text = _itemDTOObject.Price2.ToString("N2");
                    txtPrice3.Text = _itemDTOObject.Price3.ToString("N2");
                    txtPrice4.Text = _itemDTOObject.Price4.ToString("N2");
                    txtPrice5.Text = _itemDTOObject.Price5.ToString("N2");
                    txtPrice6.Text = _itemDTOObject.Price6.ToString("N2");
                    txtReOrderLevel.Text = _itemDTOObject.ReOrderLevel.ToString();
                }
            }
            catch (Exception ex)
            {
                Clipboard.SetText(string.Format("Error Message : {0} \r\nTrace Message : {1}", ex.Message, ex.StackTrace));
                MessageBox.Show("Item Data Load Error");
                this.Close();
            }
        }

        private void LoadUnitData()
        {
            DataTable _unitTable = _unitBLLObject.LoadUnitList();

            DataTable _unitTableCopy = _unitTable.Copy();
            DataRow _row1 = _unitTableCopy.NewRow();
            _row1["unit"] = "";
            _unitTableCopy.Rows.InsertAt(_row1, 0);

            cmbUnit.DataSource = _unitTableCopy;
            cmbUnit.DisplayMember = "unit";
        }        

        private bool Validation()
        {
            if (string.IsNullOrWhiteSpace(txtItemName.Text))
            {
                MessageBox.Show("Enter Item Name", "Item Name", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            else if (string.IsNullOrWhiteSpace(txtItemCode.Text))
            {
                MessageBox.Show("Enter Item Code", "Item Code", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
                    UpdateItem();
                }
            }
            else
            {
                if (Validation())
                {
                    SaveItem();
                }
            }
        }

        private void SaveItem()
        {
            try
            {
                ItemDTO _userDTOObject = new ItemDTO()
                {
                    ItemName = txtItemName.Text,
                    ItemCode = txtItemCode.Text,
                    Description = txtDes.Text,
                    Unit = cmbUnit.Text,
                    Price1 = txtPrice1.Text.Equals("") ? 0 : Convert.ToDouble(txtPrice1.Text),
                    Price2 = txtPrice2.Text.Equals("") ? 0 : Convert.ToDouble(txtPrice2.Text),
                    Price3 = txtPrice3.Text.Equals("") ? 0 : Convert.ToDouble(txtPrice3.Text),
                    Price4 = txtPrice4.Text.Equals("") ? 0 : Convert.ToDouble(txtPrice4.Text),
                    Price5 = txtPrice5.Text.Equals("") ? 0 : Convert.ToDouble(txtPrice5.Text),
                    Price6 = txtPrice6.Text.Equals("") ? 0 : Convert.ToDouble(txtPrice6.Text),
                    ReOrderLevel = txtReOrderLevel.Text.Equals("") ? 0 : Convert.ToDouble(txtReOrderLevel.Text)
                };

                int _result = _itemBLLObject.Save(_userDTOObject);

                if (_result == 1)
                {
                    MessageBox.Show("Successfully Saved", "Item Data", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }
                else if (_result == 2)
                {
                    MessageBox.Show(string.Format("Item Name : {0} is already exists !", txtItemName.Text), "Item Exists", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    MessageBox.Show("Could not be saved", "Item Data", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                Clipboard.SetText(string.Format("Error Message : {0} \r\nTrace Message : {1}", ex.Message, ex.StackTrace));
                MessageBox.Show("Item Data Save Error");
                this.Close();
            }
        }

        private void UpdateItem()
        {
            try
            {
                ItemDTO _userDTOObject = new ItemDTO()
                {
                    ID = txtID.Text,
                    ItemName = txtItemName.Text,
                    ItemCode = txtItemCode.Text,
                    Description = txtDes.Text,
                    Unit = cmbUnit.Text,
                    Price1 = Convert.ToDouble(txtPrice1.Text),
                    Price2 = Convert.ToDouble(txtPrice2.Text),
                    Price3 = Convert.ToDouble(txtPrice3.Text),
                    Price4 = Convert.ToDouble(txtPrice4.Text),
                    Price5 = Convert.ToDouble(txtPrice5.Text),
                    Price6 = Convert.ToDouble(txtPrice6.Text),
                    ReOrderLevel = txtReOrderLevel.Text.Equals("") ? 0 : Convert.ToDouble(txtReOrderLevel.Text)
                };

                bool _result = _itemBLLObject.Update(_userDTOObject);

                if (_result)
                {
                    MessageBox.Show("Successfully Updated", "Item Data", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Could not be Updated", "Item Data", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                Clipboard.SetText(string.Format("Error Message : {0} \r\nTrace Message : {1}", ex.Message, ex.StackTrace));
                MessageBox.Show("Item Data Update Error");
                this.Close();
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtPrice1_Leave(object sender, EventArgs e)
        {
            double amount = txtPrice1.Text.Equals("") ? 0.00 : Convert.ToDouble(txtPrice1.Text);
            txtPrice1.Text = amount.ToString("N2");
        }

        private void txtPrice1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsNumber(e.KeyChar) & (Keys)e.KeyChar != Keys.Back & e.KeyChar != '.')
            {
                e.Handled = true;
            }
        }

        private void txtPrice2_Leave(object sender, EventArgs e)
        {
            double amount = txtPrice2.Text.Equals("") ? 0.00 : Convert.ToDouble(txtPrice2.Text);
            txtPrice2.Text = amount.ToString("N2");
        }

        private void txtPrice3_Leave(object sender, EventArgs e)
        {
            double amount = txtPrice3.Text.Equals("") ? 0.00 : Convert.ToDouble(txtPrice3.Text);
            txtPrice3.Text = amount.ToString("N2");
        }

        private void txtPrice4_Leave(object sender, EventArgs e)
        {
            double amount = txtPrice4.Text.Equals("") ? 0.00 : Convert.ToDouble(txtPrice4.Text);
            txtPrice4.Text = amount.ToString("N2");
        }

        private void txtPrice5_Leave(object sender, EventArgs e)
        {
            double amount = txtPrice5.Text.Equals("") ? 0.00 : Convert.ToDouble(txtPrice5.Text);
            txtPrice5.Text = amount.ToString("N2");
        }

        private void txtPrice6_Leave(object sender, EventArgs e)
        {
            double amount = txtPrice6.Text.Equals("") ? 0.00 : Convert.ToDouble(txtPrice6.Text);
            txtPrice6.Text = amount.ToString("N2");
        }

        private void txtPrice2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsNumber(e.KeyChar) & (Keys)e.KeyChar != Keys.Back & e.KeyChar != '.')
            {
                e.Handled = true;
            }
        }

        private void txtPrice3_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsNumber(e.KeyChar) & (Keys)e.KeyChar != Keys.Back & e.KeyChar != '.')
            {
                e.Handled = true;
            }
        }

        private void txtPrice4_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsNumber(e.KeyChar) & (Keys)e.KeyChar != Keys.Back & e.KeyChar != '.')
            {
                e.Handled = true;
            }
        }

        private void txtPrice5_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsNumber(e.KeyChar) & (Keys)e.KeyChar != Keys.Back & e.KeyChar != '.')
            {
                e.Handled = true;
            }
        }

        private void txtPrice6_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsNumber(e.KeyChar) & (Keys)e.KeyChar != Keys.Back & e.KeyChar != '.')
            {
                e.Handled = true;
            }
        }

        private void txtReOrderLevel_Leave(object sender, EventArgs e)
        {
            double amount = txtPrice6.Text.Equals("") ? 0 : Convert.ToDouble(txtPrice6.Text);
            txtPrice6.Text = amount.ToString();
        }

        private void txtReOrderLevel_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsNumber(e.KeyChar) & (Keys)e.KeyChar != Keys.Back & e.KeyChar != '.')
            {
                e.Handled = true;
            }
        }
    }
}
