using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Sulli.Game.Core.Verification
{
    /// <summary>
    /// 类型验证容器扩展
    /// </summary>
    public static class TypeValidatorContainerExtend
    {
        /// <summary>
        /// 创建属性验证
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <typeparam name="TProperty">属性类型</typeparam>
        /// <returns>属性验证容器</returns>
        public static PropertyValidatorContainer<T, TProperty> Property<T, TProperty>(this TypeValidatorContainer<T> container, Expression<Func<T, TProperty>> expression)
        {
            PropertyValidatorContainer<T, TProperty> context = new PropertyValidatorContainer<T, TProperty>();
            context.Owner = container;

            MemberExpression member = expression.Body as MemberExpression;
            if (member == null)
                throw new ArgumentException(nameof(expression), "expression must be Property picker");

            if (member.Member is not PropertyInfo propertyInfo)
                throw new ArgumentException(nameof(expression), "expression must be Property picker");

            context.PropertyInfo = propertyInfo;
            context.GetPropertyFunc = expression.Compile();

            container.Validators.Add(context);

            return context;
        }
    }
}
