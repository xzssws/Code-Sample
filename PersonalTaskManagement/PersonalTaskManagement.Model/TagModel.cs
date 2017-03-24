using System;

namespace PersonalTaskManagement.Model
{
    /// <summary>
    /// 标签表 [实体类]
    /// </summary>
    [Serializable]
    public partial class TagModel : ICloneable
    {
        private int @id;

        /// <summary>
        /// 标签标识
        /// </summary>
        public int ID
        {
            get { return @id; }
            set { @id = value; }
        }

        private string @name;

        /// <summary>
        /// 标签名称
        /// </summary>
        public string Name
        {
            get { return @name; }
            set { @name = value; }
        }

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}