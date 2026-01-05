///////////////////////////////////////////////////////////////////////////////////////
//  程式檔名：
//  ISSO2Dao.cs
//  程式名稱：
//  工作會報-指示事項回報查詢
///////////////////////////////////////////////////////////////////////////////////////
//  開發人員		    日期             版本              備註
//  Vivian Chu       2019-07-04        1.0.0.0           初始版本
//  Vivian Chu       2019-09-20        3.0.0.0           匯出更新
///////////////////////////////////////////////////////////////////////////////////////
//  Copyright (C) NEC Taiwan Ltd. 2018-2019.
//  程式描述：
//  提供[指示事項回報]資料庫SQL查詢指令。 
//////////////////////////////////////////////////////////////////////////////////////
namespace EMIC2.Models.Interface.EEM2
{
    using EMIC2.Models.Dao.Dto.EEM2;
    using System;
    using System.Collections.Generic;

    public interface IEEM2Dao
    {
        /// <summary>
        /// 取得工作會報列表
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="meetingType"></param>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <param name="workStatus"></param>
        /// <param name="keyWork"></param>
        /// <param name="record_index"></param>
        /// <returns></returns>
        IEnumerable<EEM2010102Dto> GetDataList(string prjNo,
                                                string eocId,
                                                string orgId,
                                                byte meetingType,
                                                DateTime startTime,
                                                DateTime endTime,
                                                string workStatus,
                                                string keyWork,
                                                int record_index);

        /// <summary>
        /// 取得工作會報資訊
        /// </summary>
        /// <param name="workCode">workCode</param>
        /// <param name="orgId">orgId</param>
        /// <returns></returns>
        IEnumerable<EEM2010102WORKDto> GetWorkMainByWorkCode(decimal workCode, string eocId, string orgId, string workItemOgrUid);

        /// <summary>
        /// 取得特定工作會報的回報歷程
        /// </summary>
        /// <param name="workIemOrgUID">WORK_ITEM_ORG_UID</param>
        /// <returns></returns>
        IEnumerable<EEM2010102REPLYDto> GetReplyInfoById(decimal workIemOrgUID);

        /// <summary>
        /// 取得特定指示事項資訊
        /// </summary>
        /// <param name="workIemOrgUID"></param>
        /// <returns></returns>
        IEnumerable<EEM2010102ITEMDto> GetWorkItemById(decimal workIemOrgUID, string eocId);

        /// <summary>
        /// 取得通報表(C4-1)
        /// </summary>
        /// <param name="eocId"></param>
        /// <param name="prjNo"></param>
        /// <param name="levelType"></param>
        /// <returns></returns>
        IEnumerable<BOARD_C4_1Dto> GetBoardC4_1(string eocId, decimal prjNo, string levelType);

        /// <summary>
        /// 取得通報表(C4-2)
        /// </summary>
        /// <param name="eocId"></param>
        /// <param name="prjNo"></param>
        /// <returns></returns>
        IEnumerable<BOARD_C4_2Dto> GetBoardC4_2(string eocId, decimal prjNo, string levelType);

        /// <summary>
        /// 取得通報表(A1)
        /// </summary>
        /// <param name="eocId"></param>
        /// <param name="prjNo"></param>
        /// <returns></returns>
        IEnumerable<BOARD_A1Dto> GetBoardA1(string eocId, decimal prjNo);
       
        /// <summary>
        /// 取得通報表(A4)
        /// </summary>
        /// <param name="eocId"></param>
        /// <param name="prjNo"></param>
        /// <returns></returns>
        IEnumerable<BOARD_A4Dto> GetBoardA4(string eocId, decimal prjNo);

        /// <summary>
        /// 取得通報表(D3)
        /// </summary>
        /// <param name="eocId"></param>
        /// <param name="prjNo"></param>
        /// <returns></returns>
        IEnumerable<BOARD_D3Dto> GetBoardD3(string eocId, decimal prjNo);

        /// <summary>
        /// 取得通報表(E2)
        /// </summary>
        /// <param name="eocId"></param>
        /// <param name="prjNo"></param>
        /// <returns></returns>
        IEnumerable<BOARD_E2Dto> GetBoardE2(string eocId, decimal prjNo);

        /// <summary>
        /// 取得通報表(E4)
        /// </summary>
        /// <param name="eocId"></param>
        /// <param name="prjNo"></param>
        /// <returns></returns>
        IEnumerable<BOARD_E4Dto> GetBoardE4(string eocId, decimal prjNo);

        /// <summary>
        /// 取得通報表(J2)
        /// </summary>
        /// <param name="eocId"></param>
        /// <param name="prjNo"></param>
        /// <returns></returns>
        IEnumerable<BOARD_J2Dto> GetBoardJ2(string eocId, decimal prjNo);

        /// <summary>
        /// 取得通報表(J3)
        /// </summary>
        /// <param name="eocId"></param>
        /// <param name="prjNo"></param>
        /// <returns></returns>
        IEnumerable<BOARD_J3Dto> GetBoardJ3(string eocId, decimal prjNo);

        /// <summary>
        /// 取得水利設施 (KEY IN)、淹水、已退、處理中 (處置報告)
        /// </summary>
        /// <param name="eocId"></param>
        /// <param name="prjNo"></param>
        /// <returns></returns>
        IEnumerable<Water_FacilitiesDto> GetWaterFacilities(string eocId, decimal prjNo);

        /// <summary>
        /// 取得通報表(F1-1)
        /// </summary>
        /// <param name="eocId"></param>
        /// <param name="prjNo"></param>
        /// <returns></returns>
        IEnumerable<BOARD_F1_1Dto> GetBoardF1_1(string eocId, decimal prjNo);

        /// <summary>
        /// 取得通報表(F1-2)
        /// </summary>
        /// <param name="eocId"></param>
        /// <param name="prjNo"></param>
        /// <returns></returns>
        IEnumerable<BOARD_F1_1Dto> GetBoardF1_2(string eocId, decimal prjNo);

        /// <summary>
        /// 取得通報表(F1-3)
        /// </summary>
        /// <param name="eocId"></param>
        /// <param name="prjNo"></param>
        /// <returns></returns>
        IEnumerable<BOARD_F1_1Dto> GetBoardF1_3(string eocId, decimal prjNo);

        /// <summary>
        /// 取得通報表(F2)
        /// </summary>
        /// <param name="eocId"></param>
        /// <param name="prjNo"></param>
        /// <returns></returns>
        IEnumerable<BOARD_F2Dto> GetBoardF2(string eocId, decimal prjNo);

        /// <summary>
        /// 海運：通報表(F4)
        /// </summary>
        /// <param name="eocId"></param>
        /// <param name="prjNo"></param>
        /// <returns></returns>
        IEnumerable<BOARD_F4Dto> GetBoardF4(string eocId, decimal prjNo);

        /// <summary>
        /// 取得處置報告(農損)
        /// Disposal Report (Agricultural damage)
        /// </summary>
        /// <param name="eocId"></param>
        /// <param name="prjNo"></param>
        /// <returns></returns>
        IEnumerable<DRADDto> GetDRAD(string eocId, decimal prjNo);

        /// <summary>
        /// 取得通報表(G1)
        /// </summary>
        /// <param name="eocId"></param>
        /// <param name="prjNo"></param>
        /// <returns></returns>
        IEnumerable<BOARD_G1Dto> GetBoardG1(string eocId, decimal prjNo);

        /// <summary>
        /// 處置報告(國軍)
        /// Disposal Report
        /// </summary>
        /// <param name="eocId"></param>
        /// <param name="prjNo"></param>
        /// <returns></returns>
        IEnumerable<DRGuojunDto> GetDRGuojun(string eocId, decimal prjNo);

        /// <summary>
        /// 處置報告(消防)
        /// Disposal Report
        /// </summary>
        /// <param name="eocId"></param>
        /// <param name="prjNo"></param>
        /// <returns></returns>
        IEnumerable<DRFireDto> GetDRFire(string eocId, decimal prjNo);

        /// <summary>
        /// 取得通報表(A1a)-依鄉鎮別
        /// </summary>
        /// <param name="eocId"></param>
        /// <param name="prjNo"></param>
        /// <returns></returns>
        IEnumerable<BOARD_A1Dto> GetBoardA1a_3(string eocId, decimal prjNo);

        /// <summary>
        /// 取得通報表(A4a)-依鄉鎮別
        /// </summary>
        /// <param name="eocId"></param>
        /// <param name="prjNo"></param>
        /// <returns></returns>
        IEnumerable<BOARD_A4Dto> GetBoardA4a_3(string eocId, decimal prjNo);

        /// <summary>
        /// 取得通報表(D3a)-依鄉鎮別
        /// </summary>
        /// <param name="eocId"></param>
        /// <param name="prjNo"></param>
        /// <returns></returns>
        IEnumerable<BOARD_D3Dto> GetBoardD3a_3(string eocId, decimal prjNo);

        /// <summary>
        /// 通報表(F1a-1)-依鄉鎮別
        /// 國道
        /// </summary>
        /// <param name="eocId"></param>
        /// <param name="prjNo"></param>
        /// <returns></returns>
        /// <remarks>levelTyp e=>  中央:1 縣市:2 鄉鎮:3</remarks>
        IEnumerable<BOARD_F1_1Dto> GetBoardF1a_1_3(string eocId, decimal prjNo);

        /// <summary>
        /// 通報表(F1a-2)-依鄉鎮別
        /// 省道
        /// </summary>
        /// <param name="eocId"></param>
        /// <param name="prjNo"></param>
        /// <returns></returns>
        /// <remarks>levelTyp e=>  中央:1 縣市:2 鄉鎮:3</remarks>
        IEnumerable<BOARD_F1_1Dto> GetBoardF1a_2_3(string eocId, decimal prjNo);

        /// <summary>
        /// 通報表(F1a-3)-依鄉鎮別
        /// 縣道
        /// </summary>
        /// <param name="eocId"></param>
        /// <param name="prjNo"></param>
        /// <returns></returns>
        /// <remarks>levelTyp e=>  中央:1 縣市:2 鄉鎮:3</remarks>
        IEnumerable<BOARD_F1_1Dto> GetBoardF1a_3_3(string eocId, decimal prjNo);

        /// <summary>
        /// 通報表(A3a)-依鄉鎮別
        /// </summary>
        /// <param name="eocId"></param>
        /// <param name="prjNo"></param>
        /// <returns></returns>
        IEnumerable<DRFireDto> GetBoardA3a(string eocId, decimal prjNo);

        /// <summary>
        /// 通報表(A1a)
        /// 縣市別
        /// </summary>
        /// <param name="eocId"></param>
        /// <param name="prjNo"></param>
        /// <returns></returns>
        IEnumerable<BOARD_A1Dto> GetBoardA1a_2(string eocId, decimal prjNo);

        /// <summary>
        /// 通報表(A4a)
        /// 縣市別
        /// </summary>
        /// <param name="eocId"></param>
        /// <param name="prjNo"></param>
        /// <returns></returns>
        IEnumerable<BOARD_A4Dto> GetBoardA4a_2(string eocId, decimal prjNo);

        /// <summary>
        /// 通報表(D3a)
        /// 縣市別
        /// </summary>
        /// <param name="eocId"></param>
        /// <param name="prjNo"></param>
        /// <returns></returns>
        IEnumerable<BOARD_D3Dto> GetBoardD3a_2(string eocId, decimal prjNo);

        /// <summary>
        /// 通報表(E2)-依縣市別
        /// </summary>
        /// <param name="eocId"></param>
        /// <param name="prjNo"></param>
        /// <returns></returns>
        IEnumerable<BOARD_E2Dto> GetBoardE2_2(string eocId, decimal prjNo);

        /// <summary>
        /// 通報表(E4)-依縣市別
        /// </summary>
        /// <param name="eocId"></param>
        /// <param name="prjNo"></param>
        /// <returns></returns>
        IEnumerable<BOARD_E4Dto> GetBoardE4_2(string eocId, decimal prjNo);

        /// <summary>
        /// 通報表(J2)-依縣市別
        /// </summary>
        /// <param name="eocId"></param>
        /// <param name="prjNo"></param>
        /// <returns></returns>
        IEnumerable<BOARD_J2Dto> GetBoardJ2_2(string eocId, decimal prjNo);

        /// <summary>
        /// 通報表(J3)-依縣市別
        /// </summary>
        /// <param name="eocId"></param>
        /// <param name="prjNo"></param>
        /// <returns></returns>
        IEnumerable<BOARD_J3Dto> GetBoardJ3_2(string eocId, decimal prjNo);

        /// <summary>
        /// 通報表(F1a-1)
        /// </summary>
        /// <param name="eocId"></param>
        /// <param name="prjNo"></param>
        /// <returns></returns>
        /// <remarks>levelTyp e=>  中央:1 縣市:2 鄉鎮:3</remarks>
        IEnumerable<BOARD_F1_1Dto> GetBoardF1a_1_2(string eocId, decimal prjNo);

        /// <summary>
        /// 通報表(F1a-2)
        /// </summary>
        /// <param name="eocId"></param>
        /// <param name="prjNo"></param>
        /// <returns></returns>
        /// <remarks>levelTyp e=>  中央:1 縣市:2 鄉鎮:3</remarks>
        IEnumerable<BOARD_F1_1Dto> GetBoardF1a_2_2(string eocId, decimal prjNo);

        /// <summary>
        /// 通報表(F1a-3)
        /// </summary>
        /// <param name="eocId"></param>
        /// <param name="prjNo"></param>
        /// <returns></returns>
        /// <remarks>levelTyp e=>  中央:1 縣市:2 鄉鎮:3</remarks>
        IEnumerable<BOARD_F1_1Dto> GetBoardF1a_3_2(string eocId, decimal prjNo);

        /// <summary>
        /// 取得通報表(F2)-依縣市別
        /// </summary>
        /// <param name="eocId"></param>
        /// <param name="prjNo"></param>
        /// <returns></returns>
        /// <remarks>levelTyp e=>  中央:1 縣市:2 鄉鎮:3</remarks>
        IEnumerable<BOARD_F2Dto> GetBoardF2_2(string eocId, decimal prjNo);

        /// <summary>
        /// 取得通報表(G1)-依縣市別
        /// </summary>
        /// <param name="eocId"></param>
        /// <param name="prjNo"></param>
        /// <returns></returns>
        /// <remarks>levelTyp e=>  中央:1 縣市:2 鄉鎮:3</remarks>
        IEnumerable<BOARD_G1Dto> GetBoardG1_2(string eocId, decimal prjNo);

        /// <summary>
        /// 取得通報表(A3a)
        /// 縣市別
        /// </summary>
        /// <param name="eocId"></param>
        /// <param name="prjNo"></param>
        /// <returns></returns>
        IEnumerable<DRFireDto> GetBoardA3a_2(string eocId, decimal prjNo);

        /// <summary>
        /// 確認進駐機關是否有在其他作業留存紀錄
        /// </summary>
        /// <param name="orgId"></param>
        /// <returns></returns>
        int CheckORGCnt(decimal orgId);

        /// <summary>
        /// 取得匯入大事紀
        /// </summary>
        /// <param name="PRJ_NO"></param>
        /// <param name="StartTime"></param>
        /// <param name="EndTime"></param>
        /// <param name="EOC_ID"></param>
        /// <returns></returns>
        IEnumerable<EEM2010501Dto> Get_EEM2_Import(string PRJ_NO, DateTime StartTime, DateTime EndTime, string EOC_ID);
    }
}
