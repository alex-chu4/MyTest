///////////////////////////////////////////////////////////////////////////////////////
//  程式檔名：
//  String.Extensions.cs
//  程式名稱：
//  字串擴充方法
///////////////////////////////////////////////////////////////////////////////////////
//  開發人員		    日期             版本        備註
//  小柯                2019/03/13       1.0.0.0     初始版本
///////////////////////////////////////////////////////////////////////////////////////
//  Copyright (C) NEC Taiwan Ltd. 2018-2019.
//  程式描述：
//  字串擴充方法
///////////////////////////////////////////////////////////////////////////////////////
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utility.Extentions
{
    /// <summary>
    /// 字串擴充方法
    /// </summary>
    public static class StringExtensions
    {
        /// <summary>
        /// 單元測試使用
        /// </summary>
        public static DateTime FakeDateTime;

        /// <summary>
        /// 民國轉西元
        /// </summary>
        /// <param name="inputDate">民國日期 e.g:76-01-01 或 76/01/01</param>
        /// <returns>1987/01/01</returns>
        public static DateTime? TW_To_AD(string inputDate)
        {
            if (string.IsNullOrEmpty(inputDate))
            {
                return FakeDateTime;
            }
            else
            {
                // 使用 Calendar轉換
                CultureInfo culture = new CultureInfo("zh-TW");
                culture.DateTimeFormat.Calendar = new TaiwanCalendar();
                DateTime outputDate = DateTime.Parse(inputDate, culture);
                return outputDate;
            }
        }

        /// <summary>
        /// 從字串左邊取N字
        /// </summary>
        /// <param name="s">要擷取的字串</param>
        /// <param name="length">長度</param>
        /// <returns>擷取左邊計算第N個字</returns>
        public static string Left(this string s, int length)
        {
            length = Math.Max(0, length);

            if (s.Length > length)
            {
                return s.Substring(0, length);
            }
            else
            {
                return s;
            }
        }

        /// <summary>
        /// 從字串右邊取N字
        /// </summary>
        /// <param name="s">要擷取的字串</param>
        /// <param name="length">長度</param>
        /// <returns>擷取完的字串</returns>
        public static string Right(this string s, int length)
        {
            length = Math.Max(0, length);

            if (s.Length > length)
            {
                return s.Substring(s.Length - length, length);
            }
            else
            {
                return s;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static DateTime SecendToDateTime(this string s)
        {
            DateTime dtStart = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
            long lTime = long.Parse(s + "0000000");
            TimeSpan toNow = new TimeSpan(lTime);
            return dtStart.Add(toNow);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="s"></param>OID
        /// <returns></returns>
        public static string getParentForOID(string s)
        {
            if (string.IsNullOrEmpty(s)) return string.Empty;
            int cnt = s.Split('.').Count();
            if (cnt >=1)
            {
                return System.IO.Path.GetFileNameWithoutExtension(s);
            }

            return String.Empty;
        }


    }
}
