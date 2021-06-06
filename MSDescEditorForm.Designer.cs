namespace MsDescriptionMakeUpTool
{
    partial class MSDescEditorForm
    {
        /// <summary>
        /// 設計工具所需的變數。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清除任何使用中的資源。
        /// </summary>
        /// <param name="disposing">如果應該處置受控資源則為 true，否則為 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 設計工具產生的程式碼

        /// <summary>
        /// 此為設計工具支援所需的方法 - 請勿使用程式碼編輯器修改
        /// 這個方法的內容。
        /// </summary>
        private void InitializeComponent()
        {
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.btnConnectDB = new System.Windows.Forms.Button();
            this.tbConnStr = new System.Windows.Forms.TextBox();
            this.cbDBList = new System.Windows.Forms.ComboBox();
            this.tbExportScript = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tbMS_DescriptionForTable = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnExport = new System.Windows.Forms.Button();
            this.btnSyncDB = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(22, 100);
            this.dataGridView1.Margin = new System.Windows.Forms.Padding(2);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersWidth = 51;
            this.dataGridView1.RowTemplate.Height = 27;
            this.dataGridView1.Size = new System.Drawing.Size(1047, 170);
            this.dataGridView1.TabIndex = 0;
            this.dataGridView1.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellEndEdit);
            // 
            // btnConnectDB
            // 
            this.btnConnectDB.Location = new System.Drawing.Point(22, 10);
            this.btnConnectDB.Margin = new System.Windows.Forms.Padding(2);
            this.btnConnectDB.Name = "btnConnectDB";
            this.btnConnectDB.Size = new System.Drawing.Size(56, 22);
            this.btnConnectDB.TabIndex = 1;
            this.btnConnectDB.Text = "連線";
            this.btnConnectDB.UseVisualStyleBackColor = true;
            this.btnConnectDB.Click += new System.EventHandler(this.btnConnectDB_Click);
            // 
            // tbConnStr
            // 
            this.tbConnStr.Location = new System.Drawing.Point(84, 10);
            this.tbConnStr.Margin = new System.Windows.Forms.Padding(2);
            this.tbConnStr.Name = "tbConnStr";
            this.tbConnStr.Size = new System.Drawing.Size(479, 22);
            this.tbConnStr.TabIndex = 2;
            // 
            // cbDBList
            // 
            this.cbDBList.FormattingEnabled = true;
            this.cbDBList.Location = new System.Drawing.Point(567, 10);
            this.cbDBList.Margin = new System.Windows.Forms.Padding(2);
            this.cbDBList.Name = "cbDBList";
            this.cbDBList.Size = new System.Drawing.Size(502, 20);
            this.cbDBList.TabIndex = 3;
            this.cbDBList.SelectedIndexChanged += new System.EventHandler(this.cbDBList_SelectedIndexChanged);
            // 
            // tbExportScript
            // 
            this.tbExportScript.Location = new System.Drawing.Point(22, 303);
            this.tbExportScript.Margin = new System.Windows.Forms.Padding(2);
            this.tbExportScript.Multiline = true;
            this.tbExportScript.Name = "tbExportScript";
            this.tbExportScript.ReadOnly = true;
            this.tbExportScript.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tbExportScript.Size = new System.Drawing.Size(1048, 195);
            this.tbExportScript.TabIndex = 4;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(22, 46);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(55, 12);
            this.label1.TabIndex = 5;
            this.label1.Text = "Table描述";
            // 
            // tbMS_DescriptionForTable
            // 
            this.tbMS_DescriptionForTable.Location = new System.Drawing.Point(84, 43);
            this.tbMS_DescriptionForTable.Margin = new System.Windows.Forms.Padding(2);
            this.tbMS_DescriptionForTable.Name = "tbMS_DescriptionForTable";
            this.tbMS_DescriptionForTable.Size = new System.Drawing.Size(258, 22);
            this.tbMS_DescriptionForTable.TabIndex = 6;
            this.tbMS_DescriptionForTable.TextChanged += new System.EventHandler(this.tbMS_DescriptionForTable_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(22, 78);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(71, 12);
            this.label2.TabIndex = 7;
            this.label2.Text = "Columns描述";
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(994, 73);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 8;
            this.btnSave.Text = "儲存";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnExport
            // 
            this.btnExport.Location = new System.Drawing.Point(995, 275);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(75, 23);
            this.btnExport.TabIndex = 8;
            this.btnExport.Text = "匯出";
            this.btnExport.UseVisualStyleBackColor = true;
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
            // 
            // btnSyncDB
            // 
            this.btnSyncDB.Location = new System.Drawing.Point(22, 275);
            this.btnSyncDB.Name = "btnSyncDB";
            this.btnSyncDB.Size = new System.Drawing.Size(75, 23);
            this.btnSyncDB.TabIndex = 9;
            this.btnSyncDB.Text = "資料庫同步";
            this.btnSyncDB.UseVisualStyleBackColor = true;
            this.btnSyncDB.Click += new System.EventHandler(this.btnSyncDB_Click);
            // 
            // MSDescEditorForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1102, 521);
            this.Controls.Add(this.btnSyncDB);
            this.Controls.Add(this.btnExport);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.tbMS_DescriptionForTable);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tbExportScript);
            this.Controls.Add(this.cbDBList);
            this.Controls.Add(this.tbConnStr);
            this.Controls.Add(this.btnConnectDB);
            this.Controls.Add(this.dataGridView1);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "MSDescEditorForm";
            this.Text = "MSDesc修補工具";
            this.Load += new System.EventHandler(this.MSDescEditorForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button btnConnectDB;
        private System.Windows.Forms.TextBox tbConnStr;
        private System.Windows.Forms.ComboBox cbDBList;
        private System.Windows.Forms.TextBox tbExportScript;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbMS_DescriptionForTable;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnExport;
        private System.Windows.Forms.Button btnSyncDB;
    }
}

