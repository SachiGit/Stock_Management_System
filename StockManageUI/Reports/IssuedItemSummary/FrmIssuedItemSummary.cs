using StockManageBLL.Department;
using StockManageBLL.Item;
using StockManageBLL.Reports.IssuedItemSummary;
using StockManageDTO.Reports.IssuedItemSummary;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace StockManageUI.Reports.IssuedItemSummary
{
    public partial class FrmIssuedItemSummary : Form
    {
        IssuedItemSummaryBLL _issueditemsummaryBLLObject;
        DepartmentBLL _departmentBLLObject;
        ItemBLL _itemBLLObject;
        private string Fdate;
        private string Tdate;

        public FrmIssuedItemSummary()
        {
            InitializeComponent();
            _issueditemsummaryBLLObject = new IssuedItemSummaryBLL();
            _departmentBLLObject = new DepartmentBLL();
            _itemBLLObject = new ItemBLL();
        }

        private void FrmIssuedItemSummary_Load(object sender, EventArgs e)
        {
            Fdate = dateTimePicker1.Value.ToString("yyyy-MM-dd");
            Tdate = dateTimePicker2.Value.ToString("yyyy-MM-dd");

            LoadDepartment();
            LoadItems();

            LoadData(Fdate, Tdate, cmbDepartment.Text, cmbItemCode.Text);
        }

        private void LoadDepartment()
        {
            DataTable dt = _departmentBLLObject.LoadDepartmentList();

            #region  Remove extra columns
            string[] existingcol = { "id", "description" };
            foreach (string colName in existingcol)
            {
                dt.Columns.Remove(colName);
            }
            #endregion

            #region Add new row
            DataTable dtcopy = dt.Copy();
            DataRow _row = dtcopy.NewRow();
            _row["department"] = "All";
            dtcopy.Rows.InsertAt(_row, 0);
            #endregion

            cmbDepartment.DataSource = dtcopy;
            cmbDepartment.DisplayMember = "department";
        }

        private void LoadItems()
        {
            DataTable dt = _itemBLLObject.LoadItemList();

            #region  Remove extra columns
            string[] existingcol = { "id", "description", "unit", "price1", "price2", "price3", "price4", "price5", "price6", "reorderlevel" };
            foreach (string colName in existingcol)
            {
                dt.Columns.Remove(colName);
            }
            #endregion

            #region Add new row
            DataTable dtcopy = dt.Copy();
            DataRow _row = dtcopy.NewRow();
            _row["itemCode"] = "All";
            _row["itemName"] = "All";
            dtcopy.Rows.InsertAt(_row, 0);
            #endregion

            cmbItemCode.DataSource = dtcopy;
            cmbItemCode.DisplayMember = "itemCode";
        }

        private void LoadData(string Fdate, string Tdate, string Department, string ItemCode)
        {
            try
            {
                IssuedItemSummaryDataSet DS = _issueditemsummaryBLLObject.GetIssuedItemSummaryData(Fdate, Tdate, Department,ItemCode);

                IssuedItemSummaryReport _report = new IssuedItemSummaryReport();
                _report.SetDataSource(DS);
                _report.SetParameterValue("Fdate", Fdate);
                _report.SetParameterValue("Tdate", Tdate);
                _report.SetParameterValue("Department", Department);
                _report.SetParameterValue("ItemCode", ItemCode);
                crystalReportViewer1.ReportSource = _report;
            }
            catch (Exception ex)
            {
                Clipboard.SetText("Error Message : " + ex.Message + "/r/nTrace Message : " + ex.StackTrace.ToString());
                MessageBox.Show("Report Data Load Error");
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            Fdate = dateTimePicker1.Value.ToString("yyyy-MM-dd");
            Tdate = dateTimePicker2.Value.ToString("yyyy-MM-dd");

            LoadData(Fdate, Tdate, cmbDepartment.Text, cmbItemCode.Text);
        }
    }
}
