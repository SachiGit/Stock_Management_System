using StockManageBLL.Reports.FinalReport;
using StockManageUI.Class;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace StockManageUI.Reports.FinalReport
{
    public partial class FrmFinalReport : Form
    {
        FinalReportBLL _finalreportBLLObject;
        private string FromDate;
        private string ToDate;

        public FrmFinalReport()
        {
            InitializeComponent();
            _finalreportBLLObject = new FinalReportBLL();
        }

        private void FrmFinalReport_Load(object sender, EventArgs e)
        {
            try
            {                
                GenarateReport();
            }
            catch (Exception ex)
            {
                Clipboard.SetText("Error Message : " + ex.Message + "/r/nTrace Message : " + ex.StackTrace.ToString());
                MessageBox.Show("Report Data Load Error");
            }
        }

        private void GenarateReport()
        {
            DataGridStyles.styleGridList_2014(dataGridView1);
            FillHeaderLabel();
            LoadReportData(FromDate, ToDate);
            CustomizeGridData();
        }

        private void FillHeaderLabel()
        {
            try
            {
                DateTime FDate = dateTimePicker1.Value;
                DateTime TDate = dateTimePicker2.Value;

                FromDate = FDate.ToString("yyyy-MM-dd");
                ToDate = TDate.ToString("yyyy-MM-dd");

                lblcompanyname.Text = _finalreportBLLObject.GetCompanyname();
                lblDate.Text = string.Format("From Date : {0}  To Date : {1}", FromDate, ToDate);
            }
            catch (Exception ex)
            {
                Clipboard.SetText("Error Message : " + ex.Message + "/r/nTrace Message : " + ex.StackTrace.ToString());
                MessageBox.Show("Header Lable Data Load Error");
            }
        }

        private void LoadReportData(string Fdate, string Tdate)
        {
            dataGridView1.DataSource = _finalreportBLLObject.GetFinalReportData(Fdate, Tdate);
        }

        private void CustomizeGridData()
        {
            dataGridView1.Columns["GRN Qty"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            dataGridView1.Columns["GRN Rate"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dataGridView1.Columns["GRN Rate"].DefaultCellStyle.Format = "N2";

            dataGridView1.Columns["GRN Amount"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dataGridView1.Columns["GRN Amount"].DefaultCellStyle.Format = "N2";

            dataGridView1.Columns["GRN Return Qty"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            dataGridView1.Columns["GRN Return Rate"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dataGridView1.Columns["GRN Return Rate"].DefaultCellStyle.Format = "N2";

            dataGridView1.Columns["GRN Return Amount"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dataGridView1.Columns["GRN Return Amount"].DefaultCellStyle.Format = "N2";

            dataGridView1.Columns["Issued Qty"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            dataGridView1.Columns["Issued Rate"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dataGridView1.Columns["Issued Rate"].DefaultCellStyle.Format = "N2";

            dataGridView1.Columns["Issued Amount"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dataGridView1.Columns["Issued Amount"].DefaultCellStyle.Format = "N2";

            dataGridView1.Columns["Balance Qty"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            dataGridView1.Columns["Balance Amount"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dataGridView1.Columns["Balance Amount"].DefaultCellStyle.Format = "N2";
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            try
            {
                GenarateReport();
            }
            catch (Exception ex)
            {
                Clipboard.SetText("Error Message : " + ex.Message + "/r/nTrace Message : " + ex.StackTrace.ToString());
                MessageBox.Show("Report Data Load Error");
            }
        }

        private void btnExcel_Click(object sender, EventArgs e)
        {
            try
            {
                ExportToExcel xl = new ExportToExcel();
                xl.exportToExcel(dataGridView1, lblcompanyname.Text, lblReportName.Text, lblDate.Text);
            }
            catch (Exception ex)
            {
                Clipboard.SetText("Error Message : " + ex.Message + "/r/nTrace Message : " + ex.StackTrace.ToString());
                MessageBox.Show("Data Export To Excel Error");
            }
        }
    }
}
