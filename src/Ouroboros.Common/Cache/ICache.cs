using System;

namespace Ouroboros.Common.Cache
{
    /// <summary>
    /// 缓存帮助类接口
    /// </summary>
    public interface ICache
    {
        /// <summary>
        /// 添加缓存
        /// </summary>
        /// <param name="key"> 用于引用该项的缓存键</param>
        /// <param name="value">要插入缓存中的对象</param>
        /// <param name="expDate">所插入对象将到期并被从缓存中移除的时间。要避免可能的本地时间问题（例如从标准时间改为夏时制），请使用 System.DateTime.UtcNow 而不是 System.DateTime.Now 作为此参数值。</param>
        void AddCache(string key, object value, DateTime expDate);
        /// <summary>
        /// 添加缓存
        /// </summary>
        /// <param name="key">用于引用该项的缓存键</param>
        /// <param name="value">要插入缓存中的对象</param>
        void AddCache(string key, object value);
        /// <summary>
        /// 获取缓存
        /// </summary>
        /// <param name="key">用于引用该项的缓存键</param>
        /// <returns></returns>
        object GetCache(string key);
        /// <summary>
        /// 获取缓存
        /// </summary>
        /// <typeparam name="T">实体</typeparam>
        /// <param name="key">用于引用该项的缓存键</param>
        /// <returns></returns>
        T GetCache<T>(string key);
        /// <summary>
        /// 修改缓存
        /// </summary>
        /// <param name="key">用于引用该项的缓存键</param>
        /// <param name="value">要插入缓存中的对象</param>
        /// <param name="extDate">所插入对象将到期并被从缓存中移除的时间</param>
        void SetCache(string key, object value, DateTime extDate);
        /// <summary>
        /// 修改缓存
        /// </summary>
        /// <param name="key">用于引用该项的缓存键</param>
        /// <param name="value">要插入缓存中的对象</param>
        void SetCache(string key, object value);
    }
}
