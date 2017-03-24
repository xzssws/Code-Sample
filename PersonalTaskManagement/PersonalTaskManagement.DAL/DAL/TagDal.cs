using PersonalTaskManagement.Model;
using System;
using System.Collections.Generic;

namespace PersonalTaskManagement.DAL
{
    /// <summary>
    /// 提供标签表的基础数据访问操作（CRUD）
    /// </summary>
    public partial class TagDal
    {
        /// <summary>
        /// 增加一条[标签表]
        /// </summary>
        /// <param name="entity">[标签表]新增</param>
        /// <exception cref="ArgumentNullException">参数为空抛出异常</exception>
        /// <returns>是否成功</returns>
        public static bool Add(TagModel entity)
        {
            if (entity == null) throw new ArgumentNullException("系统异常:参数 entity 是空值");
            MyBatis.SqlMap.Insert("Insert-Tag", entity);
            return true;
        }

        /// <summary>
        /// 更改一条 [标签表]
        /// </summary>
        /// <param name="entity">[标签表]更新</param>
        /// <exception cref="ArgumentNullException">参数为空抛出异常</exception>
        /// <returns>是否成功</returns>
        public static bool Update(TagModel entity)
        {
            if (entity == null) throw new ArgumentNullException("系统异常:参数 entity 是空值");
            int result = MyBatis.SqlMap.Update("Update-Tag", entity);
            return result > 0;
        }

        /// <summary>
        /// 删除一条[标签表]
        /// </summary>
        /// <param name="entityID">要删除[标签表]的主键值</param>
        /// <exception cref="ArgumentNullException">参数为空抛出异常</exception>
        /// <returns>是否成功</returns>
        public static bool Delete(string entityID)
        {
            if (string.IsNullOrEmpty(entityID)) throw new ArgumentNullException("系统异常:参数 entityID 是空值");
            int result = MyBatis.SqlMap.Delete("Delete-Tag", entityID);
            return result > 0;
        }

        /// <summary>
        /// 根据条件批量删除[标签表]
        /// </summary>
        /// <param name="entity">[标签表]删除条件</param>
        /// <exception cref="ArgumentNullException">参数为空抛出异常</exception>
        /// <returns>是否成功</returns>
        public static bool Deletes(TagModel entity)
        {
            if (entity == null) throw new ArgumentNullException("系统异常:参数 entity 是空值");
            int result = MyBatis.SqlMap.Delete("Delete-Tag-ByCondition", entity);
            return result > 0;
        }

        /// <summary>
        /// 按条件查询[标签表]
        /// <param name="entity">[标签表]筛选</param>
        /// </summary>
        /// <exception cref="ArgumentNullException">参数为空抛出异常</exception>
        /// <returns>查询结果</returns>
        public static IList<TagModel> Select(TagModel entity)
        {
            if (entity == null) throw new ArgumentNullException("系统异常:参数 entity 是空值");
            return MyBatis.SqlMap.QueryForList<TagModel>("Select-Tag", entity);
        }

        /// <summary>
        /// 查询所有[标签表]
        /// </summary>
        /// <returns>查询结果</returns>
        public static IList<TagModel> Selects()
        {
            return MyBatis.SqlMap.QueryForList<TagModel>("Select-Tag", null);
        }
    }
}