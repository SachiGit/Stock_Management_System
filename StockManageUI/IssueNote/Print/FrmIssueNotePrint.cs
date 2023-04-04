using StockManageBLL.IssueNote.Print;
using StockManageDTO.IssueNote.Print;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace StockManageUI.IssueNote.Print
{
    public partial class FrmIssueNotePrint : Form
    {
        public string _serialNo;
        public string _printType;
        private IssueNotePrintBLL _printBLL;

        public FrmIssueNotePrint()
        {
            InitializeComponent();
            _printBLL = new IssueNotePrintBLL();
        }

        private void FrmIssueNotePrint_Load(object sender, EventArgs e)
        {
            LoadPrintData(_serialNo);
        }

        private void LoadPrintData(string _serialNo)
        {
            IssueNoteDataSet DS = _printBLL.GetIssueNoteData(_serialNo);

            IssueNotePrintReport _print = new IssueNotePrintReport();
            _print.SetDataSource(DS);
            _print.SetParameterValue("PrintType", _printType);
            crystalReportViewer1.ReportSource = _print;
        }
    }
}
