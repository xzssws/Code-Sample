using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Reflection;

namespace TrainingProject
{
    //测试成功
    public partial class 集合深Copy : TrainingProject.BaseForm
    {
        public 集合深Copy()
        {
            InitializeComponent();
            Load();
        }
        #region 属性
        public List<student> oldStus { get; set; }
        public List<student> newStus { get; set; }
        #endregion

        #region 命令触发
        //更新
        private void UpdateStudent()
        {
            newStus[3].Name = "XXXXXXXXXXX";
            FlushStudnet();
        }
        //加载
        private void LoadStudent()
        {
            oldStus = new List<student>();
            oldStus.Add(new student { ID = 1, Name = "巴嘎雅路" });
            oldStus.Add(new student { ID = 2, Name = "巴嘎雅路1" });
            oldStus.Add(new student { ID = 3, Name = "巴嘎雅路2" });
            oldStus.Add(new student { ID = 4, Name = "巴嘎雅路3" });
            oldStus.Add(new student { ID = 5, Name = "巴嘎雅路4" });
            oldStus.Add(new student { ID = 6, Name = "巴嘎雅路5" });
            oldStus.Add(new student { ID = 7, Name = "巴嘎雅路6" });
            oldStus.Add(new student { ID = 8, Name = "巴嘎雅路7" });
            oldStus.Add(new student { ID = 9, Name = "巴嘎雅路8" });
            oldStus.Add(new student { ID = 10, Name = "巴嘎雅路9" });
            oldStus.Add(new student { ID = 11, Name = "巴嘎雅路10" });
            newStus = CopyList<student>(oldStus);
            FlushStudnet();
        }
        //还原
        private void ReturnStudent()
        {
            newStus = CopyList<student>(oldStus);
            FlushStudnet();
        }
        //刷新
        private void FlushStudnet()
        {
            if (oldStus == null) return;
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = newStus;
        }

        #endregion

        #region 初始化
        private void Load()
        {
            AddCommand("加载", LoadStudent);
            AddCommand("更新", UpdateStudent);
            AddCommand("还原", ReturnStudent);
            AddCommand("刷新", FlushStudnet);
        }
        #endregion

        #region 核心代码
        public List<T> CopyList<T>(List<T> ls) where T : new()
        {
            List<T> newList = new List<T>();
            ls.ForEach((t) => newList.Add(GetNew<T>(t)));
            return newList;
        }

        public T GetNew<T>(T source) where T : new()
        {
            Type info = typeof(T);
            PropertyInfo[] propertys = info.GetProperties();
            T t = new T();
            foreach (PropertyInfo pi in propertys)
            {
                info.GetProperty(pi.Name).SetValue(t, pi.GetValue(source, null), null);
            }
            return t;
        }
        #endregion
    }
    public class student
    {
        public int ID { get; set; }
        public string Name { get; set; }
    }
}
