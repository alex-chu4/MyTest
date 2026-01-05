using EMIC2.Result;
using System.Collections.Generic;

namespace EMIC2.Models.Interface.ERA
{
    public interface IERA20302Dao
    {
        /// <summary>
        /// 查詢最新報Main表資料
        /// </summary>
        /// <param name="p_EOC_ID"></param>
        /// <param name="p_PRJ_NO"></param>
        /// <param name="p_ORG_ID"></param>
        /// <returns>資料集</returns>
        List<List<object>> ERA2_0302_M(string p_EOC_ID, long p_PRJ_NO, int p_ORG_ID, string p_USER_ID);

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

        /// <summary>
        /// 製作處置報告
        /// </summary>
        /// <param name="data">製作完成資料</param>
        /// <param name="p_EOC_ID"></param>
        /// <param name="p_PRJ_NO"></param>
        /// <param name="p_ORG_ID"></param>
        /// <param name="p_MODIFIED_USER"></param>
        /// <returns>製作狀態</returns>
        IResult ERA2_SetDispMain(out List<List<object>> data, string p_EOC_ID, long p_PRJ_NO, int p_ORG_ID, string p_MODIFIED_USER);
    }
}
