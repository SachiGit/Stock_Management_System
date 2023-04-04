using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace StockManageUI.Reports.StockValuationSummary
{
    public class PrintStockValuationSummary
    {
         #region Member Variables

        StringFormat strFormat; //Used to format the grid rows.
        ArrayList arrColumnLefts = new ArrayList();//Used to save left coordinates of columns
        ArrayList arrColumnWidths = new ArrayList();//Used to save column widths
        ArrayList columnSizes = new ArrayList();

        int iCellHeight = 0; //Used to get/set the datagridview cell height
        int iTotalWidth = 0; //
        int iRow = 0;//Used as counter
        bool bFirstPage = false; //Used to check whether we are printing first page
        bool bNewPage = false;// Used to check whether we are printing a new page
        int iHeaderHeight = 0; //Used for the header height
        int pagewidth = 0;
        int pageHeight = 0;
        int totalPage = 1;
        int pagecount = 1;

        private Font printFont;
        private Font HeaderFont;
        private Font ReportNameFont;
        private Font dateRangeFont;

        StringFormat numFormat;

        string company;
        string reportname;
        string printdate;

        #endregion

        PrintDocument printDocument;
        PrintDialog print_dialog;
        DataGridView dataGridView1;

        public PrintStockValuationSummary(DataGridView dgv, string[] arr)
        {
            printDocument = new PrintDocument();
            print_dialog = new PrintDialog();

            InitializeComponent();
            dataGridView1 = dgv;

            printFont = new Font("Arial", 6);
            HeaderFont = new Font("Arial", 10);
            ReportNameFont = new Font("Arial", 8);
            dateRangeFont = new Font("Arial", 8);
            company = arr[0];
            reportname = arr[1];
            printdate = arr[2];
        }

        private void InitializeComponent()
        {
            printDocument.BeginPrint += new PrintEventHandler(printDocument_BeginPrint);
            printDocument.PrintPage += new PrintPageEventHandler(printDocument_PrintPage);
        }

        public void PrintGrid()
        {
            print_dialog.Document = printDocument;
            print_dialog.UseEXDialog = true;

            //Get the document
            if (DialogResult.OK == print_dialog.ShowDialog())
            {
                pagewidth = printDocument.DefaultPageSettings.PaperSize.Width;
                pageHeight = printDocument.DefaultPageSettings.PaperSize.Height;
                SetPaperSizes();
                printDocument.DocumentName = "Print Stock Valution Report";
                printDocument.Print();
            }
            // printDocument.DefaultPageSettings.PaperSize = new PaperSize("A3", 1670, 1150);
            //   printDocument.DefaultPageSettings.Landscape = true;
            // printDocument.

            //printDocument.DefaultPageSettings.PrinterSettings.

            //Open the print preview dialog
            // PrintPreviewDialog objPPdialog = new PrintPreviewDialog();
            // objPPdialog.Document = printDocument;
            // objPPdialog.ShowDialog();
        }

        private void printDocument_BeginPrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            try
            {
                strFormat = new StringFormat();
                strFormat.Alignment = StringAlignment.Near;
                strFormat.LineAlignment = StringAlignment.Center;
                strFormat.Trimming = StringTrimming.EllipsisCharacter;
                //strFormat.FormatFlags = StringFormatFlags.NoWrap;

                numFormat = new StringFormat();
                numFormat.Alignment = StringAlignment.Far;
                numFormat.LineAlignment = StringAlignment.Center;
                numFormat.Trimming = StringTrimming.EllipsisCharacter;

                arrColumnLefts.Clear();
                arrColumnWidths.Clear();
                iCellHeight = 0;
                iRow = 0;
                bFirstPage = true;
                bNewPage = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void SetPaperSizes()
        {
            int longcolumn = 150;
            int mediumcolumn = 50;
            int shortcolumn = 35;
            int pagewith = 700;

            if (printDocument.DefaultPageSettings.Landscape)
            {
                longcolumn = (200 * pageHeight) / pagewith;
                mediumcolumn = (150 * pageHeight) / pagewith;
                shortcolumn = (50 * pageHeight) / pagewith;
            }
            else
            {
                longcolumn = (200 * pagewidth) / pagewith;
                mediumcolumn = (100 * pagewidth) / pagewith;
                shortcolumn = (50 * pagewidth) / pagewith;
            }

            columnSizes.Add(longcolumn); // item
            columnSizes.Add(longcolumn); // description
            columnSizes.Add(mediumcolumn);
            columnSizes.Add(shortcolumn);
            columnSizes.Add(mediumcolumn);

            // Calculating Total Widths
            //  iTotalWidth = 0;
            //int maxColWith = 0;
            //for (int k = 0; k < dataGridView1.Columns.Count; k++)
            //{

            //    if (!dataGridView1.Columns[k].Visible)
            //    {
            //        maxColWith += (int)columnSizes[k];
            //    }
            //}
            //   columnSizes[0] = (int)columnSizes[0] + maxColWith;

            int rowsPerPage = 29;
            int rowcounter = dataGridView1.Rows.Count;
            int res = 1;
            int res1 = 0;

            if (rowcounter > rowsPerPage)
            {
                res = rowcounter / rowsPerPage;
                res1 = rowcounter % rowsPerPage;
            }

            if (res1 == 0)
            {
                totalPage = res;
            }
            else
            {
                totalPage = res + 1;
            }
        }

        private void printDocument_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            try
            {
                //Set the left margin
                int iLeftMargin = e.MarginBounds.Left - 50;
                //Set the top margin
                int iTopMargin = e.MarginBounds.Top;
                //Whether more pages have to print or not
                bool bMorePagesToPrint = false;
                int iTmpWidth = 0;

                //For the first page to print set the cell width and header height
                if (bFirstPage)
                {
                    foreach (DataGridViewColumn GridCol in dataGridView1.Columns)
                    {
                        iHeaderHeight = (int)(e.Graphics.MeasureString(GridCol.HeaderText,
                                    printFont, iTmpWidth).Height) + 20;
                    }

                    SizeF size = new SizeF();
                    SizeF size1 = new SizeF();
                    SizeF size2 = new SizeF();

                    size = e.Graphics.MeasureString(company, HeaderFont);
                    int withofcompany = (int)size.Width;

                    size1 = e.Graphics.MeasureString(reportname, ReportNameFont);
                    int withofName = (int)size1.Width;

                    size2 = e.Graphics.MeasureString(printdate, dateRangeFont);
                    int withofdate = (int)size2.Width;

                    int leftbound = 0;
                    int leftbound1 = 0;
                    int leftbound2 = 0;

                    if (printDocument.DefaultPageSettings.Landscape)
                    {
                        leftbound = (pageHeight - withofcompany) / 2;
                        leftbound1 = (pageHeight - withofName) / 2;
                        leftbound2 = (pageHeight - withofdate) / 2;
                    }
                    else
                    {
                        leftbound = (pagewidth - withofcompany) / 2;
                        leftbound1 = (pagewidth - withofName) / 2;
                        leftbound2 = (pagewidth - withofdate) / 2;

                    }

                    int TitleMargin = 30;

                    e.Graphics.DrawString(company, HeaderFont, Brushes.Black, leftbound, TitleMargin);
                    e.Graphics.DrawString(reportname, ReportNameFont, Brushes.Black, leftbound1, TitleMargin + 20);
                    e.Graphics.DrawString(printdate, dateRangeFont, Brushes.Black, leftbound2, TitleMargin + 40);
                }
                //Loop till all the grid rows not get printed
                while (iRow <= dataGridView1.Rows.Count - 1)
                {
                    DataGridViewRow GridRow = dataGridView1.Rows[iRow];
                    //Set the cell height
                    iCellHeight = GridRow.Height;
                    int iCount = 0;
                    int colLeft = 25;

                    //Check whether the current page settings allo more rows to print
                    if ((iTopMargin - 50) + iCellHeight >= e.MarginBounds.Height + e.MarginBounds.Top)
                    {
                        bNewPage = true;
                        bFirstPage = false;
                        bMorePagesToPrint = true;
                        break;
                    }
                    else
                    {
                        if (bNewPage)
                        {
                            //columnSizes.Add(51);
                            // Draw Header
                            e.Graphics.DrawString("Page " + Convert.ToString(pagecount) + " of " + Convert.ToString(totalPage), printFont,
                                    Brushes.Black, 25, e.MarginBounds.Top -
                                    e.Graphics.MeasureString(Convert.ToString(totalPage), printFont, e.MarginBounds.Width).Height - 13);

                            String strDate = DateTime.Now.ToLongDateString() + " " + DateTime.Now.ToShortTimeString();
                            //Draw Date
                            //e.Graphics.DrawString(strDate, new Font(dataGridView1.Font, FontStyle.Bold),
                            //        Brushes.Black, e.MarginBounds.Left + (e.MarginBounds.Width -
                            //        e.Graphics.MeasureString(strDate, new Font(dataGridView1.Font,
                            //        FontStyle.Bold), e.MarginBounds.Width).Width), e.MarginBounds.Top -
                            //        e.Graphics.MeasureString("Customer Summary", new Font(new Font(dataGridView1.Font,
                            //        FontStyle.Bold), FontStyle.Bold), e.MarginBounds.Width).Height - 13);

                            //Draw Columns                 
                            iTopMargin = e.MarginBounds.Top;
                            foreach (DataGridViewColumn GridCol in dataGridView1.Columns)
                            {
                                if (GridCol.Visible)
                                {
                                    arrColumnLefts.Add(iLeftMargin);

                                    e.Graphics.FillRectangle(new SolidBrush(Color.LightGray),
                                       new Rectangle(colLeft, iTopMargin,
                                       (int)columnSizes[iCount], iHeaderHeight));

                                    e.Graphics.DrawRectangle(Pens.Black,
                                        new Rectangle(colLeft, iTopMargin,
                                        (int)columnSizes[iCount], iHeaderHeight));

                                    e.Graphics.DrawString(GridCol.HeaderText, printFont,
                                        new SolidBrush(GridCol.InheritedStyle.ForeColor),
                                        new RectangleF(colLeft, iTopMargin,
                                        (int)columnSizes[iCount], iHeaderHeight), strFormat);

                                    colLeft += (int)columnSizes[iCount];
                                    // arrColumnWidths.Add(iTmpWidth);
                                    // iLeftMargin +=(int)arrColumnLefts[iCount];
                                }
                                iCount++;
                            }

                            bNewPage = false;
                            iTopMargin += iHeaderHeight;
                        }
                        iCount = 0;
                        //Draw Columns Contents 
                        int icelLeft = 25;
                        foreach (DataGridViewCell Cel in GridRow.Cells)
                        {
                            if (Cel.Visible)
                            {
                                if (Cel.Value != null)
                                {
                                    IEnumerable<string> headers = new List<string> { "ItemCode", "ItemName", "Unit" };

                                    if (headers.Contains(Cel.OwningColumn.HeaderText))
                                    {
                                        if (Cel.Value.ToString() == "TOTAL")
                                        {
                                            e.Graphics.DrawString(Cel.Value.ToString(), printFont,
                                                        new SolidBrush(Cel.InheritedStyle.ForeColor),
                                                        new RectangleF(icelLeft, (float)iTopMargin,
                                                        (int)columnSizes[iCount], (float)iCellHeight), strFormat);
                                        }
                                        else
                                        {
                                            Font totalfont;
                                            e.Graphics.DrawString(Cel.Value.ToString(), printFont,
                                                        new SolidBrush(Cel.InheritedStyle.ForeColor),
                                                        new RectangleF(icelLeft, (float)iTopMargin,
                                                        (int)columnSizes[iCount], (float)iCellHeight), strFormat);
                                        }
                                    }
                                    else
                                    {
                                        e.Graphics.DrawString(Cel.Value.ToString(), printFont,
                                                 new SolidBrush(Cel.InheritedStyle.ForeColor),
                                                 new RectangleF(icelLeft, (float)iTopMargin,
                                                 (int)columnSizes[iCount], (float)iCellHeight), numFormat);
                                    }
                                }
                                //Drawing Cells Borders 
                                e.Graphics.DrawRectangle(Pens.Black, new Rectangle(icelLeft,
                                        iTopMargin, (int)columnSizes[iCount], iCellHeight));

                                icelLeft += (int)columnSizes[iCount];
                            }
                            iCount++;
                        }
                    }
                    iRow++;
                    iTopMargin += iCellHeight;
                }

                //If more lines exist, print another page.
                if (bMorePagesToPrint)
                {
                    e.HasMorePages = true;
                    pagecount += 1;
                }
                else
                {
                    e.HasMorePages = false;
                }
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
