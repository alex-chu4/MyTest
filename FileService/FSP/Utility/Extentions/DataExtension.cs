using LinqKit;
using System.ComponentModel;
using System.Linq;
using System.Linq.Dynamic;

namespace Utility.Extentions
{
    /// <summary>
    /// 正排/反排 enum
    /// </summary>
    public enum OrderType
    {
        /// <summary>
        /// 正排
        /// </summary>
        [Description("正排")]
        asc = 0,

        /// <summary>
        /// 反排
        /// </summary>
        [Description("反排")]
        desc = 1
    }

    /// <summary>
    /// 資料擴充
    /// </summary>
    public static class DataExtension
    {
        /// <summary>
        /// 依傳入字串決定OrderBy項目&順序
        /// </summary>
        /// <typeparam name="T">排序資料型別</typeparam>
        /// <param name="queryableData">排序資料</param>
        /// <param name="sortName">排序名稱</param>
        /// <param name="order">正序 倒序</param>
        /// <returns>字串結果</returns>
        public static IQueryable<T> OrderByExtension<T>(this IQueryable<T> queryableData, string sortName, string order)
        {
            string orderByString = string.Empty;
            string orderType = (order.ToLower() == OrderType.asc.ToString()) ? OrderType.asc.ToString() : OrderType.desc.ToString();
            sortName = sortName == null ? "" : sortName;

            if (typeof(T).GetProperty(sortName) != null)
            {
                orderByString = sortName + " " + orderType;
            }
            else
            {
                orderByString = PredicateBuilder.New<T>(true).Parameters[0].Type.GetProperties()[0].Name;
            }

            return queryableData.OrderBy(orderByString);
        }


        //private static string RegularText(string inputText)
        //{
        //    return Regex.Replace(inputText, @"\W", string.Empty);
        //}
    }
}
