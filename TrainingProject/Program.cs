using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Reflection;

namespace TrainingProject
{
    static class Program
    {

        public static Form MainForm;

        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Load();
            Application.Run(new MainWindow());
        }
        public static Dictionary<string, Form> WinForms = new Dictionary<string, Form>();

        public static Dictionary<string, Type> winformsX = new Dictionary<string, Type>();
        public static void Load()
        {
            Assembly am = Assembly.GetExecutingAssembly();
            Type[] typs = am.GetTypes();
            foreach (Type item in typs)
            {
                if ((item.BaseType == typeof(Form) || item.BaseType == typeof(BaseForm) || item.BaseType == typeof(DevExpress.XtraBars.Ribbon.RibbonForm)) && GoOut(item, "MainWindow", "BaseForm"))
                {
                    Form form = (Form)Activator.CreateInstance(item);
                    string guid = Guid.NewGuid().ToString();
                    winformsX.Add(guid, item);
                    WinForms.Add(guid, form);
                }
            }
        }
        /// <summary>
        /// 排除窗体名称的类型
        /// </summary>
        /// <param name="item">类型</param>
        /// <param name="OutName">窗体名称</param>
        /// <returns></returns>
        public static bool GoOut(Type item,params string[] OutName)
        {
            for (int i = 0; i < OutName.Length; i++)
            {
                if (item.Name == OutName[i]) { return false; }
            }
            return true;
        }
        public static Form CreateForm(string typeItem)
        {
            Type type = winformsX[typeItem];
            Form form = (Form)Activator.CreateInstance(type);
            return form;
        }
    }
}
