///////////////////////////////////////////////////////////////////////////////////////
//  程式檔名：
//  EEM2Dao.cs
//  程式名稱：
//  工作會報-指示事項回報查詢
///////////////////////////////////////////////////////////////////////////////////////
//  開發人員		    日期              版本              備註
//  Vivian Chu       2019-07-04         1.0.0.0           初始版本
//  Vivian Chu       2019-09-20         2.0.0.0           匯出更新
//  Vivian Chu       2019-09-30         3.0.0.0           匯入颱風警報單
//  Vivian Chu       2019-10-22         4.0.0.0           GetDataList查詢條件調整(Add UNION)
///////////////////////////////////////////////////////////////////////////////////////
//  Copyright (C) NEC Taiwan Ltd. 2018-2019.
//  程式描述：
//  提供[指示事項回報]資料庫SQL查詢指令。 
///////////////////////////////////////////////////////////////////////////////////////
namespace EMIC2.Models.Dao.EEM2
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using Dapper;
    using EMIC2.Models.Dao.Dto.EEM2;
    using EMIC2.Models.Helper;
    using EMIC2.Models.Interface.EEM2;

    public class EEM2Dao : IEEM2Dao
    {
        /// <summary>
        /// 取得工作會報列表
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="meetingType"></param>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <param name="workStatus"></param>
        /// <param name="keyWord"></param>
        /// <param name="record_index"></param>
        /// <returns></returns>
        /// 開發人員            日期              異動內容                    解決的問題
        /// Vivian Chu      2019-10-22          查詢條件調整(Add UNION)      依照需求調整
        public IEnumerable<EEM2010102Dto> GetDataList(string prjNo,
                                                        string eocId,
                                                        string orgId,
                                                        byte meetingType,
                                                        DateTime startTime,
                                                        DateTime endTime,
                                                        string workStatus,
                                                        string keyWord,
                                                        int record_index)
        {
            string sqlStatement = @" 
             SELECT Result.WORK_CODE, Result.PRJ_NO, Result.EOC_ID, Result.ORG_ID,
                    Result.RECORD_INDEX, Result.CASE_NAME, Result.NAME, Result.MEETING_TYPE,  
                    Result.WORK_ITEM, Result.REPLY_PROC, Result.ORG_NAME, Result.REPLY_TIME, Result.WORK_STATUS, Result.WORK_ITEM_ORG_UID,
                    Result.SUB_TIME 
             FROM		
		            (
                    SELECT 
                        workM.WORK_CODE
	                    , (SELECT count(*) FROM EEM2_WORK_ITEM_REPLY Where WORK_ITEM_ORG_UID = workIO.WORK_ITEM_ORG_UID) AS RECORD_INDEX
                        , workM.PRJ_NO				--專案序號
			            , (SELECT CASE_NAME FROM EEM2_EOC_PRJ WHERE PRJ_NO = workM.PRJ_NO) AS CASE_NAME     --專案名稱
                        , (SELECT EOC_ID FROM EEM2_EOC_PRJ WHERE PRJ_NO = workM.PRJ_NO) AS EOC_ID	        --應變中心代碼
			            --, eocP.CASE_NAME			--專案名稱
	                    , workM.NAME				--會議名稱
	                    , workM.MEETING_TYPE		--會議分類
	                    , workI.WORK_ITEM			--指示事項內容
                        , workI.SUB_TIME			--交辦日期時間
	                    , (SELECT Top 1 REPLY_PROC FROM EEM2_WORK_ITEM_REPLY Where WORK_ITEM_ORG_UID = workIO.WORK_ITEM_ORG_UID ORDER BY REPLY_TIME DESC) AS REPLY_PROC --處理情形
			            , ORG_ID
	                    , (Select ORG_NAME From EEM2_ORG_INFO Where ORG_ID = workIO.ORG_ID AND EOC_ID = @eocId) AS ORG_NAME     --交辦單位
	                    , (SELECT Top 1 REPLY_TIME FROM EEM2_WORK_ITEM_REPLY Where WORK_ITEM_ORG_UID = workIO.WORK_ITEM_ORG_UID ORDER BY REPLY_TIME DESC) AS REPLY_TIME --回報時間
	                    , workIO.WORK_STATUS		--列管狀態
                        , workIO.WORK_ITEM_ORG_UID  --指示事項分辦機關序號
                    FROM EEM2_WORK_MAIN workM 
                    --LEFT JOIN EEM2_EOC_PRJ eocP ON eocP.PRJ_NO = workM.PRJ_NO 
                    LEFT JOIN EEM2_WORK_ITEM workI ON workI.WORK_CODE = workM.WORK_CODE 
                    LEFT JOIN EEM2_WORK_ITEM_ORG workIO ON workIO.WORK_ITEM_UID = workI.WORK_ITEM_UID
                    WHERE 1 = 1 AND workM.PRJ_NO = @prjNo AND ORG_ID = @orgId";

            sqlStatement += @" 
                    ) AS Result
                ";

            // 20191022 add UNION
            sqlStatement += @" UNION
                             ";

            sqlStatement += @" 
             SELECT Result.WORK_CODE, Result.PRJ_NO, Result.EOC_ID, Result.ORG_ID,
                    Result.RECORD_INDEX, Result.CASE_NAME, Result.NAME, Result.MEETING_TYPE,  
                    Result.WORK_ITEM, Result.REPLY_PROC, Result.ORG_NAME, Result.REPLY_TIME, Result.WORK_STATUS, Result.WORK_ITEM_ORG_UID,
                    Result.SUB_TIME 
             FROM		
		            (
                    SELECT 
                        workM.WORK_CODE
	                    , (SELECT count(*) FROM EEM2_WORK_ITEM_REPLY Where WORK_ITEM_ORG_UID = workIO.WORK_ITEM_ORG_UID) AS RECORD_INDEX
                        , workM.PRJ_NO				--專案序號
			            , (SELECT CASE_NAME FROM EEM2_EOC_PRJ WHERE PRJ_NO = workM.PRJ_NO) + '(上級指派)' AS CASE_NAME 		--專案名稱
                        , (SELECT EOC_ID FROM EEM2_EOC_PRJ WHERE PRJ_NO = workM.PRJ_NO) AS EOC_ID	        --應變中心代碼
			            --, eocP.CASE_NAME			--專案名稱
	                    , workM.NAME				--會議名稱
	                    , workM.MEETING_TYPE		--會議分類
	                    , workI.WORK_ITEM			--指示事項內容
                        , workI.SUB_TIME			--交辦日期時間
	                    , (SELECT Top 1 REPLY_PROC FROM EEM2_WORK_ITEM_REPLY Where WORK_ITEM_ORG_UID = workIO.WORK_ITEM_ORG_UID ORDER BY REPLY_TIME DESC) AS REPLY_PROC --處理情形
			            , ORG_ID
	                    , (Select ORG_NAME From EEM2_ORG_INFO Where ORG_ID = workIO.ORG_ID AND EOC_ID = @eocId) AS ORG_NAME     --交辦單位
	                    , (SELECT Top 1 REPLY_TIME FROM EEM2_WORK_ITEM_REPLY Where WORK_ITEM_ORG_UID = workIO.WORK_ITEM_ORG_UID ORDER BY REPLY_TIME DESC) AS REPLY_TIME --回報時間
	                    , workIO.WORK_STATUS		--列管狀態
                        , workIO.WORK_ITEM_ORG_UID  --指示事項分辦機關序號
                    FROM EEM2_WORK_MAIN workM 
                    --LEFT JOIN EEM2_EOC_PRJ eocP ON eocP.PRJ_NO = workM.PRJ_NO 
                    LEFT JOIN EEM2_WORK_ITEM workI ON workI.WORK_CODE = workM.WORK_CODE 
                    LEFT JOIN EEM2_WORK_ITEM_ORG workIO ON workIO.WORK_ITEM_UID = workI.WORK_ITEM_UID
                    LEFT JOIN EEM2_EOC_PRJ ePJ ON ePJ.EOC_ID=(SELECT EOC_PARENT FROM EEM2_EOC_DATA WHERE EOC_ID = @eocId) and ePJ.PRJ_ETIME is null 
                    WHERE 1 = 1 AND WORK_ITEM_ORG_TYPE = 'A' AND ORG_ID = @orgId";

            sqlStatement += @" 
                    ) AS Result
                ";

            // 整合至一個TABLE
            sqlStatement = "SELECT * FROM (" + sqlStatement  + ") AS UR WHERE 1 = 1 ";

            if (meetingType > 0)
            {
                sqlStatement += " AND UR.MEETING_TYPE = @meetingType";
            }

            if (!string.IsNullOrEmpty(orgId))
            {
                sqlStatement += " AND UR.ORG_ID = @orgId";
            }

            if (startTime > new DateTime(1911, 1, 1) && endTime > new DateTime(1911, 1, 1))
            {
                sqlStatement += @" AND UR.REPLY_TIME >= @startTime AND UR.REPLY_TIME <= @endTime ";
            }

            if (!string.IsNullOrEmpty(workStatus) && workStatus != "-1")
            {
                sqlStatement += " AND UR.WORK_STATUS = @workStatus";
            }

            if (!string.IsNullOrEmpty(keyWord))
            {
                //sqlStatement += " AND (Result.NAME LIKE '%@keyWord%' OR Result.WORK_ITEM LIKE '%@keyWord%')";
                sqlStatement += " AND (UR.NAME LIKE '%" + keyWord + "%' OR UR.WORK_ITEM LIKE '%" + keyWord + "%')";  // TO DO
            }

            if (record_index >= 0)
            {
                if (record_index > 0)
                { sqlStatement += " AND UR.RECORD_INDEX >= @record_index"; }
                else
                { sqlStatement += " AND UR.RECORD_INDEX = @record_index"; }
            }

            sqlStatement += " ORDER BY UR.SUB_TIME DESC";

            using (var conn = new SqlConnection(DBHelper.GetEMIC2DBConnection()))
            {
                //設定參數
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@prjNo", prjNo);
                parameters.Add("@eocId", eocId);
                parameters.Add("@orgId", orgId);
                parameters.Add("@meetingType", meetingType);
                parameters.Add("@startTime", startTime);
                parameters.Add("@endTime", endTime);
                parameters.Add("@workStatus", workStatus);
                parameters.Add("@keyWord", keyWord);
                parameters.Add("@record_index", record_index);

                var result = conn.Query<EEM2010102Dto>(sqlStatement, parameters);
                return result;
            }
        }

        /// <summary>
        /// 取得工作會報資訊
        /// </summary>
        /// <param name="workCode"></param>
        /// <param name="orgId"></param>
        /// <param name="workItemOgrUid"></param>
        /// <returns></returns>
        public IEnumerable<EEM2010102WORKDto> GetWorkMainByWorkCode(decimal workCode, string eocId, string orgId, string workItemOgrUid)
        {
            string sqlStatement = @" 
                                    SELECT eocP.CASE_NAME, workM.COMMANDER, workM.NAME, workM.MEETING_TYPE, workM.WK_MEET_TIME, workM.NEX_MEET_TIME
	                                    , workF.FILE_ID, workF.FILE_NAME, workF.CREATED_TIME
	                                    , workI.WORK_ITEM_UID, workI.WORK_ITEM, workI.SUB_TIME
                                        , (Select ORG_NAME From EEM2_ORG_INFO Where ORG_ID = workIO.ORG_ID AND EOC_ID = @eocId) AS ORG_NAME		--交辦單位
	                                    , workIO.WORK_ITEM_ORG_UID
                                        , eocP.EOC_ID
	                                    FROM 
	                                    EEM2_WORK_MAIN workM
	                                    LEFT JOIN EEM2_EOC_PRJ eocP ON eocP.PRJ_NO = workM.PRJ_NO 
	                                    LEFT JOIN EEM2_WORK_FILE workF ON workF.WORK_CODE = workM.WORK_CODE 
	                                    LEFT JOIN EEM2_WORK_ITEM workI ON workI.WORK_CODE = workM.WORK_CODE 
	                                    LEFT JOIN EEM2_WORK_ITEM_ORG workIO ON workIO.WORK_ITEM_UID = workI.WORK_ITEM_UID
                                    WHERE 1=1                              
                                    ";

            if (!string.IsNullOrEmpty(workCode.ToString()))
            {
                sqlStatement += " AND workM.WORK_CODE = @workCode";
            }

            if (!string.IsNullOrEmpty(orgId))
            {
                sqlStatement += " AND ORG_ID = @orgId";
            }

            if (!string.IsNullOrEmpty(workItemOgrUid))
            {
                sqlStatement += " AND workIO.WORK_ITEM_ORG_UID = @workItemOgrUid";
            }

            using (var conn = new SqlConnection(DBHelper.GetEMIC2DBConnection()))
            {
                //設定參數
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@workCode", workCode);
                parameters.Add("@orgId", orgId);
                parameters.Add("@eocId", eocId);
                parameters.Add("@workItemOgrUid", workItemOgrUid);

                var result = conn.Query<EEM2010102WORKDto>(sqlStatement, parameters);
                return result;
            }
        }

        /// <summary>
        /// 指示事項回報資訊
        /// </summary>
        /// <param name="workIemOrgUID"></param>
        /// <returns></returns>
        public IEnumerable<EEM2010102REPLYDto> GetReplyInfoById(decimal workIemOrgUID)
        {
            string sqlStatement = @" 
                                    SELECT
                                        workIR.WORK_REPLY_UID,
	                                    REPLY_PROC, REPLY_TIME
	                                    , workIRF.FILE_ID, workIRF.FILE_NAME	
	                                    FROM EEM2_WORK_ITEM_REPLY workIR
	                                    LEFT JOIN EEM2_WORK_ITEM_REPLY_FILE workIRF on workIRF.WORK_REPLY_UID = workIR.WORK_REPLY_UID
                                    WHERE 1=1                                
                                    ";

            if (!string.IsNullOrEmpty(workIemOrgUID.ToString()))
            {
                sqlStatement += " AND WORK_ITEM_ORG_UID = @workIemOrgUID";
            }

            sqlStatement += " ORDER BY workIR.CREATED_TIME DESC";

            using (var conn = new SqlConnection(DBHelper.GetEMIC2DBConnection()))
            {
                //設定參數
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@workIemOrgUID", workIemOrgUID);

                var result = conn.Query<EEM2010102REPLYDto>(sqlStatement, parameters);
                return result;
            }
        }

        /// <summary>
        /// 取得特定指示事項資訊
        /// </summary>
        /// <param name="workIemOrgUID">workIemOrgUID</param>
        /// <param name="eocId">eocId</param>
        /// <returns></returns>
        IEnumerable<EEM2010102ITEMDto> IEEM2Dao.GetWorkItemById(decimal workIemOrgUID, string eocId)
        {
            string sqlStatement = @" 
                                    SELECT 
	                                    workI.WORK_ITEM, workI.SUB_TIME
	                                    , (SELECT ORG_NAME FROM EEM2_ORG_INFO WHERE ORG_ID = workIO.ORG_ID AND EOC_ID = @eocId) AS ORG_NAME     --交辦單位	
	                                    , (SELECT TOP 1 WORK_REPLY_UID FROM EEM2_WORK_ITEM_REPLY workIR WHERE workIR.WORK_ITEM_ORG_UID = workIO.WORK_ITEM_ORG_UID ORDER BY REPLY_TIME DESC) AS WORK_REPLY_UID
	                                    FROM EEM2_WORK_ITEM_ORG workIO
	                                    LEFT JOIN EEM2_WORK_ITEM workI ON workI.WORK_ITEM_UID = workIO.WORK_ITEM_UID 
                                    WHERE 1=1                                
                                    ";

            if (!string.IsNullOrEmpty(workIemOrgUID.ToString()))
            {
                sqlStatement += " AND WORK_ITEM_ORG_UID = @workIemOrgUID";
            }

            using (var conn = new SqlConnection(DBHelper.GetEMIC2DBConnection()))
            {
                //設定參數
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@workIemOrgUID", workIemOrgUID);
                parameters.Add("@eocId", eocId);

                var result = conn.Query<EEM2010102ITEMDto>(sqlStatement, parameters);
                return result;
            }
        }

        /// <summary>
        /// 取得通報表(C4-1)
        /// 中央
        /// </summary>
        /// <param name="eocId"></param>
        /// <param name="prjNo"></param>
        /// <returns></returns>
        public IEnumerable<BOARD_C4_1Dto> GetBoardC4_1(string eocId, decimal prjNo, string levelType)
        {
            string sqlStatement = @" 
                                    SELECT 
                                        SUM(RED_RIVERS) RED_RIVERS,				-- '紅色警戒溪流數',
                                        COUNT(CITY_NAME) RED_CITY_NAME,			-- '紅色警戒分布縣市'                                       
                                        COUNT(RED_TOWNSHIP) RED_TOWNSHIP,		-- '紅色警戒分布鄉鎮',
                                        COUNT(RED_VILLAGE) RED_VILLAGE,			-- '紅色警戒分布村里',
                                        SUM(AMBER_RIVERS) AMBER_RIVERS,			-- '黃色警戒溪流數',
                                        COUNT(CITY_NAME) AMBER_CITY_NAME,		-- '黃色警戒分布縣市',
                                        COUNT(AMBER_TOWNSHIP) AMBER_TOWNSHIP,	-- '黃色警戒分布鄉鎮',
                                        COUNT(AMBER_VILLAGE) AMBER_VILLAGE		-- '黃色警戒分布村里'
                                    FROM ERA2_QRY_MAX_C4 (default, @prjNo, 0)
                                    WHERE NO_DATA_MARK is NULL
                                    ";

            if (levelType == "2") // 縣市別
            {
                sqlStatement += " AND CITY_NAME = (SELECT DEP_NAME FROM EEM2_EOC_DATA WHERE EOC_ID = @eocId)";
            }

            using (var conn = new SqlConnection(DBHelper.GetEMIC2DBConnection()))
            {
                //設定參數
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@eocId", decimal.Parse(eocId));
                parameters.Add("@prjNo", prjNo);

                var result = conn.Query<BOARD_C4_1Dto>(sqlStatement, parameters);
                return result;
            }
        }

        /// <summary>
        /// 取得通報表(C4-2)
        /// 中央
        /// </summary>
        /// <param name="eocId"></param>
        /// <param name="prjNo"></param>
        /// <returns></returns>
        public IEnumerable<BOARD_C4_2Dto> GetBoardC4_2(string eocId, decimal prjNo, string levelType)
        {
            string sqlStatement = @" 
                                    SELECT 
                                        SUM(RED_RIVERS) RED_RIVERS,				-- '紅色警戒溪流數'
                                        COUNT(CITY_NAME) RED_CITY_NAME,			-- '紅色警戒分布縣市'
                                        COUNT(RED_TOWNSHIP) RED_TOWNSHIP,		-- '紅色警戒分布鄉鎮',
                                        COUNT(RED_VILLAGE) RED_VILLAGE,			-- '紅色警戒分布村里',
                                        SUM(AMBER_RIVERS) AMBER_RIVERS,			-- '黃色警戒溪流數',
                                        COUNT(CITY_NAME) AMBER_CITY_NAME,		-- '黃色警戒分布縣市',
                                        COUNT(AMBER_TOWNSHIP) AMBER_TOWNSHIP,	-- '黃色警戒分布鄉鎮',
                                        COUNT(AMBER_VILLAGE) AMBER_VILLAGE		-- '黃色警戒分布村里'
                                    FROM ERA2_QRY_MAX_C4 (default, @prjNo, 0)
                                    WHERE NO_DATA_MARK is NULL
                                    ";

            if (levelType == "2") // 縣市別
            {
                sqlStatement += " AND CITY_NAME = (SELECT DEP_NAME FROM EEM2_EOC_DATA WHERE EOC_ID = @eocId)";
            }

            using (var conn = new SqlConnection(DBHelper.GetEMIC2DBConnection()))
            {
                //設定參數
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@eocId", eocId);
                parameters.Add("@prjNo", prjNo);

                var result = conn.Query<BOARD_C4_2Dto>(sqlStatement, parameters);
                return result;
            }
        }

        /// <summary>
        /// 取得通報表(A1)
        /// </summary>
        /// <param name="eocId"></param>
        /// <param name="prjNo"></param>
        /// <returns></returns>
        public IEnumerable<BOARD_A1Dto> GetBoardA1(string eocId, decimal prjNo)
        {
            string sqlStatement = @"
                                    SELECT  
                                        SUM(DEAD) DEAD,		        --'死亡人數'
                                        SUM(MISSING) MISSING,	    --'失蹤人數'
                                        SUM(INJURED) INJURED	    --'受傷人數'
                                    FROM ERA2_QRY_MAX_A1 (default, @prjNo, 0)
                                    WHERE NO_DATA_MARK is NULL;     --無資料可填報 NO_DATA_MARK = 1
                                   ";

            using (var conn = new SqlConnection(DBHelper.GetEMIC2DBConnection()))
            {
                //設定參數
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@eocId", eocId);
                parameters.Add("@prjNo", prjNo);

                var result = conn.Query<BOARD_A1Dto>(sqlStatement, parameters);
                return result;
            }
        }

        /// <summary>
        /// 取得通報表(A4)
        /// </summary>
        /// <param name="eocId"></param>
        /// <param name="prjNo"></param>
        /// <returns></returns>
        public IEnumerable<BOARD_A4Dto> GetBoardA4(string eocId, decimal prjNo)
        {
            string sqlStatement = @"
                                    SELECT 
                                        SUM(EVACUATE_TOTAL) EVACUATE_TOTAL      --'累計疏散撤離人數' 
                                    FROM ERA2_QRY_MAX_A4 (default, @prjNo, 0)
                                    WHERE NO_DATA_MARK is NULL;                 --無資料可填報 NO_DATA_MARK = 1
                                   ";

            using (var conn = new SqlConnection(DBHelper.GetEMIC2DBConnection()))
            {
                //設定參數
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@eocId", eocId);
                parameters.Add("@prjNo", prjNo);

                var result = conn.Query<BOARD_A4Dto>(sqlStatement, parameters);
                return result;
            }
        }

        /// <summary>
        /// 取得通報表(D3)
        /// </summary>
        /// <param name="eocId"></param>
        /// <param name="prjNo"></param>
        /// <returns></returns>
        public IEnumerable<BOARD_D3Dto> GetBoardD3(string eocId, decimal prjNo)
        {
            string sqlStatement = @" 
                                    SELECT  
	                                    SUM(REFUGEE_TOTAL) REFUGEE_TOTAL,	    --'最高收容人數'
	                                    SUM(REFUGEE) REFUGEE --'目前收容人數'
                                    FROM ERA2_QRY_MAX_D3 (default, @prjNo, 0)
                                    WHERE NO_DATA_MARK is NULL;				    --無資料可填報 NO_DATA_MARK = 1
                                   ";

            using (var conn = new SqlConnection(DBHelper.GetEMIC2DBConnection()))
            {
                //設定參數
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@eocId", eocId);
                parameters.Add("@prjNo", prjNo);

                var result = conn.Query<BOARD_D3Dto>(sqlStatement, parameters);
                return result;
            }
        }

        /// <summary>
        /// 取得通報表(E2)
        /// </summary>
        /// <param name="eocId"></param>
        /// <param name="prjNo"></param>
        /// <returns></returns>
        public IEnumerable<BOARD_E2Dto> GetBoardE2(string eocId, decimal prjNo)
        {
            string sqlStatement = @" 
                                    SELECT  
	                                    SUM(ELECTRO_DMG_USED) ELECTRO_DMG_USED,					--'修護戶數'
	                                    SUM(ELECTRO_DMG_NOW) ELECTRO_DMG_NOW,					--'待修護戶數'
	                                    SUM(ELECTRO_DMG_USED+ELECTRO_DMG_NOW) POWER_FAILURE		--'停電戶數
                                    FROM ERA2_QRY_MAX_E2 (default, @prjNo, 0)
                                    WHERE NO_DATA_MARK is NULL;  --無資料可填報 NO_DATA_MARK = 1
                                   ";

            using (var conn = new SqlConnection(DBHelper.GetEMIC2DBConnection()))
            {
                //設定參數
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@eocId", eocId);
                parameters.Add("@prjNo", prjNo);

                var result = conn.Query<BOARD_E2Dto>(sqlStatement, parameters);
                return result;
            }
        }

        /// <summary>
        /// 取得通報表(E4)
        /// </summary>
        /// <param name="eocId"></param>
        /// <param name="prjNo"></param>
        /// <returns></returns>
        public IEnumerable<BOARD_E4Dto> GetBoardE4(string eocId, decimal prjNo)
        {
            string sqlStatement = @" 
                                    SELECT  
	                                    SUM(WATER_DMG) WATER_DMG,		--'停水'
	                                    SUM(WATER_RECVY) WATER_RECVY,	--'修護'
	                                    SUM(WATER_STOP) WATER_STOP		--'待修護'
                                    FROM ERA2_QRY_MAX_E4 (default, @prjNo, 0)
                                    WHERE NO_DATA_MARK is NULL;  --無資料可填報 NO_DATA_MARK = 1
                                   ";

            using (var conn = new SqlConnection(DBHelper.GetEMIC2DBConnection()))
            {
                //設定參數
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@eocId", eocId);
                parameters.Add("@prjNo", prjNo);

                var result = conn.Query<BOARD_E4Dto>(sqlStatement, parameters);
                return result;
            }
        }

        /// <summary>
        /// 取得通報表(J2)
        /// </summary>
        /// <param name="eocId"></param>
        /// <param name="prjNo"></param>
        /// <returns></returns>
        public IEnumerable<BOARD_J2Dto> GetBoardJ2(string eocId, decimal prjNo)
        {
            string sqlStatement = @" 
                                    SELECT  
	                                    SUM(PHONE_DAMAGE_TOTAL) PHONE_DAMAGE_TOTAL,		--'市話中斷戶數',
	                                    SUM(PHONE_FIXED) PHONE_FIXED,					--'市話修復戶數',
	                                    SUM(PHONE_DAMAGE) PHONE_DAMAGE					--'市話待修護戶數'
                                    FROM ERA2_QRY_MAX_J2 (default, @prjNo, 0)
                                    WHERE NO_DATA_MARK is NULL;  --無資料可填報 NO_DATA_MARK = 1
                                   ";

            using (var conn = new SqlConnection(DBHelper.GetEMIC2DBConnection()))
            {
                //設定參數
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@eocId", eocId);
                parameters.Add("@prjNo", prjNo);

                var result = conn.Query<BOARD_J2Dto>(sqlStatement, parameters);
                return result;
            }
        }

        /// <summary>
        /// 取得通報表(J3)
        /// </summary>
        /// <param name="eocId"></param>
        /// <param name="prjNo"></param>
        /// <returns></returns>
        public IEnumerable<BOARD_J3Dto> GetBoardJ3(string eocId, decimal prjNo)
        {
            string sqlStatement = @"
                                    SELECT  
	                                    SUM(SITE_DAMAGE_TOTAL) SITE_DAMAGE_TOTAL,									--'受損座數'
	                                    SUM(SITE_FIXED) SITE_FIXED,													--'修護座數'
	                                    SUM(SITE_FIXED_NOT+STATION_FIXED_NOT+OTHERS_FIXED_NOT) SITE_PENDING_REPAIR	--'待修護座數'
                                    FROM ERA2_QRY_MAX_J3 (default, @prjNo, 0)
                                    WHERE NO_DATA_MARK is NULL;  --無資料可填報 NO_DATA_MARK = 1
                                   ";

            using (var conn = new SqlConnection(DBHelper.GetEMIC2DBConnection()))
            {
                //設定參數
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@eocId", eocId);
                parameters.Add("@prjNo", prjNo);

                var result = conn.Query<BOARD_J3Dto>(sqlStatement, parameters);
                return result;
            }
        }

        /// <summary>
        /// 取得水利設施 (KEY IN)、淹水、已退、處理中 (處置報告)
        /// </summary>
        /// <param name="eocId"></param>
        /// <param name="prjNo"></param>
        /// <returns></returns>
        public IEnumerable<Water_FacilitiesDto> GetWaterFacilities(string eocId, decimal prjNo)
        {
            string sqlStatement = @" 
                                    SELECT TOP 1
	                                    COUNT(I.START_FLOOD) START_FLOOD,					        --'淹水幾處'
	                                    COUNT(I.END_FLOOD) END_FLOOD,						        --'已退幾處'
	                                    (COUNT(I.START_FLOOD)-count(I.END_FLOOD)) PROCESSING        --'處理中'
		                                     FROM ERA2_DISP_LOCAL_FLOOD I
		                                    , ERA2_DISP_MAIN M
		                                    , ERA2_DISP_DETAIL D
		                                    WHERE I.DISP_DETAIL_ID = D.DISP_DETAIL_ID
		                                    AND D.DISP_MAIN_ID = M.DISP_MAIN_ID
		                                    AND M.EOC_ID = '00000'                      --一定是僅查中央(此條件固定)
		                                    AND M.PRJ_NO = @prjNo                       --須提供專案代碼PRJ_NO(按照專案別輸入)
		                                    AND M.DISP_NO IN (SELECT MAX(DISP_NO)
		                                    FROM ERA2_DISP_LOCAL_FLOOD I
		                                    , ERA2_DISP_MAIN M
		                                    , ERA2_DISP_DETAIL D
		                                    WHERE I.DISP_DETAIL_ID = D.DISP_DETAIL_ID
		                                    AND D.DISP_MAIN_ID = M.DISP_MAIN_ID
		                                    AND M.EOC_ID = '00000'                       --一定是僅查中央(此條件固定)
		                                    AND M.PRJ_NO = @prjNo                       --須提供專案代碼PRJ_NO(按照專案別輸入)
	                                    ) 
                                    ";

            using (var conn = new SqlConnection(DBHelper.GetEMIC2DBConnection()))
            {
                //設定參數
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@eocId", eocId);
                parameters.Add("@prjNo", prjNo);

                var result = conn.Query<Water_FacilitiesDto>(sqlStatement, parameters);
                return result;
            }
        }

        /// <summary>
        /// 取得通報表(F1-1)
        /// </summary>
        /// <param name="eocId"></param>
        /// <param name="prjNo"></param>
        /// <returns></returns>
        public IEnumerable<BOARD_F1_1Dto> GetBoardF1_1(string eocId, decimal prjNo)
        {
            string sqlStatement = @" 
                                    SELECT  
	                                    SUM(BLOCK_COUNT) BLOCK_COUNT,		--'中斷數'
	                                    SUM(UNBLOCK_COUNT) UNBLOCK_COUNT,	--'搶通數'
	                                    SUM(STOP_COUNT) STOP_COUNT			--'預警封閉數' 
                                    FROM ERA2_QRY_MAX_F1_S (default, @prjNo, 0)
                                    WHERE ROADTYPE_ID = 0 --國道代碼=0
                                   ";

            using (var conn = new SqlConnection(DBHelper.GetEMIC2DBConnection()))
            {
                //設定參數
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@eocId", eocId);
                parameters.Add("@prjNo", prjNo);

                var result = conn.Query<BOARD_F1_1Dto>(sqlStatement, parameters);
                return result;
            }
        }

        /// <summary>
        /// 取得通報表(F1-2)
        /// </summary>
        /// <param name="eocId"></param>
        /// <param name="prjNo"></param>
        /// <returns></returns>
        public IEnumerable<BOARD_F1_1Dto> GetBoardF1_2(string eocId, decimal prjNo)
        {
            string sqlStatement = @" 
                                    SELECT  
	                                    SUM(BLOCK_COUNT) BLOCK_COUNT,		--'中斷數' 
	                                    SUM(UNBLOCK_COUNT) UNBLOCK_COUNT,	--'搶通數'
	                                    SUM(STOP_COUNT) STOP_COUNT			--'預警封閉數' 
                                    FROM ERA2_QRY_MAX_F1_S (default, @prjNo, 0)
                                    WHERE ROADTYPE_ID = 1 --省道代碼=1
                                   ";

            using (var conn = new SqlConnection(DBHelper.GetEMIC2DBConnection()))
            {
                //設定參數
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@eocId", eocId);
                parameters.Add("@prjNo", prjNo);

                var result = conn.Query<BOARD_F1_1Dto>(sqlStatement, parameters);
                return result;
            }
        }

        /// <summary>
        /// 取得通報表(F1-3)
        /// </summary>
        /// <param name="eocId"></param>
        /// <param name="prjNo"></param>
        /// <returns></returns>
        public IEnumerable<BOARD_F1_1Dto> GetBoardF1_3(string eocId, decimal prjNo)
        {
            string sqlStatement = @" 
                                    SELECT  
	                                    SUM(BLOCK_COUNT) BLOCK_COUNT,		--'中斷數' , 
	                                    SUM(UNBLOCK_COUNT) UNBLOCK_COUNT,	--'搶通數', 
	                                    SUM(STOP_COUNT) STOP_COUNT			--'預警封閉數' 
                                    FROM ERA2_QRY_MAX_F1_S (default, @prjNo, 0)
                                    WHERE ROADTYPE_ID in (2,3) --縣道代碼=2,3
                                   ";

            using (var conn = new SqlConnection(DBHelper.GetEMIC2DBConnection()))
            {
                //設定參數
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@eocId", eocId);
                parameters.Add("@prjNo", prjNo);

                var result = conn.Query<BOARD_F1_1Dto>(sqlStatement, parameters);
                return result;
            }
        }

        /// <summary>
        /// 取得通報表(F2)
        /// </summary>
        /// <param name="eocId"></param>
        /// <param name="prjNo"></param>
        /// <returns></returns>
        public IEnumerable<BOARD_F2Dto> GetBoardF2(string eocId, decimal prjNo)
        {
            // '00000'固定輸入值(因為中央才會填)
            string sqlStatement = @" 
                                    SELECT  
                                        '台鐵停駛' 'ITEM',count(*) 'NUMBER_STOP'
                                    FROM ERA2_QRY_MAX_F21 ('00000', @prjNo, 0)  
                                    WHERE RAILTYPE = '台鐵'
                                    UNION
                                    SELECT  
                                        '高鐵停駛' 'ITEM',count(*) 'NUMBER_STOP'
                                    FROM ERA2_QRY_MAX_F21 ('00000', @prjNo, 0)
                                    WHERE RAILTYPE = '高鐵'
                                   ";

            using (var conn = new SqlConnection(DBHelper.GetEMIC2DBConnection()))
            {
                //設定參數
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@eocId", eocId);
                parameters.Add("@prjNo", prjNo);

                var result = conn.Query<BOARD_F2Dto>(sqlStatement, parameters);
                return result;
            }
        }

        /// <summary>
        /// 海運：通報表(F4)
        /// TO DO
        /// </summary>
        /// <param name="eocId"></param>
        /// <param name="prjNo"></param>
        /// <returns></returns>
        public IEnumerable<BOARD_F4Dto> GetBoardF4(string eocId, decimal prjNo)
        {
            string sqlStatement = @" 
                                   ";

            using (var conn = new SqlConnection(DBHelper.GetEMIC2DBConnection()))
            {
                //設定參數
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@eocId", eocId);
                parameters.Add("@prjNo", prjNo);

                var result = conn.Query<BOARD_F4Dto>(sqlStatement, parameters);
                return result;
            }
        }

        /// <summary>
        /// 取得處置報告(農損)
        /// Disposal Report (Agricultural damage)
        /// </summary>
        /// <param name="eocId"></param>
        /// <param name="prjNo"></param>
        /// <returns></returns>
        public IEnumerable<DRADDto> GetDRAD(string eocId, decimal prjNo)
        {
            string sqlStatement = @" 
                                    SELECT TOP 1
	                                    SUM(I.CROP) CROP --'農業損失'
	                                        FROM ERA2_DISP_INDUSTRY_LOSE I
	                                    , ERA2_DISP_MAIN M
	                                    , ERA2_DISP_DETAIL D
	                                    WHERE I.DISP_DETAIL_ID = D.DISP_DETAIL_ID
	                                    AND D.DISP_MAIN_ID = M.DISP_MAIN_ID
	                                    AND M.EOC_ID = '00000'                      --一定是僅查中央(此條件固定)
	                                    AND M.PRJ_NO = @prjNo                       --須提供專案代碼PRJ_NO(按照專案別輸入)
	                                    AND M.DISP_NO IN (SELECT max(DISP_NO)
	                                    FROM ERA2_DISP_INDUSTRY_LOSE I
	                                    , ERA2_DISP_MAIN M
	                                    , ERA2_DISP_DETAIL D
	                                    WHERE I.DISP_DETAIL_ID = D.DISP_DETAIL_ID
	                                    AND D.DISP_MAIN_ID = M.DISP_MAIN_ID
	                                    AND M.EOC_ID = '00000'                      --一定是僅查中央(此條件固定)
	                                    AND M.PRJ_NO = @prjNo                       --須提供專案代碼PRJ_NO(按照專案別輸入)
                                    )
                                    ";

            using (var conn = new SqlConnection(DBHelper.GetEMIC2DBConnection()))
            {
                //設定參數
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@eocId", eocId);
                parameters.Add("@prjNo", prjNo);

                var result = conn.Query<DRADDto>(sqlStatement, parameters);
                return result;
            }
        }

        /// <summary>
        /// 取得通報表(G1)
        /// </summary>
        /// <param name="eocId"></param>
        /// <param name="prjNo"></param>
        /// <returns></returns>
        public IEnumerable<BOARD_G1Dto> GetBoardG1(string eocId, decimal prjNo)
        {
            string sqlStatement = @"
                                    SELECT  
	                                     SUM(DAMAGE_AMOUNT) DAMAGE_AMOUNT           --'學校受損金額' 
	                                    FROM ERA2_QRY_MAX_G1 (default, @prjNo, 0)
	                                    WHERE NO_DATA_MARK is NULL;                 --無資料可填報 NO_DATA_MARK = 1
                                    ";

            using (var conn = new SqlConnection(DBHelper.GetEMIC2DBConnection()))
            {
                //設定參數
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@eocId", eocId);
                parameters.Add("@prjNo", prjNo);

                var result = conn.Query<BOARD_G1Dto>(sqlStatement, parameters);
                return result;
            }
        }

        /// <summary>
        /// 處置報告(國軍)
        /// Disposal Report
        /// </summary>
        /// <param name="eocId"></param>
        /// <param name="prjNo"></param>
        /// <returns></returns>
        public IEnumerable<DRGuojunDto> GetDRGuojun(string eocId, decimal prjNo)
        {
            string sqlStatement = @" 
                                    SELECT TOP 1
	                                    SUM(I.PEOPLE) PEOPLE,                           --'國軍支援兵力',
	                                    SUM(I.CAR+I.SHIP+I.MACHINE) CSM,                --'國軍支援機具',
	                                    SUM(I.HELICOPTER+I.AIR_CAMERA) HA_C            --'國軍支援航空器'
		                                     FROM ERA2_DISP_MANPOWER I
		                                    , ERA2_DISP_MAIN M
		                                    , ERA2_DISP_DETAIL D
		                                    WHERE I.DISP_DETAIL_ID = D.DISP_DETAIL_ID
		                                    AND D.DISP_MAIN_ID = M.DISP_MAIN_ID
		                                    AND M.EOC_ID = '00000'                      --一定是僅查中央(此條件固定)
		                                    AND M.PRJ_NO = @prjNo                       --須提供專案代碼CASE_ID(按照專案別輸入)
		                                    AND M.DISP_NO IN (SELECT max(DISP_NO)
		                                    FROM ERA2_DISP_MANPOWER I
		                                    , ERA2_DISP_MAIN M
		                                    , ERA2_DISP_DETAIL D
		                                    WHERE I.DISP_DETAIL_ID = D.DISP_DETAIL_ID
		                                    AND D.DISP_MAIN_ID = M.DISP_MAIN_ID
		                                    AND M.EOC_ID = '00000'                      --一定是僅查中央(此條件固定)
		                                    AND M.PRJ_NO = @prjNo                       --須提供專案代碼CASE_ID(按照專案別輸入)
		                                    )
		                                    AND I.UNIT_NAME = '國軍'
                                    ";

            using (var conn = new SqlConnection(DBHelper.GetEMIC2DBConnection()))
            {
                //設定參數
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@eocId", eocId);
                parameters.Add("@prjNo", prjNo);

                var result = conn.Query<DRGuojunDto>(sqlStatement, parameters);
                return result;
            }
        }

        /// <summary>
        /// 處置報告(消防)
        /// Disposal Report
        /// </summary>
        /// <param name="eocId"></param>
        /// <param name="prjNo"></param>
        /// <returns></returns>
        public IEnumerable<DRFireDto> GetDRFire(string eocId, decimal prjNo)
        {
            string sqlStatement = @"
                                    SELECT TOP 1
	                                    SUM(I.PEOPLE) PEOPLE,                   --'消防支援兵力'
	                                    SUM(I.CAR+I.MACHINE) CARMACHINE,        --'消防支援機具'
	                                    SUM(I.SHIP) SHIP,                       --'消防支援船艇'
	                                    SUM(I.HELICOPTER+I.AIR_CAMERA) HA_C     --'消防支援航空器'
	                                     FROM ERA2_DISP_MANPOWER I
	                                    , ERA2_DISP_MAIN M
	                                    , ERA2_DISP_DETAIL D
	                                    WHERE I.DISP_DETAIL_ID = D.DISP_DETAIL_ID
	                                    AND D.DISP_MAIN_ID = M.DISP_MAIN_ID
	                                    AND M.EOC_ID = '00000'                      --一定是僅查中央(此條件固定)
	                                    AND M.PRJ_NO = @prjNo                       --須提供專案代碼CASE_ID(按照專案別輸入)
	                                    AND M.DISP_NO IN (SELECT MAX(DISP_NO)
	                                    FROM ERA2_DISP_MANPOWER I
	                                    , ERA2_DISP_MAIN M
	                                    , ERA2_DISP_DETAIL D
	                                    WHERE I.DISP_DETAIL_ID = D.DISP_DETAIL_ID
	                                    AND D.DISP_MAIN_ID = M.DISP_MAIN_ID
	                                    AND M.EOC_ID = '00000'                      --一定是僅查中央(此條件固定)
	                                    AND M.PRJ_NO = @prjNo                       --須提供專案代碼CASE_ID(按照專案別輸入)
	                                    )
	                                    AND I.UNIT_NAME like '%消防%'
                                    ";

            using (var conn = new SqlConnection(DBHelper.GetEMIC2DBConnection()))
            {
                //設定參數
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@eocId", eocId);
                parameters.Add("@prjNo", prjNo);

                var result = conn.Query<DRFireDto>(sqlStatement, parameters);
                return result;
            }
        }

        /// <summary>
        /// 取得通報表(A1a)-依鄉鎮別
        /// </summary>
        /// <param name="eocId"></param>
        /// <param name="prjNo"></param>
        /// <returns></returns>
        public IEnumerable<BOARD_A1Dto> GetBoardA1a_3(string eocId, decimal prjNo)
        {
            string sqlStatement = @"
                                    SELECT  
	                                        SUM(DEAD) DEAD,　			--'死亡人數'
	                                        SUM(MISSING) MISSING,		--'失蹤人數'
	                                        SUM(INJURED) INJURED		--'受傷人數'
                                    FROM ERA2_QRY_MAX_A1a (@eocId, @prjNo, 0)
                                    WHERE NO_DATA_MARK is NULL  --無資料可填報 NO_DATA_MARK = 1
                                    AND TOWN_NAME = (SELECT DEP_NAME FROM EEM2_EOC_DATA WHERE EOC_ID = @eocId)   
                                   ";

            using (var conn = new SqlConnection(DBHelper.GetEMIC2DBConnection()))
            {
                //設定參數
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@eocId", eocId);
                parameters.Add("@prjNo", prjNo);

                var result = conn.Query<BOARD_A1Dto>(sqlStatement, parameters);
                return result;
            }
        }

        /// <summary>
        /// 取得通報表(A4a)-依鄉鎮別
        /// </summary>
        /// <param name="eocId"></param>
        /// <param name="prjNo"></param>
        /// <returns></returns>
        public IEnumerable<BOARD_A4Dto> GetBoardA4a_3(string eocId, decimal prjNo)
        {
            string sqlStatement = @"
                                    SELECT 
	                                    SUM(EVACUATE_TOTAL) EVACUATE_TOTAL　--'累計疏散撤離人數' 
                                    FROM ERA2_QRY_MAX_A4a (@eocId, @prjNo, 0)
                                    WHERE NO_DATA_MARK is NULL  --無資料可填報 NO_DATA_MARK = 1
                                    AND TOWN_NAME = (SELECT DEP_NAME FROM EEM2_EOC_DATA WHERE EOC_ID = @eocId) --token.EOC_ID%
                                   ";

            using (var conn = new SqlConnection(DBHelper.GetEMIC2DBConnection()))
            {
                //設定參數
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@eocId", eocId);
                parameters.Add("@prjNo", prjNo);

                var result = conn.Query<BOARD_A4Dto>(sqlStatement, parameters);
                return result;
            }
        }

        /// <summary>
        /// 取得通報表(D3a)-依鄉鎮別
        /// </summary>
        /// <param name="eocId"></param>
        /// <param name="prjNo"></param>
        /// <returns></returns>
        public IEnumerable<BOARD_D3Dto> GetBoardD3a_3(string eocId, decimal prjNo)
        {
            string sqlStatement = @"
                                    SELECT  
	                                    SUM(REFUGEE_TOTAL) REFUGEE_TOTAL,	--'最高收容人數',
	                                    SUM(REFUGEE) REFUGEE				--'目前收容人數'
                                    FROM ERA2_QRY_MAX_D3a (@eocId, @prjNo, 0)
                                    WHERE NO_DATA_MARK is NULL  --無資料可填報 NO_DATA_MARK = 1
                                    AND TOWN_NAME = (SELECT DEP_NAME FROM EEM2_EOC_DATA WHERE EOC_ID = @eocId) --token.EOC_ID%
                                   ";

            using (var conn = new SqlConnection(DBHelper.GetEMIC2DBConnection()))
            {
                //設定參數
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@eocId", eocId);
                parameters.Add("@prjNo", prjNo);

                var result = conn.Query<BOARD_D3Dto>(sqlStatement, parameters);
                return result;
            }
        }

        /// <summary>
        /// 通報表(F1a-1)-依鄉鎮別
        /// 國道
        /// </summary>
        /// <param name="eocId"></param>
        /// <param name="prjNo"></param>
        /// <returns></returns>
        public IEnumerable<BOARD_F1_1Dto> GetBoardF1a_1_3(string eocId, decimal prjNo)
        {
            string sqlStatement = @"
                                    SELECT 
	                                    (SELECT COUNT(*) BLOCK_COUNT　	--'國道中斷' 
	                                     FROM ERA2_QRY_MAX_F1a (@eocId, @prjNo, 0)
	                                     WHERE ROADTYPE_ID = '0'		--0=國道
	                                     AND TRFSTATUS = 'D0'
	                                     AND TOWN_ID = 6500100			--變數輸入規則=登入者EOC所在鄉鎮%token.EOC_DEP_CODE%
	                                    ) BLOCK_COUNT
	                                    ,
	                                    (SELECT COUNT(*) UNBLOCK_COUNT	--'國道搶通' 
	                                     FROM ERA2_QRY_MAX_F1a (@eocId, @prjNo, 0)
	                                     WHERE ROADTYPE_ID = '0'		--0=國道
	                                     AND TRFSTATUS = 'D1'
	                                     AND TOWN_ID = 6500100			--變數輸入規則=登入者EOC所在鄉鎮%token.EOC_DEP_CODE%
	                                    ) UNBLOCK_COUNT
	                                    ,
	                                    (SELECT COUNT(*) STOP_COUNT		--'國道預警封閉' 
	                                     FROM ERA2_QRY_MAX_F1a (@eocId, @prjNo5, 0)
	                                     WHERE ROADTYPE_ID = '0'		--0=國道
	                                     AND TRFSTATUS = 'P0'
	                                     AND TOWN_ID = 6500100			--變數輸入規則=登入者EOC所在鄉鎮%token.EOC_DEP_CODE%
	                                    ) STOP_COUNT
                                    ";

            using (var conn = new SqlConnection(DBHelper.GetEMIC2DBConnection()))
            {
                //設定參數
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@eocId", eocId);
                parameters.Add("@prjNo", prjNo);

                var result = conn.Query<BOARD_F1_1Dto>(sqlStatement, parameters);
                return result;
            }
        }

        /// <summary>
        /// 通報表(F1a-2)-依鄉鎮別
        /// 省道
        /// </summary>
        /// <param name="eocId"></param>
        /// <param name="prjNo"></param>
        /// <returns></returns>
        public IEnumerable<BOARD_F1_1Dto> GetBoardF1a_2_3(string eocId, decimal prjNo)
        {
            string sqlStatement = @"
                                    SELECT　
	                                    (SELECT COUNT(*) BLOCK_COUNT	--'省道中斷' 
	                                    FROM ERA2_QRY_MAX_F1a (@eocId, @prjNo, 0)
	                                    WHERE TOWN_ID=6500100			--變數輸入規則=登入者EOC所在鄉鎮%token.EOC_DEP_CODE%
	                                    AND ROADTYPE_ID = '1'			--1=省道
	                                    AND TRFSTATUS = 'D0'
	                                    ) BLOCK_COUNT,
	                                    (SELECT COUNT(*) UNBLOCK_COUNT	--'省道搶通' 
	                                    FROM ERA2_QRY_MAX_F1a (@eocId, @prjNo, 0)
	                                    where TOWN_ID=6500100			--變數輸入規則=登入者EOC所在鄉鎮%token.EOC_DEP_CODE%
	                                    AND ROADTYPE_ID = '1'			--1=省道
	                                    AND TRFSTATUS = 'D1'
	                                    ) UNBLOCK_COUNT,
	                                    (SELECT COUNT(*) STOP_COUNT		--'省道預警封閉' 
	                                    FROM ERA2_QRY_MAX_F1a (@eocId, @prjNo, 0)
	                                    WHERE TOWN_ID=6500100			--變數輸入規則=登入者EOC所在鄉鎮%token.EOC_DEP_CODE%
	                                    AND ROADTYPE_ID = '1'			--1=省道
	                                    AND TRFSTATUS = 'P0'
	                                    ) STOP_COUNT
                                    ";

            using (var conn = new SqlConnection(DBHelper.GetEMIC2DBConnection()))
            {
                //設定參數
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@eocId", eocId);
                parameters.Add("@prjNo", prjNo);

                var result = conn.Query<BOARD_F1_1Dto>(sqlStatement, parameters);
                return result;
            }
        }

        /// <summary>
        /// 通報表(F1a-3)-依鄉鎮別
        /// 省道
        /// </summary>
        /// <param name="eocId"></param>
        /// <param name="prjNo"></param>
        /// <returns></returns>
        public IEnumerable<BOARD_F1_1Dto> GetBoardF1a_3_3(string eocId, decimal prjNo)
        {
            string sqlStatement = @"
                                    SELECT　
	                                    (SELECT COUNT(*) BLOCK_COUNT    --'自養縣道中斷' 
	                                    FROM ERA2_QRY_MAX_F1a (@eocId, @prjNo, 0)
	                                    WHERE TOWN_ID=6500100			--變數輸入規則=登入者EOC所在鄉鎮%token.EOC_DEP_CODE%
	                                    AND ROADTYPE_ID = '3'			--3=自養縣道
	                                    AND TRFSTATUS = 'D0'
	                                    ) BLOCK_COUNT,
	                                    (SELECT COUNT(*) UNBLOCK_COUNT	--'自養縣道搶通' 
	                                    FROM ERA2_QRY_MAX_F1a (@eocId, @prjNo, 0)
	                                    where TOWN_ID=6500100			--變數輸入規則=登入者EOC所在鄉鎮%token.EOC_DEP_CODE%
	                                    AND ROADTYPE_ID = '3'			--3=自養縣道
	                                    AND TRFSTATUS = 'D1'
	                                    ) UNBLOCK_COUNT, 
	                                    (SELECT COUNT(*) STOP_COUNT		--'自養縣道預警封閉' 
	                                    FROM ERA2_QRY_MAX_F1a (@eocId, @prjNo, 0)
	                                    WHERE TOWN_ID=6500100			--變數輸入規則=登入者EOC所在鄉鎮%token.EOC_DEP_CODE%
	                                    AND ROADTYPE_ID = '3'			--3=自養縣道
	                                    AND TRFSTATUS = 'P0'
	                                    ) STOP_COUNT
                                    ";

            using (var conn = new SqlConnection(DBHelper.GetEMIC2DBConnection()))
            {
                //設定參數
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@eocId", eocId);
                parameters.Add("@prjNo", prjNo);

                var result = conn.Query<BOARD_F1_1Dto>(sqlStatement, parameters);
                return result;
            }
        }

        /// <summary>
        /// 通報表(A3a)-依鄉鎮別
        /// TO DO
        /// </summary>
        /// <param name="eocId"></param>
        /// <param name="prjNo"></param>
        /// <returns></returns>
        public IEnumerable<DRFireDto> GetBoardA3a(string eocId, decimal prjNo)
        {
            string sqlStatement = @"
                                    ";

            using (var conn = new SqlConnection(DBHelper.GetEMIC2DBConnection()))
            {
                //設定參數
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@eocId", eocId);
                parameters.Add("@prjNo", prjNo);

                var result = conn.Query<DRFireDto>(sqlStatement, parameters);
                return result;
            }
        }

        /// <summary>
        /// 通報表(A1a)
        /// 縣市別
        /// </summary>
        /// <param name="eocId"></param>
        /// <param name="prjNo"></param>
        /// <returns></returns>
        public IEnumerable<BOARD_A1Dto> GetBoardA1a_2(string eocId, decimal prjNo)
        {
            string sqlStatement = @"
                                    SELECT  
	                                     SUM(DEAD) DEAD,			--'死亡人數',
	                                     SUM(MISSING) MISSING,		--'失蹤人數',
	                                     SUM(INJURED) INJURED		--'受傷人數'
                                    FROM ERA2_QRY_MAX_A1a (@eocId, @prjNo, 0)
                                    WHERE NO_DATA_MARK is NULL  --無資料可填報 NO_DATA_MARK = 1
                                   ";

            using (var conn = new SqlConnection(DBHelper.GetEMIC2DBConnection()))
            {
                //設定參數
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@eocId", eocId);
                parameters.Add("@prjNo", prjNo);

                var result = conn.Query<BOARD_A1Dto>(sqlStatement, parameters);
                return result;
            }
        }

        /// <summary>
        /// 通報表(A4a)
        /// 縣市別
        /// </summary>
        /// <param name="eocId"></param>
        /// <param name="prjNo"></param>
        /// <returns></returns>
        public IEnumerable<BOARD_A4Dto> GetBoardA4a_2(string eocId, decimal prjNo)
        {
            string sqlStatement = @"
                                    SELECT 
	                                    SUM(EVACUATE_TOTAL) EVACUATE_TOTAL		--'累計疏散撤離人數' 
                                    FROM ERA2_QRY_MAX_A4a (@eocId, @prjNo, 0)
                                    WHERE NO_DATA_MARK is NULL;  --無資料可填報 NO_DATA_MARK = 1
                                   ";

            using (var conn = new SqlConnection(DBHelper.GetEMIC2DBConnection()))
            {
                //設定參數
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@eocId", eocId);
                parameters.Add("@prjNo", prjNo);

                var result = conn.Query<BOARD_A4Dto>(sqlStatement, parameters);
                return result;
            }
        }

        /// <summary>
        /// 通報表(D3a)
        /// 縣市別
        /// </summary>
        /// <param name="eocId"></param>
        /// <param name="prjNo"></param>
        /// <returns></returns>
        public IEnumerable<BOARD_D3Dto> GetBoardD3a_2(string eocId, decimal prjNo)
        {
            string sqlStatement = @"
                                    SELECT  
	                                    SUM(REFUGEE_TOTAL) REFUGEE_TOTAL,		--'最高收容人數',
	                                    SUM(REFUGEE) REFUGEE					--'目前收容人數'
                                    FROM ERA2_QRY_MAX_D3a (@eocId, @prjNo, 0)
                                    WHERE NO_DATA_MARK is NULL;  --無資料可填報 NO_DATA_MARK = 1
                                   ";

            using (var conn = new SqlConnection(DBHelper.GetEMIC2DBConnection()))
            {
                //設定參數
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@eocId", eocId);
                parameters.Add("@prjNo", prjNo);

                var result = conn.Query<BOARD_D3Dto>(sqlStatement, parameters);
                return result;
            }
        }

        /// <summary>
        /// 通報表(E2)-依縣市別
        /// </summary>
        /// <param name="eocId"></param>
        /// <param name="prjNo"></param>
        /// <returns></returns>
        public IEnumerable<BOARD_E2Dto> GetBoardE2_2(string eocId, decimal prjNo)
        {
            string sqlStatement = @"
                                    SELECT  
	                                    SUM(ELECTRO_DMG_USED) ELECTRO_DMG_USED,					--'修護戶數',
	                                    SUM(ELECTRO_DMG_NOW) ELECTRO_DMG_NOW,					--'待修護戶數',
	                                    SUM(ELECTRO_DMG_USED+ELECTRO_DMG_NOW) POWER_FAILURE		--'停電戶數'
                                    FROM ERA2_QRY_MAX_E2 (default, @prjNo, 0)
                                    WHERE NO_DATA_MARK is NULL  --無資料可填報 NO_DATA_MARK = 1
                                    AND CITY_NAME = (SELECT DEP_NAME from EEM2_EOC_DATA WHERE EOC_ID = @eocId)
                                   ";

            using (var conn = new SqlConnection(DBHelper.GetEMIC2DBConnection()))
            {
                //設定參數
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@eocId", eocId);
                parameters.Add("@prjNo", prjNo);

                var result = conn.Query<BOARD_E2Dto>(sqlStatement, parameters);
                return result;
            }
        }

        /// <summary>
        /// 通報表(E4)-依縣市別
        /// </summary>
        /// <param name="eocId"></param>
        /// <param name="prjNo"></param>
        /// <returns></returns>
        public IEnumerable<BOARD_E4Dto> GetBoardE4_2(string eocId, decimal prjNo)
        {
            string sqlStatement = @"
                                    SELECT  
	                                    SUM(WATER_DMG) WATER_DMG,			-- '停水',
	                                    SUM(WATER_RECVY) WATER_RECVY,		-- '修護',
	                                    SUM(WATER_STOP) WATER_STOP			-- '待修護'
                                    FROM ERA2_QRY_MAX_E4 (default, @prjNo, 0)
                                    WHERE NO_DATA_MARK is NULL   --無資料可填報 NO_DATA_MARK = 1
                                    AND CITY_NAME = (SELECT DEP_NAME from EEM2_EOC_DATA WHERE EOC_ID = @eocId) 
                                   ";

            using (var conn = new SqlConnection(DBHelper.GetEMIC2DBConnection()))
            {
                //設定參數
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@eocId", eocId);
                parameters.Add("@prjNo", prjNo);

                var result = conn.Query<BOARD_E4Dto>(sqlStatement, parameters);
                return result;
            }
        }

        /// <summary>
        /// 通報表(J2)-依縣市別
        /// </summary>
        /// <param name="eocId"></param>
        /// <param name="prjNo"></param>
        /// <returns></returns>
        public IEnumerable<BOARD_J2Dto> GetBoardJ2_2(string eocId, decimal prjNo)
        {
            string sqlStatement = @"
                                    SELECT   
	                                    SUM(PHONE_DAMAGE_TOTAL) PHONE_DAMAGE_TOTAL,		-- '市話中斷戶數',
	                                    SUM(PHONE_FIXED) PHONE_FIXED,					-- '市話修復戶數',
	                                    SUM(PHONE_DAMAGE) PHONE_DAMAGE					-- '市話待修護戶數'
                                    FROM ERA2_QRY_MAX_J2 (default, @prjNo, 0)
                                    WHERE NO_DATA_MARK is NULL  --無資料可填報 NO_DATA_MARK = 1
                                    AND CITY_NAME = (SELECT DEP_NAME from EEM2_EOC_DATA WHERE EOC_ID = @eocId)
                                   ";

            using (var conn = new SqlConnection(DBHelper.GetEMIC2DBConnection()))
            {
                //設定參數
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@eocId", eocId);
                parameters.Add("@prjNo", prjNo);

                var result = conn.Query<BOARD_J2Dto>(sqlStatement, parameters);
                return result;
            }
        }

        /// <summary>
        /// 通報表(J3)-依縣市別
        /// </summary>
        /// <param name="eocId"></param>
        /// <param name="prjNo"></param>
        /// <returns></returns>
        public IEnumerable<BOARD_J3Dto> GetBoardJ3_2(string eocId, decimal prjNo)
        {
            string sqlStatement = @"
                                    SELECT  
	                                    SUM(SITE_DAMAGE_TOTAL) SITE_DAMAGE_TOTAL,										-- '受損座數',
	                                    SUM(SITE_FIXED) SITE_FIXED,														-- '修護座數',
	                                    SUM(SITE_FIXED_NOT+STATION_FIXED_NOT+OTHERS_FIXED_NOT) SITE_PENDING_REPAIR		-- '待修護座數'
                                    FROM ERA2_QRY_MAX_J3 (default, @prjNo, 0)
                                    WHERE NO_DATA_MARK is NULL  --無資料可填報 NO_DATA_MARK = 1
                                    AND CITY_NAME = (SELECT DEP_NAME from EEM2_EOC_DATA WHERE EOC_ID = @eocId)
                                   ";

            using (var conn = new SqlConnection(DBHelper.GetEMIC2DBConnection()))
            {
                //設定參數
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@eocId", eocId);
                parameters.Add("@prjNo", prjNo);

                var result = conn.Query<BOARD_J3Dto>(sqlStatement, parameters);
                return result;
            }
        }

        /// <summary>
        /// 通報表(F1a-1)
        /// </summary>
        /// <param name="eocId"></param>
        /// <param name="prjNo"></param>
        /// <returns></returns>
        /// <remarks>levelTyp e=>  中央:1 縣市:2 鄉鎮:3</remarks>
        public IEnumerable<BOARD_F1_1Dto> GetBoardF1a_1_2(string eocId, decimal prjNo)
        {
            string sqlStatement = @"
                                    SELECT  
	                                    SUM(BLOCK_COUNT) BLOCK_COUNT,			--'中斷數 '
	                                    SUM(UNBLOCK_COUNT) UNBLOCK_COUNT,		--'搶通數 ' 
	                                    SUM(STOP_COUNT) STOP_COUNT				--'預警封閉數' 
                                    FROM ERA2_QRY_MAX_F1a_S (@eocId, @prjNo, 0)
                                    WHERE ROADTYPE_ID = 0						--國道代碼=0
                                   ";

            using (var conn = new SqlConnection(DBHelper.GetEMIC2DBConnection()))
            {
                //設定參數
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@eocId", eocId);
                parameters.Add("@prjNo", prjNo);

                var result = conn.Query<BOARD_F1_1Dto>(sqlStatement, parameters);
                return result;
            }
        }

        /// <summary>
        /// 通報表(F1a-2)
        /// </summary>
        /// <param name="eocId"></param>
        /// <param name="prjNo"></param>
        /// <returns></returns>
        /// <remarks>levelTyp e=>  中央:1 縣市:2 鄉鎮:3</remarks>
        public IEnumerable<BOARD_F1_1Dto> GetBoardF1a_2_2(string eocId, decimal prjNo)
        {
            string sqlStatement = @"
                                    SELECT  
	                                    SUM(BLOCK_COUNT) BLOCK_COUNT,			--'中斷數 '
	                                    SUM(UNBLOCK_COUNT) UNBLOCK_COUNT,		--'搶通數 ' 
	                                    SUM(STOP_COUNT) STOP_COUNT				--'預警封閉數' 
                                    FROM ERA2_QRY_MAX_F1_S (@eocId, @prjNo, 0)
                                    WHERE ROADTYPE_ID = 1                       --省道代碼=1
                                   ";

            using (var conn = new SqlConnection(DBHelper.GetEMIC2DBConnection()))
            {
                //設定參數
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@eocId", eocId);
                parameters.Add("@prjNo", prjNo);

                var result = conn.Query<BOARD_F1_1Dto>(sqlStatement, parameters);
                return result;
            }
        }

        /// <summary>
        /// 通報表(F1a-3)
        /// </summary>
        /// <param name="eocId"></param>
        /// <param name="prjNo"></param>
        /// <returns></returns>
        /// <remarks>levelTyp e=>  中央:1 縣市:2 鄉鎮:3</remarks>
        public IEnumerable<BOARD_F1_1Dto> GetBoardF1a_3_2(string eocId, decimal prjNo)
        {
            string sqlStatement = @"
                                    SELECT  
	                                    SUM(BLOCK_COUNT) BLOCK_COUNT,			--'中斷數 ' 
	                                    SUM(UNBLOCK_COUNT) UNBLOCK_COUNT,		--'搶通數 '
	                                    SUM(STOP_COUNT) STOP_COUNT				--'預警封閉數' 
                                    FROM ERA2_QRY_MAX_F1_S (@eocId, @prjNo, 0)
                                    WHERE ROADTYPE_ID in (2,3)	                --縣道代碼=2,3
                                   ";

            using (var conn = new SqlConnection(DBHelper.GetEMIC2DBConnection()))
            {
                //設定參數
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@eocId", eocId);
                parameters.Add("@prjNo", prjNo);

                var result = conn.Query<BOARD_F1_1Dto>(sqlStatement, parameters);
                return result;
            }
        }

        /// <summary>
        /// 取得通報表(F2)-依縣市別 (缺)
        /// TO DO
        /// </summary>
        /// <param name="eocId"></param>
        /// <param name="prjNo"></param>
        /// <returns></returns>
        /// <remarks>levelTyp e=>  中央:1 縣市:2 鄉鎮:3</remarks>
        public IEnumerable<BOARD_F2Dto> GetBoardF2_2(string eocId, decimal prjNo)
        {
            //'00000'固定輸入值(因為中央才會填)
            string sqlStatement = @"
                                    SELECT  
                                        '台鐵停駛' 'ITEM',count(*) 'NUMBER_STOP'
                                    FROM ERA2_QRY_MAX_F21 ('00000', @prjNo, 0) 
                                    WHERE RAILTYPE='台鐵'  and CITY_NAME = (SELECT DEP_NAME from EEM2_EOC_DATA WHERE EOC_ID = @eocId)
                                    UNION
                                    SELECT  
                                        '高鐵停駛' 'ITEM',count(*) 'NUMBER_STOP'
                                    FROM ERA2_QRY_MAX_F21 ('00000', @prjNo, 0)
                                    WHERE RAILTYPE='高鐵' AND CITY_NAME = (SELECT DEP_NAME from EEM2_EOC_DATA WHERE EOC_ID = @eocId)
                                    ";

            using (var conn = new SqlConnection(DBHelper.GetEMIC2DBConnection()))
            {
                //設定參數
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@eocId", eocId);
                parameters.Add("@prjNo", prjNo);

                var result = conn.Query<BOARD_F2Dto>(sqlStatement, parameters);
                return result;
            }
        }

        /// <summary>
        /// 取得通報表(G1)-依縣市別
        /// </summary>
        /// <param name="eocId"></param>
        /// <param name="prjNo"></param>
        /// <returns></returns>
        /// <remarks>levelTyp e=>  中央:1 縣市:2 鄉鎮:3</remarks>
        public IEnumerable<BOARD_G1Dto> GetBoardG1_2(string eocId, decimal prjNo)
        {
            string sqlStatement = @"
                                    SELECT  
	                                    SUM(DAMAGE_AMOUNT) DAMAGE_AMOUNT				--'學校受損金額' 
                                    FROM ERA2_QRY_MAX_G1 (default, @prjNo, 0)
                                    WHERE NO_DATA_MARK is NULL  --無資料可填報 NO_DATA_MARK = 1
                                    AND CITY_NAME = (SELECT DEP_NAME from EEM2_EOC_DATA WHERE EOC_ID = @eocId)
                                   ";

            using (var conn = new SqlConnection(DBHelper.GetEMIC2DBConnection()))
            {
                //設定參數
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@eocId", eocId);
                parameters.Add("@prjNo", prjNo);

                var result = conn.Query<BOARD_G1Dto>(sqlStatement, parameters);
                return result;
            }
        }

        /// <summary>
        /// 取得通報表(A3a)
        /// 縣市別
        /// TO DO
        /// </summary>
        /// <param name="eocId"></param>
        /// <param name="prjNo"></param>
        /// <returns></returns>
        public IEnumerable<DRFireDto> GetBoardA3a_2(string eocId, decimal prjNo)
        {
            string sqlStatement = @"
                                   ";

            using (var conn = new SqlConnection(DBHelper.GetEMIC2DBConnection()))
            {
                //設定參數
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@eocId", eocId);
                parameters.Add("@prjNo", prjNo);

                var result = conn.Query<DRFireDto>(sqlStatement, parameters);
                return result;
            }
        }

        public int CheckORGCnt(decimal orgId)
        {
            string sqlStatement = @"
                                    SELECT COUNT(*) CNT 
                                    FROM ( 
		                                    SELECT ORG_ID FROM EEM2_WORK_ITEM_ORG WHERE ORG_ID = @orgId
		                                    UNION
		                                    SELECT ORG_ID FROM EEM2_BOARD_BASE WHERE ORG_ID = @orgId　
		                                    UNION
		                                    SELECT ORG_ID FROM EEM2_DIS_ADMIN_ORG WHERE ORG_ID = @orgId　
		                                    UNION
		                                    SELECT CASE_REPLY_UNIT as ORG_ID FROM DIM2_CASE_REPLY WHERE CASE_REPLY_UNIT = @orgId
	　                                    ) T
                                   ";

            using (var conn = new SqlConnection(DBHelper.GetEMIC2DBConnection()))
            {
                //設定參數
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@orgId", orgId.ToString(), System.Data.DbType.String);

                var result = conn.ExecuteScalar(sqlStatement, parameters);
                return Convert.ToInt32(result);
            }
        }

        public int Get_EEM2_QRY_WORK_NOT_REPLY_COUNT(string prjNo, string orgID)
        {
            int resultRoles = 0;

            SqlConnection con = new SqlConnection(DBHelper.GetEMIC2DBConnection());
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("EEM2_QRY_WORK_NOT_REPLY_COUNT", con);
                cmd.CommandType = CommandType.StoredProcedure;

                // add parameter for return value
                SqlParameter rv = new SqlParameter("@result", SqlDbType.Int);
                rv.Direction = ParameterDirection.ReturnValue;

                cmd.Parameters.Add(rv);

                // add input parameter
                cmd.Parameters.AddWithValue("@P_PRJ_NO", prjNo);
                cmd.Parameters.AddWithValue("@P_ORG_ID", orgID);

                cmd.ExecuteNonQuery();

                // return value is in the parameter @returnvalue
                Object roleCode = cmd.Parameters["@result"].Value;

                // If return value is not null then use value
                if (DBNull.Value != roleCode)
                    resultRoles = int.Parse(roleCode.ToString());
                else
                    resultRoles = 0;
            }
            catch (Exception ex)
            {
                resultRoles = -1;
            }
            finally
            {
                con.Close();
            }

            return resultRoles;
        }

        public IEnumerable<EEM2010501Dto> Get_EEM2_Import(string PRJ_NO, DateTime StartTime, DateTime EndTime, string EOC_ID)
        {
            string sqlStatement = @"
                                    SELECT 
                                            '情資研判會議' 'REC_NAME',
                                            WK_MEET_TIME 'REC_TIME',
                                            [NAME] 'REC_REASON', 
                                            WORK_ITEM 'REC_REMARK'
                                    FROM  EEM2_WORK_MAIN
                                    LEFT JOIN EEM2_WORK_ITEM ON EEM2_WORK_MAIN.WORK_CODE = EEM2_WORK_ITEM.WORK_CODE
                                    WHERE PRJ_NO = @PRJ_NO                      --%token.PRJ_NO%
                                            AND MEETING_TYPE = '3' 
                                            AND WK_MEET_TIME >= @StartTime      --%畫面上查詢起日 00:00:00% --傳入參數
                                            AND WK_MEET_TIME <= @EndTime        --%畫面上查詢訖日 23:59:59% --傳入參數
                                    UNION
                                    SELECT 
                                            '工作會報' 'REC_NAME',
                                            WK_MEET_TIME 'REC_TIME',  
                                            [NAME] 'REC_REASON', 
                                            WORK_ITEM 'REC_REMARK'
                                    FROM  EEM2_WORK_MAIN
                                    LEFT JOIN EEM2_WORK_ITEM ON EEM2_WORK_MAIN.WORK_CODE = EEM2_WORK_ITEM.WORK_CODE
                                    WHERE PRJ_NO = @PRJ_NO                      --%token.PRJ_NO%
                                            AND MEETING_TYPE = '1' 
                                            AND WK_MEET_TIME >= @StartTime      --%畫面上查詢起日 00:00:00% --傳入參數
                                            AND WK_MEET_TIME <= @EndTime        --%畫面上查詢訖日 23:59:59% --傳入參數
                                    UNION
                                    SELECT 
                                            '傳真通報' 'REC_NAME',
                                            TRANSFER_TIME 'REC_TIME',  
                                            BULLETIN_TITLE  'REC_REASON',  
                                            BULLETIN_TITLE  'REC_REMARK'
                                    FROM NTT2_BULLETIN_10
                                    WHERE PRJ_NO = @PRJ_NO                      --%token.PRJ_NO%            --傳入參數
                                            AND TRANSFER_TIME >= @StartTime     --%畫面上查詢起日 00:00:00% --傳入參數
                                            AND TRANSFER_TIME <= @EndTime       --%畫面上查詢訖日 23:59:59% --傳入參數
                                    UNION
                                    SELECT 
                                            '處置報告' 'REC_NAME',
                                            M.DISP_TIME 'REC_TIME', 
                                            '處置報告第'+ Ltrim(str(M.DISP_NO)) + '報' AS 'REC_REASON', 
                                            '處置報告第'+ Ltrim(str(M.DISP_NO)) + '報' AS 'REC_REMARK'
                                            FROM ERA2_DISP_MAIN M
                                    WHERE M.EOC_ID = @EOC_ID                    --變數來源=%token.EOC_ID%
                                            AND M.PRJ_NO = @PRJ_NO              --變數來源=%token.PRJ_NO%
                                            AND M.DISP_NO = (SELECT max(DISP_NO) 
                                    FROM ERA2_DISP_MAIN M 
                                    WHERE M.EOC_ID = @EOC_ID                    --變數來源=%token.EOC_ID%
                                            AND M.PRJ_NO = @PRJ_NO)             --變數來源=%token.PRJ_NO%
                                            AND M.DISP_TIME >= @StartTime       --%畫面上查詢起日 00:00:00% --傳入參數
                                            AND M.DISP_TIME <= @EndTime         --%畫面上查詢訖日 23:59:59% --傳入參數
                                    UNION
                                    SELECT 
                                            '颱風警報' 'REC_NAME',
                                            M.EFFECTIVE 'REC_TIME', 
                                            '颱風警報單 第' + D.WARNNO + '報' AS 'REC_REASON',  
                                            D.TCUR AS 'REC_REMARK'
                                    FROM DSP_TYPHOONINFO M, DSP_TYPHOONDESC D
                                    WHERE 1 = 1
                                            AND M.EFFECTIVE >= @StartTime           --%畫面上查詢起日 00:00:00% --傳入參數
                                            AND M.EFFECTIVE <= @EndTime             --%畫面上查詢訖日 23:59:59% --傳入參數
                                            AND M.TYPHOONINFO_ID = D.TYPHOONINFO_ID
                                    ORDER BY 2
                                   ";

            using (var conn = new SqlConnection(DBHelper.GetEMIC2DBConnection()))
            {
                //設定參數
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@PRJ_NO", PRJ_NO);
                parameters.Add("@StartTime", StartTime);
                parameters.Add("@EndTime", EndTime);
                parameters.Add("@EOC_ID", EOC_ID);
                var result = conn.Query<EEM2010501Dto>(sqlStatement, parameters);
                return result;
            }
        }
    }
}
