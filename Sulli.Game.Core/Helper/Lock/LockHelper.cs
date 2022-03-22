using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Sulli.Game.Core
{
    /// <summary>
    /// 锁辅助类
    /// </summary>
    public static class LockHelper
    {
        /// <summary>
        /// 锁内读
        /// </summary>
        /// <param name="lockSlim">锁对象</param>
        /// <param name="action">行为</param>
        /// <param name="timeout">超时时间</param>
        public static void Read(ReaderWriterLockSlim lockSlim, Action action, TimeSpan timeout)
        {
            if (!lockSlim.TryEnterReadLock(timeout))
            {
                throw new Exception("try enter read lock error");
            }

            try
            {
                action();
            }
            catch
            {
                throw;
            }
            finally
            {
                lockSlim.ExitReadLock();
            }
        }

        /// <summary>
        /// 锁内读
        /// </summary>
        /// <param name="lockSlim">锁对象</param>
        /// <param name="action">行为</param>
        public static void Read(ReaderWriterLockSlim lockSlim, Action action)
        {
            Read(lockSlim, action, TimeSpan.FromSeconds(1));
        }

        /// <summary>
        /// 锁内写
        /// </summary>
        /// <param name="lockSlim">锁对象</param>
        /// <param name="action">行为</param>
        /// <param name="timeout">超时时间</param>
        public static void Write(ReaderWriterLockSlim lockSlim, Action action, TimeSpan timeout)
        {
            if (!lockSlim.TryEnterWriteLock(timeout))
            {
                throw new Exception("try enter write lock error");
            }

            try
            {
                action();
            }
            catch
            {
                throw;
            }
            finally
            {
                lockSlim.ExitWriteLock();
            }
        }

        /// <summary>
        /// 锁内写
        /// </summary>
        /// <param name="lockSlim">锁对象</param>
        /// <param name="action">行为</param>
        public static void Write(ReaderWriterLockSlim lockSlim, Action action)
        {
            Write(lockSlim, action, TimeSpan.FromSeconds(1));
        }

        /// <summary>
        /// 可升级读锁
        /// </summary>
        /// <param name="lockSlim">锁对象</param>
        /// <param name="action">行为</param>
        /// <param name="timeout">超时时间</param>
        public static void UpgradeableRead(ReaderWriterLockSlim lockSlim, Action action, TimeSpan timeout)
        {
            if (!lockSlim.TryEnterUpgradeableReadLock(timeout))
            {
                throw new Exception("try enter upgradeable read lock error");
            }

            try
            {
                action();
            }
            catch
            {
                throw;
            }
            finally
            {
                lockSlim.ExitUpgradeableReadLock();
            }
        }

        /// <summary>
        /// 可升级读锁
        /// </summary>
        /// <param name="lockSlim">锁对象</param>
        /// <param name="action">行为</param>
        public static void UpgradeableRead(ReaderWriterLockSlim lockSlim, Action action)
        {
            UpgradeableRead(lockSlim, action, TimeSpan.FromSeconds(1));
        }
    }
}
