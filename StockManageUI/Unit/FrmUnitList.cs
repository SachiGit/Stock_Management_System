using StockManageBLL.Unit;
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

namespace StockManageUI.Unit
{
    public partial class FrmUnitList : Form
    {
        UnitBLL _unitBLLObject;

        public FrmUnitList()
        {
            InitializeComponent();
            _unitBLLObject = new UnitBLL();
        }

        private void FrmUnitList_Load(object sender, EventArgs e)
        {
            try
            {
                DataGridStyles.styleGridList_2014(dataGridView1);
                LoadUnitList();
            }
            catch (Exception ex)
            {
                Clipboard.SetText(string.Format("Error Message : {0} \r\nTrace Message : {1}", ex.Message, ex.StackTrace));
                MessageBox.Show("Unit List Load Error");
            }
        }

        private void LoadUnitList()
        {
            dataGridView1.DataSource = _unitBLLObject.LoadUnitList();
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            FrmEditUnitList _unit = new FrmEditUnitList();
            _unit.ShowDialog();
            btnRefresh.PerformClick(); 
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            string ID = dataGridView1.SelectedCells[0].Value.ToString();

            FrmEditUnitList _unit = new FrmEditUnitList();
            _unit.ID = ID;
            _unit.btnSaveUpdate.Text = "Update";
            _unit.ShowDialog();
            btnRefresh.PerformClick();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            bool _isDelete = false;

            try
            {
                if (MessageBox.Show(string.Format("Do you want to delete Unit {0} ? ", dataGridView1.SelectedCells[1].Value.ToString()), "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    _isDelete = _unitBLLObject.DeleteUnitDetails(int.Parse(dataGridView1.SelectedCells[0].Value.ToString()));
                }

                if (_isDelete)
                {
                    MessageBox.Show("Successfully Deleted.", "Unit Data", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    btnRefresh.PerformClick();
                }
            }
            catch (Exception ex)
            {
                Clipboard.SetText(string.Format("Error Message : {0} \r\nTrace Message : {1}", ex.Message, ex.StackTrace));
                MessageBox.Show("Unit Data Delete Error");
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            try
            {
                LoadUnitList();
            }
            catch (Exception ex)
            {
                Clipboard.SetText(string.Format("Error Message : {0} \r\nTrace Message : {1}", ex.Message, ex.StackTrace));
                MessageBox.Show("Unit List Load Error");
            }
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            string ID = dataGridView1.SelectedCells[0].Value.ToString();

            FrmEditUnitList _unit = new FrmEditUnitList();
            _unit.ID = ID;
            _unit.btnSaveUpdate.Text = "Update";
            _unit.ShowDialog();
            btnRefresh.PerformClick();
        }


    }
}
