using StockManageBLL.Reports.ItemDetail;
using StockManageDTO.Reports.ItemDetail;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace StockManageUI.Reports.ItemDetail
{
    public partial class FrmItemDetail : Form
    {
        ItemDetailBLL _itemDetailBLLObject;

        public FrmItemDetail()
        {
            InitializeComponent();
            _itemDetailBLLObject = new ItemDetailBLL();
        }

        private void FrmItemDetail_Load(object sender, EventArgs e)
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
            ItemDetailDataSet DS = _itemDetailBLLObject.GetItemDetailData();

            ItemDetailReport _report = new ItemDetailReport();
            _report.SetDataSource(DS);
            crystalReportViewer1.ReportSource = _report;
        }
    }
}
