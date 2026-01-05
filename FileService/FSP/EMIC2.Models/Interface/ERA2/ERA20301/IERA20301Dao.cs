using EMIC2.Result;
using System.Collections.Generic;
using System.Data;

namespace EMIC2.Models.Interface.ERA
{
    public interface IERA20301Dao
    {
        /// <summary>
        /// 查詢最新報Main表資料
        /// </summary>
        /// <param name="p_EOC_ID"></param>
        /// <param name="p_PRJ_NO"></param>
        /// <param name="p_ORG_ID"></param>
        /// <returns>資料集</returns>
        IResult ERA2_0301_M(out List<List<object>> tbData, string p_EOC_ID, long p_PRJ_NO, int p_ORG_ID, string p_USER_ID);

        /// <summary>
        /// 查詢Frame設定檔
        /// </summary>
        /// <param name="p_EOC_ID"></param>
        /// <param name="p_PRJ_NO"></param>
        /// <param name="p_ORG_ID"></param>
        /// <returns>資料集</returns>
        IResult ERA2_0301_STYLE(out List<List<object>> tbData, string p_EOC_ID, long p_PRJ_NO, int p_ORG_ID, int p_DISP_MAIN_ID);

        /// <summary>
        /// 查詢單筆明細資料
        /// </summary>
        /// <param name="p_DISP_MAIN_ID">主表Id</param>
        /// <param name="dISP_STYLE_ID">明細style Id</param>
        /// <param name="dISP_CAT_ID"></param>
        /// <param name="eOC_LEVEL"></param>
        /// <returns></returns>
        IResult ERA2_GET_DISP_DATA_ONE(out List<List<dynamic>> dataCollection, int p_DISP_MAIN_ID, int p_DISP_STYLE_ID, string p_DISP_CAT_ID, string p_EOC_LEVEL);

        /// <summary>
        /// 查詢多筆明細資料
        /// </summary>
        /// <param name="p_DISP_MAIN_ID">主表Id</param>
        /// <param name="p_TITLE_ID">頁籤id</param>
        /// <param name="dISP_CAT_ID"></param>
        /// <param name="eOC_LEVEL"></param>
        /// <returns></returns>
        IResult ERA2_GET_DISP_DATA_BLOCK(out List<List<dynamic>> dataCollection, int p_DISP_MAIN_ID, int p_TITLE_ID, string p_DISP_CAT_ID, string p_EOC_LEVEL);

        /// <summary>
        /// 檢查設置無資料可填報
        /// </summary>
        /// <param name="p_EOC_ID"></param>
        /// <param name="p_PRJ_NO"></param>
        /// <param name="p_ORG_ID"></param>
        /// <param name="p_DISP_MAIN_ID">主表Id</param>
        /// <param name="dISP_STYLE_ID">明細style Id</param>
        /// <returns></returns>
        IResult ERA2_0301_D_NoData(out List<List<object>> tbData, string p_EOC_ID, long p_PRJ_NO, int p_ORG_ID, int p_DISP_MAIN_ID, int p_DISP_STYLE_ID, string p_USER_ID);

        /// <summary>
        ///
        /// </summary>
        /// <param name="p_EOC_ID"></param>
        /// <param name="p_PRJ_NO"></param>
        /// <param name="p_ORG_ID"></param>
        /// <param name="p_DISP_MAIN_ID">主表Id</param>
        /// <param name="dISP_STYLE_ID">明細style Id</param>
        /// <returns></returns>
        IResult ERA2_0301_D_maintain(out List<List<object>> tbData, string p_EOC_ID, long p_PRJ_NO, int p_ORG_ID, int p_DISP_MAIN_ID, int p_DISP_STYLE_ID, string p_USER_ID);

        /// <summary>
        ///
        /// </summary>
        /// <param name="p_EOC_ID"></param>
        /// <param name="p_PRJ_NO"></param>
        /// <param name="p_ORG_ID"></param>
        /// <param name="p_DISP_MAIN_ID">主表Id</param>
        /// <param name="dISP_STYLE_ID">明細style Id</param>
        /// <returns></returns>
        IResult ERA2_history(out List<List<dynamic>> tbData, string p_EOC_ID, long p_PRJ_NO, int p_DISP_NO, int p_DISP_STYLE_ID, string p_DISP_CAT_ID, string p_EOC_LEVEL);

        /// <summary>
        ///ERA2_0301_M_Export
        /// </summary>
        /// <param name="tbData"></param>
        /// <param name="p_EOC_ID"></param>
        /// <param name="p_PRJ_NO"></param>
        /// <param name="ORG_LEVEL_ID"></param>
        /// <param name="DISP_NO"></param>
        /// <returns></returns>
        IResult ERA2_0301_Export(out List<List<object>> tbData, string p_EOC_ID, long p_PRJ_NO, string ORG_LEVEL_ID, int DISP_NO);

        /// <summary>
        /// 過濾 ITEM_LEVEL = 1
        /// </summary>
        /// <param name="tbData"></param>
        /// <param name="p_EOC_ID"></param>
        /// <param name="p_PRJ_NO"></param>
        /// <param name="p_ORG_ID"></param>
        /// <param name="p_DISP_MAIN_ID"></param>
        /// <returns></returns>
        IResult ERA2_0301_STYLE_TITLE(out List<List<object>> tbData, string p_EOC_ID, long p_PRJ_NO, int p_ORG_ID, int p_DISP_MAIN_ID);

        /// <summary>
        /// 查詢多筆明細資料
        /// </summary>
        /// <param name="p_DISP_MAIN_ID">主表Id</param>
        /// <param name="p_TITLE_ID">頁籤id</param>
        /// <param name="dISP_CAT_ID"></param>
        /// <param name="eOC_LEVEL"></param>
        /// <returns></returns>
        IResult ERA2_GET_DISP_DATA_BLOCK_DS(out DataSet dataCollection, int p_DISP_MAIN_ID, int p_TITLE_ID, string p_DISP_CAT_ID, string p_EOC_LEVEL);
    }
}