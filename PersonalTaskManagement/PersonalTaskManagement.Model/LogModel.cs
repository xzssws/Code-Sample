using System;

namespace PersonalTaskManagement.Model
{
    /// <summary>
    /// 日志表 [实体类]
    /// </summary>
    [Serializable]
    public partial class LogModel
    {
        private DateTime @time;

        /// <summary>
        /// 时间
        /// </summary>
        public DateTime Time
        {
            get { return @time; }
            set { @time = value; }
        }

        private string @type;

        /// <summary>
        /// 类型
        /// </summary>
        public string Type
        {
            get { return @type; }
            set { @type = value; }
        }

        private string @message;

        /// <summary>
        /// 信息
        /// </summary>
        public string Message
        {
            get { return @message; }
            set { @message = value; }
        }
    }
}