using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sulli.Game.Core
{
    /// <summary>
    /// 随机辅助类
    /// </summary>
    public static class RandomHelper
    {
        /// <summary>
        /// 权重随机
        /// </summary>
        /// <typeparam name="T">权重项类型</typeparam>
        /// <param name="items">权重项集合</param>
        /// <param name="funcWeight">权重项值</param>
        /// <returns>随机结果</returns>
        public static T Weight<T>(IEnumerable<T> items, Func<T, int> funcWeight)
        {
            Random random = new Random();

            int total = items.Sum(funcWeight);
            int value = random.Next(0, total);

            int sum = 0;
            return items.FirstOrDefault(p => (sum += funcWeight(p)) > value);
        }
    }
}
