using System;

namespace PersonalTaskManagement.Model
{
    /// <summary>
    /// 任务表 [实体类]
    /// </summary>
    [Serializable]
    public partial class TaskModel : ICloneable
    {
        private int @id;

        /// <summary>
        /// 名称
        /// </summary>
        public int ID
        {
            get { return @id; }
            set { @id = value; }
        }

        private string @name;

        /// <summary>
        /// 任务名称
        /// </summary>
        public string Name
        {
            get { return @name; }
            set { @name = value; }
        }

        private string @tag;

        /// <summary>
        /// 关联标签
        /// </summary>
        public string Tag
        {
            get { return @tag; }
            set { @tag = value; }
        }

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}