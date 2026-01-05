///////////////////////////////////////////////////////////////////////////////////////
//  程式檔名：
//  IERA2.cs
//  程式名稱：
//  A3通報表下層匯整介面
///////////////////////////////////////////////////////////////////////////////////////
//  開發人員            日期             版本        備註
//  Joe             2019-05-20       1.0.0.0     初始版本
///////////////////////////////////////////////////////////////////////////////////////
//  Copyright (C) NEC Taiwan Ltd. 2018-2019.
//  程式描述：
//  ERA2共用資料存取功能
///////////////////////////////////////////////////////////////////////////////////////
using EMIC2.Models.Dao.Dto;
using EMIC2.Models.Dao.Dto.ERA.ERA20521;
using EMIC2.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMIC2.Models.Interface.ERA2
{
    public interface IERA2
    {
        /// <summary>
        /// 匯入下層資訊，呼叫 Stored Procedure 回傳成功或失敗
        /// </summary>
        /// <returns>int 1 成功 0 失敗</returns>
        IResult LowerLevelCityIntegration(ERA2Dto data);

        /// <summary>
        /// 審核前確認
        /// </summary>
        /// <returns>int 1 成功 0 失敗</returns>
        IResult Confirmation(int RPT_MAIN_ID, DateTime? RPT_TIME);

        /// <summary>
        /// 專案查詢 原住民通報表最新填報統計
        /// </summary>
        /// <returns>List<ERA2Dto></returns>
        List<ERA2Dto> ERA2_PROJECT_SEARCH(ERA2Dto data);

        /// <summary>
        /// 匯入通報表，呼叫 Stored Procedure 回傳成功或失敗
        /// </summary>
        /// <returns>int 1 成功 0 失敗</returns>
        IResult ImportRptToDisp(string DISP_MAIN_ID, string DISP_DETAIL_ID, string DISP_STYLE_ID, string EOC_ID);


        /// <summary>
        /// 匯入外部資料，呼叫 Stored Procedure 回傳成功或失敗
        /// </summary>
        /// <returns>int 1 成功 0 失敗</returns>
        IResult ImportNccToRpt(string P_MTMP_REC_ID, string P_RPT_MAIN_ID, string P_RPT_CODE);

        /// <summary>
        /// 匯入外部 F2 - F8 資料，呼叫 Stored Procedure 回傳成功或失敗
        /// </summary>
        /// <returns>int 1 成功 0 失敗</returns>
        IResult ImportMOTCToRpt(string P_MTMP_REC_ID, string P_RPT_MAIN_ID, string P_RPT_CODE);

        /// <summary>
        /// 收容資料查詢 中央 D3, 縣市 D3a, 鄉鎮 D3a, 都要有該功能
        /// </summary>
        /// <returns>List<EEA2_SHELTER></returns>
        List<EEA2_SHELTER> ERA2_QRY_EEA2_SHELTER(ERA2Dto data);

        /// <summary>
        /// 匯入外部 中央 D3, 縣市 D3a, 鄉鎮 D3a 資料，呼叫 Stored Procedure 回傳成功或失敗
        /// </summary>
        /// <returns>int 1 成功 0 失敗</returns>
        IResult ERA2_IMP_EEA2_TO_RPT(ERA2Dto data);

        /// <summary>
        /// 收容資料查詢 中央 A4, 縣市 A4a, 鄉鎮 A4a 都要有該功能
        /// </summary>
        /// <returns>List<EEA2_EVACUATE></returns>
        List<EEA2_EVACUATE> ERA2_QRY_EEA2_EVACUATE(ERA2Dto data);

        /// <summary>
        /// 外部匯入API C1-C2
        /// 匯入外部資料，呼叫 Stored Procedure 回傳成功或失敗
        /// </summary>
        /// <returns>int 1 成功 0 失敗</returns>
        IResult ERA2_IMP_EXT_TO_RPT(string P_ATMP_REC_ID, string P_RPT_CODE);

        /// <summary>
        /// 外部匯入API 小A表
        /// 匯入外部資料，呼叫 Stored Procedure 回傳成功或失敗
        /// </summary>
        /// <returns>int 1 成功 0 失敗</returns>
        IResult ERA2_IMP_EXT_TO_RPTa(string P_ATMP_REC_ID, string P_RPT_CODE, string P_EOC_ID, string P_PRJ_NO);

        /// <summary>
        /// 確認是否為指定的災害應變中心EOC_ID、是否為正確的RPT_CODE 資料，呼叫 Stored Procedure 回傳成功或失敗
        /// </summary>
        /// <returns>int 1 成功 0 失敗</returns>
        ERA2_CHECK_EXT_TAG ERA2_CHECK_EXT_TAG(string P_EOC_NAME, string P_PRJ_NO, string P_RPT_CODE);
    }
}
