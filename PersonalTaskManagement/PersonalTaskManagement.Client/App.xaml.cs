using System.Windows;

namespace PersonalTaskManagement.Client
{
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            Bootstrapper bs = new Bootstrapper();
            bs.Run();
        }
    }
}