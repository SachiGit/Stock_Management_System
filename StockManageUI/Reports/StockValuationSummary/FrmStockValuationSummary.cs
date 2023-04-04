using StockManageBLL.Reports.StockValuationSummary;
using StockManageUI.Class;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace StockManageUI.Reports.StockValuationSummary
{
    public partial class FrmStockValuationSummary : Form
    {
        StockValuationSummaryBLL _stockvaluationsummaryBLLObject;
        DataTable dt = new DataTable ();
        DataTable dtreport = new DataTable();

        double totalopenStockQty = 0;
        double totalopenStockValue = 0;

        string FromDate;
        string ToDate;

        string prv_itemname = string.Empty;
        string curr_itemname = string.Empty;
        string prv_serial = string.Empty;
        string current_serial = string.Empty;

        Bitmap objBmp;

        public FrmStockValuationSummary()
        {
            InitializeComponent();
            _stockvaluationsummaryBLLObject = new StockValuationSummaryBLL();
        }

        private void FrmStockValuationSummary_Load(object sender, EventArgs e)
        {
            try
            {
                CustomizeDataTable();
                GenarateReport();
            }
            catch (Exception ex)
            {
                Clipboard.SetText("Error Message : " + ex.Message + "/r/nTrace Message : " + ex.StackTrace.ToString());
                MessageBox.Show("Report Data Load Error");
            }
        }

        private void CustomizeDataTable()
        {
            dtreport.Columns.Add("ItemCode");
            dtreport.Columns.Add("ItemName"); 
            dtreport.Columns.Add("Description");
            dtreport.Columns.Add("BalanceQty");
            dtreport.Columns.Add("Unit");
            dtreport.Columns.Add("BalanceValue");
        }

        private void GenarateReport()
        {
            FillHeaderLabel();

            if (dataGridView1.Rows.Count > 0)
            {
                dataGridView1.Rows.Clear();
            }

            totalopenStockQty = 0;
            totalopenStockValue = 0;

            DateTime FDate = dateTimePicker1.Value;
            DateTime TDate = dateTimePicker2.Value;

            FromDate = FDate.ToString("yyyy-MM-dd");
            TDate = TDate.AddDays(1);
            ToDate = TDate.ToString("yyyy-MM-dd");

            dt = _stockvaluationsummaryBLLObject.GetStockValuationSummaryData(FromDate,ToDate);
            DataTable distinctValues = SelectDistinct(dt, "ItemCode", "ItemName");

            if (dataGridView1 != null)
                dataGridView1.Rows.Clear();

            for (int i = 0; i < distinctValues.Rows.Count; i++)
            {
                string ItemCode = distinctValues.Rows[i]["ItemCode"].ToString();
                string ItemName = distinctValues.Rows[i]["ItemName"].ToString();
                CreateReportByItem(TDate, ItemCode, ItemName);
            }

            string[] totalrow = new string[11];
            totalrow[0] = "TOTAL"; // itemname
            totalrow[1] = null; // description
            totalrow[2] = null; // date
            totalrow[3] = totalopenStockQty.ToString("#,##0.0"); // 
            totalrow[4] = null; // 
            totalrow[5] = totalopenStockValue.ToString("#,##0.00"); //

            dataGridView1.Rows.Add(totalrow);

        }

        private void FillHeaderLabel()
        {
            try
            {
                DateTime FDate = dateTimePicker1.Value;
                DateTime TDate = dateTimePicker2.Value;

                FromDate = FDate.ToString("yyyy-MM-dd");
                ToDate = TDate.ToString("yyyy-MM-dd");

                lblcompanyname.Text = _stockvaluationsummaryBLLObject.GetCompanyname();
                lblmonthof.Text = string.Format("From Date : {0}  To Date : {1}", FromDate, ToDate);
            }
            catch (Exception ex)
            {
                Clipboard.SetText("Error Message : " + ex.Message + "/r/nTrace Message : " + ex.StackTrace.ToString());
                MessageBox.Show("Header Lable Data Load Error");
            }
        }

        #region DATASET HELPER
        private static DataTable SelectDistinct(DataTable SourceTable, params string[] FieldNames)
        {
            object[] lastValues;
            DataTable newTable;
            DataRow[] orderedRows;

            if (FieldNames == null || FieldNames.Length == 0)
                throw new ArgumentNullException("FieldNames");

            lastValues = new object[FieldNames.Length];
            newTable = new DataTable();

            foreach (string fieldName in FieldNames)
                newTable.Columns.Add(fieldName, SourceTable.Columns[fieldName].DataType);

            orderedRows = SourceTable.Select("", string.Join(", ", FieldNames));

            foreach (DataRow row in orderedRows)
            {
                if (!fieldValuesAreEqual(lastValues, row, FieldNames))
                {
                    newTable.Rows.Add(createRowClone(row, newTable.NewRow(), FieldNames));
                    setLastValues(lastValues, row, FieldNames);
                }
            }
            return newTable;
        }

        private static bool fieldValuesAreEqual(object[] lastValues, DataRow currentRow, string[] fieldNames)
        {
            bool areEqual = true;
            if (lastValues[0] == null || !lastValues[0].Equals(currentRow[fieldNames[0]]))
            {
                areEqual = false;
            }
            return areEqual;
        }

        private static DataRow createRowClone(DataRow sourceRow, DataRow newRow, string[] fieldNames)
        {
            foreach (string field in fieldNames)
                newRow[field] = sourceRow[field];

            return newRow;
        }

        private static void setLastValues(object[] lastValues, DataRow sourceRow, string[] fieldNames)
        {
            lastValues[0] = sourceRow[fieldNames[0]];
        }
        #endregion

        private void CreateReportByItem(DateTime sdate, string ItemCode, string ItemName)
        {
            string itemcode = ItemCode;
            string itemname = ItemName;
            string Description = string.Empty;
            string SerialNo = string.Empty;
            string unitofmesure = string.Empty;
            double openStockQty = 0.0d;
            double openStockValue = 0.0d;

            var results = from myRow in dt.AsEnumerable()
                          where myRow.Field<string>("ItemCode") == itemcode
                          orderby myRow.Field<DateTime>("Date")
                          select myRow;

            List<PurchaseTemp> purchase_temp = new List<PurchaseTemp>();
            int rowcount = 0;
            int counter = 0;

            foreach (DataRow ro in results)
            {
                DateTime gdate = Convert.ToDateTime(ro["Date"].ToString());
                if (gdate < sdate)
                {
                    rowcount += 1;
                }
            }

            foreach (DataRow row in results)
            {
                counter += 1;
                List<SalesTemp> sales_temp = new List<SalesTemp>();

                string TransactionType = row["Type"].ToString();
                double quantity = Convert.ToDouble(row["Quantity"]);//TODO check null
                double rate = Convert.ToDouble(row["Amount"]);
                double amount = (quantity * rate);
                Description = row["Description"].ToString();
                unitofmesure = row["uom"].ToString();
                SerialNo = row["Number"].ToString();

                if (TransactionType == "GRN" || TransactionType == "GRNRT")
                {
                    var ptemp = new PurchaseTemp();
                    ptemp.Qty = quantity;//TODO check null
                    ptemp.Rate = rate;
                    ptemp.Amount = amount;
                    purchase_temp.Add(ptemp);
                }

                double total = purchase_temp.Sum(c => c.Qty);

                if (TransactionType == "ISSU")
                {
                    if (total >= quantity)
                    {
                        var PTemp = new PurchaseTemp();
                        PTemp = purchase_temp[0];

                        while (quantity > PTemp.Qty)
                        {
                            var stemp = new SalesTemp();
                            quantity = quantity - PTemp.Qty;
                            stemp.Qty = PTemp.Qty;
                            stemp.Rate = PTemp.Rate;
                            stemp.Amount = stemp.Qty * PTemp.Rate;
                            sales_temp.Add(stemp);
                            purchase_temp.Remove(purchase_temp[0]);
                            PTemp = purchase_temp[0];
                        }

                        if (quantity == PTemp.Qty)
                        {
                            var stemp = new SalesTemp();
                            stemp.Qty = quantity;
                            stemp.Rate = PTemp.Rate;
                            stemp.Amount = stemp.Qty * PTemp.Rate;
                            sales_temp.Add(stemp);
                            purchase_temp.Remove(purchase_temp[0]);
                        }
                        else if (quantity < PTemp.Qty)
                        {
                            var stemp = new SalesTemp();
                            stemp.Qty = quantity;
                            stemp.Rate = PTemp.Rate;
                            stemp.Amount = stemp.Qty * PTemp.Rate;
                            sales_temp.Add(stemp);
                            quantity = PTemp.Qty - quantity;

                            var p_temp = new PurchaseTemp();
                            p_temp.Qty = quantity;
                            p_temp.Rate = PTemp.Rate;
                            p_temp.Amount = (quantity * PTemp.Rate);
                            purchase_temp[0] = p_temp;
                        }
                    }
                }

                DateTime maingdate = Convert.ToDateTime(row["Date"].ToString());
                if (maingdate < sdate)
                {
                    if (rowcount == counter)
                    {
                        for (int k = 0; k < purchase_temp.Count; k++)
                        {
                            openStockQty += purchase_temp[k].Qty;
                            openStockValue += purchase_temp[k].Amount;
                        }
                    }
                }
            }

            string shortitem;

            if (itemcode.Contains(':'))
            {
                string item_name = itemcode.Remove(itemcode.LastIndexOf(':') + 1);
                curr_itemname = item_name;

                shortitem = itemcode.Substring(itemcode.LastIndexOf(':') + 1);

                if (prv_itemname != curr_itemname)
                {
                    string[] innerrow = new string[11];
                    innerrow[0] = item_name;
                    dataGridView1.Rows.Add(innerrow);
                }

                prv_itemname = item_name;
            }
            else
            {
                shortitem = itemcode;
            }

            string[] rows1 = new string[11];
            rows1[0] = shortitem; // itemname
            rows1[1] = itemname; // description
            rows1[2] = Description;
            rows1[3] = openStockQty.ToString("0.0"); // type
            rows1[4] = unitofmesure; 
            rows1[5] = openStockValue.ToString("#,##0.00"); // sno

            if (!(rows1[3] == "0.0") && !(rows1[4] == "0.00"))
            {
                dataGridView1.Rows.Add(rows1);
                dtreport.Rows.Add(shortitem, itemname, Description, openStockQty.ToString("0.0"), unitofmesure, openStockValue.ToString("#,##0.00"));
            }

            totalopenStockQty += openStockQty;
            totalopenStockValue += openStockValue;
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            try
            {
                dtreport.Rows.Clear();
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
                xl.exportToExcel(dataGridView1, lblcompanyname.Text, lblReportName.Text, lblmonthof.Text);
            }
            catch (Exception ex)
            {
                Clipboard.SetText("Error Message : " + ex.Message + "/r/nTrace Message : " + ex.StackTrace.ToString());
                MessageBox.Show("Data Export To Excel Error");
            }
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            //string[] Headerdetail = new string[3];
            //Headerdetail[0] = lblcompanyname.Text;
            //Headerdetail[1] = lblReportName.Text;
            //Headerdetail[2] = lblmonthof.Text;

            //PrintStockValuationSummary _print = new PrintStockValuationSummary(dataGridView1, Headerdetail);
            //_print.PrintGrid();

            try
            {
                printPreviewDialog1.Document = printDocument1;
                printPreviewDialog1.Document.DefaultPageSettings.Landscape = true;
                printPreviewDialog1.ShowDialog();
            }
            catch (Exception ex)
            {
                Clipboard.SetText("Error Message : " + ex.Message + "/r/nTrace Message : " + ex.StackTrace.ToString());
                MessageBox.Show("Data Print Error");
            }
        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            StringFormat sf = new StringFormat();
            sf.Alignment = StringAlignment.Center;

            Bitmap bm = new Bitmap(this.dataGridView1.Width, this.dataGridView1.Height);
            dataGridView1.DrawToBitmap(bm, new Rectangle(0, 0, this.dataGridView1.Width, this.dataGridView1.Height));
            e.Graphics.DrawImage(bm, 50, 200);

            e.Graphics.DrawString(lblcompanyname.Text, new Font("Californian FB", 20, FontStyle.Bold), Brushes.Black, new Point(100, 50));
            e.Graphics.DrawString(lblReportName.Text, new Font("Californian FB", 30, FontStyle.Bold), Brushes.Black, new Point(100, 90));
            e.Graphics.DrawString(lblmonthof.Text, new Font("Californian FB", 20, FontStyle.Bold), Brushes.Black, new Point(100, 150));
        }

        private void printDocument1_BeginPrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            
        }
    }
}
