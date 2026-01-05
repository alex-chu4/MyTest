///////////////////////////////////////////////////////////////////////////////////////
//  開發人員            日期             版本       備註
//  Jacob             2019-04-12      1.0.0.0     初始版本
//  Jacob             2019-04-26      1.0.0.1     ╭（′▽‵）╭（′▽‵）╭（′▽‵）╯　GO!
///////////////////////////////////////////////////////////////////////////////////////
//  Copyright (C) NEC Taiwan Ltd. 2018-2019.
///////////////////////////////////////////////////////////////////////////////////////
namespace Utility.Format
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class DateTimeTransform
    {
        public static DateTime _FakeDateTime;
        public static string _FakeDateString;

        /// <summary>
        /// 西元轉民國
        /// </summary>
        /// <param name="inputDate">時間輸入</param>
        /// <returns>字串結果</returns>
        public static string AD_To_TW(DateTime? inputDate)
        {
            string output_date = string.Empty;
            if (inputDate.HasValue)
            {
                int year_temp = inputDate.Value.Year - 1911;

                output_date = (year_temp < 100)
                    ? "0" + year_temp.ToString() + inputDate.Value.ToString("-MM-dd")
                    : output_date = year_temp.ToString() + inputDate.Value.ToString("-MM-dd");
            }
            else
            {
                output_date = _FakeDateString;
            }

            return output_date;
        }

        /// <summary>
        /// 民國轉西元
        /// </summary>
        /// <param name="inputDate">字串輸入</param>
        /// <returns>時間結果</returns>
        public static DateTime? TW_To_AD(string inputDate)
        {
            if (string.IsNullOrEmpty(inputDate))
            {
                return _FakeDateTime;
            }
            else
            {
                DateTime outputDate = DateTime.Parse((int.Parse(inputDate.Split('-')[0]) + 1911).ToString() + "-" + inputDate.Split('-')[1] + "-" + inputDate.Split('-')[2]);
                return outputDate;
            }
        }

    }
}
