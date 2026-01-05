///////////////////////////////////////////////////////////////////////////////////////
//  開發人員            日期             版本       備註
//  Jacob             2019-04-12      1.0.0.0     初始版本
//  Jacob             2019-04-26      1.0.0.1     ╭（′▽‵）╭（′▽‵）╭（′▽‵）╯　GO!
///////////////////////////////////////////////////////////////////////////////////////
//  Copyright (C) NEC Taiwan Ltd. 2018-2019.
///////////////////////////////////////////////////////////////////////////////////////
namespace Utility.Verify
{
    using System.Text.RegularExpressions;


    /// <summary>
    /// TTTT
    /// </summary>
    public class StringVerify
    {
        public static string FakePassID;
        public static string FakeEmail;
        public static string FakeChinese;
        public static string FakeNumber;

        /// <summary>
        /// 身分證驗證
        /// </summary>
        /// <param name="passID">身分證</param>
        /// <returns>True Or False</returns>
        public static bool VerifyID(string passID)
        {
            string regular = @"/^[A-Z][12]\d{8}$/";
            Regex regPassID = new Regex(regular);

            return (!string.IsNullOrEmpty(passID)) ? regPassID.IsMatch(passID) : regPassID.IsMatch(FakePassID);
        }

        /// <summary>
        /// 驗證信箱
        /// /w = [A-Za-z0-9_]
        /// </summary>
        /// <param name="email">信箱</param>
        /// <returns>True Or False</returns>
        public static bool VerifyEmail(string email)
        {
            string regular = @"^\w+((-\w+)|(\.\w+))*\@\w+((\.|-)\w+)*\.[A-Za-z]+$";
            Regex regEmail = new Regex(regular);

            return (!string.IsNullOrEmpty(email)) ? regEmail.IsMatch(email) : regEmail.IsMatch(FakeEmail);
        }

        /// <summary>
        /// 驗證中文
        /// </summary>
        /// <param name="chinese">字串</param>
        /// <returns>True Or False</returns>
        public static bool VertfyChinese(string chinese)
        {
            string regular = @"^[\u4e00-\u9fa5]+$";
            Regex regChinese = new Regex(regular);

            return (!string.IsNullOrEmpty(chinese)) ? regChinese.IsMatch(chinese) : regChinese.IsMatch(FakeChinese);
        }

        /// <summary>
        /// 驗證數字
        /// </summary>
        /// <param name="number">數字</param>
        /// <returns>True Or False</returns>
        public static bool VerifyNumber(string number)
        {
            string regular = @"^[0-9]+$";
            Regex regNumber = new Regex(regular);

            return (!string.IsNullOrEmpty(number)) ? regNumber.IsMatch(number) : regNumber.IsMatch(FakeNumber);
        }
    }
}
