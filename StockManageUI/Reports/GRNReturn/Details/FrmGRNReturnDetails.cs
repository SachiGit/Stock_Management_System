using StockManageBLL.Reports.GRNReturn.Details;
using StockManageDTO.Reports.GRNReturn.Details;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace StockManageUI.Reports.GRNReturn.Details
{
    public partial class FrmGRNReturnDetails : Form
    {
        GRNReturnDetailsBLL _grnreturnDetailsBLLObject;
        private string Fdate;
        private string Tdate;

        public FrmGRNReturnDetails()
        {
            InitializeComponent();
            _grnreturnDetailsBLLObject = new GRNReturnDetailsBLL();
        }

        private void FrmGRNReturnDetails_Load(object sender, EventArgs e)
        {
            Fdate = dateTimePicker1.Value.ToString("yyyy-MM-dd");
            Tdate = dateTimePicker2.Value.ToString("yyyy-MM-dd");

            LoadData(Fdate, Tdate);
        }

        private void LoadData(string Fdate, string Tdate)
        {
            try
            {
                GRNReturnDetailsDataSet DS = _grnreturnDetailsBLLObject.GetGRNReturnDetails(Fdate, Tdate);

                GRNReturnDetailsReport _report = new GRNReturnDetailsReport();
                _report.SetDataSource(DS);
                _report.SetParameterValue("Fdate", Fdate);
                _report.SetParameterValue("Tdate", Tdate);
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

            LoadData(Fdate, Tdate);
        }
    }
}
