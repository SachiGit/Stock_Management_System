using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace StockManageUI.Class
{
    public class ExportToExcel
    {
        public void exportToExcel(DataGridView Dgv, string CompanyName, string ReportName, string DateRange)
        {

            Microsoft.Office.Interop.Excel._Application app = new Microsoft.Office.Interop.Excel.Application();
            // creating new WorkBook within Excel application
            Microsoft.Office.Interop.Excel._Workbook workbook = app.Workbooks.Add(Type.Missing);


            // creating new Excelsheet in workbook
            Microsoft.Office.Interop.Excel._Worksheet worksheet = null;


            // see the excel sheet behind the program

            app.Visible = true;


            // get the reference of first sheet. By default its name is Sheet1.
            // store its reference to worksheet
            try
            {
                //Fixed:(Microsoft.Office.Interop.Excel.Worksheet)
                worksheet = (Microsoft.Office.Interop.Excel.Worksheet)workbook.Sheets["Sheet1"];
                worksheet = (Microsoft.Office.Interop.Excel.Worksheet)workbook.ActiveSheet;
                ((Microsoft.Office.Interop.Excel.Range)worksheet.Cells[1, 1]).EntireColumn.ColumnWidth = 35;
                ((Microsoft.Office.Interop.Excel.Range)worksheet.Cells[1, 2]).EntireColumn.ColumnWidth = 20;

                // changing the name of active sheet
                worksheet.Name = "Inventory Control System";
                worksheet.Cells[1, 4] = CompanyName;
                worksheet.Cells[2, 4] = ReportName;
                worksheet.Cells[3, 4] = DateRange;

                // storing header part in Excel
                for (int i = 1; i < Dgv.Columns.Count + 1; i++)
                {
                    worksheet.Cells[5, i] = Dgv.Columns[i - 1].HeaderText;
                }

                // storing Each row and column value to excel sheet
                for (int i = 0; i < Dgv.Rows.Count; i++)
                {
                    for (int j = 0; j < Dgv.Columns.Count; j++)
                    {
                        if (Dgv.Rows[i].Cells[j].Value != null)
                        {
                            // worksheet.Cells[i + 4, j + 1] = dataGridView1.Rows[i].Cells[j].Value.ToString();

                            worksheet.Cells[i + 8, j + 1] = Dgv.Rows[i].Cells[j].Value.ToString();

                        }
                    }
                }

                string fileName = String.Empty;
                SaveFileDialog savefile = new SaveFileDialog();
                savefile.Filter = "Excel files |*.xls";
                savefile.FilterIndex = 2;
                savefile.RestoreDirectory = true;

                if (savefile.ShowDialog() == DialogResult.OK)
                {
                    fileName = savefile.FileName;

                    workbook.SaveAs(fileName, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlExclusive, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);

                }

            }
            catch (System.Exception)
            {

            }
            finally
            {
                workbook = null;
                app = null;
            }
        }
    }
}
