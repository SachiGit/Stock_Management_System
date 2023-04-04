using StockManageBLL.Reports.GRN.Details;
using StockManageDTO.Reports.GRN.Details;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace StockManageUI.Reports.GRN.Details
{
    public partial class FrmGRNDetails : Form
    {
        GRNDetailsBLL _grnDetailsBLLObject;
        private string Fdate;
        private string Tdate;

        public FrmGRNDetails()
        {
            InitializeComponent();
            _grnDetailsBLLObject = new GRNDetailsBLL();
        }

        private void FrmGRNDetails_Load(object sender, EventArgs e)
        {
            Fdate = dateTimePicker1.Value.ToString("yyyy-MM-dd");
            Tdate = dateTimePicker2.Value.ToString("yyyy-MM-dd");

            LoadData(Fdate, Tdate);
        }

        private void LoadData(string Fdate, string Tdate)
        {
            try
            {
                GRNDetailsDataSet DS = _grnDetailsBLLObject.GetGRNDetails(Fdate, Tdate);

                GRNDetailsReport _report = new GRNDetailsReport();
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
