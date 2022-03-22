using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sulli.Game.Storage
{
    /// <summary>
    /// 仓储分页返回
    /// </summary>
    /// <typeparam name="TEntity">实体</typeparam>
    /// <typeparam name="TKey">键</typeparam>
    public class StoragePageResult<TEntity, TKey> : StorageResult<TEntity, TKey>
        where TEntity : EntityBase<TKey>
        where TKey : IEquatable<TKey>
    {
        /// <summary>
        /// 页码
        /// </summary>
        public int Page { get; set; }

        /// <summary>
        /// 每页长度
        /// </summary>
        public int PageSize { get; set; }

        /// <summary>
        /// 总页数
        /// </summary>
        public int PageCount { get; set; }

        /// <summary>
        /// 总记录数
        /// </summary>
        public int TotalCount { get; set; }

        /// <summary>
        /// 成功
        /// </summary>
        /// <returns>仓储返回</returns>
        new public static StoragePageResult<TEntity, TKey> Success()
        {
            StoragePageResult<TEntity, TKey> result = new StoragePageResult<TEntity, TKey>();

            return result;
        }

        /// <summary>
        /// 成功
        /// </summary>
        /// <param name="message">信息</param>
        /// <returns>仓储返回</returns>
        new public static StoragePageResult<TEntity, TKey> Success(string message)
        {
            StoragePageResult<TEntity, TKey> result = new StoragePageResult<TEntity, TKey>();
            result.Message = message;

            return result;
        }

        /// <summary>
        /// 成功
        /// </summary>
        /// <param name="innerResults">内部返回集合</param>
        /// <returns>仓储返回</returns>
        new public static StoragePageResult<TEntity, TKey> Success(params StorageResult[] innerResults)
        {
            StoragePageResult<TEntity, TKey> result = new StoragePageResult<TEntity, TKey>();
            result.InnerResults.AddRange(innerResults);

            return result;
        }

        /// <summary>
        /// 成功
        /// </summary>
        /// <param name="message">信息</param>
        /// <param name="innerResults">内部返回集合</param>
        /// <returns>仓储返回</returns>
        new public static StoragePageResult<TEntity, TKey> Success(string message, params StorageResult[] innerResults)
        {
            StoragePageResult<TEntity, TKey> result = new StoragePageResult<TEntity, TKey>();
            result.Message = message;
            result.InnerResults.AddRange(innerResults);

            return result;
        }

        /// <summary>
        /// 失败
        /// </summary>
        /// <param name="message">信息</param>
        /// <returns>仓储返回</returns>
        new public static StoragePageResult<TEntity, TKey> Fail(string message)
        {
            StoragePageResult<TEntity, TKey> result = new StoragePageResult<TEntity, TKey>();
            result.IsSuccess = false;
            result.Message = message;

            return result;
        }

        /// <summary>
        /// 失败
        /// </summary>
        /// <param name="message">信息</param>
        /// <param name="ex">异常信息</param>
        /// <returns>仓储返回</returns>
        new public static StoragePageResult<TEntity, TKey> Fail(string message, Exception ex)
        {
            StoragePageResult<TEntity, TKey> result = new StoragePageResult<TEntity, TKey>();
            result.IsSuccess = false;
            result.Message = message;
            result.Exception = ex;

            return result;
        }

        /// <summary>
        /// 失败
        /// </summary>
        /// <param name="ex">异常信息</param>
        /// <returns>仓储返回</returns>
        new public static StoragePageResult<TEntity, TKey> Fail(Exception ex)
        {
            StoragePageResult<TEntity, TKey> result = new StoragePageResult<TEntity, TKey>();
            result.IsSuccess = false;
            result.Exception = ex;

            return result;
        }

        /// <summary>
        /// 失败
        /// </summary>
        /// <param name="message">信息</param>
        /// <param name="innerResults">内部返回集合</param>
        /// <returns>仓储集合</returns>
        new public static StoragePageResult<TEntity, TKey> Fail(string message, params StorageResult[] innerResults)
        {
            StoragePageResult<TEntity, TKey> result = new StoragePageResult<TEntity, TKey>();
            result.IsSuccess = false;
            result.InnerResults.AddRange(innerResults);

            return result;
        }
    }
}
