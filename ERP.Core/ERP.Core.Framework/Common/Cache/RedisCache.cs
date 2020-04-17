using System;
using ERP.Framework.Common.Config;
using ERP.Framework.Common.Helpers;
using StackExchange.Redis;

namespace ERP.Framework.Common.Cache
{
    /// <summary>
    /// Redis缓存
    /// </summary>
    public class RedisCache
    {
        #region 属性
         

        #endregion

        #region -- 连接信息 --
        //10.0.18.8:6379
        private static readonly ConnectionMultiplexer Prcm = CreateManager();
        private static ConnectionMultiplexer CreateManager()
        {
            try
            {
                string redisPath = (string)SysConfig.Params.RedisPath;
                var hosts = string.Format("{1}:{2},password={0}", "xxzx-2017", redisPath.Split(':')[0], redisPath.Split(':')[1]);
                var connect = ConnectionMultiplexer.Connect(hosts); 
                return connect;
            }
            catch (Exception)
            { 
                throw new Exception("缓存服务器未启动");
            }
        }


        #endregion

        private static IDatabase Client
        {
            get
            {
                IDatabase redis = Prcm.GetDatabase();
                return redis;
            }
        }

        /// <summary>
        /// 设置单体
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="systemNo">系统编码</param>
        /// <param name="key">键</param>
        /// <param name="t">值</param>  
        /// <returns></returns>
        public static bool Add<T>(string systemNo, string key, T t)
        {
            try
            {
                RedisKey redisKey = systemNo + "|" + key;
                RedisValue redisValue = JsonHelper.ObjectToJson(t); 
                IDatabase redis = Client;
                return redis.StringSet(redisKey, redisValue); 
            }
            catch (Exception)
            { 
                throw new Exception("缓存服务器未启动");
            } 
        }

        /// <summary>
        /// 获取单体
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="systemNo">系统编码</param>
        /// <param name="key">键</param> 
        /// <returns></returns>
        public static T Get<T>(string systemNo, string key) where T : class
        {
            try
            {
                RedisKey redisKey = systemNo + "|" + key;
                IDatabase redis = Client;
                RedisValue redisValue = redis.StringGet(redisKey);
                return JsonHelper.JsonToObject<T>(redisValue.ToString()); 
            }
            catch (Exception)
            { 
                throw new Exception("缓存服务器未启动");
            }
        }

        /// <summary>
        /// 移除单体
        /// </summary>
        /// <param name="systemNo">系统编码</param>
        /// <param name="key">键</param> 
        public static bool Remove(string systemNo, string key)
        {
            try
            {
                RedisKey redisKey = systemNo + "|" + key;
                IDatabase redis = Client;
                return redis.KeyDelete(redisKey);
            }
            catch (Exception)
            { 
                throw new Exception("缓存服务器未启动");
            }
        }

    }
    
}
