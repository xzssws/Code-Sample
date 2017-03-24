using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace TrainingProject
{
    public partial class MainWindow : Form
    {
        public MainWindow()
        {
            InitializeComponent();
            GetForms();
        }

        public void GetForms()
        {
            foreach (KeyValuePair<string, Form> item in Program.WinForms)
            {
                Button button = new Button();
                button.Text = item.Value.Text;
                button.Tag = item.Key;
                button.Visible = true;
                button.Click += new EventHandler(ItemClick);
                flowLayoutPanel1.Controls.Add(button);
            }
        }

        private void ItemClick(object sender, EventArgs e)
        {
            Button button = (sender as Button);
            string formguid = button.Tag.ToString();
            if (chkTabPage.Checked)
            {
                AddTabPage(formguid);
            }
            else
            {
                Program.CreateForm(formguid).Show();
            }
        }

        //关闭一个Tab页
        private void CloseTagPage(TabPage tabpage)
        {
            OpenTabPage.Remove(tabpage.Tag.ToString());
            tabControl1.Controls.Remove(tabpage);
        }

        //创建一个包含窗体的Tab页
        private bool CreateOrSelectFormPage(string FormCode, out TabPage tp)
        {
            if (!OpenTabPage.ContainsKey(FormCode))
            {
                Form form = Program.CreateForm(FormCode);
                form.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
                form.Dock = DockStyle.Fill;
                form.TopLevel = false;
                form.Visible = true;
                TabPage tabpage = new TabPage(form.Text);
                tabpage.Tag = FormCode;
                tabpage.Controls.Add(form);
                OpenTabPage.Add(FormCode, tabpage);
                tp = tabpage;
                return true;
            }
            else
            {
                tp = OpenTabPage[FormCode];
                return false;
            }
        }

        private void AddTabPage(string FormCode)
        {
            TabPage tabpage;
            bool result = CreateOrSelectFormPage(FormCode, out tabpage);
            if (result)
            {
                tabControl1.Controls.Add(tabpage);
            }
            tabControl1.SelectedTab = tabpage;
        }

        //所有打开的页
        private Dictionary<string, TabPage> OpenTabPage = new Dictionary<string, TabPage>();

        private void 关闭当前窗体ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CloseTagPage(tabControl1.SelectedTab);
        }

        private void 关闭其他窗体ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (TabPage item in tabControl1.Controls)
            {
                if (item != tabControl1.SelectedTab)
                {
                    CloseTagPage(item);
                }
            }
        }

        private void 关闭所有窗体ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tabControl1.Controls.Clear();
            OpenTabPage.Clear();
        }


    }
}