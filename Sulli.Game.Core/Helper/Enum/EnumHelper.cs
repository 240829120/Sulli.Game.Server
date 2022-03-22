using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Sulli.Game.Core
{
    /// <summary>
    /// 枚举辅助类
    /// </summary>
    public static class EnumHelper
    {
        /// <summary>
        /// 根据枚举的名称获取枚举
        /// </summary>
        /// <typeparam name="T">枚举类型</typeparam>
        /// <param name="name">枚举名称</param>
        /// <returns>枚举值</returns>
        public static T GetEnumFromName<T>(string name)
        {
            Type type = typeof(T);
            if (!type.IsEnum)
                throw new ArgumentException(nameof(T));

            FieldInfo fieldInfo = type.GetField(name);
            if (fieldInfo == null)
                throw new ArgumentException(nameof(T));

            return (T)fieldInfo.GetValue(null);
        }
    }
}
