using System.Collections.Generic;
using System.Linq;

namespace Sansunt.Infra.Tools
{
    /// <summary>
    /// 系统扩展 - 公共
    /// </summary>
    public static partial class Extensions
    {
        /// <summary>
        /// 安全获取值，当值为null时，不会抛出异常
        /// </summary>
        /// <param name="value">可空值</param>
        public static T SafeValue<T>(this T? value) where T : struct
        {
            return value ?? default(T);
        }

        /// <summary>
        /// 获取枚举值
        /// </summary>
        /// <param name="instance">枚举实例</param>
        public static int Value(this System.Enum instance)
        {
            if (instance == null)
                return 0;
            return Helpers.Enum.GetValue(instance.GetType(), instance);
        }

        /// <summary>
        /// 获取枚举值
        /// </summary>
        /// <typeparam name="TResult">返回值类型</typeparam>
        /// <param name="instance">枚举实例</param>
        public static TResult Value<TResult>(this System.Enum instance)
        {
            if (instance == null)
                return default(TResult);
            return Helpers.Convert.To<TResult>(Value(instance));
        }

        /// <summary>
        /// 获取枚举描述,使用System.ComponentModel.Description特性设置描述
        /// </summary>
        /// <param name="instance">枚举实例</param>
        public static string Description(this System.Enum instance)
        {
            if (instance == null)
                return string.Empty;
            return Helpers.Enum.GetDescription(instance.GetType(), instance);
        }

        /// <summary>
        /// 转换为用分隔符连接的字符串
        /// </summary>
        /// <typeparam name="T">集合元素类型</typeparam>
        /// <param name="list">集合</param>
        /// <param name="quotes">引号，默认不带引号，范例：单引号 "'"</param>
        /// <param name="separator">分隔符，默认使用逗号分隔</param>
        public static string Join<T>(this IEnumerable<T> list, string quotes = "", string separator = ",")
        {
            return Helpers.String.Join(list, quotes, separator);
        }

        /// <summary>
        /// 把字典转成字符串
        /// </summary>
        /// <param name="dic">字典</param>
        /// <param name="isKey">是否处理key</param>
        /// <param name="separator">分隔符 默认,</param>
        /// <param name="firstStr">首字符填充 默认“”</param>
        /// <param name="lastStr">末尾字符填充 默认“”</param>
        /// <returns></returns>
        public static string ToStr(this Dictionary<int, string> dic, bool isKey = true, string separator = ",", string firstStr = "", string lastStr = "")
        {
            if (dic == null || !dic.Any())
                return null;
            else if (isKey)
                return $"{firstStr}{dic.Keys.Join("", separator)}{lastStr}";
            else
                return $"{firstStr}{dic.Values.Join("", separator)}{lastStr}";
        }
    }
}
