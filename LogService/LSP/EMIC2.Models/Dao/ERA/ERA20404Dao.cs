using Dapper;
using EMIC2.Models.Dao.Dto.ERA.ERA20404;
using EMIC2.Models.Helper;
using EMIC2.Models.Interface.ERA2.ERA20404;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMIC2.Models.Dao.ERA
{
    public class ERA20404Dao : IERA20404Dao
    {
        /// <summary>
        /// 各部會處置報告最新填報狀況查詢頁面
        /// </summary>
        /// <returns> IEnumerable<ERA20401Dto></returns>
        public IEnumerable<ERA20404Dto> ERA2_0404_M(ERA20404Dto data)
        {
            List<ERA20404Dto> result = new List<ERA20404Dto>();
            using (var conn = new SqlConnection(DBHelper.GetEMIC2DBConnection()))
            {
                string sql = string.Empty;
                // 全部
                if (data.APC_ID == "-1" && data.CITY_ID == null && data.TOWN_ID == null)
                {
                    sql = "select * from ERA2_0404_M (@EOC_ID, @PRJ_NO, -1, -1, -1)";
                }
                // 山地全部
                else if (data.APC_ID == "1" && data.CITY_ID == null && data.TOWN_ID == null)
                {
                    sql = "select * from ERA2_0404_M (@EOC_ID, @PRJ_NO, 1, -1, -1)";
                }
                // 山地縣市
                else if (data.APC_ID == "1" && data.CITY_ID != null && data.TOWN_ID == null)
                {
                    sql = "select * from ERA2_0404_M (@EOC_ID, @PRJ_NO, 1, @CITY_ID, -1)";
                }
                // 平地全部
                else if (data.APC_ID == "2" && data.CITY_ID == null && data.TOWN_ID == null)
                {
                    sql = "select * from ERA2_0404_M (@EOC_ID, @PRJ_NO, 2, -1, -1)";
                }
                // 平地縣市
                else if (data.APC_ID == "2" && data.CITY_ID != null && data.TOWN_ID == null)
                {
                    sql = "select * from ERA2_0404_M (@EOC_ID, @PRJ_NO, 2, @CITY_ID, -1)";
                }
                // 縣市、鄉鎮
                else
                {
                    sql = "select * from ERA2_0404_M (@EOC_ID, @PRJ_NO, @APC_ID, @CITY_ID, @TOWN_ID)";
                }


                var parameters = new
                {
                    EOC_ID = "00000",
                    PRJ_NO = data.PRJ_NO,
                    APC_ID = data.APC_ID,
                    CITY_ID = data.CITY_ID,
                    TOWN_ID = data.TOWN_ID,
                };

                var query = conn.Query<ERA20404Dto>(sql, parameters);

                foreach(var item in query)
                {
                    item.AREA = item.CITY_NAME + " " + item.TOWN_NAME;
                }

                result = query.ToList();

                return result;
            }
        }
    }
}
