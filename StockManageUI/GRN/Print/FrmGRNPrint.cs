using StockManageBLL.GRN.Print;
using StockManageDTO.GRN.Print;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace StockManageUI.GRN.Print
{
    public partial class FrmGRNPrint : Form
    {
        public string _serialNo;
        public string _printType;
        private GRNPrintBLL _printBLL;

        public FrmGRNPrint()
        {
            InitializeComponent();
            _printBLL = new GRNPrintBLL();
        }

        private void FrmGRNPrint_Load(object sender, EventArgs e)
        {
            LoadPrintData(_serialNo);
        }

        private void LoadPrintData(string _serialNo)
        {
            GRNPrintDataSet DS = _printBLL.GetGRNData(_serialNo);

            GRNPrintReport _print = new GRNPrintReport();
            _print.SetDataSource(DS);
            _print.SetParameterValue("PrintType", _printType);
            crystalReportViewer1.ReportSource = _print;
        }
    }
}
