//////////////////////////////////////////////////////////////////////////////////////
//  程式檔名：
//  ERA2020150Dao.cs
//  程式名稱：
//  F1
///////////////////////////////////////////////////////////////////////////////////////
//  開發人員            日期             版本        備註
//  Joe             2019-09-03       1.0.0.0     初始版本
///////////////////////////////////////////////////////////////////////////////////////
//  Copyright (C) NEC Taiwan Ltd. 2018-2019.
//  程式描述：
//  TABLE FUNC
///////////////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using Dapper;
using EMIC2.Models.Dao.Dto;
using EMIC2.Models.Helper;
using EMIC2.Models.Interface.ERA2;
using EMIC2.Result;

namespace EMIC2.Models.Dao.ERA
{
    public class ERA2020150Dao : IERA2020150Dao
    {
                /// <summary>
        /// F1a_S的更新SP
        /// 呼叫SP：ERA2_UPD_F1F1a_S
        /// (1)第一次產生主檔ERA_RPT_MAIN後，得到RPT_MAIN_ID，呼叫SP
        /// (2)異動明細(CRUD、狀態改變EX:搶通->阻斷)
        /// (3)無資料可填報
        /// (4)匯入第N報
        /// (5)刪除此區塊內容
        /// (6)主管機關 確定、取消確認(該處理時P_ORG_ID、P_ORG_CHECK需都給值，其它處理呼叫時這二參數都給NULL)
        /// </summary>
        /// <returns>IResult</returns>
        public IResult ERA2_UPD_F1F1a_S(ERA2Dto data)
        {
            IResult result = new Result.Result(false);
            using (var conn = new SqlConnection(DBHelper.GetEMIC2DBConnection()))
            {

                //設定參數
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@P_RPT_CODE", data.RPT_CODE, dbType: DbType.String, direction: ParameterDirection.Input);
                parameters.Add("@P_RPT_MAIN_ID", data.RPT_MAIN_ID, dbType: DbType.Int32, direction: ParameterDirection.Input);
                parameters.Add("@P_PRJ_NO", data.PRJ_NO, dbType: DbType.Int64, direction: ParameterDirection.Input);
                parameters.Add("@P_EOC_ID", data.EOC_ID, dbType: DbType.String, direction: ParameterDirection.Input);

                parameters.Add("@P_ORG_ID", null, dbType: DbType.Int64, direction: ParameterDirection.Input);
                parameters.Add("@P_ORG_CHECK", null, dbType: DbType.String, direction: ParameterDirection.Input);

                parameters.Add("@O_IsSuccessful", dbType: DbType.Int32, direction: ParameterDirection.Output);
                parameters.Add("@O_Msg", dbType: DbType.String, size: 4000, direction: ParameterDirection.Output);
                conn.Execute("[dbo].[ERA2_UPD_F1F1a_S]", parameters, commandType: CommandType.StoredProcedure);

                //接回Output值      
                int outputResult = parameters.Get<int>("O_IsSuccessful");
                //outputResult = 2;
                if (outputResult == 1)
                {
                    result.Success = true;
                }
                else
                {
                    result.Success = false;
                }

                //接回Return值
                result.ReturnValue = outputResult.ToString();
                result.Message = parameters.Get<string>("O_Msg");

                return result;
            }
        }

        /// <summary>
        /// 查詢災情
        /// </summary>
        /// <returns>IResult</returns>
        public List<F1DaoResult> ERA2_QRY_DIM2_IS_ROADBLOCK(ERA2Dto data)
        {
            ERA2Dto era2dto = new ERA2Dto();
            era2dto.f1DaoResult = new List<F1DaoResult>();
            using (var conn = new SqlConnection(DBHelper.GetEMIC2DBConnection()))
            {
                try
                {
                    string sql =
                   @"select 
                        CASE_NO, 
                        CASE_TIME, 
                        (CITY_NAME + TOWN_NAME) as CITY_AREA, 
                        CASE_LOCATION,
                        CASE_WGS84_PTS, 
                        CASE_DESC 
                        from ERA2_QRY_DIM2_IS_ROADBLOCK (@P_EOC_ID, @P_PRJ_NO, @P_TIME_S, @P_TIME_E) order by CASE_TIME desc";

                    var parameters = new
                    {
                        P_EOC_ID = data.EOC_ID,
                        P_PRJ_NO = data.PRJ_NO,
                        P_TIME_S = 0,
                        P_TIME_E = 0

                    };

                    era2dto.f1DaoResult = conn.Query<F1DaoResult>(sql, parameters).ToList();

                    foreach (var item in era2dto.f1DaoResult)
                    {
                        item.CASE_TIME_TEXT = item.CASE_TIME?.ToString("yyyy-MM-dd HH:mm");
                    }

                }
                catch (Exception e)
                {
                    var error = e;
                    throw;
                }
                return era2dto.f1DaoResult;
            }
        }

        /// <summary>
        /// 災情匯入
        /// </summary>
        /// <returns>IResult</returns>
        public IResult ERA2_IMP_DIM2_TO_F1F1a(ERA2Dto data)
        {
            IResult result = new Result.Result(false);
            using (var conn = new SqlConnection(DBHelper.GetEMIC2DBConnection()))
            {
                //設定參數
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@P_RPT_MAIN_ID", data.RPT_MAIN_ID, dbType: DbType.Int32, direction: ParameterDirection.Input);
                parameters.Add("@P_RPT_CODE", data.RPT_CODE, dbType: DbType.String, direction: ParameterDirection.Input);
                parameters.Add("@P_EOC_ID", data.EOC_ID, dbType: DbType.String, direction: ParameterDirection.Input);
                parameters.Add("@P_PRJ_NO", data.PRJ_NO, dbType: DbType.Int64, direction: ParameterDirection.Input);

                parameters.Add("@O_IsSuccessful", dbType: DbType.Int32, direction: ParameterDirection.Output);
                parameters.Add("@O_Msg", dbType: DbType.String, size: 4000, direction: ParameterDirection.Output);
                conn.Execute("[dbo].[ERA2_IMP_DIM2_TO_F1F1a]", parameters, commandType: CommandType.StoredProcedure);

                //接回Output值      
                int outputResult = parameters.Get<int>("O_IsSuccessful");
                //outputResult = 2;
                if (outputResult == 1)
                {
                    result.Success = true;
                }
                else
                {
                    result.Success = false;
                }

                //接回Return值
                result.ReturnValue = outputResult.ToString();
                result.Message = parameters.Get<string>("O_Msg");

                return result;
            }
        }

        /// <summary>
        /// 查詢公路總局
        /// </summary>
        /// <returns>(List<HightWayDaoResult>, int MTMP_REC_ID)</returns>
        public (List<HightWayDaoResult>, int MTMP_REC_ID) ERA2_MTMP_REC(ERA2Dto data)
        {
            ERA2Dto era2dto = new ERA2Dto();
            era2dto.highwayDaoResult = new List<HightWayDaoResult>();
            int recId = 0;
            using (var conn = new SqlConnection(DBHelper.GetEMIC2DBConnection()))
            {
                try
                {
                    // 先查詢目前外部匯入的最新資料
                    string sql = @"select MTMP_REC_ID, SENDER_TIME
                                   from ERA2_MTMP_REC 
                                   where EOC_ID = @EOC_ID 
                                   and PRJ_NO = @PRJ_NO 
                                   and RPT_CODE = @RPT_CODE";
                    var parameters = new { EOC_ID = data.EOC_ID, PRJ_NO = data.PRJ_NO, RPT_CODE = "F1" };
                    var rec = conn.Query<HightWayDaoResult>(sql, parameters);

                    var mtmpRecId = rec.Select(e => e.MTMP_REC_ID).FirstOrDefault();
                    recId = mtmpRecId;

                    // 查詢明細
                    string sql2 = @"select 
                                    b.CODE_NAME as ROADTYPE_ID, 
                                    TRFSTATUS,
                                    (CITY_NAME+TOWN_NAME) CITY_AREA, 
                                    LINECODE, 
                                    ADDNAME, 
                                    CLOSE_DATETIME 
                                    from ERA2_MTMP_F1 as a
                                    join ERA2_CODETABLE as b on b.CODE_VALUE = a.ROADTYPE_ID
                                    where MTMP_REC_ID = @MTMP_REC_ID  
                                    and b.CODE_USEEN ='ROADTYPE' 
                                    and TRFSTATUS = @TRFSTATUS";
                    var parameters2 = new { MTMP_REC_ID = mtmpRecId, TRFSTATUS = data.TRFSTATUS };
                    era2dto.highwayDaoResult = conn.Query<HightWayDaoResult>(sql2, parameters2).ToList();

                }
                catch (Exception e)
                {
                    Debug.WriteLine(e);
                    throw;
                }

                return (era2dto.highwayDaoResult, recId);
            }
        }

        /// <summary>
        /// 公路總局匯入
        /// </summary>
        /// <returns>IResult</returns>
        public IResult ERA2_IMP_DGOH_TO_RPT(ERA2Dto data)
        {
            IResult result = new Result.Result(false);
            using (var conn = new SqlConnection(DBHelper.GetEMIC2DBConnection()))
            {
                //設定參數
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@P_MTMP_REC_ID", data.MTMP_REC_ID, dbType: DbType.Int32, direction: ParameterDirection.Input);
                parameters.Add("@P_RPT_MAIN_ID", data.RPT_MAIN_ID, dbType: DbType.Int32, direction: ParameterDirection.Input);
                parameters.Add("@P_RPT_CODE", data.RPT_CODE, dbType: DbType.String, direction: ParameterDirection.Input);
                parameters.Add("@P_TRFSTATUS", data.TRFSTATUS, dbType: DbType.String, direction: ParameterDirection.Input);

                parameters.Add("@O_IsSuccessful", dbType: DbType.Int32, direction: ParameterDirection.Output);
                parameters.Add("@O_Msg", dbType: DbType.String, size: 4000, direction: ParameterDirection.Output);
                conn.Execute("[dbo].[ERA2_IMP_DGOH_TO_RPT]", parameters, commandType: CommandType.StoredProcedure);

                //接回Output值      
                int outputResult = parameters.Get<int>("O_IsSuccessful");
                //outputResult = 2;
                if (outputResult == 1)
                {
                    result.Success = true;
                }
                else
                {
                    result.Success = false;
                }

                //接回Return值
                result.ReturnValue = outputResult.ToString();
                result.Message = parameters.Get<string>("O_Msg");

                return result;
            }
        }
    }
}
