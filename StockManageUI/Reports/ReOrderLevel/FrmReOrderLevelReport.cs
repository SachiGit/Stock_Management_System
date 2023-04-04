using StockManageBLL.Reports.ReOrderLevel;
using StockManageDTO.Reports.ReOrderLevel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace StockManageUI.Reports.ReOrderLevel
{
    public partial class FrmReOrderLevelReport : Form
    {
        ReOrderLevelBLL _reorderlevelBLLObject;

        public FrmReOrderLevelReport()
        {
            InitializeComponent();
            _reorderlevelBLLObject = new ReOrderLevelBLL();
        }

        private void FrmReOrderLevelReport_Load(object sender, EventArgs e)
        {
            try
            {
                LoadData();
            }
            catch (Exception ex)
            {
                Clipboard.SetText("Error Message : " + ex.Message + "/r/nTrace Message : " + ex.StackTrace.ToString());
                MessageBox.Show("Report Data Load Error");
            }
        }

        private void LoadData()
        {
            ReOrderLevelDataSet DS = _reorderlevelBLLObject.GetReOrderLevelData();

            ReOrderLevelReport _report = new ReOrderLevelReport();
            _report.SetDataSource(DS);
            crystalReportViewer1.ReportSource = _report;
        }
    }
}
