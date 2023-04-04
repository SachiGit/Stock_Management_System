using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace StockManageUI.Class
{
    public class DataGridStyles
    {
        public static void styleGridList_2014(DataGridView dgv)
        {
            dgv.RowTemplate.Height = 18;
            dgv.RowsDefaultCellStyle.BackColor = Color.White;
            //dgv.AlternatingRowsDefaultCellStyle.BackColor = Color.AliceBlue;
            dgv.AlternatingRowsDefaultCellStyle.BackColor = Color.GhostWhite;

            dgv.CellBorderStyle = DataGridViewCellBorderStyle.SingleVertical;
            //dataGridView1.DefaultCellStyle.SelectionBackColor = Color.BlueViolet;
            dgv.DefaultCellStyle.SelectionBackColor = Color.LightSteelBlue;
            dgv.DefaultCellStyle.SelectionForeColor = Color.Black;
            dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgv.AllowUserToResizeRows = false;
            //dataGridView1.Columns["advance_amount"].HeaderText = "Advance";    

            //======== set column header style===============
            dgv.EnableHeadersVisualStyles = false;
            dgv.ColumnHeadersDefaultCellStyle.BackColor = Color.DimGray;
            dgv.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            //==============================================
        }

        public static void styleGridList_new(DataGridView dgv)
        {

            dgv.RowTemplate.Height = 18;
            dgv.RowsDefaultCellStyle.BackColor = Color.White;
            //dgv.AlternatingRowsDefaultCellStyle.BackColor = Color.AliceBlue;
            dgv.AlternatingRowsDefaultCellStyle.BackColor = Color.GhostWhite;

            dgv.CellBorderStyle = DataGridViewCellBorderStyle.SingleVertical;
            //dataGridView1.DefaultCellStyle.SelectionBackColor = Color.BlueViolet;
            dgv.DefaultCellStyle.SelectionBackColor = Color.Gray;
            dgv.DefaultCellStyle.SelectionForeColor = Color.White;
            dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgv.AllowUserToResizeRows = false;
            //dataGridView1.Columns["advance_amount"].HeaderText = "Advance";    

            //======== set column header style===============
            dgv.EnableHeadersVisualStyles = false;
            dgv.ColumnHeadersDefaultCellStyle.BackColor = Color.DimGray;
            dgv.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            //==============================================
        }

        public static void styleGrid(DataGridView dgv)
        {

            dgv.RowTemplate.Height = 18;
            dgv.RowsDefaultCellStyle.BackColor = Color.White;
            //dataGridView1.RowsDefaultCellStyle.BackColor = Color.AliceBlue;
            //dataGridView1.AlternatingRowsDefaultCellStyle.BackColor = Color.LightSteelBlue;
            dgv.AlternatingRowsDefaultCellStyle.BackColor = Color.Beige;
            dgv.CellBorderStyle = DataGridViewCellBorderStyle.SingleVertical;

            dgv.DefaultCellStyle.SelectionBackColor = Color.LightSlateGray;
            dgv.DefaultCellStyle.SelectionForeColor = Color.White;

            dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgv.AllowUserToResizeRows = false;

            //======== set column header style===============
            dgv.EnableHeadersVisualStyles = false;
            dgv.ColumnHeadersDefaultCellStyle.BackColor = Color.DimGray;
            dgv.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            //==============================================

            //====== Disable Datagrid Sort =================
            disable_sort(dgv);
            //==============================================
        }

        public static void disable_sort(DataGridView dgv)
        {
            //------------disable datagrid sort option-------------------- 
            foreach (DataGridViewColumn column in dgv.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
            }
            //----------------------------------------------------------
        }

        public static void styleGridList_resizeRow(DataGridView dgv)
        {
            dgv.RowTemplate.Height = 22;
            dgv.RowsDefaultCellStyle.BackColor = Color.White;
            //dgv.AlternatingRowsDefaultCellStyle.BackColor = Color.AliceBlue;
            dgv.AlternatingRowsDefaultCellStyle.BackColor = Color.GhostWhite;

            dgv.CellBorderStyle = DataGridViewCellBorderStyle.SingleVertical;
            //dataGridView1.DefaultCellStyle.SelectionBackColor = Color.BlueViolet;
            dgv.DefaultCellStyle.SelectionBackColor = Color.LightSteelBlue;
            dgv.DefaultCellStyle.SelectionForeColor = Color.Black;
            dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgv.AllowUserToResizeRows = false;
            //dataGridView1.Columns["advance_amount"].HeaderText = "Advance";    

            //======== set column header style===============
            dgv.EnableHeadersVisualStyles = false;
            dgv.ColumnHeadersDefaultCellStyle.BackColor = Color.DimGray;
            dgv.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            //==============================================
        }
    }
}
