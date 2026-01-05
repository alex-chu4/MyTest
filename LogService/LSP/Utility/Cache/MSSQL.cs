///////////////////////////////////////////////////////////////////////////////////////
//  程式檔名：
//  MSSQL.cs
//  程式名稱：
//  將Token寫入DB的模組
///////////////////////////////////////////////////////////////////////////////////////
//  開發人員            日期             版本        備註
//  小柯             2019/05/08       1.0.0.0     初始版本
///////////////////////////////////////////////////////////////////////////////////////
//  Copyright (C) NEC Taiwan Ltd. 2018-2019.
//  程式描述：
//  將Token寫入DB的模組。
///////////////////////////////////////////////////////////////////////////////////////
using EMIC2.Models;
using EMIC2.Models.Interface;
using EMIC2.Models.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility.Extentions;
using Utility.Json;
using Utility.Token;

namespace Utility.Cache
{
    public class MSSQL : CacheBase
    {
        IRepository<SSO2_USER_TOKEN> repository = new GenericRepository<SSO2_USER_TOKEN>();
        UnitOfWorkRepository<SSO2_USER_TOKEN> Unitofworkrepository;

        public override string Get(string key)
        {
            string returnvalue = string.Empty;

            returnvalue = cache.Get(key);

            if (string.IsNullOrEmpty(returnvalue))
            {
                returnvalue = Get4SQL(key);
            }

            return returnvalue;
        }

        public override T Get<T>(string key)
        {
            T rtn = default(T);

            // Redis取到資料就不取MSSQL
            rtn = cache.Get<T>(key);

            if (rtn == null)
            {
                string user = Get(key);
                rtn = JsonService.ConvertJsonStringToObject<T>(user);
            }

            return rtn;
        }

        private string Gets(string[] keys)
        {
            var user = repository.Get(o => keys.Any(key => key == o.TOKEN));

            return JsonService.ConvertObjectToJsonString(user);
        }

        private List<SSO2_USER_TOKEN> Gets<SSO2_USER_TOKEN>(string[] key)
        {
            string jsondata = Gets(key);
            if (string.IsNullOrEmpty(jsondata))
                return new List<SSO2_USER_TOKEN>();

            return JsonService.ConvertJsonStringToObject<List<SSO2_USER_TOKEN>>(jsondata);
        }

        public override bool Remove(string key)
        {
            if (string.IsNullOrEmpty(key) == false)
            {
                // Redis 刪除
                cache.Remove(key);

                // MSSQL
                var user = repository.Get(x => x.TOKEN == key);
                if (user != null)
                {
                    return repository.Delete(user);
                }
            }

            return true;
        }

        public override bool Remove(string[] key)
        {
            // Redis 刪除
            cache.Remove(key);

            var data = Gets<SSO2_USER_TOKEN>(key);
            Unitofworkrepository.RemoveRange(data);
            return false;
        }

        /// <inheritdoc/>
        public override bool Set(string key, string value)
        {
            // 寫入Redis
            cache.Set(key, value);

            // MSSSQL
            //var user = Get4SQL<SSO2_USER_TOKEN>(key);
            var user = repository.Get(o => o.TOKEN == key);
            if (user != null)
            {
                user.REFRESHTOKEN = value;
                return repository.Update(user);
            }
            else
            {
                JwtTokenHelper jwtTokenHelper = new JwtTokenHelper(JwtService.Issuer, JwtService.Audience, JwtService.KEY);
                var jwtToken = jwtTokenHelper.DeCode(value);
                string iat = jwtToken.Payload["iat"].ToString();
                string exp = jwtToken.Payload["exp"].ToString();
                var userinfo = jwtToken.Payload["UserInfo"];
                var payload = userinfo as Dictionary<string, object>;
                string USERID = payload["USERID"] != null ? payload["USERID"].ToString() : string.Empty;
                string USERNAME = payload["NAME"] != null ? payload["NAME"].ToString() : string.Empty;

                DateTime dt_exp = exp.SecendToDateTime();
                DateTime dt_iat = iat.SecendToDateTime();

                user = new SSO2_USER_TOKEN()
                {
                    USERID = USERID,
                    TOKEN = key,
                    REFRESHTOKEN = value,
                    CREATE_TIME = dt_iat,
                    LOGIN_SYS_CODE = "SSO",
                    EXP_TIME = dt_exp,
                    USER_NAME = USERNAME,
                    MOD_TIME = DateTime.Now
                };
                return repository.Create(user);
            }
        }

        public override bool Set(string key, string value, int expiresAtMinutes)
        {
            // 寫入Redis
            cache.Set(key, value, expiresAtMinutes);

            // MSSQL
            return Set(key, value);
        }

        public override bool Set(string key, object value)
        {
            // Redis
            cache.Set(key, value);

            // MSSQL
            return Set(key, value.ToString());
        }

        /// <summary>
        /// 取得資料庫的資料
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        private string Get4SQL(string key)
        {
            string returnvalue = string.Empty;

            if (string.IsNullOrEmpty(returnvalue))
            {
                var user = repository.Get(o => o.TOKEN == key);
                if (user == null)
                {
                    return string.Empty;
                }
                else
                {
                    returnvalue = user.REFRESHTOKEN;
                }
            }

            return returnvalue;
        }

        /// <summary>
        /// 取得MSSQL資料
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        private T Get4SQL<T>(string key)
        {
            T rtn = default(T);
            string user = Get4SQL(key);
            rtn = JsonService.ConvertJsonStringToObject<T>(user);

            return rtn;
        }


    }
}
