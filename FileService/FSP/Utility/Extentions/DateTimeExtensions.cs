///////////////////////////////////////////////////////////////////////////////////////
//  程式檔名：
//  DateTimeExtension.cs
//  程式名稱：
//  DateTime擴充方法
///////////////////////////////////////////////////////////////////////////////////////
//  開發人員		    日期             版本        備註
//  小柯                2019/03/12       1.0.0.0     將Jacob的DateTimeTransform.cs修改為此檔案並微調
///////////////////////////////////////////////////////////////////////////////////////
//  Copyright (C) NEC Taiwan Ltd. 2018-2019.
//  程式描述：
//  DateTimeExtension擴充方法。
///////////////////////////////////////////////////////////////////////////////////////
using System;

namespace Utility.Extentions
{
    /// <summary>
    /// DateTimeExtension擴充方法
    /// </summary>
    public static class DateTimeExtensions
    {
        /// <summary>
        /// 單元測試使用變數Date
        /// </summary>
        public static DateTime _FakeDateTime;

        /// <summary>
        /// 單元測試使用變數string
        /// </summary>
        public static string _FakeDateString;

        /// <summary>
        /// Token存活時間
        /// </summary>
        /// <param name="mydate">轉換的日期</param>
        /// <returns>回傳設定的時間到達的秒數 e.g:1552283807</returns>
        public static long ToUnixExpires(this DateTime mydate)
        {
            return (long)Math.Round((mydate - new DateTimeOffset(1970, 1, 1, 0, 0, 0, System.TimeSpan.Zero)).TotalSeconds);
        }

        /// <summary>
        /// UnixTimeStamp To DateTime
        /// </summary>
        /// <param name="unixTimeStamp"></param>
        /// <returns></returns>
        public static DateTime UnixTimeStampToDateTime(double unixTimeStamp)
        {
            // Unix timestamp is seconds past epoch
            System.DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
            dtDateTime = dtDateTime.AddSeconds(unixTimeStamp).ToLocalTime();
            return dtDateTime;
        }

        /// <summary>
        /// 西元轉民國
        /// </summary>
        /// <param name="inputDate">西元年</param>
        /// <returns>2019/01/01</returns>
        public static string AD_To_TW(this DateTime inputDate)
        {
            //string output_date = "";
            //if (inputDate.HasValue)
            //{
            //    int year_temp = inputDate.Year - 1911;

            //    output_date = (year_temp < 100)
            //        ? "0" + year_temp.ToString() + inputDate.ToString("-MM-dd")
            //        : output_date = year_temp.ToString() + inputDate.ToString("-MM-dd");
            //}
            //else
            //{
            //    output_date = _FakeDateString;
            //}
            //return output_date;
            string output_date = string.Empty;
            int year_temp = inputDate.Year - 1911;
            output_date = (year_temp < 100) ? "0" + year_temp.ToString() + inputDate.ToString("-MM-dd") : output_date = year_temp.ToString() + inputDate.ToString("-MM-dd");
            return output_date;
        }

        public static DateTime CheckDateRange(this DateTime date)
        {
            DateTime defaultStartDate = Convert.ToDateTime("1911/01/01");
            DateTime defaultEndDate = DateTime.Now.AddYears(5);
            DateTime resultDate;

            if (date < defaultStartDate)
            {
                resultDate = defaultStartDate;
            }
            else if (date > defaultEndDate)
            {
                resultDate = defaultEndDate;
            }
            else
            {
                resultDate = date;
            }

            return resultDate;
        }

        /// <summary>
        /// 轉換日期時分最後加上秒
        /// </summary>
        /// <param name="mydate">轉換的日期</param>
        /// <returns>回傳設定的時間到達的秒數 e.g:1552283807</returns>
        public static DateTime TransformDateTime(DateTime? dateTime, int? hour, int? minute)
        {
            string YearMonthDay = dateTime?.ToString("yyyy-MM-dd");
            string Hour = hour.ToString();
            string Minute = minute.ToString();
            string Second = DateTime.Now.Second.ToString();
            string resultDateTime = YearMonthDay + " " + Hour + ":" + Minute + ":" + Second;
            DateTime datetime = Convert.ToDateTime(resultDateTime);
            return datetime;
        }
    }
}
