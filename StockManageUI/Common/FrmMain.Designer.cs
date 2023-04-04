using StockManageUI.Login;
namespace StockManageUI.Common
{
    partial class FrmMain
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);

            FrmLogin login = new FrmLogin();
            login.IsMainPageClose = true;
            login.Show();
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmMain));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.btnFind = new System.Windows.Forms.ToolStripMenuItem();
            this.btnExit = new System.Windows.Forms.ToolStripMenuItem();
            this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.btnHome = new System.Windows.Forms.ToolStripMenuItem();
            this.listToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.btnUser = new System.Windows.Forms.ToolStripMenuItem();
            this.btnUnit = new System.Windows.Forms.ToolStripMenuItem();
            this.btnDepartment = new System.Windows.Forms.ToolStripMenuItem();
            this.btnItem = new System.Windows.Forms.ToolStripMenuItem();
            this.btnVendors = new System.Windows.Forms.ToolStripMenuItem();
            this.btnCompany = new System.Windows.Forms.ToolStripMenuItem();
            this.reportsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.gRNToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.btnGRNDetails = new System.Windows.Forms.ToolStripMenuItem();
            this.issueNoteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.btnIssueNoteDetails = new System.Windows.Forms.ToolStripMenuItem();
            this.gRNReturnToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.btnGRNReturnDetails = new System.Windows.Forms.ToolStripMenuItem();
            this.stockValuationSummaryToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.btnIssuedItemSummary = new System.Windows.Forms.ToolStripMenuItem();
            this.btnFinalReport = new System.Windows.Forms.ToolStripMenuItem();
            this.btnReOrderLevel = new System.Windows.Forms.ToolStripMenuItem();
            this.btnItemDetail = new System.Windows.Forms.ToolStripMenuItem();
            this.btnWindow = new System.Windows.Forms.ToolStripMenuItem();
            this.btnContactUs = new System.Windows.Forms.ToolStripMenuItem();
            this.btnIssueNoteVoteSummary = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.AutoSize = false;
            this.menuStrip1.BackColor = System.Drawing.Color.White;
            this.menuStrip1.Font = new System.Drawing.Font("Californian FB", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.editToolStripMenuItem,
            this.listToolStripMenuItem,
            this.btnCompany,
            this.reportsToolStripMenuItem,
            this.btnWindow,
            this.btnContactUs});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1044, 40);
            this.menuStrip1.TabIndex = 2;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnFind,
            this.btnExit});
            this.fileToolStripMenuItem.ForeColor = System.Drawing.Color.Black;
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(56, 36);
            this.fileToolStripMenuItem.Text = "&File";
            // 
            // btnFind
            // 
            this.btnFind.Name = "btnFind";
            this.btnFind.Size = new System.Drawing.Size(215, 28);
            this.btnFind.Text = "Find";
            this.btnFind.Click += new System.EventHandler(this.btnFind_Click);
            // 
            // btnExit
            // 
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(215, 28);
            this.btnExit.Text = "Exit       Alt+F4";
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // editToolStripMenuItem
            // 
            this.editToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnHome});
            this.editToolStripMenuItem.ForeColor = System.Drawing.Color.Black;
            this.editToolStripMenuItem.Name = "editToolStripMenuItem";
            this.editToolStripMenuItem.Size = new System.Drawing.Size(68, 36);
            this.editToolStripMenuItem.Text = "&View";
            // 
            // btnHome
            // 
            this.btnHome.Name = "btnHome";
            this.btnHome.Size = new System.Drawing.Size(134, 28);
            this.btnHome.Text = "Home";
            this.btnHome.Click += new System.EventHandler(this.btnHome_Click);
            // 
            // listToolStripMenuItem
            // 
            this.listToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnUser,
            this.btnUnit,
            this.btnDepartment,
            this.btnItem,
            this.btnVendors});
            this.listToolStripMenuItem.ForeColor = System.Drawing.Color.Black;
            this.listToolStripMenuItem.Name = "listToolStripMenuItem";
            this.listToolStripMenuItem.Size = new System.Drawing.Size(55, 36);
            this.listToolStripMenuItem.Text = "&List";
            // 
            // btnUser
            // 
            this.btnUser.Name = "btnUser";
            this.btnUser.Size = new System.Drawing.Size(185, 28);
            this.btnUser.Text = "Users";
            this.btnUser.Click += new System.EventHandler(this.btnUser_Click);
            // 
            // btnUnit
            // 
            this.btnUnit.Name = "btnUnit";
            this.btnUnit.Size = new System.Drawing.Size(185, 28);
            this.btnUnit.Text = "Unit";
            this.btnUnit.Click += new System.EventHandler(this.btnUnit_Click);
            // 
            // btnDepartment
            // 
            this.btnDepartment.Name = "btnDepartment";
            this.btnDepartment.Size = new System.Drawing.Size(185, 28);
            this.btnDepartment.Text = "Department";
            this.btnDepartment.Click += new System.EventHandler(this.btnDepartment_Click);
            // 
            // btnItem
            // 
            this.btnItem.Name = "btnItem";
            this.btnItem.Size = new System.Drawing.Size(185, 28);
            this.btnItem.Text = "Items";
            this.btnItem.Click += new System.EventHandler(this.btnItem_Click);
            // 
            // btnVendors
            // 
            this.btnVendors.Name = "btnVendors";
            this.btnVendors.Size = new System.Drawing.Size(185, 28);
            this.btnVendors.Text = "Vendors";
            this.btnVendors.Click += new System.EventHandler(this.btnVendors_Click);
            // 
            // btnCompany
            // 
            this.btnCompany.Name = "btnCompany";
            this.btnCompany.Size = new System.Drawing.Size(105, 36);
            this.btnCompany.Text = "&Company";
            this.btnCompany.Click += new System.EventHandler(this.btnCompany_Click);
            // 
            // reportsToolStripMenuItem
            // 
            this.reportsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.gRNToolStripMenuItem,
            this.issueNoteToolStripMenuItem,
            this.gRNReturnToolStripMenuItem,
            this.stockValuationSummaryToolStripMenuItem,
            this.btnIssuedItemSummary,
            this.btnFinalReport,
            this.btnReOrderLevel,
            this.btnItemDetail,
            this.btnIssueNoteVoteSummary});
            this.reportsToolStripMenuItem.Name = "reportsToolStripMenuItem";
            this.reportsToolStripMenuItem.Size = new System.Drawing.Size(92, 36);
            this.reportsToolStripMenuItem.Text = "&Reports";
            // 
            // gRNToolStripMenuItem
            // 
            this.gRNToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnGRNDetails});
            this.gRNToolStripMenuItem.Name = "gRNToolStripMenuItem";
            this.gRNToolStripMenuItem.Size = new System.Drawing.Size(308, 28);
            this.gRNToolStripMenuItem.Text = "GRN";
            // 
            // btnGRNDetails
            // 
            this.btnGRNDetails.Name = "btnGRNDetails";
            this.btnGRNDetails.Size = new System.Drawing.Size(142, 28);
            this.btnGRNDetails.Text = "Details";
            this.btnGRNDetails.Click += new System.EventHandler(this.btnGRNDetails_Click);
            // 
            // issueNoteToolStripMenuItem
            // 
            this.issueNoteToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnIssueNoteDetails});
            this.issueNoteToolStripMenuItem.Name = "issueNoteToolStripMenuItem";
            this.issueNoteToolStripMenuItem.Size = new System.Drawing.Size(308, 28);
            this.issueNoteToolStripMenuItem.Text = "Issue Note";
            // 
            // btnIssueNoteDetails
            // 
            this.btnIssueNoteDetails.Name = "btnIssueNoteDetails";
            this.btnIssueNoteDetails.Size = new System.Drawing.Size(142, 28);
            this.btnIssueNoteDetails.Text = "Details";
            this.btnIssueNoteDetails.Click += new System.EventHandler(this.btnIssueNoteDetails_Click);
            // 
            // gRNReturnToolStripMenuItem
            // 
            this.gRNReturnToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnGRNReturnDetails});
            this.gRNReturnToolStripMenuItem.Name = "gRNReturnToolStripMenuItem";
            this.gRNReturnToolStripMenuItem.Size = new System.Drawing.Size(308, 28);
            this.gRNReturnToolStripMenuItem.Text = "GRN Return";
            // 
            // btnGRNReturnDetails
            // 
            this.btnGRNReturnDetails.Name = "btnGRNReturnDetails";
            this.btnGRNReturnDetails.Size = new System.Drawing.Size(142, 28);
            this.btnGRNReturnDetails.Text = "Details";
            this.btnGRNReturnDetails.Click += new System.EventHandler(this.btnGRNReturnDetails_Click);
            // 
            // stockValuationSummaryToolStripMenuItem
            // 
            this.stockValuationSummaryToolStripMenuItem.Name = "stockValuationSummaryToolStripMenuItem";
            this.stockValuationSummaryToolStripMenuItem.Size = new System.Drawing.Size(308, 28);
            this.stockValuationSummaryToolStripMenuItem.Text = "Stock Valuation Summary";
            this.stockValuationSummaryToolStripMenuItem.Click += new System.EventHandler(this.stockValuationSummaryToolStripMenuItem_Click);
            // 
            // btnIssuedItemSummary
            // 
            this.btnIssuedItemSummary.Name = "btnIssuedItemSummary";
            this.btnIssuedItemSummary.Size = new System.Drawing.Size(308, 28);
            this.btnIssuedItemSummary.Text = "Issued Item Summary";
            this.btnIssuedItemSummary.Click += new System.EventHandler(this.btnIssuedItemSummary_Click);
            // 
            // btnFinalReport
            // 
            this.btnFinalReport.Name = "btnFinalReport";
            this.btnFinalReport.Size = new System.Drawing.Size(308, 28);
            this.btnFinalReport.Text = "Final ";
            this.btnFinalReport.Click += new System.EventHandler(this.btnFinalReport_Click);
            // 
            // btnReOrderLevel
            // 
            this.btnReOrderLevel.Name = "btnReOrderLevel";
            this.btnReOrderLevel.Size = new System.Drawing.Size(308, 28);
            this.btnReOrderLevel.Text = "Re-Order Level";
            this.btnReOrderLevel.Click += new System.EventHandler(this.btnReOrderLevel_Click);
            // 
            // btnItemDetail
            // 
            this.btnItemDetail.Name = "btnItemDetail";
            this.btnItemDetail.Size = new System.Drawing.Size(308, 28);
            this.btnItemDetail.Text = "Item Detail";
            this.btnItemDetail.Click += new System.EventHandler(this.btnItemDetail_Click);
            // 
            // btnWindow
            // 
            this.btnWindow.Name = "btnWindow";
            this.btnWindow.Size = new System.Drawing.Size(99, 36);
            this.btnWindow.Text = "&Window";
            this.btnWindow.Click += new System.EventHandler(this.btnWindow_Click);
            // 
            // btnContactUs
            // 
            this.btnContactUs.Name = "btnContactUs";
            this.btnContactUs.Size = new System.Drawing.Size(124, 36);
            this.btnContactUs.Text = "&Contact US";
            this.btnContactUs.Click += new System.EventHandler(this.btnContactUs_Click);
            // 
            // btnIssueNoteVoteSummary
            // 
            this.btnIssueNoteVoteSummary.Name = "btnIssueNoteVoteSummary";
            this.btnIssueNoteVoteSummary.Size = new System.Drawing.Size(308, 28);
            this.btnIssueNoteVoteSummary.Text = "Issue Note Vote Summary";
            this.btnIssueNoteVoteSummary.Click += new System.EventHandler(this.btnIssueNoteVoteSummary_Click);
            // 
            // FrmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.ClientSize = new System.Drawing.Size(1044, 468);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.IsMdiContainer = true;
            this.Name = "FrmMain";
            this.Text = "Main";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmMain_FormClosing);
            this.Load += new System.EventHandler(this.FrmMain_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem btnExit;
        private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem btnHome;
        private System.Windows.Forms.ToolStripMenuItem listToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem btnUser;
        private System.Windows.Forms.ToolStripMenuItem btnItem;
        private System.Windows.Forms.ToolStripMenuItem btnUnit;
        private System.Windows.Forms.ToolStripMenuItem reportsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem btnWindow;
        private System.Windows.Forms.ToolStripMenuItem btnDepartment;
        private System.Windows.Forms.ToolStripMenuItem btnVendors;
        private System.Windows.Forms.ToolStripMenuItem btnCompany;
        private System.Windows.Forms.ToolStripMenuItem gRNToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem btnGRNDetails;
        private System.Windows.Forms.ToolStripMenuItem issueNoteToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem btnIssueNoteDetails;
        private System.Windows.Forms.ToolStripMenuItem gRNReturnToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem btnGRNReturnDetails;
        private System.Windows.Forms.ToolStripMenuItem stockValuationSummaryToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem btnIssuedItemSummary;
        private System.Windows.Forms.ToolStripMenuItem btnFinalReport;
        private System.Windows.Forms.ToolStripMenuItem btnReOrderLevel;
        private System.Windows.Forms.ToolStripMenuItem btnContactUs;
        private System.Windows.Forms.ToolStripMenuItem btnFind;
        private System.Windows.Forms.ToolStripMenuItem btnItemDetail;
        private System.Windows.Forms.ToolStripMenuItem btnIssueNoteVoteSummary;
    }
}