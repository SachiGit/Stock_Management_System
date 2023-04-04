using StockManageBLL.Common;
using StockManageBLL.GRN;
using StockManageBLL.Item;
using StockManageBLL.Vendor;
using StockManageDTO.Common;
using StockManageDTO.GRN;
using StockManageUI.Class;
using StockManageUI.GRN.Print;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StockManageUI.GRN
{
    public partial class FrmGRN : Form
    {
        #region Layer Objects
        private GRNBLL _GRNBLL;
        private GRNDTO _Nexprevious;
        private ItemBLL _itemBLLObject;
        private VendorBLL _vendorBLLObject;
        #endregion

        #region Form variables & Objects
        private bool _isInLoadNext = false;
        private bool _isInDelete = false;
        private bool _isInFormLoad = false;
        private bool _istxtItemCode = false;
        private bool _isinloadcmbName = false;
        private string getBeforeName;
        #endregion

        #region Variables for serial number
        private string _serialMethod;
        private string _vendorCustomerLoadMethod;
        private string[] _serialNumberList = new string[3];
        #endregion

        #region Enum Objects
        private enum ButtonType
        {
            SaveAndNew,
            SaveAndClose,
            DeleteRow,
            Print
        }

        private enum ClearType
        {
            FromButton,
            FromNextPrevious
        }
        #endregion

        private DataGridViewMultiColumnComboColumn GridComboItemList;
        private DataGridViewMultiColumnComboColumn GridComboItemRateList;
        private DataGridViewMultiColumnComboColumn GridComboUnitList;

        public FrmGRN()
        {
            InitializeComponent();
            _GRNBLL = new GRNBLL();
            _itemBLLObject = new ItemBLL();
            _vendorBLLObject = new VendorBLL();
        }

        private void FrmGRN_Load(object sender, EventArgs e)
        {
            try
            {
                InitializeForm();
                LoadFormData();
                SetFormStartDetails();
            }
            catch (Exception ex)
            {
                Clipboard.SetText("Error Message : " + ex.Message + "/r/nTrace Message : " + ex.StackTrace.ToString());
                MessageBox.Show("GRN Form Data Load Error");
            }
            DataGridStyles.styleGrid(dataGridView1);
        }

        private void InitializeForm()
        {
            _GRNBLL.SetFormBehaviour(CommonBLL.FormType.GRN);
        }

        private void LoadFormData()
        {
            CreateGrid();
            InitializeSequence();
        }

        private void CreateGrid()
        {
            GridComboItemList = new DataGridViewMultiColumnComboColumn()
            {
                CellTemplate = new DataGridViewMultiColumnComboCell(),
                DisplayStyle = DataGridViewComboBoxDisplayStyle.Nothing
            };
            dataGridView1.Columns.Insert(0, GridComboItemList);
            dataGridView1.Columns[0].Width = 200;

            dataGridView1.Columns.Add("ItemName", "ItemName");
            dataGridView1.Columns["ItemName"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridView1.Columns["ItemName"].Width = 150;
            dataGridView1.Columns["ItemName"].SortMode = DataGridViewColumnSortMode.NotSortable;

            dataGridView1.Columns.Add("Description", "Description");
            dataGridView1.Columns["Description"].Width = 250;
            dataGridView1.Columns["Description"].SortMode = DataGridViewColumnSortMode.NotSortable;
            this.dataGridView1.DefaultCellStyle.WrapMode = DataGridViewTriState.False;
            dataGridView1.Columns["Description"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView1.Columns["Description"].MinimumWidth = 200;

            dataGridView1.Columns.Add("Quantity", "Qty");
            dataGridView1.Columns["Quantity"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView1.Columns["Quantity"].Width = 60;
            dataGridView1.Columns["Quantity"].SortMode = DataGridViewColumnSortMode.NotSortable;

            //dataGridView1.Columns.Add("Rate", "Rate");
            ////dataGridView1.Columns["Rate"].ReadOnly = true;
            //dataGridView1.Columns["Rate"].Width = 100;
            //dataGridView1.Columns["Rate"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            //dataGridView1.Columns["Rate"].SortMode = DataGridViewColumnSortMode.NotSortable;

            dataGridView1.Columns.Add("Amount", "Amount");
            //dataGridView1.Columns["Amount"].ReadOnly = true;
            dataGridView1.Columns["Amount"].Width = 100;
            dataGridView1.Columns["Amount"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dataGridView1.Columns["Amount"].DefaultCellStyle.Format = "##,0";
            this.dataGridView1.Columns["Amount"].ValueType = typeof(decimal);
            dataGridView1.Columns["Amount"].SortMode = DataGridViewColumnSortMode.NotSortable;

            dataGridView1.Columns.Add("up", "up");
            dataGridView1.Columns["up"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dataGridView1.Columns["up"].Width = 80;
            dataGridView1.Columns["up"].ReadOnly = true;
            dataGridView1.Columns["up"].Visible = false;

            GridComboItemRateList = new DataGridViewMultiColumnComboColumn()
            {
                CellTemplate = new DataGridViewMultiColumnComboCell(),
                Name = "Rate",
                DisplayStyle = DataGridViewComboBoxDisplayStyle.Nothing
            };
            const int _itemratePosition = 4;
            dataGridView1.Columns.Insert(_itemratePosition, GridComboItemRateList);
            dataGridView1.Columns[_itemratePosition].Width = 100;
            dataGridView1.Columns[_itemratePosition].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            GridComboUnitList = new DataGridViewMultiColumnComboColumn()
            {
                CellTemplate = new DataGridViewMultiColumnComboCell(),
                Name = "Unit",
                DisplayStyle = DataGridViewComboBoxDisplayStyle.Nothing
            };
            const int _unitPosition = 7;
            dataGridView1.Columns.Insert(_unitPosition, GridComboUnitList);
            dataGridView1.Columns[_unitPosition].Width = 100;

            CreateGridRows();
        }

        private void CreateGridRows()
        {
            dataGridView1.Rows.Clear();

            for (int i = 0; i < 50; i++)
            {
                dataGridView1.Rows.Add();
            }
        }

        private void InitializeSequence()
        {
            InitializeSerialNumber();
            InitializeVendor();
            InitializeItems();
            InitializeUnit();
        }

        private void InitializeSerialNumber()
        {
            PrepareSerialNumberList();
            LoadSerialNumberList();
            GenerateSerialNumber();
        }

        #region Serial Number Info

        private void PrepareSerialNumberList()
        {
            try
            {
                _GRNBLL.PrepareSerialNumberList(CommonBLL.FormType.GRN);
            }
            catch (Exception ex)
            {
                Clipboard.SetText(ex.Message);
                MessageBox.Show("Could not load serial number details", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadSerialNumberList()
        {
            try
            {
                _GRNBLL.LoadSerialNumberList();
            }
            catch (Exception ex)
            {
                Clipboard.SetText(ex.Message);
                MessageBox.Show("Could not load transaction serial number details", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void GenerateSerialNumber()
        {
            try
            {
                _serialNumberList = _GRNBLL.GenerateSerialNumber(CommonBLL.SerialNumberListType.Generic);
                txtSerialNo.Text = _serialNumberList[0];
                _serialMethod = _serialNumberList[1];
                _vendorCustomerLoadMethod = _serialNumberList[2];

            }
            catch (Exception ex)
            {
                Clipboard.SetText(ex.Message);
                MessageBox.Show("Could not genarate transaction serial number", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion

        private void InitializeVendor()
        {
            LoadVendor();
            SetVendor();
        }

        #region Vendor Info
        private void LoadVendor()
        {
            try
            {
                _GRNBLL.LoadVendor();
            }
            catch (Exception ex)
            {
                Clipboard.SetText(ex.Message);
                MessageBox.Show("Could not load Vendor", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SetVendor()
        {
            try
            {
                _isinloadcmbName = true;
                cmbVendorName.DataSource = _GRNBLL.GetVendor();
                cmbVendorName.DisplayMember = "vendorname";
                _isinloadcmbName = false;
            }
            catch (Exception ex)
            {
                Clipboard.SetText(ex.Message);
                MessageBox.Show("Could not set Vendor", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion

        private void InitializeItems()
        {
            LoadItems();
            SetGridItemList();
        }

        #region Items Info
        private void LoadItems()
        {
            try
            {
                _GRNBLL.LoadItems();
            }
            catch (Exception ex)
            {
                Clipboard.SetText(ex.Message);
                MessageBox.Show("Could not load Items", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SetGridItemList()
        {
            try
            {
                #region Prepare data table for grid combobox
                DataTable _itemListTable = _GRNBLL.GetItems();
                DataView _view = new DataView(_itemListTable);
                DataTable _tableGrid = _view.ToTable(_itemListTable.TableName, false, "itemCode", "itemName");
                #endregion

                GridComboItemList.DataSource = null;
                GridComboItemList.DataSource = _tableGrid;
                GridComboItemList.HeaderText = "Item Code";
                GridComboItemList.ValueMember = "itemCode";
            }
            catch (Exception ex)
            {
                Clipboard.SetText(ex.Message);
                MessageBox.Show("Could not set item in grid", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion

        private void InitializeUnit()
        {
            LoadUnit();
            SetGridUnitList();
        }

        #region Unit Info
        private void LoadUnit()
        {
            try
            {
                _GRNBLL.LoadUnit();
            }
            catch (Exception ex)
            {
                Clipboard.SetText(ex.Message);
                MessageBox.Show("Could not load Unit", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SetGridUnitList()
        {
            try
            {
                #region Prepare data table for grid combobox
                DataTable _unitListTable = _GRNBLL.GetUnit();
                DataView _view = new DataView(_unitListTable);
                DataTable _tableGrid = _view.ToTable(_unitListTable.TableName, false, "unit");
                #endregion

                GridComboUnitList.DataSource = null;
                GridComboUnitList.DataSource = _tableGrid;
                GridComboUnitList.HeaderText = "Unit";
                GridComboUnitList.ValueMember = "unit";
            }
            catch (Exception ex)
            {
                Clipboard.SetText(ex.Message);
                MessageBox.Show("Could not set Unit in grid", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion

        private void SetFormStartDetails()
        {
            txtTotal.Text = "0.00";
        }

        private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (!_isInLoadNext)
            {
                int _row = dataGridView1.CurrentRow.Index;

                if (dataGridView1.Rows[_row].Cells[0].Value != null)
                {
                    if (e.ColumnIndex == 0)
                    {
                        SetSelectedItemDetails();
                        CalculateAmount(_row);
                    }

                    if (e.ColumnIndex == dataGridView1.Columns["Quantity"].Index || e.ColumnIndex == dataGridView1.Columns["Rate"].Index)
                    {
                        CalculateAmount(_row);
                    }
                }
            }
        }

        private void SetSelectedItemDetails()
        {
            try
            {
                int _currentRow = this.dataGridView1.CurrentCell.RowIndex;
                string _itemCode = dataGridView1.Rows[_currentRow].Cells[0].Value.ToString();

                if (!string.IsNullOrEmpty(_itemCode))
                {
                    DataTable dt = _itemBLLObject.GetDetailsOfItem(_itemCode);

                    SetItemRate(_itemCode);

                    if (dt.Rows.Count > 0)
                    {
                        dataGridView1["ItemName", _currentRow].Value = dt.Rows[0]["itemName"].ToString();
                        dataGridView1["Description", _currentRow].Value = dt.Rows[0]["description"].ToString();
                        dataGridView1["unit", _currentRow].Value = dt.Rows[0]["unit"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                Clipboard.SetText(ex.Message);
                MessageBox.Show("Could not load Item data \n \n Please re-open the form", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void SetItemRate(string _itemCode)
        {
            try
            {
                DataTable dtRate = _itemBLLObject.GetItemPriceList(_itemCode);

                if (dtRate.Rows.Count > 0)
                {
                    #region Prepare data table for grid combobox
                    DataView _view = new DataView(dtRate);
                    DataTable _tableGrid = _view.ToTable(dtRate.TableName, false, "price1");
                    //String.Format("0.00", _tableGrid.Columns["price1"]);
                    #endregion

                    GridComboItemRateList.DataSource = null;
                    GridComboItemRateList.DataSource = _tableGrid;
                    GridComboItemRateList.HeaderText = "Rate";
                    GridComboItemRateList.ValueMember = "price1";
                }
            }
            catch (Exception ex)
            {
                Clipboard.SetText(ex.Message);
                MessageBox.Show("Could not set Rate in grid", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CalculateAmount(int _row)
        {
            try
            {
                if (dataGridView1.Rows[_row].Cells[0].Value != null)
                {

                    double _qty = 0;
                    double _rate = 0;
                    double _amount = 0;
                    try
                    {
                        #region Qty
                        try
                        {
                            if (dataGridView1.Rows[_row].Cells["Quantity"].Value == null)
                            {
                                dataGridView1.Rows[_row].Cells["Quantity"].Value = "0";
                            }
                            else if (string.IsNullOrEmpty(dataGridView1.Rows[_row].Cells["Quantity"].Value.ToString()))
                            {
                                dataGridView1.Rows[_row].Cells["Quantity"].Value = "0";
                            }
                            else
                            {
                                _qty = double.Parse(dataGridView1.Rows[_row].Cells["Quantity"].Value.ToString());
                                dataGridView1.Rows[_row].Cells["Quantity"].Value = _qty;
                            }
                        }
                        catch
                        {
                            _qty = 0;
                            dataGridView1.Rows[_row].Cells["Quantity"].Value = _qty.ToString();
                        }
                        #endregion

                        #region Rate
                        try
                        {
                            if (dataGridView1.Rows[_row].Cells["Rate"].Value == null)
                            {
                                _rate = 0;
                            }
                            else if (string.IsNullOrEmpty(dataGridView1.Rows[_row].Cells["Rate"].Value.ToString()))
                            {
                                _rate = 0;
                            }
                            else
                            {
                                _rate = double.Parse(dataGridView1.Rows[_row].Cells["Rate"].Value.ToString());
                            }
                        }
                        catch
                        {
                            _rate = 0;
                        }
                        dataGridView1.Rows[_row].Cells["Rate"].Value = _rate.ToString("N2");
                        #endregion

                        _amount = _qty * _rate;
                    }
                    catch
                    {
                        _amount = 0;
                    }

                    dataGridView1.Rows[_row].Cells["Amount"].Value = _amount.ToString("N2");
                }

                CalculateGridFooterValue();
            }
            catch (Exception ex)
            {
                Clipboard.SetText(ex.Message);
            }
        }

        public void CalculateGridFooterValue()
        {
            double total = dataGridView1.Rows.Cast<DataGridViewRow>().Sum(t => Convert.ToDouble(t.Cells["Amount"].Value));
            txtTotal.Text = total.ToString("N2");
        }

        private void dataGridView1_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            try
            {
                if (e.Control is DataGridViewComboBoxEditingControl)
                {
                    ((System.Windows.Forms.ComboBox)e.Control).DropDownStyle = ComboBoxStyle.DropDown;
                    ((System.Windows.Forms.ComboBox)e.Control).AutoCompleteSource = AutoCompleteSource.ListItems;
                    ((System.Windows.Forms.ComboBox)e.Control).AutoCompleteMode = AutoCompleteMode.Append;
                }
            }
            catch (Exception ex)
            {
                Clipboard.SetText(ex.Message);
            }

            #region Validate Numaric Cells

            int indexQuantity = dataGridView1.Columns["Quantity"].Index;
            int indexRate = dataGridView1.Columns["Rate"].Index;
            int indexAmount = dataGridView1.Columns["Amount"].Index;

            e.Control.KeyPress -= new KeyPressEventHandler(Quantity_KeyPress);
            e.Control.KeyPress -= new KeyPressEventHandler(Rate_KeyPress);
            e.Control.KeyPress -= new KeyPressEventHandler(Amount_KeyPress);

            if (dataGridView1.CurrentCell.ColumnIndex == indexQuantity) //Desired Column
            {
                TextBox tb = e.Control as TextBox;
                if (tb != null)
                {
                    tb.KeyPress += new KeyPressEventHandler(Quantity_KeyPress);
                }
            }
            else if (dataGridView1.CurrentCell.ColumnIndex == indexRate) //Desired Column
            {
                TextBox tb = e.Control as TextBox;
                if (tb != null)
                {
                    tb.KeyPress += new KeyPressEventHandler(Rate_KeyPress);
                }
            }
            else if (dataGridView1.CurrentCell.ColumnIndex == indexAmount) //Desired Column
            {
                TextBox tb = e.Control as TextBox;
                if (tb != null)
                {
                    tb.KeyPress += new KeyPressEventHandler(Amount_KeyPress);
                }
            }

            #endregion
        }

        private void Quantity_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsNumber(e.KeyChar) & (Keys)e.KeyChar != Keys.Back & e.KeyChar != '.')
            {
                e.Handled = true;
            }
        }

        private void Rate_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsNumber(e.KeyChar) & (Keys)e.KeyChar != Keys.Back & e.KeyChar != '.')
            {
                e.Handled = true;
            }
        }

        private void Amount_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsNumber(e.KeyChar) & (Keys)e.KeyChar != Keys.Back & e.KeyChar != '.')
            {
                e.Handled = true;
            }
        }

        private void dataGridView1_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                if (dataGridView1.CurrentCell.ColumnIndex == 0)
                {
                    dataGridView1.BeginEdit(true);
                }

                try
                {
                    if (dataGridView1.Rows[e.RowIndex].Cells[0].Value != null)
                    {
                        getBeforeName = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
                    }
                    else
                    {
                        getBeforeName = string.Empty;
                    }
                }
                catch { getBeforeName = string.Empty; }
            }
        }

        private void dataGridView1_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            if (dataGridView1.IsCurrentCellDirty)
            {
                dataGridView1.CommitEdit(DataGridViewDataErrorContexts.Commit);
            }
        }

        private void dataGridView1_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (dataGridView1.Rows[e.RowIndex].Cells[0].Value != null)
                {
                    getBeforeName = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
                }
                else
                {
                    getBeforeName = string.Empty;
                }
            }
            catch
            {
                getBeforeName = string.Empty;
            }
        }

        private bool ValidateSaveUpdateTransaction()
        {
            if (GRNData.Grid.Count < 1)
            {
                MessageBox.Show("You could not save blank transaction. Select items and try again.", "Grid Items", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            return true;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (string.CompareOrdinal(btnSave.Text, "Save") == 0)
            {
                if (ValidateSaveUpdateTransaction())
                {
                    SaveTransaction(ButtonType.SaveAndNew);
                }
            }
            else
            {
                if (ValidateSaveUpdateTransaction())
                {
                    UpdateTransaction(ButtonType.SaveAndNew);
                }
            }
        }

        private bool SaveTransaction(ButtonType _buttonType, int _serialNoIndex = -1)
        {
            bool _isSave = false;

            try
            {
                CommonDTO _txnDTO = new CommonDTO()
                {
                    GRN = GRNData
                };

                try
                {
                    if (txtSerialNo.ReadOnly)
                    {
                        GenerateSerialNumber();
                        _txnDTO.GRN.Fields.SerialNumber = txtSerialNo.Text;
                    }

                    _isSave = _GRNBLL.Save(_txnDTO);

                    if (_isSave)
                    {
                        MessageBox.Show("Saved successfully", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);

                        if (_buttonType.Equals(ButtonType.SaveAndNew))
                        {
                            if (_serialNoIndex < 0)
                            {
                                clear(ClearType.FromButton);
                            }
                            else
                            {
                                InitializeSerialNumber();
                                LoadSameTransaction(_serialNoIndex, "Save");
                            }
                        }
                        else if (_buttonType.Equals(ButtonType.Print))
                        {
                            clear(ClearType.FromButton);
                            FindTransactionBySerialNumber(_txnDTO.GRN.Fields.SerialNumber);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Clipboard.SetText(ex.Message);
                    MessageBox.Show("Could not save transaction", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                Clipboard.SetText(ex.Message);
            }
            return _isSave;
        }

        private bool UpdateTransaction(ButtonType _buttonType, int _serialNoIndex = -1)
        {
            bool _isUpdate = false;

            try
            {
                CommonDTO _previousTxnDTO = new CommonDTO()
                {
                    GRN = _Nexprevious
                };

                CommonDTO _txnDTO = new CommonDTO()
                {
                    GRN = GRNData
                };

                try
                {
                    _isUpdate = _GRNBLL.Update(_txnDTO);

                    if (_isUpdate)
                    {
                        MessageBox.Show("Updated successfully", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);

                        if (_buttonType.Equals(ButtonType.SaveAndNew))
                        {
                            if (_serialNoIndex < 0)
                            {
                                clear(ClearType.FromButton);
                            }
                            else
                            {
                                LoadSameTransaction(_serialNoIndex, "Update");
                            }
                        }
                        else if (!_buttonType.Equals(ButtonType.DeleteRow))
                        {
                            this.Close();
                        }
                    }
                    else
                    {
                        MessageBox.Show("Could not be updated", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                catch (Exception ex)
                {
                    Clipboard.SetText(ex.Message);
                    MessageBox.Show("Could not update transaction", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch
            {
                if (!_isUpdate && _buttonType.Equals(ButtonType.DeleteRow) && _serialNoIndex >= 0)
                {
                    _isUpdate = false;
                    LoadSameTransaction(_serialNoIndex, "Update");
                }
            }

            return _isUpdate;
        }

        private bool clear(ClearType _clearType)
        {
            try
            {
                _Nexprevious = null;

                txtSerialNo.ResetText();
                dateTimePicker1.Value = DateTime.Now.Date;
                cmbVendorName.ResetText();
                txtAddress.ResetText();
                txtTotal.ResetText();
                txtMemo.ResetText();

                CreateGridRows();

                if (_clearType.Equals(ClearType.FromButton))
                {
                    btnSave.Text = "Save";
                    btnNext.Enabled = true;
                    btnPrevious.Enabled = true;
                }

                SetFormStartDetails();

                if (_clearType.Equals(ClearType.FromButton))
                {
                    InitializeSerialNumber();
                }

                return true;
            }
            catch (Exception ex)
            {
                Cursor.Current = Cursors.Default; // Set cursor as default arrow

                Clipboard.SetText(ex.Message);
                MessageBox.Show("Could not clear data. Please re-open this form.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
        }

        private void LoadSameTransaction(int _serialNoIndex, string _saveUpdateMode)
        {
            if (_serialNoIndex <= _GRNBLL.GetMaximunSerialIndex() - 1)
            {
                _isInLoadNext = true;

                NextPreviousData = _GRNBLL.NextPreviousTransaction(_serialNoIndex);

                btnSave.Text = "Update";

                if (string.Compare(_saveUpdateMode, "Update", true) == 0)
                {
                    #region Refresh serial Lists & Set current Index
                    PrepareSerialNumberList();
                    LoadSerialNumberList();

                    int _txnID = _Nexprevious.Fields.ID;
                    _serialNoIndex = _GRNBLL.GetIndexOfSelectedSerialID(_txnID);
                    _GRNBLL.SetCurrentSerialIndex(_serialNoIndex);
                    #endregion
                }

                _isInLoadNext = false;
            }
            else
            {
                clear(ClearType.FromButton);
            }
        }

        private GRNDTO GRNData
        {
            get
            {
                GRNDTO _txnDTO = new GRNDTO()
                {
                    Fields = new GRNField(),
                    Grid = new List<GRNGrid>()
                };

                _txnDTO.Fields.SerialNumber = txtSerialNo.Text;
                _txnDTO.Fields.ID = _GRNBLL.GetIDForSerialIndex(_GRNBLL.GetCurrenctSerialIndex());
                _txnDTO.Fields.VendorName = cmbVendorName.Text;
                _txnDTO.Fields.Address = txtAddress.Text;
                _txnDTO.Fields.Date = DateTime.Parse(dateTimePicker1.Text);
                _txnDTO.Fields.Total = Convert.ToDouble(txtTotal.Text);
                _txnDTO.Fields.Memo = txtMemo.Text;

                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    if (dataGridView1.Rows[i].Cells[0].Value != null)
                    {
                        if (dataGridView1.Rows[i].Cells[0].Value.ToString() != string.Empty)
                        {
                            GRNGrid _gridDTO = new GRNGrid()
                                           {
                                               ItemCode = dataGridView1[0, i].Value.ToString(),
                                               ItemName = dataGridView1[1, i].Value == null ? string.Empty : dataGridView1[1, i].Value.ToString(),
                                               Description = dataGridView1[2, i].Value == null ? string.Empty : dataGridView1[2, i].Value.ToString(),
                                               QTY = dataGridView1[3, i].Value == null || dataGridView1[3, i].Value.ToString().Equals(string.Empty) ? 0 : Convert.ToDouble(dataGridView1[3, i].Value.ToString()),
                                               Rate = dataGridView1[4, i].Value == null || dataGridView1[4, i].Value.ToString().Equals(string.Empty) ? 0 : Convert.ToDouble(dataGridView1[4, i].Value.ToString()),
                                               Amount = dataGridView1[5, i].Value == null || dataGridView1[5, i].Value.ToString().Equals(string.Empty) ? 0 : Convert.ToDouble(dataGridView1[5, i].Value.ToString()),
                                               UpID = dataGridView1[6, i].Value == null ? string.Empty : dataGridView1[6, i].Value.ToString(),
                                               Unit = dataGridView1[7, i].Value == null ? string.Empty : dataGridView1[7, i].Value.ToString()
                                           };
                            _txnDTO.Grid.Add(_gridDTO); 
                        }
                    }
                }

                return _txnDTO;
            }
        }

        private CommonDTO NextPreviousData
        {
            set
            {
                try
                {
                    clear(ClearType.FromNextPrevious);

                    if (_Nexprevious != null)
                    {
                        _Nexprevious = null;
                    }

                    _Nexprevious = new GRNDTO()
                    {
                        Fields = new GRNField(),
                        Grid = new List<GRNGrid>()
                    };

                    if (value.GRN.Fields != null)
                    {
                        txtSerialNo.Text = value.GRN.Fields.SerialNumber;
                        dateTimePicker1.Text = value.GRN.Fields.Date.ToString();
                        cmbVendorName.Text = value.GRN.Fields.VendorName;
                        txtAddress.Text = value.GRN.Fields.Address;
                        txtTotal.Text = value.GRN.Fields.Total.ToString("N2");
                        txtMemo.Text = value.GRN.Fields.Memo;

                        for (int i = 0; i < value.GRN.Grid.Count; i++)
                        {
                            SetItemRate(value.GRN.Grid[i].ItemCode);

                            dataGridView1.Rows[i].Cells[0].Value = value.GRN.Grid[i].ItemCode;
                            dataGridView1.Rows[i].Cells[1].Value = value.GRN.Grid[i].ItemName;
                            dataGridView1.Rows[i].Cells[2].Value = value.GRN.Grid[i].Description;
                            dataGridView1.Rows[i].Cells[3].Value = value.GRN.Grid[i].QTY;
                            dataGridView1.Rows[i].Cells[4].Value = value.GRN.Grid[i].Rate.ToString("N2");
                            dataGridView1.Rows[i].Cells[5].Value = value.GRN.Grid[i].Amount.ToString("N2");
                            dataGridView1.Rows[i].Cells[6].Value = value.GRN.Grid[i].UpID;
                            dataGridView1.Rows[i].Cells[7].Value = value.GRN.Grid[i].Unit;
                        }

                        _Nexprevious.Fields = value.GRN.Fields;
                        _Nexprevious.Grid = value.GRN.Grid;
                    }
                }
                catch
                {
                    throw;
                }
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            _isInDelete = true;

            if (string.CompareOrdinal(btnSave.Text, "Save") != 0)
            {
                if (MessageBox.Show("Are you sure you want to delete this transaction", "Confirm delete", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    try
                    {
                        int _currentIndex = _GRNBLL.GetCurrenctSerialIndex();
                        _GRNBLL.Delete(_GRNBLL.GetIDForSerialIndex(_currentIndex).ToString());

                        clear(ClearType.FromButton);
                        MessageBox.Show("Successfully deleted", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        Clipboard.SetText(ex.Message);
                        MessageBox.Show("Could not delete this transaction.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
        }

        private void btnRowDelete_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells[0].Value != null)
            {
                if (MessageBox.Show("Are you sure you want to delete selected Item ", "Confirm delete", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    int _rowIndex = this.dataGridView1.CurrentCell.RowIndex;

                    foreach (DataGridViewRow row in dataGridView1.SelectedRows)
                    {
                        if (!row.IsNewRow)
                        {
                            dataGridView1.CurrentCell = dataGridView1[2, _rowIndex];

                            if (btnSave.Text == "Save")
                            {
                                dataGridView1.Rows.Add();
                                dataGridView1.Rows.Remove(row);
                                CalculateGridFooterValue();
                                MessageBox.Show("Data Successfully Deleted", "Delete Transaction", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            else
                            {
                                if (dataGridView1.Rows[_rowIndex].Cells["up"].Value == null)
                                {
                                    dataGridView1.Rows.Remove(row);
                                    dataGridView1.Rows.Add();
                                    CalculateGridFooterValue();
                                    MessageBox.Show("Data Successfully Deleted", "Delete Transaction", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }
                                else
                                {
                                    string _upID = dataGridView1.Rows[_rowIndex].Cells["up"].Value.ToString();

                                    if (!string.IsNullOrWhiteSpace(_upID))
                                    {
                                        int _nonEmptyRowCount = this.dataGridView1.Rows.Cast<DataGridViewRow>()
                                                                    .Select(myRow => myRow.Cells[0].Value)
                                                                    .Where(value => value != null)
                                                                    .Count();


                                        if (_nonEmptyRowCount > 1)
                                        {
                                            dataGridView1.Rows.Remove(row);
                                            dataGridView1.Rows.Add();
                                            dataGridView1.CurrentCell = dataGridView1.Rows[_rowIndex].Cells["Description"];
                                            CalculateGridFooterValue();
                                            int _txnID = _Nexprevious.Fields.ID;
                                            int _indexOfSerialNumber = _GRNBLL.GetIndexOfSelectedSerialID(_txnID);

                                            bool _isUpdate = UpdateTransaction(ButtonType.DeleteRow, _indexOfSerialNumber);

                                            if (_isUpdate)
                                            {
                                                Boolean _isDeleted = _GRNBLL.DeleteRow(_upID);
                                                if (_isDeleted)
                                                {
                                                    #region Load same transaction after save
                                                    if (_indexOfSerialNumber <= _GRNBLL.GetMaximunSerialIndex() - 1)
                                                    {
                                                        _isInLoadNext = true;

                                                        NextPreviousData = _GRNBLL.NextPreviousTransaction(_indexOfSerialNumber);
                                                        //btnSave.Text = "Save";

                                                        #region Refresh serial Lists & Set current Index
                                                        PrepareSerialNumberList();
                                                        LoadSerialNumberList();

                                                        _indexOfSerialNumber = _GRNBLL.GetIndexOfSelectedSerialID(_txnID);
                                                        _GRNBLL.SetCurrentSerialIndex(_indexOfSerialNumber);
                                                        #endregion

                                                        _isInLoadNext = false;
                                                    }
                                                    else
                                                    {
                                                        clear(ClearType.FromButton);
                                                    }
                                                    #endregion

                                                    MessageBox.Show("Successfully deleted", "Delete Transaction", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                                }
                                                else
                                                {
                                                    MessageBox.Show("Could not delete row", "Delete Transaction", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                                }
                                            }
                                        }
                                        else
                                        {
                                            MessageBox.Show("You could not delete this row. Please enter another row item record & try again.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        private void btnPrevious_Click(object sender, EventArgs e)
        {
            int _index = _GRNBLL.GetCurrenctSerialIndex();
            int _indexForException = _index;

            try
            {
                btnNext.Enabled = true;

                if (_index <= 0)
                {
                    btnPrevious.Enabled = false;
                }
                else
                {
                    _isInLoadNext = true;
                    _index -= 1;
                    _GRNBLL.SetCurrentSerialIndex(_index);

                    if (_index <= 0)
                    {
                        btnPrevious.Enabled = false;
                    }

                    if (!btnNext.Enabled)
                    {
                        btnNext.Enabled = true;
                    }

                    dataGridView1.CurrentCell = dataGridView1.Rows[0].Cells["Description"];

                    NextPreviousData = _GRNBLL.NextPreviousTransaction(_index);
                    btnSave.Text = "Update";

                    _isInLoadNext = false;
                }

            }
            catch (Exception ex)
            {
                _GRNBLL.SetCurrentSerialIndex(_indexForException);
                Clipboard.SetText(ex.Message);
                MessageBox.Show("Could not load next previous data", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            int _index = _GRNBLL.GetCurrenctSerialIndex();
            int _indexForException = _index;

            _isInLoadNext = true;

            try
            {
                int _maxIndex = _GRNBLL.GetMaximunSerialIndex();

                if (_index >= (_maxIndex - 1))
                {
                    bool _canClearForm = clear(ClearType.FromButton);
                    btnNext.Enabled = false;

                    if (_canClearForm)
                    {
                        _index = _maxIndex;
                        _GRNBLL.SetCurrentSerialIndex(_index);
                    }
                }
                else
                {
                    _index += 1;
                    _GRNBLL.SetCurrentSerialIndex(_index);

                    if (!btnPrevious.Enabled)
                    {
                        btnPrevious.Enabled = true;
                    }

                    NextPreviousData = _GRNBLL.NextPreviousTransaction(_index);
                    btnSave.Text = "Update";
                }
            }
            catch (Exception ex)
            {
                _GRNBLL.SetCurrentSerialIndex(_indexForException);
                Clipboard.SetText(ex.Message);
                MessageBox.Show("Could not load next previous data", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            _isInLoadNext = false;
        }

        private void txtSearch_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((Keys)e.KeyChar == Keys.Enter)
            {
                FindTransactionBySerialNumber(txtSearch.Text);
            }
        }

        public void FindTransactionBySerialNumber(string _serialNumber)
        {
            Cursor.Current = Cursors.WaitCursor;// Set cursor as hourglass
            try
            {
                if (!string.IsNullOrEmpty(_serialNumber))
                {
                    try
                    {
                        int _index = _GRNBLL.GetIndexOfSelectedSerial(_serialNumber);
                        if (_index == 0)
                        {
                            _GRNBLL.SetCurrentSerialIndex(_index + 1);
                            btnPrevious.PerformClick();

                            txtSearch.Text = string.Empty;
                        }
                        else if (_index > 0)
                        {
                            _GRNBLL.SetCurrentSerialIndex(_index - 1);
                            btnNext.PerformClick();

                            txtSearch.Text = string.Empty;
                        }
                        else
                        {
                            MessageBox.Show("Could not found transaction", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                    catch (Exception _ex)
                    {
                        Clipboard.SetText(_ex.Message);
                        MessageBox.Show(_ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                {
                    MessageBox.Show("Please enter serial number", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                Cursor.Current = Cursors.Default;// Set cursor as default arrow
                Clipboard.SetText(ex.Message);
                MessageBox.Show("Could not find transaction using find ID", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            Cursor.Current = Cursors.Default;// Set cursor as default arrow
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            try
            {
                bool _canPrint = false;

                if (dataGridView1[0, 0].Value != null)
                {
                    if (_Nexprevious == null)
                    {
                        _canPrint = SaveTransaction(ButtonType.Print);
                        btnSave.Text = "&Update";
                    }
                    else
                    {
                        _canPrint = true;
                    }

                    if (_canPrint)
                    {
                        if (txtSerialNo.Text != string.Empty)
                        {
                            FrmGRNPrint _print = new FrmGRNPrint();
                            _print._serialNo = txtSerialNo.Text;
                            _print._printType = cmbPrintType.Text;
                            _print.MdiParent = this.MdiParent;
                            _print.WindowState = FormWindowState.Maximized;
                            _print.Show();
                        }
                    }
                }
                else
                {
                    MessageBox.Show("No Data to Print");
                }
            }
            catch (Exception ex)
            {
                Clipboard.SetText("Error Message : " + ex.Message + "/r/nTrace Message : " + ex.StackTrace.ToString());
                MessageBox.Show("Invoice Print Error");
            }
        }

        private void cmbVendorName_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (!_isinloadcmbName)
                {
                    string Vname = cmbVendorName.Text;
                    DataTable _dtdata = new DataTable();

                    if (!string.IsNullOrEmpty(Vname))
                    {
                        DataTable dt = _vendorBLLObject.LoadVendorList();

                        if (dt.Rows.Count > 0)
                        {
                            _dtdata = dt.AsEnumerable().Where(x => x.Field<string>("vendorname") == Vname).CopyToDataTable();
                            txtAddress.Text = string.Format("{0}\r\nTel : {1}", _dtdata.Rows[0]["address"].ToString(), _dtdata.Rows[0]["phone"].ToString());
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Clipboard.SetText("Error Message : " + ex.Message + "/r/nTrace Message : " + ex.StackTrace.ToString());
                MessageBox.Show("Vendor Address Load Error");
            }
        }

    }
}
