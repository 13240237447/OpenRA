using System;
using System.Globalization;
using System.Text;

namespace Util
{
    public static class StringExts
    {
        /// <summary>
        /// 高级比较，可设定是否忽略大小写
        /// </summary>
        public static bool Contains(this string @this, string toCheck, StringComparison comp)
        {
            return @this.IndexOf(toCheck, comp) >= 0;
        }
        /// <summary>
        /// 是否含有中文
        /// </summary>
        public static bool IsContainChinese(this string @this)
        {
            bool flag = false;
            foreach (var a in @this)
            {
                if (a >= 0x4e00 && a <= 0x9fbb)
                {
                    flag = true;
                    break;
                }
            }
            return flag;
        }
        /// <summary>
        /// 是否为空字符串
        /// </summary>
        public static bool IsNullOrEmpty(this string @this)
        {
            return string.IsNullOrEmpty(@this);
        }
        /// <summary>
        /// string Base64加密
        /// </summary>
        public static string StringToBase64(this string @this)
        {
            var b = Encoding.Default.GetBytes(@this);
            return Convert.ToBase64String(b);
        }
        public static string Base64ToString(this string @this)
        {
            var b = Convert.FromBase64String(@this);
            return Encoding.Default.GetString(b);
        }
        /// <summary>
        /// 获取字符串之间的内容
        /// </summary>
        /// <param name="start">首部</param>
        /// <param name="end">尾部</param>
        /// <param name="inculdeStartAndEnd">是否包含首位</param>
        /// <returns>首尾之间的字段</returns>
        public static string Between(this string @this, string start, string end, bool inculdeStartAndEnd = false)
        {
            if (start.Equals(end))
                throw new ArgumentException("Start string can't equals a end string.");
            int startIndex = @this.LastIndexOf(start) + 1;
            int endIndex = @this.LastIndexOf(end) - 1 - @this.LastIndexOf(start);
            if (!inculdeStartAndEnd)
                return @this.Substring(startIndex + start.Length, endIndex - end.Length);
            else
                return @this.Substring(startIndex, endIndex + end.Length);
        }
        /// <summary>
		/// 移除首个字符
		/// </summary>
		public static string RemoveFirstChar(this string @this)
        {
            if (string.IsNullOrEmpty(@this))
                return @this;
            return @this.Substring(1);
        }
        /// <summary>
        /// 移除末尾字符
        /// </summary>
        public static string RemoveLastChar(this string @this)
        {
            if (string.IsNullOrEmpty(@this))
                return @this;
            return @this.Substring(0, @this.Length - 1);
        }

        /// <summary>
        /// 尝试转换成int
        /// </summary>
        public static int TryParseInt(this string @this)
        {
            if (string.IsNullOrEmpty(@this))
                return 0;
            return int.TryParse(@this, out int val) ? val : 0;
        }

        
        public static long TryParseLong(this string @this)
        {
            if (string.IsNullOrEmpty(@this))
                return 0;
            return long.TryParse(@this, out long val) ? val : 0;
        }
        
        /// <summary>
        /// 尝试转换成float
        /// </summary>
        public static float TryParseFloat(this string @this)
        {
            if (string.IsNullOrEmpty(@this))
                return 0;
            return float.TryParse(@this, NumberStyles.Float, CultureInfo.InvariantCulture, out float val) ? val : 0;
        }

        /// <summary>
        /// 尝试转换成double
        /// </summary>
        public static double TryParseDouble(this string @this)
        {
            if (string.IsNullOrEmpty(@this))
                return 0;
            return double.TryParse(@this, NumberStyles.Float, CultureInfo.InvariantCulture, out double val) ? val : 0;
        }
        
        /// <summary>
        /// 安全转换为字符串，去除两端空格，当值为null时返回""
        /// </summary>
        /// <param name="input">输入值</param>
        public static string SafeString( this object input ) {
            return input?.ToString()?.Trim() ?? string.Empty;
        }
    }
}
