using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace TrainingProject
{
    public partial class 文件实时监控 : TrainingProject.BaseForm
    {
        public 文件实时监控()
        {
            InitializeComponent();
        }

        private void 文件实时监控_Load(object sender, EventArgs e)
        {

        }
    }
    public class Watcher
    {
        FileSystemWatcher watcher = new FileSystemWatcher();

        public void Start(string path)
        {
            //设置监视路径
            watcher.Path = path;
            //监视哪些元素
            watcher.NotifyFilter = NotifyFilters.LastAccess | NotifyFilters.LastWrite | NotifyFilters.FileName | NotifyFilters.DirectoryName;
            //监视文档类型
            watcher.Filter = "*.txt";

            watcher.Changed += new FileSystemEventHandler(watcher_Changed);
            watcher.Created += new FileSystemEventHandler(watcher_Created);
            watcher.Deleted += new FileSystemEventHandler(watcher_Deleted);
            watcher.Renamed += new RenamedEventHandler(watcher_Renamed);

            //启动控件
            watcher.EnableRaisingEvents = true;
        }

        void watcher_Renamed(object sender, RenamedEventArgs e)
        {
            ShowMessage("命名操作:" + e.OldName + "重新命名为" + e.Name);
        }

        void watcher_Deleted(object sender, FileSystemEventArgs e)
        {
            ShowMessage("删除操作:" + e.Name + "已删除");
        }

        void watcher_Created(object sender, FileSystemEventArgs e)
        {
            ShowMessage("创建操作:" + e.Name);
        }

        void watcher_Changed(object sender, FileSystemEventArgs e)
        {
            ShowMessage("更改目录:" + e.Name);
        }
        void ShowMessage(string msg)
        {
            Console.WriteLine(msg);
        }
    }
}
