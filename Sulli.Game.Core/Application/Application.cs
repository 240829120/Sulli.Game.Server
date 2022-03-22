using Autofac;
using Sulli.Game.Core.Message;
using Sulli.Game.Core.Verification;
using log4net;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Sulli.Game.Core
{
    /// <summary>
    /// 应用程序
    /// </summary>
    public class Application
    {
        /// <summary>
        /// 当前应用程序
        /// </summary>
        public static Application? Current { get; private set; } = new Application();

        /// <summary>
        /// 获取应用程序
        /// </summary>
        /// <typeparam name="T">应用程序类型</typeparam>
        /// <returns>应用程序</returns>
        public static T? Get<T>() where T : Application
        {
            return Current as T;
        }

        /// <summary>
        /// 日志
        /// </summary>
        private static ILog log = LogManager.GetLogger(typeof(Application));

        /// <summary>
        /// 应用程序信息
        /// </summary>
        public ApplicationInfo Info { get; private set; } = new ApplicationInfo();

        /// <summary>
        /// 系统配置
        /// </summary>
        public SystemConfig SystemConfig { get; set; } = new SystemConfig();

        /// <summary>
        /// IOC容器构建器
        /// </summary>
        public ContainerBuilder ContainerBuilder { get; set; } = new ContainerBuilder();

        /// <summary>
        /// IOC容器
        /// </summary>
        public IContainer? Container { get; set; }

        /// <summary>
        /// 构建器
        /// </summary>
        public List<IApplicationBuilder> Builders { get; private set; } = new List<IApplicationBuilder>();

        /// <summary>
        /// 构建IOC
        /// </summary>
        public void BuildIoc()
        {
            this.Container = this.ContainerBuilder.Build();
        }

        /// <summary>
        /// 构建
        /// </summary>
        public async Task BuildAsync()
        {
            foreach (IApplicationBuilder builder in this.Builders)
            {
                try
                {
                    Type type = builder.GetType();

                    Debug.WriteLine($"begin build {type.FullName}");

                    await builder.BuildAsync();

                    Debug.WriteLine($"end build {type.FullName}");
                }
                catch (Exception ex)
                {
                    log.Error(ex);

                    throw;
                }
            }
        }

        /// <summary>
        /// 处理间隔
        /// </summary>
        public TimeSpan Interval { get; set; } = TimeSpan.FromSeconds(1);

        /// <summary>
        /// 工作队列
        /// </summary>
        private HashSet<ApplicationWorkInfo> workInfos = new HashSet<ApplicationWorkInfo>(72);

        /// <summary>
        /// 是否正在loop
        /// </summary>
        private bool isLooping;

        /// <summary>
        /// 消息管理器
        /// </summary>
        public MessageManager MessageManager { get; private set; } = new MessageManager();

        /// <summary>
        /// 验证器管理器
        /// </summary>
        public ValidatorManager ValidatorManager { get; private set; } = new ValidatorManager();

        /// <summary>
        /// 添加执行方法
        /// </summary>
        /// <param name="mode">模式</param>
        /// <param name="wait">等待时间</param>
        /// <param name="action">方法</param>
        public void BeginInvoke(ApplicationWorkMode mode, TimeSpan wait, Action action)
        {
            lock (this.workInfos)
            {
                this.workInfos.Add(new ApplicationWorkInfo(mode, wait, action));
            }
        }

        /// <summary>
        /// 添加执行方法（执行一次）
        /// </summary>
        /// <param name="wait">等待时间</param>
        /// <param name="action">方法</param>
        public void BeginInvoke(TimeSpan wait, Action action)
        {
            this.BeginInvoke(ApplicationWorkMode.Once, wait, action);
        }

        /// <summary>
        /// 循环处理工作
        /// </summary>
        [DebuggerHidden]
        [DebuggerNonUserCode]
        public void Loop()
        {
            if (this.isLooping)
            {
                throw new ApplicationException("application is already in loop.");
            }

            this.isLooping = true;

            while (this.isLooping)
            {
                if (this.workInfos.Count > 0)
                {
                    List<ApplicationWorkInfo>? works = null;
                    lock (this.workInfos)
                    {
                        works = this.GetExecuteWorks();
                    }
                    if (works != null && works.Count > 0)
                    {
                        this.ExecuteWorks(works);
                    }
                }

                Thread.Sleep(this.Interval);
            }
        }

        /// <summary>
        /// 启动一个线程执行循环
        /// </summary>
        [DebuggerHidden]
        [DebuggerNonUserCode]
        public void BeginLoop()
        {
            if (this.isLooping)
            {
                throw new ApplicationException("application is already in loop.");
            }

            ThreadPool.QueueUserWorkItem(new WaitCallback(o => this.Loop()));
        }

        /// <summary>
        /// 停止
        /// </summary>
        public void Stop()
        {
            this.isLooping = false;
        }

        /// <summary>
        /// 销毁
        /// </summary>
        public void Dispose()
        {
            this.Stop();
        }

        /// <summary>
        /// 获取待执行工作
        /// </summary>
        [DebuggerHidden]
        [DebuggerNonUserCode]
        private List<ApplicationWorkInfo> GetExecuteWorks()
        {
            DateTime now = DateTimeHelper.Now;

            List<ApplicationWorkInfo> list = new List<ApplicationWorkInfo>(this.workInfos.Count);
            foreach (ApplicationWorkInfo work in this.workInfos)
            {
                if (now - work.BeginTime >= work.Wait)
                {
                    list.Add(work);
                }
            }

            this.workInfos.RemoveWhere(p => p.Mode == ApplicationWorkMode.Once && (now - p.BeginTime >= p.Wait));

            return list;
        }

        /// <summary>
        /// 执行工作
        /// </summary>
        /// <param name="works">工作集合</param>
        [DebuggerHidden]
        [DebuggerNonUserCode]
        private void ExecuteWorks(List<ApplicationWorkInfo> works)
        {
            foreach (ApplicationWorkInfo work in works)
            {
                try
                {
                    work.Action();
                }
                catch (Exception ex)
                {
                    log.Error(ex);
                }
            }
        }
    }
}
