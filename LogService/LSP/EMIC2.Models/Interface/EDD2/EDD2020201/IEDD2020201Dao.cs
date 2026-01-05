using EMIC2.Result;
using System.Collections.Generic;
using System.Data;

namespace EMIC2.Models.Interface.EDD
{
    public interface IEDD2020201Dao
    {
        IResult ViewLoadingInfo(out List<List<object>> data, int unitID);

        IResult EDD2_020201_M(out List<List<dynamic>> data, int p_UNIT_ID, string p_RESOURCE_NAME, string p_CITY_NAME, string p_TOWN_NAME);

        IResult UpdateTableRows(short p_FUNCTION, string p_ACTION, int? p_AUDITING_RECORD_ID, int p_UNIT_ID, string p_MODIFIED_USER, DataTable p_DATA_TBL);

        IResult ExportRsc(out List<List<dynamic>> data, int p_UNIT_ID);

        // ----- 0502
        IResult UpdateUnitLocation(List<string> insertDetailParas, List<object> insertDetailVals, List<string> insertMasterParas = null, List<object> insertMasterVals = null);

        IResult SearchUnitLocation(
            out List<List<dynamic>> data, string p_QRY_TYPE, int p_UNIT_ID, int? p_RESOURCE_ID,
            string w_CITY_NAME = "", string w_TOWN_NAME = "", string w_LOCATION_NAME = "", string w_CONTACT_NAME = "");


        //--------------------other
        /// <summary>
        /// 匯入外部資料，呼叫 Stored Procedure 回傳成功或失敗
        /// </summary>
        /// <returns>int 1 成功 0 失敗</returns>
        IResult EDD2_IMP_XLS_TO_RESOURCE_STOCK_DATA(int MTMP_REC_ID, int UNIT_ID);
    }
}
