using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Text;
using System.Windows.Forms;
using Microsoft.Office.Interop.Word;

namespace TrainingProject
{
    public partial class 全文索引 : BaseForm
    {
        private const string conn = @"server=.;database=test;uid=sa;pwd=123456";

        private string file;

        public 全文索引()
        {
            InitializeComponent();
            PublicConn.Rtt = this.richTextBox1;
            AddCommand("打开", OpenFile);
            AddCommand("保存", SaveFile);
        }

        private void SaveFile()
        {
            if (Save(out file))
            {
                PublicConn.SaveAsWord(file, WdSaveFormat.wdFormatRTF);
                PublicConn.SaveBinary(file);
                MessageBox.Show("成功");
            }
        }

        private void OpenFile()
        {
            if (Open(out file))
            {
                PublicConn.OpenAsWord(file);
            }
        }

        private bool Open(out string file)
        {
            file = string.Empty;
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                file = ofd.FileName;
                return true;
            }
            return false;
        }

        private bool Save(out string file)
        {
            file = string.Empty;
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                file = sfd.FileName;
                return true;
            }
            return false;
        }
    }
    public class PublicConn
    {
        /// <summary>
        /// 数据库连接字符串
        /// </summary>
        private const string conn = @"server=.;database=test;uid=sa;pwd=123456";

        private static RichTextBox rtt;
        public static RichTextBox Rtt
        {
            get { return rtt; }
            set { rtt = value; }
        }

        private PublicConn()
        {

        }

        public void RtfToDoc(string strSource, string strTarget, WdSaveFormat ToFormat)
        {
            Microsoft.Office.Interop.Word.Application newApp = new Microsoft.Office.Interop.Word.Application();

            // 指定源文件和目标文件
            object Source = strSource;
            object Target = strTarget;

            object Unknown = Type.Missing;
            // 打开要转换的Word文件
            newApp.Documents.Open(ref Source, ref Unknown,
             ref Unknown, ref Unknown, ref Unknown,
             ref Unknown, ref Unknown, ref Unknown,
             ref Unknown, ref Unknown, ref Unknown,
             ref Unknown);

            // 指定文档的类型
            object format = ToFormat;

            //改变文档类型
            newApp.ActiveDocument.SaveAs(ref Target, ref format,
             ref Unknown, ref Unknown, ref Unknown,
             ref Unknown, ref Unknown, ref Unknown,
             ref Unknown, ref Unknown, ref Unknown);

            //关闭word实例
            newApp.Quit(ref Unknown, ref Unknown, ref Unknown);
        }

        public static void SaveAsWord(string fileName, WdSaveFormat saveFormat)
        {
            ApplicationClass app = new ApplicationClass();
            Document doc = null;
            object missing = System.Reflection.Missing.Value;
            object File = fileName;
            try
            {
                doc = app.Documents.Add(ref missing, ref missing, ref missing, ref missing);
                doc.ActiveWindow.Selection.WholeStory();//全选
                //内容
                Rtt.SelectAll();

                Clipboard.SetData(DataFormats.Rtf, Rtt.SelectedRtf);//复制RTF数据到剪贴板
                doc.ActiveWindow.Selection.Paste();
                object dd = saveFormat;
                doc.SaveAs(ref File, ref dd, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing);
            }
            finally
            {
                if (doc != null)
                {
                    doc.Close(ref missing, ref missing, ref missing);
                    doc = null;
                }

                if (app != null)
                {
                    app.Quit(ref missing, ref missing, ref missing);
                    app = null;
                }
            }
        }

        public static void OpenAsWord(string fileName)
        {
            ApplicationClass app = new ApplicationClass();
            Document doc = null;
            object missing = System.Reflection.Missing.Value;
            object File = fileName;
            object readOnly = false;
            object isVisible = true;
            try
            {
                doc = app.Documents.Open(ref File, ref missing, ref readOnly, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref isVisible, ref missing, ref missing, ref missing, ref missing);
                doc.ActiveWindow.Selection.WholeStory();//全选word文档中的数据
                doc.ActiveWindow.Selection.Copy();//复制数据到剪切板
                Rtt.Paste();//richTextBox粘贴数据
            }
            finally
            {
                if (doc != null)
                {
                    doc.Close(ref missing, ref missing, ref missing);
                    doc = null;
                }
                if (app != null)
                {
                    app.Quit(ref missing, ref missing, ref missing);
                    app = null;
                }
            }
        }

        /// <summary>
        /// 文件转SplParameter 
        /// </summary>
        /// <param name="name">参数名称</param>
        /// <param name="filepath">文件路径</param>
        /// <returns>参数</returns>
        private static SqlParameter FileToSplParam(string name, string filepath)
        {
            SqlParameter sp = new SqlParameter();
            FileInfo fi = new FileInfo(filepath);
            byte[] b = null;

            if (fi.Exists)
            {
                using (FileStream fs = File.OpenRead(filepath))
                {
                    b = new byte[fi.Length];
                    UTF8Encoding temp = new UTF8Encoding(true);
                    fs.Read(b, 0, b.Length);
                    sp.Value = b;
                }
            }

            sp.ParameterName = name;
            sp.Value = b;
            return sp;
        }

        public static void SaveBinary(string FilePath)
        {
            FileInfo fi = new FileInfo(FilePath);
            string name = fi.Name;
            string type = fi.Extension.Replace(".", "");

            SqlConnection Conn;
            SqlCommand cmdUploadDoc;
            Conn = new SqlConnection(conn);

            cmdUploadDoc = new SqlCommand("CPP_InsertOneBlobDataToTable", Conn);
            cmdUploadDoc.CommandType = CommandType.StoredProcedure;
            cmdUploadDoc.Parameters.AddWithValue("@FileName", name);
            cmdUploadDoc.Parameters.Add(FileToSplParam("@FileContent", FilePath));
            cmdUploadDoc.Parameters.AddWithValue("@FileType", type);

            Conn.Open();
            cmdUploadDoc.ExecuteNonQuery();
            Conn.Close();
        }

        private static string FilePath;

    }
}