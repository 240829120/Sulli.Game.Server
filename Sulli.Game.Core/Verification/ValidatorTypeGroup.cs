using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sulli.Game.Core.Verification
{
    /// <summary>
    /// 验证类型分组
    /// </summary>
    public class ValidatorTypeGroup
    {
        /// <summary>
        /// 类型
        /// </summary>
        public Type Type { get; set; }

        /// <summary>
        /// 容器
        /// </summary>
        public ConcurrentDictionary<string, IValidatorContainer> Containers { get; private set; } = new ConcurrentDictionary<string, IValidatorContainer>(Environment.ProcessorCount, 100);
    }
}
