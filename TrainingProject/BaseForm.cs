using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Reflection;
using System.Runtime.Serialization.Formatters.Binary;

namespace TrainingProject
{
    /// <summary>
    /// 公共父类练习窗体
    /// </summary>
    public partial class BaseForm : Form
    {
        public BaseForm()
        {
            InitializeComponent();
        }

        public override string Text
        {
            get
            {
                return base.Text;
            }
            set
            {
                label1.Text = value;
                base.Text = value;
            }
        }
        /// <summary>
        /// 命令列表
        /// </summary>
        private List<Command> CommandList;

        //添加命令
        public void AddCommand(string Name, Action Go)
        {
            if (CommandList == null) CommandList = new List<Command>();
            CommandList.Add(new Command
            {
                Name = Name,
                Click = (sender, e) =>
                {
                    this.CommandSource = sender;
                    this.eventarg = e;
                    Go();
                    ClearParam();
                }
            });
        }
        //清除事件参数
        private void ClearParam()
        {
            CommandSource = null;
            eventarg = null;
        }
        public object CommandSource { get; set; }
        public EventArgs eventarg { get; set; }
        //加载到工具栏
        public void FlushTool()
        {
            if (CommandList != null && CommandList.Count > 0)
            {
                foreach (Command item in CommandList)
                {
                    ToolStripItem titem = toolStrip1.Items.Add(item.Name);
                    titem.ForeColor = Color.White;
                    titem.Click += item.Click;
                }
            }
            else
            {
                toolStrip1.Hide();
                label1.Dock = DockStyle.Fill;
            }
        }

        private void BaseForm_Load(object sender, EventArgs e)
        {
            FlushTool();
        }
        //强制刷新策略  添加完运行特定方法更新z

    }
    public class Command
    {
        public string Name { get; set; }
        public EventHandler Click { get; set; }
    }
}