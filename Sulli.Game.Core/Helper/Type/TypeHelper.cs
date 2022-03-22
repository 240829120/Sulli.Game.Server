using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Sulli.Game.Core
{
    /// <summary>
    /// 类型辅助类
    /// </summary>
    public static class TypeHelper
    {
        /// <summary>
        /// 是否是可为空类型
        /// </summary>
        /// <param name="type">类型</param>
        /// <returns>是否是可为空类型</returns>
        public static bool IsNullable(Type type)
        {
            return type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>);
        }

        /// <summary>
        /// 获取基础类型, 如果是 Nullable[T] 那么返回T类型
        /// </summary>
        /// <param name="type">类型</param>
        /// <returns></returns>
        public static Type GetBaseType(Type type)
        {
            if (IsNullable(type))
                return GetBaseType(type.GetGenericArguments()[0]);

            return type;
        }

        /// <summary>
        /// 是否是基础类型
        /// </summary>
        /// <param name="type">类型</param>
        /// <returns>是否是基础类型</returns>
        public static bool IsPrimitive(Type type)
        {
            return type.IsPrimitive || type == typeof(string);
        }

        /// <summary>
        /// 是否是数组或列表类型
        /// </summary>
        /// <param name="type">类型</param>
        /// <returns>是否是列表类型</returns>
        public static bool IsArrayOrList(Type type)
        {
            return typeof(System.Collections.IEnumerable).IsAssignableFrom(type);
        }

        /// <summary>
        /// 是否是字典类型
        /// </summary>
        /// <param name="type">类型</param>
        /// <returns>是否是字典类型</returns>
        public static bool IsDictionary(Type type)
        {
            if (!type.IsGenericType)
                return false;

            return type.GetInterface(typeof(IDictionary<,>).Name) != null;
        }
    }
}
