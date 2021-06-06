using Dapper;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MsDescriptionMakeUpTool
{
    public partial class MSDescEditorForm : Form
    {
        public MSDescEditorForm()
        {
            InitializeComponent();
        }

        private string _connectionString = string.Empty;

        private ConcurrentDictionary<string, TableDesciption> _collections;

        public static bool CheckSQLConnection(string tb資料庫主機帳號Text, bool isShowSuccess)
        {
            try
            {
                using (var connection = new SqlConnection(tb資料庫主機帳號Text))
                {
                    try
                    {
                        connection.Open();

                        if (connection.State == ConnectionState.Open)
                        {
                            if (isShowSuccess)
                                MessageBox.Show("連線成功");
                            connection.Close();
                            return true;
                        }
                        else
                        {
                            MessageBox.Show("連線失敗");
                        }
                    }
                    catch (SqlException ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("連線失敗, Msg:" + e.Message);
            }

            return false;
        }

        private void btnConnectDB_Click(object sender, EventArgs e)
        {


            _connectionString = tbConnStr.Text;
            var scsb = new SqlConnectionStringBuilder(_connectionString);

            CheckSQLConnection(_connectionString, true);

            if (!string.IsNullOrEmpty(scsb.InitialCatalog))
            {
                loadTableList(scsb.InitialCatalog);
            }
            else
            {
                MessageBox.Show("連線字串必須包含資料庫");
                return;
            }
        }


        private void loadTableList(string dbName)
        {
            var scriptForListDB =
                @"SELECT TABLE_NAME FROM {0}.INFORMATION_SCHEMA.TABLES WHERE TABLE_TYPE = 'BASE TABLE' Order By 1 ";
            //ForTest 
            //@"SELECT top 2 TABLE_NAME FROM {0}.INFORMATION_SCHEMA.TABLES WHERE TABLE_TYPE = 'BASE TABLE' Order By 1 ";

            scriptForListDB = string.Format(scriptForListDB, dbName);

            var cn = new SqlConnection(_connectionString);

            var table_list = cn.Query<string>(scriptForListDB).ToList();

            var tableListCollections = new ConcurrentDictionary<string, string>();
            _collections = new ConcurrentDictionary<string, TableDesciption>();

            Parallel.ForEach(table_list.ToArray(), (tableName) =>
            {
                try
                {
                    var cn_inner = new SqlConnection(_connectionString);

                    _collections.TryAdd(tableName,
                                        new TableDesciption() { Name = tableName, Columns = new List<ColumnDesciption>() });

                    var scriptForListTableColumn =
                        @"SELECT COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = N'{0}'";
                    scriptForListTableColumn = string.Format(scriptForListTableColumn, tableName);

                    var columns_list = cn_inner.Query<string>(scriptForListTableColumn).ToList();

                    var checkMSDescForTable =
                        "SELECT value FROM SYS.EXTENDED_PROPERTIES WHERE [major_id] = OBJECT_ID('{0}') AND [name] = N'MS_Description' AND [minor_id] = 0";

                    var columnSumCount = 0;
                    foreach (var colName in columns_list)
                    {
                        var checkMSDescForColumn =
                            "SELECT value FROM SYS.EXTENDED_PROPERTIES WHERE [major_id] = OBJECT_ID('{0}') AND [name] = N'MS_Description' AND [minor_id] = (SELECT [column_id] FROM SYS.COLUMNS WHERE [name] = '{1}' AND [object_id] = OBJECT_ID('{0}'))";
                        checkMSDescForColumn = string.Format(checkMSDescForColumn, tableName, colName);

                        var ColumnExistMSDescResult = cn_inner.Query<string>(checkMSDescForColumn).ToList();

                        var ColumnExistMSDesc = ColumnExistMSDescResult.FirstOrDefault();
                        var isExist = false;
                        if (ColumnExistMSDesc != null && ColumnExistMSDesc.ToString().Trim() != "")
                        {
                            columnSumCount += 1;
                            isExist = true;
                        }

                        _collections[tableName].Columns.Add(new ColumnDesciption()
                        { Name = colName, MS_Description = ColumnExistMSDesc, isExist = isExist });
                    }


                    checkMSDescForTable = string.Format(checkMSDescForTable, tableName);
                    var tableExistMSDescResult = cn_inner.Query<string>(checkMSDescForTable).ToList();

                    var tableExistMSDesc = tableExistMSDescResult.FirstOrDefault();

                    var tableMSDescCount = 0;
                    if (tableExistMSDesc != null && tableExistMSDesc.ToString().Trim() != "")
                    {
                        _collections[tableName].isExist = true;
                        _collections[tableName].MS_Description = tableExistMSDesc.ToString().Trim();
                        tableMSDescCount++;
                    }

                    var DisplayTableName = string.Format("{0} ({1}) 欄位註釋 - 已:{2}/總:{3}", tableName, tableExistMSDesc,
                        columnSumCount, columns_list.Count);

                    if (!tableListCollections.ContainsKey(DisplayTableName))
                    {
                        tableListCollections.TryAdd(DisplayTableName, tableName);
                    }
                    else
                    {
                        tableListCollections.TryAdd(DisplayTableName + "/重覆 " + DateTime.Now.Ticks, tableName);
                    }
                }
                catch (Exception e)
                {

                    MessageBox.Show(string.Format("load table error, table:{0}, msg:{1}", tableName, e.Message));
                }

            });

            var orderedCollections = new SortedList<string, string>();
            var orderdkey = tableListCollections.Keys.ToArray().OrderBy(a => a);

            foreach (var key in orderdkey)
            {
                orderedCollections.Add(key, tableListCollections[key]); 
            }

            cbDBList.DisplayMember = "Key";
            cbDBList.ValueMember = "Value";
            cbDBList.DataSource = orderedCollections.ToList();
        }

        private void cbDBList_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (CheckSQLConnection(_connectionString, false))
                {
                    if (cbDBList.SelectedValue == null) return;
                    var tableName = cbDBList.SelectedValue.ToString();

                    tbMS_DescriptionForTable.Text = _collections[tableName].MS_Description;

                    dataGridView1.DataSource = _collections[tableName].Columns;
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show("發生錯誤, Msg:" + exception.Message);
            }

        }

        private void btnSyncDB_Click(object sender, EventArgs e)
        {
            MessageBox.Show("尚未支援");
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (CheckSQLConnection(_connectionString, false))
                {
                    //統一處理異動

                    var updateCount = 0;
                    var modifiedTables = _collections.Where(a => a.Value.modify).ToList();
                    var commandList = new List<string>();

                    List<object> table_paras = new List<object>();

                    if (modifiedTables.Count > 0)
                    {
                        var updateScript = getTableMSDescriptionUpdateScript();
                        foreach (var tableObj in modifiedTables)
                        {
                            table_paras.Add(new { table = tableObj.Value.Name, desc = tableObj.Value.MS_Description });
                            updateCount++;
                        }

                        if (table_paras.Count > 0)
                        {
                            var cn = new SqlConnection(_connectionString);

                            cn.Execute(updateScript, table_paras);
                        }

                        var updateScript1 = getColumnMSDescriptionUpdateScript();
                        List<object> column_paras = new List<object>();
                        foreach (var tables in _collections)
                        {
                            if (tables.Value.Columns.Any(a => a.modify))
                            {
                                foreach (var columnObj in tables.Value.Columns.Where(a => a.modify).ToList())
                                {
                                    column_paras.Add(new { table = tables.Key, column = columnObj.Name, desc = columnObj.MS_Description });
                                    updateCount++;
                                }
                            }
                        }

                        if (column_paras.Count > 0)
                        {
                            var cn = new SqlConnection(_connectionString);

                            cn.Execute(updateScript1, column_paras);
                        }
                    }

                    var connStrBuilder = new SqlConnectionStringBuilder(_connectionString);

                    if (updateCount > 0)
                    {
                        loadTableList(connStrBuilder.InitialCatalog);
                    }
                    else
                    {
                        MessageBox.Show("未發現任何異動!");
                    }
                }


                MessageBox.Show("儲存成功，確定後，將重新刷新，請稍後!!");
            }
            catch (Exception exception)
            {
                MessageBox.Show("發生錯誤, Msg:" + exception.Message);
            }
        }

        private static string getColumnMSDescriptionUpdateScript()
        {
            var updateScript1 =
                @"
IF NOT EXISTS (SELECT NULL FROM SYS.EXTENDED_PROPERTIES WHERE [major_id] = OBJECT_ID(@table) AND [name] = N'MS_Description' AND [minor_id] = (SELECT [column_id] FROM SYS.COLUMNS WHERE [name] =@column AND [object_id] = OBJECT_ID(@table)))
BEGIN
    EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = @desc, @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = @table, @level2type = N'COLUMN', @level2name = @column;;
END
ELSE
BEGIN
    EXECUTE sp_updateextendedproperty @name = N'MS_Description', @value = @desc, @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = @table, @level2type = N'COLUMN', @level2name = @column;
END
";
            return updateScript1;
        }

        private static string getTableMSDescriptionUpdateScript()
        {
            return @"
IF NOT EXISTS (SELECT NULL FROM SYS.EXTENDED_PROPERTIES WHERE [major_id] = OBJECT_ID(@table) AND [name] = N'MS_Description' AND [minor_id] = 0)
BEGIN 
    EXECUTE sp_addextendedproperty @name = N'MS_Description', @value =@desc, @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = @table;
END
ELSE
BEGIN 
    EXECUTE sp_updateextendedproperty @name = N'MS_Description', @value =@desc, @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = @table;
END
     ";
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            try
            {
                if (CheckSQLConnection(_connectionString, false))
                {
                    if (cbDBList.SelectedValue == null) return;
                    var tableName = cbDBList.SelectedValue.ToString();

                    if (!string.IsNullOrEmpty(tbMS_DescriptionForTable.Text))
                    {
                        _collections[tableName].MS_Description = tbMS_DescriptionForTable.Text;
                        _collections[tableName].modify = true;
                    }

                    var idx = 0;
                    foreach (var tableObj in _collections)
                    {
                        if (string.IsNullOrEmpty(tableObj.Value.MS_Description)) continue;
                        var updateScript = getTableMSDescriptionUpdateScript();

                        updateScript = "declare @table nvarchar(100)='" + tableName + "';declare @desc nvarchar(100) = '" + tableObj.Value.MS_Description + "';\r\n " + updateScript;
                        updateScript = updateScript.Replace("@table", "@table" + idx);
                        updateScript = updateScript.Replace("@desc", "@desc" + idx);
                        tbExportScript.Text += updateScript;

                        tbExportScript.Text += "\r\n GO \r\n";

                        idx++;
                    }

                    foreach (var tables in _collections)
                    {
                        foreach (var column in tables.Value.Columns)
                        {
                            if (string.IsNullOrEmpty(column.MS_Description)) continue;

                            var updateScript = getColumnMSDescriptionUpdateScript();

                            updateScript = "declare @table nvarchar(100)='" + tables.Value.Name + "';declare @column nvarchar(100) = '" + column.Name + "'; declare @desc nvarchar(100) = '" + column.MS_Description + "';\r\n " + updateScript;
                            updateScript = updateScript.Replace("@table", "@table" + idx);
                            updateScript = updateScript.Replace("@column", "@column" + idx);
                            updateScript = updateScript.Replace("@desc", "@desc" + idx);
                            tbExportScript.Text += updateScript;

                            tbExportScript.Text += "\r\n GO \r\n";

                            idx++;
                        }
                    }
                    var connStrBuilder = new SqlConnectionStringBuilder(_connectionString);
                }

            }
            catch (Exception exception)
            {
                MessageBox.Show("發生錯誤, Msg:" + exception.Message);
            }
        }

        private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (CheckSQLConnection(_connectionString, false))
            {
                if (cbDBList.SelectedValue == null) return;
                var tableName = cbDBList.SelectedValue.ToString();

                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    var colName = row.Cells["Name"].Value;

                    if (row.Cells["MS_Description"].Value != null)
                    {
                        var colMS_Description = row.Cells["MS_Description"].Value.ToString();

                        var ColumnObject = _collections[tableName].Columns.FirstOrDefault(a => a.Name == colName);
                        if (ColumnObject != null)
                        {
                            ColumnObject.MS_Description = colMS_Description;
                            ColumnObject.modify = true;
                        }
                    }
                }
            }
        }

        private void tbMS_DescriptionForTable_TextChanged(object sender, EventArgs e)
        {
            if (CheckSQLConnection(_connectionString, false))
            {
                if (cbDBList.SelectedValue == null) return;
                var tableName = cbDBList.SelectedValue.ToString();

                if (!string.IsNullOrEmpty(tbMS_DescriptionForTable.Text))
                {
                    _collections[tableName].MS_Description = tbMS_DescriptionForTable.Text;
                    _collections[tableName].modify = true;
                }
            }

        }

        private void MSDescEditorForm_Load(object sender, EventArgs e)
        {
            if (ConfigurationManager.AppSettings.Get("defaultConn") != null)
                tbConnStr.Text = ConfigurationManager.AppSettings.Get("defaultConn");
        }
    }

    public class TableDesciption
    {
        public string Name { get; set; }
        public string MS_Description { get; set; }

        public List<ColumnDesciption> Columns { get; set; }

        public bool isExist = false;
        public bool modify = false;
    }


    public class ColumnDesciption
    {
        public string Name { get; set; }
        public string MS_Description { get; set; }

        public bool isExist = false;

        public bool modify = false;
    }

}
