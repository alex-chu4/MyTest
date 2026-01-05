//  程式檔名：
//  EDD2020303.cs
//  程式名稱：
//  EDD2020303Dao
///////////////////////////////////////////////////////////////////////////////////////
//  開發人員            日期             版本        備註
//  Hong                2019-10-21       1.0.0.0     初始版本
///////////////////////////////////////////////////////////////////////////////////////
//  Copyright (C) NEC Taiwan Ltd. 2018-2019.
//  程式描述：
//
///////////////////////////////////////////////////////////////////////////////////////
using Dapper;
using EMIC2.Models.Dao.Dto.EDD2.EDD2020303;
using EMIC2.Models.Helper;
using EMIC2.Models.Interface.EDD2.EDD2020303;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMIC2.Models.Dao.EDD2.EDD2020303
{
    public class EDD2020303Dao : IEDD2020303Dao
    {
        /// <summary>
        /// 稽催填報進度查詢
        /// </summary>
        /// <param name="typeCode">typecode</param>
        /// <param name="num">num</param>
        /// <param name="resourceIdList">resourceIdList</param>
        /// <param name="UnitIdList">UnitIdList</param>
        /// <returns>List<EDD2_020302_M></returns>
        public List<EDD2020303_Create_Result_Dto> EDD2_020303_RANDOM(string typeCode, int num, List<int> resourceIdList, List<int> UnitIdList)
        {
            List<EDD2020303_Create_Result_Dto> result = new List<EDD2020303_Create_Result_Dto>();
            using (var conn = new SqlConnection(DBHelper.GetEMIC2DBConnection()))
            {
                StringBuilder sql = new StringBuilder();

                DynamicParameters parameters = new DynamicParameters();

                // 先來建立 Resource 的 Tmp Table
                sql.Append("DECLARE @ResourceTABLE EDD2_RESOURCE_ID_TYPE;");

                for (var i = 0; i < resourceIdList.Count(); i++)
                {
                    sql.Append($"INSERT INTO @ResourceTABLE(RESOURCE_ID) VALUES(@RID_{i});");
                    parameters.Add($"RID_{i}", resourceIdList[i]);
                }


                sql.Append("SELECT * FROM EDD2_020303_RANDOM(@P_SPOT_CHECK_KIND, @P_SPOT_CHECK_NUM, @ResourceTABLE) ");

                
                parameters.Add("P_SPOT_CHECK_KIND", typeCode);
                parameters.Add("P_SPOT_CHECK_NUM", num);

                if (UnitIdList.Count() > 0)
                {
                    // 如果是 沒輸入 unitID => 找全部的資料；反之 依給的情況 查
                    sql.Append(" WHERE UNIT_ID in ( ");
                    for (var j = 0; j < UnitIdList.Count(); j++)
                    {
                        string pString = j == 0 ? $" @UID_{j} " : $" , @UID_{j} ";
                        sql.Append(pString);
                        parameters.Add($"UID_{j}", UnitIdList[j]);
                    }
                    sql.Append(" ) ");
                }


                sql.Append(" ORDER BY UNIT_ID ");

                result = conn.Query<EDD2020303_Create_Result_Dto>(sql.ToString(), parameters).ToList();

                return result;
            }
        }
    }
}
