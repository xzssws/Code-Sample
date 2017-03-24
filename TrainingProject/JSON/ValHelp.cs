using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace TrainingProject.JSON
{
    /// <summary>
    /// 验证工具包
    /// </summary>
    class ValHelp
    {
        /// <summary>
        /// 验证邮箱
        /// </summary>
        /// <param name="source">目标字符串</param>
        /// <returns>验证结果</returns>
        public static bool IsEmail(string source)
        {
            return Regex.IsMatch(source, @"^[A-Za-z0-9](([_\.\-]?[a-zA-Z0-9]+)*)@([A-Za-z0-9]+)(([\.\-]?[a-zA-Z0-9]+)*)\.([A-Za-z]{2,})$", RegexOptions.IgnoreCase);
        }
        /// <summary>
        /// 验证网址
        /// </summary>
        /// <param name="source">目标字符串</param>
        /// <returns>验证结果</returns>
        public static bool IsUrl(string source)
        {
            return Regex.IsMatch(source, @"^(((file|gopher|news|nntp|telnet|http|ftp|https|ftps|sftp)://)|(www\.))+(([a-zA-Z0-9\._-]+\.[a-zA-Z]{2,6})|([0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}))(/[a-zA-Z0-9\&amp;%_\./-~-]*)?$", RegexOptions.IgnoreCase);
        }
        /// <summary>
        /// 验证是否含有Url
        /// </summary>
        /// <param name="source">目标字符串</param>
        /// <returns>验证结果</returns>
        public static bool HasUrl(string source)
        {
            return Regex.IsMatch(source, @"(((file|gopher|news|nntp|telnet|http|ftp|https|ftps|sftp)://)|(www\.))+(([a-zA-Z0-9\._-]+\.[a-zA-Z]{2,6})|([0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}))(/[a-zA-Z0-9\&amp;%_\./-~-]*)?", RegexOptions.IgnoreCase);
        }
        /// <summary>
        /// 验证是否是时间
        /// </summary>
        /// <param name="source">输入</param>
        /// <returns>输出</returns>
        public static bool IsDateTime(string source)
        {
            try
            {
                DateTime time = Convert.ToDateTime(source);
                return true;
            }
            catch
            {
                return false;
            }
        }
        /// <summary>
        /// 是否是手机号
        /// </summary>
        /// <param name="source">手机号</param>
        /// <returns>验证结果</returns>
        public static bool IsPhone(string source)
        {
            return Regex.IsMatch(source, @"^0{0,1}(13[0-9]|14[0-9]|15[0-9]|18[0-9])[0-9]{8}$", RegexOptions.IgnoreCase);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="source"></param>
        /// <returns>验证结果</returns>
        public static bool IsIPV4(string source)
        {
            return Regex.IsMatch(source, @"^(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[1-9])\.(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[1-9]|0)\.(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[1-9]|0)\.(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[0-9])$", RegexOptions.IgnoreCase);
        }
        /// <summary>
        /// 正常字符
        /// </summary>
        /// <param name="source"></param>
        /// <returns>验证结果</returns>
        public static bool IsNormalChar(string source)
        {
            return Regex.IsMatch(source, @"[\w\d_]+", RegexOptions.IgnoreCase);
        }
        /// <summary>
        /// 是否中文
        /// </summary>
        /// <param name="source"></param>
        /// <returns>验证结果</returns>
        public static bool IsChinese(string source)
        {
            return Regex.IsMatch(source, @"^[\u4e00-\u9fa5]+$", RegexOptions.IgnoreCase);
        }

    }
}
