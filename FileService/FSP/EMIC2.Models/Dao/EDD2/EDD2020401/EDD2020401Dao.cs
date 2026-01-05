using Dapper;
using EMIC2.Models.Dao.Dto.EDD2.EDD2020401;
using EMIC2.Models.Helper;
using EMIC2.Models.Interface.EDD2.EDD2020401;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMIC2.Models.Dao.EDD2.EDD2020401
{
    public class EDD2020401Dao : IEDD2020401Dao
    {

        /// <summary>
        ///  得到 此 UnitID 機關 轄下的所有機關
        /// </summary>
        /// <param name="unitID">unitID</param>
        /// <returns>
        ///     轄下所有機關資料
        /// </returns>
        public List<EDD2_UNIT_MASTER> EDD2_QRY_UNIT_OWN(int unitID)
        {
            List<EDD2_UNIT_MASTER> result = new List<EDD2_UNIT_MASTER>();
            using (var conn = new SqlConnection(DBHelper.GetEMIC2DBConnection()))
            {
                StringBuilder sql = new StringBuilder();

                DynamicParameters parameters = new DynamicParameters();

                // 用 -1 查 是查到全部機關
                unitID = unitID <= 0 ? -1 : unitID;

                sql.Append("SELECT B.* FROM EDD2_QRY_UNIT_OWN( @UNIT_ID , 1) A JOIN EDD2_UNIT_MASTER B ON A.UNIT_ID = B.UNIT_ID ORDER BY UNIT_LEVEL;");

                parameters.Add("UNIT_ID", unitID);

                result = conn.Query<EDD2_UNIT_MASTER>(sql.ToString(), parameters).ToList();

                return result;
            }
        }


        /// <summary>
        ///  查出所有的當前物資
        /// </summary>
        /// <param name="UnitIdList">UnitIdList</param>
        /// <returns>
        ///     回傳 EDD2020401_Unit_Resource_Dto List 物件
        /// </returns>
        public List<EDD2020401_Unit_Resource_Dto> EDD2_UNIT_RESOURCE(List<int> UnitIdList)
        {
            List<EDD2020401_Unit_Resource_Dto> result = new List<EDD2020401_Unit_Resource_Dto>();
            using (var conn = new SqlConnection(DBHelper.GetEMIC2DBConnection()))
            {
                StringBuilder sql = new StringBuilder();

                DynamicParameters parameters = new DynamicParameters();

                sql.Append("SELECT * FROM EDD2_020201_M (-1, '', -1, -1)");

                if (UnitIdList.Count() > 0)
                {
                    // 如果是 沒輸入 unitID => 找全部的資料；反之 依給的情況 查
                    sql.Append(" WHERE UNIT_ID IN ( ");
                    for (var j = 0; j < UnitIdList.Count(); j++)
                    {
                        string pString = j == 0 ? $" @UID_{j} " : $" , @UID_{j} ";
                        sql.Append(pString);
                        parameters.Add($"UID_{j}", UnitIdList[j]);
                    }
                    sql.Append(" ) ");
                }

                result = conn.Query<EDD2020401_Unit_Resource_Dto>(sql.ToString(), parameters).ToList();

                return result;
            }
        }
    }
}
