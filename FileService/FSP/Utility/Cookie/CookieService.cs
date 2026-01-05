///////////////////////////////////////////////////////////////////////////////////////
//  程式檔名：
//  CookieService.cs
//  程式名稱：
//  操控網站Cookie
///////////////////////////////////////////////////////////////////////////////////////
//  開發人員		    日期             版本        備註
//  小柯            2019-03-14       1.0.0.0     初始版本
///////////////////////////////////////////////////////////////////////////////////////
//  Copyright (C) NEC Taiwan Ltd. 2018-2019.
//  程式描述：
//  操控網站Cookie。
///////////////////////////////////////////////////////////////////////////////////////
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Utility.Extentions;

namespace Utility.Cookie
{
    /// <summary>
    /// 操控Cookie
    /// </summary>
    public class CookieService
    {
        /// <summary>
        /// Get the cookie
        /// </summary>
        /// <param name="key">Key </param>
        /// <returns>string value</returns>
        public static HttpCookie Get(string key)
        {
            return HttpContext.Current.Request.Cookies[key];
        }

        /// <summary>
        /// Get the cookie
        /// </summary>
        /// <param name="key">Key </param>
        /// <returns>string value</returns>
        public static string GetValue(string key)
        {
            HttpCookie cookie = HttpContext.Current.Request.Cookies[key];
            return cookie != null ? cookie.Value : string.Empty;
        }

        /// <summary>
        /// set the cookie
        /// </summary>
        /// <param name="key">key (unique indentifier)</param>
        /// <param name="value">value to store in cookie object</param>
        /// <param name="expireTime">expiration time</param>
        public static void Set(string key, string value, int expireTime = 14400)
        {
            var cookie = HttpContext.Current.Request.Cookies[key] ?? new HttpCookie(key);
            cookie.HttpOnly = true;
            cookie.Value = HttpContext.Current.Server.UrlEncode(value);
            //cookie.Path = HttpContext.Current.Request.Url.Host;
            cookie.Expires = DateTime.Now.AddMinutes(expireTime);
            HttpContext.Current.Response.Cookies.Add(cookie);
        }

        /// <summary>
        /// Delete the key
        /// </summary>
        /// <param name="key">Key</param>
        public static void Remove(string key)
        {
            HttpCookie cookie = new HttpCookie(key)
            {
                Expires = DateTime.Now.AddDays(-1) // or any other time in the past
            };

            HttpContext.Current.Response.Cookies.Set(cookie);
        }

        /// <summary>
        /// 檢查Expire time cookie
        /// </summary>
        /// <returns>boolean</returns>
        public static bool ChkExpireTime()
        {
            // check expire_time cookie
            string time = CookieService.GetValue("expire_time");
            if (string.IsNullOrEmpty(time))
            {
                return false;
            }

            long now_time = DateTime.Now.ToUnixExpires();
            if (now_time >= long.Parse(time))
            {
                return false;
            }

            return true;
        }
    }
}
