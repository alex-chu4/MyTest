using EMIC2.Result;
using System.Collections.Generic;

namespace EMIC2.Models.Interface.ERA
{
    public interface IERA20303Dao
    {
        /// <summary>
        /// 取得應變中心項目資料
        /// </summary>
        /// <param name="result"></param>
        /// <param name="p_EOC_ID"></param>
        /// <param name="p_LEVEL">max level</param>
        /// <returns></returns>
        List<List<object>> GetEOCItems(out IResult result, string p_EOC_ID, string p_LEVEL);

        /// <summary>
        /// 查詢最新報Main表資料
        /// </summary>
        /// <param name="p_EOC_ID"></param>
        /// <param name="p_PRJ_NO"></param>
        /// <returns>資料集</returns>
        List<List<object>> ERA2_SearchMain(string p_EOC_ID, long p_PRJ_NO);

        /// <summary>
        /// 查詢Frame設定檔
        /// </summary>
        /// <param name="p_EOC_ID"></param>
        /// <param name="p_PRJ_NO"></param>
        /// <param name="p_ORG_ID"></param>
        /// <returns>資料集</returns>
        List<List<object>> ERA2_0302_STYLE(string p_EOC_ID, long p_PRJ_NO, int p_ORG_ID, int p_DISP_MAIN_ID);

        /// <summary>
        /// 查詢單筆明細資料
        /// </summary>
        /// <param name="p_DISP_MAIN_ID">主表Id</param>
        /// <param name="dISP_STYLE_ID">明細style Id</param>
        /// <param name="dISP_CAT_ID"></param>
        /// <param name="eOC_LEVEL"></param>
        /// <returns></returns>
        List<List<dynamic>> ERA2_GET_DISP_DATA_ONE(int p_DISP_MAIN_ID, int dISP_STYLE_ID, string dISP_CAT_ID, string eOC_LEVEL);

        /// <summary>
        /// 查詢多筆明細資料
        /// </summary>
        /// <param name="p_DISP_MAIN_ID">主表Id</param>
        /// <param name="p_TITLE_ID">頁籤id</param>
        /// <param name="dISP_CAT_ID"></param>
        /// <param name="eOC_LEVEL"></param>
        /// <returns></returns>
        List<List<dynamic>> ERA2_GET_DISP_DATA_BLOCK(int p_DISP_MAIN_ID, int p_TITLE_ID, string dISP_CAT_ID, string eOC_LEVEL);
    }
}
