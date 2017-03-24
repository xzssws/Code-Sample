using System;
using System.Windows.Forms;
using Microsoft.Office.Interop.Word;
using System.IO;

namespace TrainingProject
{
    public partial class 保存DOC : BaseForm
    {
        public 保存DOC()
        {
            InitializeComponent();
            AddCommand("打开",OpenFile);
            AddCommand("保存", SaveFile);
        }

        //打开
        private string filename;

        private void OpenFile()
        {
            if (Open(out filename))
            {
                OpenAsWord(filename);
            }
        }

        private void OpenAsWord(string fileName)
        {
            ApplicationClass app = new ApplicationClass();

            Document doc = null;

            object missing = System.Reflection.Missing.Value;

            object File = fileName;

            object readOnly = false;

            object isVisible = true;

            try
            {
                doc = app.Documents.Open(ref File, ref missing, ref readOnly,

                 ref missing, ref missing, ref missing, ref missing, ref missing,

                 ref missing, ref missing, ref missing, ref isVisible, ref missing,

                 ref missing, ref missing, ref missing);

                doc.ActiveWindow.Selection.WholeStory();//全选word文档中的数据

                doc.ActiveWindow.Selection.Copy();//复制数据到剪切板

                richTextBox1.Paste();//richTextBox粘贴数据

                //richTextBox1.Text = doc.Content.Text;//显示无格式数据
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

        public void SaveAsWord(string fileName)
        {
            ApplicationClass app = new ApplicationClass();
            Document doc = null;
            object missing = System.Reflection.Missing.Value;
            object File = fileName;
            try
            {
                doc = app.Documents.Add(ref missing, ref missing, ref missing, ref missing);

                doc.ActiveWindow.Selection.WholeStory();//全选  
                richTextBox1.SelectAll();
                Clipboard.SetData(DataFormats.Rtf, richTextBox1.SelectedRtf);//复制RTF数据到剪贴板   
                doc.ActiveWindow.Selection.Paste();

                doc.SaveAs(ref File, ref missing, ref missing,
                    ref missing, ref missing, ref missing,
                    ref missing, ref missing, ref missing,
                    ref missing, ref missing, ref missing,
                    ref missing, ref missing, ref missing,
                    ref missing);
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

        //保存
        private void SaveFile()
        {
            if (Save(out filename))
            {
                SaveAsWord(filename);
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
}