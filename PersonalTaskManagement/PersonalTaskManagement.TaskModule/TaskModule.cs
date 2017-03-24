using Prism.Modularity;
using Prism.Regions;

namespace PersonalTaskManagement.TaskModule
{
    /// <summary>
    /// 任务模块类
    /// </summary>
    /// <remarks>无</remarks>
    public class TaskModule : IModule
    {
        /// <summary>
        /// 区域管理器
        /// </summary>
        private IRegionManager _regionManager;

        /// <summary>
        /// 构造<see cref="TaskModule"/>的新实例
        /// </summary>
        /// <param name="regionManager">区域管理器</param>
        public TaskModule(IRegionManager regionManager)
        {
            _regionManager = regionManager;
        }

        /// <summary>
        /// 初始化
        /// </summary>
        public void Initialize()
        {
            _regionManager.RegisterViewWithRegion("TaskManagementRegion", typeof(TaskManagement));
        }
    }
}