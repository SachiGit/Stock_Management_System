namespace StockManageUI.Common
{
    partial class FrmHome
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
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmHome));
            this.pbIssueNote = new System.Windows.Forms.PictureBox();
            this.label7 = new System.Windows.Forms.Label();
            this.pbGRN = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label2 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pbIssueNote)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbGRN)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // pbIssueNote
            // 
            this.pbIssueNote.BackColor = System.Drawing.Color.Transparent;
            this.pbIssueNote.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pbIssueNote.Image = ((System.Drawing.Image)(resources.GetObject("pbIssueNote.Image")));
            this.pbIssueNote.Location = new System.Drawing.Point(306, 32);
            this.pbIssueNote.Name = "pbIssueNote";
            this.pbIssueNote.Size = new System.Drawing.Size(188, 145);
            this.pbIssueNote.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbIssueNote.TabIndex = 8;
            this.pbIssueNote.TabStop = false;
            this.pbIssueNote.Click += new System.EventHandler(this.pbIssueNote_Click);
            // 
            // label7
            // 
            this.label7.BackColor = System.Drawing.Color.Transparent;
            this.label7.Font = new System.Drawing.Font("Californian FB", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(306, 190);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(188, 39);
            this.label7.TabIndex = 9;
            this.label7.Text = "Issue Note";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pbGRN
            // 
            this.pbGRN.BackColor = System.Drawing.Color.Transparent;
            this.pbGRN.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pbGRN.Image = ((System.Drawing.Image)(resources.GetObject("pbGRN.Image")));
            this.pbGRN.Location = new System.Drawing.Point(37, 32);
            this.pbGRN.Name = "pbGRN";
            this.pbGRN.Size = new System.Drawing.Size(200, 145);
            this.pbGRN.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbGRN.TabIndex = 6;
            this.pbGRN.TabStop = false;
            this.pbGRN.Click += new System.EventHandler(this.pbGRN_Click);
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Californian FB", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(37, 190);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(200, 39);
            this.label1.TabIndex = 7;
            this.label1.Text = "GRN";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(566, 32);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(200, 145);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 10;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click);
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Californian FB", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(566, 190);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(200, 39);
            this.label2.TabIndex = 11;
            this.label2.Text = "GRN Return";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // FrmHome
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(812, 261);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.pbIssueNote);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.pbGRN);
            this.Controls.Add(this.label1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmHome";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Home";
            this.Load += new System.EventHandler(this.FrmHome_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pbIssueNote)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbGRN)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pbIssueNote;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.PictureBox pbGRN;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label2;
    }
}