using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Collections;
using System.Windows.Forms;
using System.Data.OleDb;
using System.Data.Common;
using System.Data.SqlClient;

public partial class ImportExcel
{
    private static string filename;
    private static string savePath;
    private static DataSet ds; //要插入的数据
    private static DataTable dt;

    private void UpLoad(string excelFile)
    {
        filename = excelFile;
        //上传代码
        savePath = "";//服务器存储位置
        bool isExcel = IsExcel();
        if (isExcel)
        {
            //上传失败
        }
        else
        {
            //上传成功
        }
    }

    //判断文件是否是excel文件函数
    private bool IsExcel()
    {
        string fileExtend = System.IO.Path.GetExtension(filename);
        if (fileExtend == ".xlsx" || fileExtend == ".xls") return true;
        else return false;
    }

    //从excel表中获取数据的函数
    public void ExcelToDataSet()
    {
        string strConn = "Provider = Microsoft.Jet.OLEDB.4.0 ; Data Source = " + savePath + ";Extended Properties=\"Excel 8.0;HDR=Yes;IMEX=1\"";
        OleDbConnection conn = new OleDbConnection(strConn); //连接excel            
        if (conn.State.ToString() == "Open")
        {
            conn.Close();
        }
        conn.Open();    //外部表不是预期格式，不兼容2010的excel表结构
        string s = conn.State.ToString();
        OleDbDataAdapter myCommand = null;
        ds = null;
        /*DataTable yTable = conn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, new Object[] { null, null, null, "TABLE" });//获取表的框架,几行几列
        string tableName = yTable.Rows[0]["filename"].ToString(); //表示的是几行几列  
        string strSel = "select * from [" + filename + "]";//xls */
        string strExcel = "select * from [sheet1$]";  //如果有多个sheet表时可以选择是第几张sheet表    
        myCommand = new OleDbDataAdapter(strExcel, conn);//用strExcel初始化myCommand，查看myCommand里面的表的数据？？
        ds = new DataSet();
        myCommand.Fill(ds);     //把表中的数据存放在ds(dataSet)
        conn.Close();
        try
        {
            dt = ds.Tables[0];
        }
        catch (Exception err)
        {
            MessageBox.Show("操作失败！" + err.ToString());
        }

    }

    //excel导入数据库sql的按钮触发
    protected void Button3_Click(object sender, EventArgs e)
    {
        string connString = "server=localhost;uid=sa;pwd=1234;database=Test";   //连接数据库的路径方法

        SqlConnection conn = new SqlConnection(connString);
        conn.Open();

        DataRow dr = null;
        int DataCount = dt.Columns.Count;//获取列数
        for (int i = 0; i < dt.Rows.Count; i++)  //记录表中的行数，循环插入
        {
            dr = dt.Rows[i];
            insertToSql(dr, DataCount, conn);
        }
        conn.Close();

        try
        {
            using (System.Data.SqlClient.SqlBulkCopy Bcopy = new SqlBulkCopy(conn))
            {
                Bcopy.SqlRowsCopied += new SqlRowsCopiedEventHandler(Bcopy_SqlRowsCopied);
                Bcopy.BatchSize = 100;
                Bcopy.NotifyAfter = 100;
                Bcopy.DestinationTableName = savePath;
                Bcopy.WriteToServer(ds.Tables[0]);
            }
        }
        catch (Exception ex)
        {

            throw;
        }
        //可另行查询判断是否取回数据 然后判断
    }

    void Bcopy_SqlRowsCopied(object sender, SqlRowsCopiedEventArgs e)
    {
    }
    //使用bcp，不容易出错而且效率高
    /*try
    {
        using (System.Data.SqlClient.SqlBulkCopy bcp = new System.Data.SqlClient.SqlBulkCopy(connString))
        {
            bcp.SqlRowsCopied += new System.Data.SqlClient.SqlRowsCopiedEventHandler(bcp_SqlRowsCopied);
            bcp.BatchSize = 100;//每次传输的行数   
            bcp.NotifyAfter = 100;//进度提示的行数   
            bcp.DestinationTableName = savePath;//目标表   
            bcp.WriteToServer(ds.Tables[0]);
        }   
    }
    catch
    {
        System.Windows.Forms.MessageBox.Show(ex.Message);
    }*/

    //插入数据库的函数
    protected void insertToSql(DataRow dr, int column_count, SqlConnection conn)
    {
        //excel表中的列名和数据库中的列名一定要对应  
        string name = dr[0].ToString();//需要把内个列都列出来
        string age = dr[1].ToString();
        string sex = dr[2].ToString();
        //当数据库中有多个表时，怎么分辨插入的表
        string sql = "insert into 客户 values('" + name + "','" + age + "','" + sex + "')";
        SqlCommand cmd = new SqlCommand(sql, conn);
        cmd.ExecuteNonQuery();
    }

    //从excel表中获取数据并存在
    //    protected void ImportFromExcel()
    //    {
    //        string execelConnectionStr = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=filename;
    //        Extended Properties=""Excel 8.0;HDR=YES;IMEX=1\""";//表第一行是标题，不做为数据使用, Excel 档案只能用来做“读取”用途。

    //        ds = new DataSet();
    //        string connString = "Provider = Microsoft.Jet.OLEDB.4.0 ; Data Source = " 
    //            + savePath + ";Extended Properties=\"Excel 8.0;HDR=Yes;IMEX=1\"";

    //        DataTable table = OleDbHelper.GetExcelTables(connString);
    //        if (table == null || table.Rows.Count <= 0)
    //        {
    //            return;
    //        }

    //        foreach (DataRow dr in table.Rows)
    //        {
    //            string cmdText = "select * from [" + dr["TABLE_NAME"].ToString() + "]";
    //            DataTable dt = OleDbHelper.FillDataTable(connString, cmdText);
    //            dt.TableName = dr["TABLE_NAME"].ToString();
    //            ds.Tables.Add(dt);
    //        }

    //    }

}
