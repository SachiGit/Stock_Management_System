using StockManageBLL.GRNReturn.Print;
using StockManageDTO.GRNReturn.Print;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace StockManageUI.GRNReturn.Print
{
    public partial class FrmGRNReturnPrint : Form
    {
        public string _serialNo;
        private GRNReturnPrintBLL _printBLL;

        public FrmGRNReturnPrint()
        {
            InitializeComponent();
            _printBLL = new GRNReturnPrintBLL();
        }

        private void FrmGRNReturnPrint_Load(object sender, EventArgs e)
        {
            LoadPrintData(_serialNo);
        }

        private void LoadPrintData(string _serialNo)
        {
            GRNReturnPrintDataSet DS = _printBLL.GetGRNData(_serialNo);

            GRNReturnPrintReport _print = new GRNReturnPrintReport();
            _print.SetDataSource(DS);
            crystalReportViewer1.ReportSource = _print;
        }
    }
}
