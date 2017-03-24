using Prism.Modularity;
using Prism.Regions;

namespace PersonalTaskManagement.TagModule
{
    /// <summary>
    /// TagModule类
    /// </summary>
    /// <remarks>无</remarks>
    public class TagModule : IModule
    {
        /// <summary>
        /// 区域管理对象
        /// </summary>
        private IRegionManager _regionManager;

        /// <summary>
        /// 构造<see cref="TagModule"/>的新实例
        /// </summary>
        /// <param name="regionManager">区域管理器</param>
        public TagModule(IRegionManager regionManager)
        {
            _regionManager = regionManager;
        }

        /// <summary>
        /// 初始化占位符映射
        /// </summary>
        /// <remarks>无</remarks>
        public void Initialize()
        {
            _regionManager.RegisterViewWithRegion("TagManagementRegion", typeof(TagManagement));
        }
    }
}