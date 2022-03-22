using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sulli.Game.Storage
{
    /// <summary>
    /// 仓储返回
    /// </summary>
    public class StorageResult
    {
        /// <summary>
        /// 是否成功
        /// </summary>
        public bool IsSuccess { get; set; } = true;

        /// <summary>
        /// 消息
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// 异常信息
        /// </summary>
        public Exception Exception { get; set; }

        /// <summary>
        /// 内部返回集合
        /// </summary>
        public List<StorageResult> InnerResults { get; set; } = new List<StorageResult>();

        /// <summary>
        /// 成功
        /// </summary>
        /// <returns>仓储返回</returns>
        public static StorageResult Success()
        {
            StorageResult result = new StorageResult();

            return result;
        }

        /// <summary>
        /// 成功
        /// </summary>
        /// <param name="message">信息</param>
        /// <returns>仓储返回</returns>
        public static StorageResult Success(string message)
        {
            StorageResult result = new StorageResult();
            result.Message = message;

            return result;
        }

        /// <summary>
        /// 成功
        /// </summary>
        /// <param name="innerResults">内部返回集合</param>
        /// <returns>仓储返回</returns>
        public static StorageResult Success(params StorageResult[] innerResults)
        {
            StorageResult result = new StorageResult();
            result.InnerResults.AddRange(innerResults);

            return result;
        }

        /// <summary>
        /// 成功
        /// </summary>
        /// <param name="message">信息</param>
        /// <param name="innerResults">内部返回集合</param>
        /// <returns>仓储返回</returns>
        public static StorageResult Success(string message, params StorageResult[] innerResults)
        {
            StorageResult result = new StorageResult();
            result.Message = message;
            result.InnerResults.AddRange(innerResults);

            return result;
        }

        /// <summary>
        /// 失败
        /// </summary>
        /// <param name="message">信息</param>
        /// <returns>仓储返回</returns>
        public static StorageResult Fail(string message)
        {
            StorageResult result = new StorageResult();
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
        public static StorageResult Fail(string message, Exception ex)
        {
            StorageResult result = new StorageResult();
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
        public static StorageResult Fail(Exception ex)
        {
            StorageResult result = new StorageResult();
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
        public static StorageResult Fail(string message, params StorageResult[] innerResults)
        {
            StorageResult result = new StorageResult();
            result.IsSuccess = false;
            result.InnerResults.AddRange(innerResults);

            return result;
        }
    }

    /// <summary>
    /// 仓储返回
    /// </summary>
    /// <typeparam name="TEntity">实体类型</typeparam>
    /// <typeparam name="TKey">键</typeparam>
    public class StorageResult<TEntity, TKey> : StorageResult
        where TEntity : EntityBase<TKey>
        where TKey : IEquatable<TKey>
    {
        /// <summary>
        /// 列表返回值
        /// </summary>
        public List<TEntity> Result { get; set; }

        /// <summary>
        /// 成功
        /// </summary>
        /// <returns>仓储返回</returns>
        new public static StorageResult<TEntity, TKey> Success()
        {
            StorageResult<TEntity, TKey> result = new StorageResult<TEntity, TKey>();

            return result;
        }

        /// <summary>
        /// 成功
        /// </summary>
        /// <param name="message">信息</param>
        /// <returns>仓储返回</returns>
        new public static StorageResult<TEntity, TKey> Success(string message)
        {
            StorageResult<TEntity, TKey> result = new StorageResult<TEntity, TKey>();
            result.Message = message;

            return result;
        }

        /// <summary>
        /// 成功
        /// </summary>
        /// <param name="innerResults">内部返回集合</param>
        /// <returns>仓储返回</returns>
        new public static StorageResult<TEntity, TKey> Success(params StorageResult[] innerResults)
        {
            StorageResult<TEntity, TKey> result = new StorageResult<TEntity, TKey>();
            result.InnerResults.AddRange(innerResults);

            return result;
        }

        /// <summary>
        /// 成功
        /// </summary>
        /// <param name="message">信息</param>
        /// <param name="innerResults">内部返回集合</param>
        /// <returns>仓储返回</returns>
        new public static StorageResult<TEntity, TKey> Success(string message, params StorageResult[] innerResults)
        {
            StorageResult<TEntity, TKey> result = new StorageResult<TEntity, TKey>();
            result.Message = message;
            result.InnerResults.AddRange(innerResults);

            return result;
        }

        /// <summary>
        /// 失败
        /// </summary>
        /// <param name="message">信息</param>
        /// <returns>仓储返回</returns>
        new public static StorageResult<TEntity, TKey> Fail(string message)
        {
            StorageResult<TEntity, TKey> result = new StorageResult<TEntity, TKey>();
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
        new public static StorageResult<TEntity, TKey> Fail(string message, Exception ex)
        {
            StorageResult<TEntity, TKey> result = new StorageResult<TEntity, TKey>();
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
        new public static StorageResult<TEntity, TKey> Fail(Exception ex)
        {
            StorageResult<TEntity, TKey> result = new StorageResult<TEntity, TKey>();
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
        new public static StorageResult<TEntity, TKey> Fail(string message, params StorageResult[] innerResults)
        {
            StorageResult<TEntity, TKey> result = new StorageResult<TEntity, TKey>();
            result.IsSuccess = false;
            result.InnerResults.AddRange(innerResults);

            return result;
        }
    }
}
