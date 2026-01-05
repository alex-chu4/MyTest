///////////////////////////////////////////////////////////////////////////////////////
//  程式檔名：
//  IERA2020123Dao.cs
//  程式名稱：
//  F1
///////////////////////////////////////////////////////////////////////////////////////
//  開發人員            日期             版本        備註
//  Joe             2019-08-23       1.0.0.0     初始版本
///////////////////////////////////////////////////////////////////////////////////////
//  Copyright (C) NEC Taiwan Ltd. 2018-2019.
//  程式描述：
//  TABLE FUNC
///////////////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using EMIC2.Result;

namespace EMIC2.Models.Interface.ERA2
{
    using EMIC2.Models.Dao.Dto;
    public interface IERA2020123Dao
    {
        /// <summary>
        /// 查詢按鈕狀態TABLE FUNC (F1才需要，F1a不需要)
        /// </summary>
        /// <returns>(int type, string status)</returns>
        ERA2Dto F1_QPER(ERA2Dto data);

        /// <summary>
        /// F1_S的更新SP
        /// 呼叫SP：ERA2_UPD_F1F1a_S
        /// (1)第一次產生主檔ERA_RPT_MAIN後，得到RPT_MAIN_ID，呼叫SP
        /// (2)異動明細(CRUD、狀態改變EX:搶通->阻斷)
        /// (3)無資料可填報
        /// (4)匯入第N報
        /// (5)刪除此區塊內容
        /// (6)主管機關 確定、取消確認(該處理時P_ORG_ID、P_ORG_CHECK需都給值，其它處理呼叫時這二參數都給NULL)
        /// </summary>
        /// <returns>IResult</returns>
        IResult ERA2_UPD_F1F1a_S(ERA2Dto data);

        /// <summary>
        /// 查詢災情
        /// </summary>
        /// <returns>IResult</returns>
        List<F1DaoResult> ERA2_QRY_DIM2_IS_ROADBLOCK(ERA2Dto data);

        /// <summary>
        /// 災情匯入
        /// </summary>
        /// <returns>IResult</returns>
        IResult ERA2_IMP_DIM2_TO_F1F1a(ERA2Dto data);

        /// <summary>
        /// 查詢公路總局
        /// </summary>
        /// <returns>(List<HightWayDaoResult>, int MTMP_REC_ID, DateTime? SENDER_TIME)</returns>
        (List<HightWayDaoResult>, int MTMP_REC_ID, DateTime? SENDER_TIME) ERA2_MTMP_REC(ERA2Dto data);

        /// <summary>
        /// 公路總局匯入
        /// </summary>
        /// <returns>IResult</returns>
        IResult ERA2_IMP_DGOH_TO_RPT(ERA2Dto data);
    }
}
