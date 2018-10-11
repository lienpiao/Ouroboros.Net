using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ouroboros.Common.Cache
{
    /// <summary>
    /// 缓存帮助类
    /// </summary>
    public class CacheHelper
    {
        public static ICache Cache { get; set; }

        static CacheHelper()
        {
            Cache = new HttpRuntimeCache();
        }

        /// <summary>
        /// 添加缓存
        /// </summary>
        /// <param name="key">用于引用该项的缓存键</param>
        /// <param name="value">要插入缓存中的对象</param>
        public static void AddCache(string key, object value)
        {
            Cache.AddCache(key, value);
        }

        /// <summary>
        /// 添加缓存
        /// </summary>
        /// <param name="key"> 用于引用该项的缓存键</param>
        /// <param name="value">要插入缓存中的对象</param>
        /// <param name="expDate">所插入对象将到期并被从缓存中移除的时间。要避免可能的本地时间问题（例如从标准时间改为夏时制），请使用 System.DateTime.UtcNow 而不是 System.DateTime.Now 作为此参数值。</param>
        public static void AddCache(string key, object value, DateTime expDate)
        {
            Cache.AddCache(key, value, expDate);
        }

        /// <summary>
        /// 获取缓存
        /// </summary>
        /// <param name="key">用于引用该项的缓存键</param>
        /// <returns></returns>
        public static object GetCache(string key)
        {
            return Cache.GetCache(key);
        }

        /// <summary>
        /// 获取缓存
        /// </summary>
        /// <typeparam name="T">实体</typeparam>
        /// <param name="key">用于引用该项的缓存键</param>
        /// <returns></returns>
        public static T GetCache<T>(string key)
        {
            return (T)Cache.GetCache(key);
        }

        /// <summary>
        /// 获取缓存
        /// </summary>
        /// <typeparam name="T">实体</typeparam>
        /// <param name="key">用于引用该项的缓存键</param>
        /// <returns></returns>
        public static void SetCache(string key, object value)
        {
            Cache.SetCache(key, value);
        }

        /// <summary>
        /// 修改缓存
        /// </summary>
        /// <param name="key">用于引用该项的缓存键</param>
        /// <param name="value">要插入缓存中的对象</param>
        /// <param name="extDate">所插入对象将到期并被从缓存中移除的时间</param>
        public static void SetCache(string key, object value, DateTime extDate)
        {
            Cache.SetCache(key, value, extDate);
        }
    }
}
