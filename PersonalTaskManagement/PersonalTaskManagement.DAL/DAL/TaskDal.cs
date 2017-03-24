using PersonalTaskManagement.Model;
using System;
using System.Collections.Generic;

namespace PersonalTaskManagement.DAL
{
    /// <summary>
    /// 提供任务表的基础数据访问操作（CRUD）
    /// </summary>
    public partial class TaskDal
    {
        /// <summary>
        /// 增加一条[任务表]
        /// </summary>
        /// <param name="entity">[任务表]新增</param>
        /// <exception cref="ArgumentNullException">参数为空抛出异常</exception>
        /// <returns>是否成功</returns>
        public static bool Add(TaskModel entity)
        {
            if (entity == null) throw new ArgumentNullException("系统异常:参数 entity 是空值");
            MyBatis.SqlMap.Insert("Insert-Task", entity);
            return true;
        }

        /// <summary>
        /// 更改一条 [任务表]
        /// </summary>
        /// <param name="entity">[任务表]更新</param>
        /// <exception cref="ArgumentNullException">参数为空抛出异常</exception>
        /// <returns>是否成功</returns>
        public static bool Update(TaskModel entity)
        {
            if (entity == null) throw new ArgumentNullException("系统异常:参数 entity 是空值");
            int result = MyBatis.SqlMap.Update("Update-Task", entity);
            return result > 0;
        }

        /// <summary>
        /// 删除一条[任务表]
        /// </summary>
        /// <param name="entityID">要删除[任务表]的主键值</param>
        /// <exception cref="ArgumentNullException">参数为空抛出异常</exception>
        /// <returns>是否成功</returns>
        public static bool Delete(string entityID)
        {
            if (string.IsNullOrEmpty(entityID)) throw new ArgumentNullException("系统异常:参数 entityID 是空值");
            int result = MyBatis.SqlMap.Delete("Delete-Task", entityID);
            return result > 0;
        }

        /// <summary>
        /// 根据条件批量删除[任务表]
        /// </summary>
        /// <param name="entity">[任务表]删除条件</param>
        /// <exception cref="ArgumentNullException">参数为空抛出异常</exception>
        /// <returns>是否成功</returns>
        public static bool Deletes(TaskModel entity)
        {
            if (entity == null) throw new ArgumentNullException("系统异常:参数 entity 是空值");
            int result = MyBatis.SqlMap.Delete("Delete-Task-ByCondition", entity);
            return result > 0;
        }

        /// <summary>
        /// 按条件查询[任务表]
        /// <param name="entity">[任务表]筛选</param>
        /// </summary>
        /// <exception cref="ArgumentNullException">参数为空抛出异常</exception>
        /// <returns>查询结果</returns>
        public static IList<TaskModel> Select(TaskModel entity)
        {
            if (entity == null) throw new ArgumentNullException("系统异常:参数 entity 是空值");
            return MyBatis.SqlMap.QueryForList<TaskModel>("Select-Task", entity);
        }

        /// <summary>
        /// 查询所有[任务表]
        /// </summary>
        /// <returns>查询结果</returns>
        public static IList<TaskModel> Selects()
        {
            return MyBatis.SqlMap.QueryForList<TaskModel>("Select-Task", null);
        }
    }
}