using PersonalTaskManagement.Model;
using System;
using System.Collections.Generic;

namespace PersonalTaskManagement.DAL
{
    /// <summary>
    /// 提供日志表的基础数据访问操作[CRUD]
    /// </summary>
    public partial class LogDal
    {
        /// <summary>
        /// 增加一条[日志表]
        /// </summary>
        /// <param name="entity">[日志表]新增</param>
        /// <exception cref="ArgumentNullException">参数为空抛出异常</exception>
        /// <returns>是否成功</returns>
        public static bool Add(LogModel entity)
        {
            if (entity == null) throw new ArgumentNullException("系统异常:参数 entity 是空值");
            MyBatis.SqlMap.Insert("Insert-Log", entity);
            return true;
        }

        /// <summary>
        /// 更改一条 [日志表]
        /// </summary>
        /// <param name="entity">[日志表]更新</param>
        /// <exception cref="ArgumentNullException">参数为空抛出异常</exception>
        /// <returns>是否成功</returns>
        public static bool Update(LogModel entity)
        {
            if (entity == null) throw new ArgumentNullException("系统异常:参数 entity 是空值");
            int result = MyBatis.SqlMap.Update("Update-Log", entity);
            return result > 0;
        }

        /// <summary>
        /// 删除一条[日志表]
        /// </summary>
        /// <param name="entityID">要删除[日志表]的主键值</param>
        /// <exception cref="ArgumentNullException">参数为空抛出异常</exception>
        /// <returns>是否成功</returns>
        public static bool Delete(string entityID)
        {
            if (string.IsNullOrEmpty(entityID)) throw new ArgumentNullException("系统异常:参数 entityID 是空值");
            int result = MyBatis.SqlMap.Delete("Delete-Log", entityID);
            return result > 0;
        }

        /// <summary>
        /// 根据条件批量删除[日志表]
        /// </summary>
        /// <param name="entity">[日志表]删除条件</param>
        /// <exception cref="ArgumentNullException">参数为空抛出异常</exception>
        /// <returns>是否成功</returns>
        public static bool Deletes(LogModel entity)
        {
            if (entity == null) throw new ArgumentNullException("系统异常:参数 entity 是空值");
            int result = MyBatis.SqlMap.Delete("Delete-Log-ByCondition", entity);
            return result > 0;
        }

        /// <summary>
        /// 按条件查询[日志表]
        /// <param name="entity">[日志表]筛选</param>
        /// </summary>
        /// <exception cref="ArgumentNullException">参数为空抛出异常</exception>
        /// <returns>查询结果</returns>
        public static IList<LogModel> Select(LogModel entity)
        {
            if (entity == null) throw new ArgumentNullException("系统异常:参数 entity 是空值");
            return MyBatis.SqlMap.QueryForList<LogModel>("Select-Log", entity);
        }

        /// <summary>
        /// 查询所有[日志表]
        /// </summary>
        /// <returns>查询结果</returns>
        public static IList<LogModel> Selects()
        {
            return MyBatis.SqlMap.QueryForList<LogModel>("Select-Log", null);
        }
    }
}