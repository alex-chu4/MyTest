///////////////////////////////////////////////////////////////////////////////////////
//  程式檔名：
//  RedisService.cs
//  程式名稱：
//  Redis操作
///////////////////////////////////////////////////////////////////////////////////////
//  開發人員		    日期             版本        備註
//  小柯            2019/03/14      1.0.0.0     初始版本
//  小柯            2019/05/10      1.0.0.1     未填寫Connection，預設為10.1.2.181
//  David           2019/06/13      1.0.0.2     將寫死的數字改為讀web.config
//  David           2019/07/08      1.0.0.3     change csredis to stackexchange.redis
///////////////////////////////////////////////////////////////////////////////////////
//  Copyright (C) NEC Taiwan Ltd. 2018-2019.
//  程式描述：
//  操作Redis。
///////////////////////////////////////////////////////////////////////////////////////
using StackExchange.Redis;
using System;
using System.Configuration;
using Utility.Extentions;
using Utility.Json;

namespace Utility.Cache
{
    public class Redis : ICache
    {
        public int useDb { get; set; } = 0;
        public int m_expiresAtMinutes { get; set; } = int.Parse(ConfigurationManager.AppSettings["Exprie_Refresh"]);

        private int SendTimeout = 6000;
        private int ReceiveTimeout = 6000;
        private int ReconnectWait = 300;

        ConnectionMultiplexer m_rd = null;
        IDatabase db = null;

        /// <summary>
        /// 連線 server 
        /// </summary>
        private string Connection { get; set; } = ConfigurationManager.AppSettings["RedisConnectionString"];

        /// <summary>
        /// 建構式
        /// </summary>
        public Redis()
        {
            Init();
        }

        /// <summary>
        /// 設定Redis DB
        /// </summary>
        /// <param name="usedb"></param>
        public Redis(int usedb)
        {
            Init();
            setdb(usedb);
        }

        /// <summary>
        /// 建構
        /// </summary>
        /// <param name="conn">連線字串</param>
        /// <param name="usedb">DB</param>
        public Redis(string conn, int usedb)
        {
            Connection = conn;
            Init();
            setdb(usedb);
        }


        private void Init()
        {
            if (string.IsNullOrEmpty(Connection))
                Connection = "10.1.2.181:6379, syncTimeout =30000";
            try
            {
                RedisConnection.Init(Connection);
                m_rd = RedisConnection.Instance.ConnectionMultiplexer;
            }
            catch (Exception ex) { }

        }

        public void setdb(int usedb)
        {
            try
            {
                db = m_rd.GetDatabase(usedb);
            }
            catch (Exception ex) { }
            this.useDb = usedb;
        }

        /// <summary>
        /// 取得資料
        /// </summary>
        /// <param name="key"></param>
        /// <param name="useDb">資料存放的DB: 0~15</param>
        public string Get(string key)
        {
            string rtn = string.Empty;

            try
            {
                rtn = db.StringGet(key);
            }
            catch (Exception ex) { }

            return rtn;
        }

        /// <summary>
        /// 取得CLASS物件資料
        /// </summary>
        /// <param name="key"></param>
        /// <param name="useDb">資料存放的DB: 0~15</param>
        public T Get<T>(string key)
        {
            T rtn = default(T);
            string jsonstr = Get(key);
            if (string.IsNullOrEmpty(jsonstr))
                return rtn;

            rtn = JsonService.ConvertJsonStringToObject<T>(jsonstr);
            return rtn;
        }

        /// <summary>
        /// 使用陣列移除Key
        /// </summary>
        /// <param name="key"></param>
        /// <param name="useDb"></param>
        public bool Remove(string[] keys)
        {
            if (keys.Length == 0) return false;

            try
            {
                foreach (string key in keys)
                {
                    db.KeyDelete(key);
                }
            }
            catch (Exception ex) { return false; }

            return true;
        }

        /// <summary>
        /// 移除key
        /// </summary>
        /// <param name="key"></param>
        /// <param name="useDb"></param>
        public bool Remove(string key)
        {
            return Remove(new string[] { key });
        }

        /// <summary>
        /// 儲存字串資料
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="expiresAtMinutes">n分鐘後redis自動刪除資料</param>
        /// <param name="useDb">資料存放的DB: 0~15</param>
        public bool Set(string key, string value)
        {
            return Set(key, value, m_expiresAtMinutes);
        }

        public bool Set(string key, string value, int expiresAtMinutes)
        {
            try
            {
                if (expiresAtMinutes == 0)
                {
                    db.StringSet(key, value);
                }
                else
                {
                    db.StringSet(key, value, TimeSpan.FromMinutes(expiresAtMinutes));
                }
            }
            catch (Exception ex) { return false; }
            return true;
        }
        /// <summary>
        /// 儲存CLASS物件資料 (會轉成JSON STRING再儲存)
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value">CLASS 物件</param>
        /// <param name="expiresAtMinutes">n分鐘後redis自動刪除資料</param>
        /// <param name="useDb">資料存放的DB: 0~15</param>
        public bool Set(string key, object value)
        {
            return Set(key, JsonService.ConvertObjectToJsonString(value));
        }

        /// <summary>
        /// check key is exist in redis
        /// </summary>
        /// <param name="key">redis key</param>
        /// <returns>true or false</returns>
        public bool KeyExists(string key)
        {
            try
            {
                return db.KeyExists(key);
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        /// <summary>
        /// key rename
        /// </summary>
        /// <param name="key">key</param>
        /// <param name="newKey">new key</param>
        /// <returns>true or false</returns>
        public bool KeyRename(string key, string newKey)
        {
            try
            {
                return db.KeyRename(key, newKey);
            }
            catch
            {
                return false;
            }
        }



    }
}
