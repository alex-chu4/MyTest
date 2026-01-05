using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMIC2.Models.Interface.ERA
{
    public interface IERA20203Dao
    {
        /// <summary>
        /// 依通報日期查詢資料
        /// </summary>
        /// <param name="p_EOC_ID"></param>
        /// <param name="p_RPT_TIME_S">時間(起)</param>
        /// <param name="p_RPT_TIME_E">時間(迄)</param>
        /// <param name="p_DIS_DATA_UID">災害類別</param>
        /// <param name="p_RPT_NO_S">報別(起)</param>
        /// <param name="p_RPT_NO_E">報別(迄)</param>
        /// <param name="P_RPT_DEF_ID">報表定義ID</param>
        /// <returns>資料集</returns>
        List<List<object>> ERA2_0203_QD(string p_EOC_ID, DateTime p_RPT_TIME_S, DateTime p_RPT_TIME_E, int p_DIS_DATA_UID, int p_RPT_NO_S, int p_RPT_NO_E, int P_RPT_DEF_ID);

        /// <summary>
        /// 依專案查詢資料
        /// </summary>
        /// <param name="p_EOC_ID"></param>
        /// <param name="p_PRJ_NO"></param>
        /// <param name="p_RPT_NO_S">報別(起)</param>
        /// <param name="p_RPT_NO_E">報別(迄)</param>
        /// <returns>資料集</returns>
        List<List<object>> ERA2_0203_QP(string p_EOC_ID, long p_PRJ_NO, int p_RPT_NO_S, int p_RPT_NO_E);

        /// <summary>
        /// 層級1之報表集合
        /// </summary>
        /// <returns>資料集</returns>
        List<List<object>> ERA2_RPT_DEF_L1();

        /// <summary>
        /// 層級2之報表集合
        /// </summary>
        /// <returns>資料集</returns>
        List<List<object>> ERA2_RPT_DEF_L2();

        /// <summary>
        /// 層級3之報表集合
        /// </summary>
        /// <returns>資料集</returns>
        List<List<object>> ERA2_RPT_DEF_L3();

        /// <summary>
        /// 專案查詢取專案資料集
        /// </summary>
        /// <param name="p_EOC_ID"></param>
        /// <param name="p_RPT_TIME_S"></param>
        /// <param name="p_RPT_TIME_E"></param>
        /// <param name="p_DIS_DATA_UID"></param>
        /// <param name="p_CASE_NAME"></param>
        /// <returns>資料集</returns>
        List<List<object>> ERA2_PROJECT_SEARCH(string p_EOC_ID, DateTime p_RPT_TIME_S, DateTime p_RPT_TIME_E, int p_DIS_DATA_UID, string p_CASE_NAME);
    }
}
