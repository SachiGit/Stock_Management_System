﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace StockManageUI.Class
{
    public class DataGridViewMultiColumnComboColumn : DataGridViewComboBoxColumn
    {
        public DataGridViewMultiColumnComboColumn()
        {
            //Set the type used in the DataGridView
            this.CellTemplate = new DataGridViewMultiColumnComboCell();
        }
    }
    public class DataGridViewMultiColumnComboCell : DataGridViewComboBoxCell
    {
        public override Type EditType
        {
            get
            {
                return typeof(DataGridViewMultiColumnComboEditingControl);
                //DataGridViewRow rowToSelect = new DataGridViewRow();
                //rowToSelect.Selected = true;

                //rowToSelect.Cells[0].Selected = true;
                //this.dgvJobList.CurrentCell = rowToSelect.Cells[0];
            }
        }
        public override void InitializeEditingControl(int rowIndex, object initialFormattedValue, DataGridViewCellStyle dataGridViewCellStyle)
        {
            base.InitializeEditingControl(rowIndex, initialFormattedValue, dataGridViewCellStyle);
            DataGridViewMultiColumnComboEditingControl ctrl = DataGridView.EditingControl as DataGridViewMultiColumnComboEditingControl;
            ctrl.ownerCell = this;
        }
    }
    public class DataGridViewMultiColumnComboEditingControl : DataGridViewComboBoxEditingControl
    {
        /**************************************************************************************************/
        //const int fixedAlignColumnSize = 150; //TODO: change to be configurable for every column...
        int fixedAlignColumnSize = 150; //TODO: change to be configurable for every column... // udesh 

        const int lineWidth = 1; //TODO: make this line width configurable
        public DataGridViewMultiColumnComboCell ownerCell = null;
        /**************************************************************************************************/
        public DataGridViewMultiColumnComboEditingControl()
            : base()
        {
            this.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.DropDownStyle = ComboBoxStyle.DropDownList;
            DropDownHeight = 250;
            //DropDownWidth = 400;
        }
        /**************************************************************************************************/
        protected override void OnDrawItem(System.Windows.Forms.DrawItemEventArgs e)
        {
            try
            {
                Rectangle rec = new Rectangle(e.Bounds.X, e.Bounds.Y, e.Bounds.Width, e.Bounds.Height);
                fixedAlignColumnSize = e.Bounds.Width / 2;
                DataGridViewMultiColumnComboColumn column = ownerCell.OwningColumn as DataGridViewMultiColumnComboColumn;

                DataTable valuesTbl = column.DataSource as DataTable;
                string joinByField = column.ValueMember;
                SolidBrush NormalText = new SolidBrush(System.Drawing.SystemColors.ControlText);

                //If there is an item
                if (e.Index > -1)
                {
                    DataRowView currentRow = Items[e.Index] as DataRowView;

                    if (currentRow != null)
                    {
                        DataRow row = currentRow.Row;

                        string currentText = GetItemText(Items[e.Index]);

                        //first redraw the normal while background
                        SolidBrush normalBack = new SolidBrush(Color.White); //TODO: fix to be system edit box background
                        //DropDownHeight = 250;
                        ////DropDownWidth = 400;
                        if (DropDownHeight > 250)
                        {
                            DropDownHeight = 250;
                        }
                        e.Graphics.FillRectangle(normalBack, rec);
                        if (DroppedDown && !(Margin.Top == rec.Top))
                        {
                            int currentOffset = rec.Left;

                            SolidBrush HightlightedBack = new SolidBrush(System.Drawing.SystemColors.Highlight);
                            if ((e.State & DrawItemState.Selected) == DrawItemState.Selected)
                            {
                                //draw selected color background
                                e.Graphics.FillRectangle(HightlightedBack, rec);
                            }

                            bool addBorder = false;

                            object valueItem;
                            foreach (object dataRowItem in row.ItemArray)
                            {
                                valueItem = dataRowItem;
                                string value = dataRowItem.ToString(); //TODO: support for different types!!!

                                if (addBorder)
                                {
                                    //draw dividing line
                                    //currentOffset ++; 
                                    SolidBrush gridBrush = new SolidBrush(Color.Gray); //TODO: make the border color configurable
                                    long linesNum = lineWidth;
                                    while (linesNum > 0)
                                    {
                                        linesNum--;
                                        Point first = new Point(rec.Left + currentOffset, rec.Top);
                                        Point last = new Point(rec.Left + currentOffset, rec.Bottom);
                                        e.Graphics.DrawLine(new Pen(gridBrush), first, last);
                                        currentOffset++;
                                    }
                                }
                                else
                                    addBorder = true;

                                SizeF extent = e.Graphics.MeasureString(value, e.Font);
                                decimal width = (decimal)extent.Width;
                                //measure the string that we are goin to draw and cut it with wrapping if too large
                                Rectangle textRec = new Rectangle(currentOffset, rec.Y, (int)decimal.Ceiling(width), 250);

                                //now draw the relevant to this column value text
                                if ((e.State & DrawItemState.Selected) == DrawItemState.Selected)
                                {
                                    //draw selected
                                    SolidBrush HightlightedText = new SolidBrush(System.Drawing.SystemColors.HighlightText);
                                    //now redraw the backgrond it order to wrap the previous field if was too large
                                    e.Graphics.FillRectangle(HightlightedBack, currentOffset, rec.Y, fixedAlignColumnSize, extent.Height);
                                    //draw text as is 
                                    e.Graphics.DrawString(value, e.Font, HightlightedText, textRec);
                                }
                                else
                                {
                                    //now redraw the backgrond it order to wrap the previous field if was too large
                                    e.Graphics.FillRectangle(normalBack, currentOffset, rec.Y, fixedAlignColumnSize, extent.Height);
                                    //draw text as is 
                                    e.Graphics.DrawString(value, e.Font, NormalText, textRec);
                                }
                                //advance the offset to the next position
                                currentOffset += fixedAlignColumnSize;
                            }
                        }
                        else
                            //if happens when the combo is closed, draw single standard item view
                            e.Graphics.DrawString(currentText, e.Font, NormalText, rec);
                        // DropDownWidth = 400;
                    }
                }
                else
                {
                    DropDownHeight = 250;
                }

            }
            catch (Exception)
            {
            }

        }

        /**************************************************************************************************/
    }
}
