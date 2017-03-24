using Microsoft.Practices.Unity;
using Prism.Logging;
using Prism.Modularity;
using Prism.Unity;
using System.Windows;

namespace PersonalTaskManagement.Client
{
    public class Bootstrapper : UnityBootstrapper
    {
        //  private read only CallbackLogger callbackLogger = new CallbackLogger();
        /// <summary>
        /// 创建主窗口
        /// </summary>
        /// <returns></returns>
        protected override DependencyObject CreateShell()
        {
            return Container.Resolve<MainWindow>();
        }

        /// <summary>
        /// 初始化主窗口
        /// </summary>
        protected override void InitializeShell()
        {
            Application.Current.MainWindow.Show();
        }

        /// <summary>
        /// 配置模块
        /// </summary>
        protected override void ConfigureModuleCatalog()
        {
            var m = (ModuleCatalog)ModuleCatalog;
            m.AddModule(typeof(PersonalTaskManagement.TagModule.TagModule));
            m.AddModule(typeof(PersonalTaskManagement.TaskModule.TaskModule));
        }

        /// <summary>
        /// 创建日志
        /// </summary>
        /// <returns></returns>
        protected override ILoggerFacade CreateLogger()
        {
            // return this.callbackLogger;
            return base.CreateLogger();
        }

        /// <summary>
        /// 配置容器
        /// </summary>
        protected override void ConfigureContainer()
        {
            base.ConfigureContainer();

            //this.RegisterTypeIfMissing(typeof(IModuleTracker), typeof(ModuleTracker), true);
            // this.Container.RegisterInstance<CallbackLogger>(this.callbackLogger);
        }
    }
}