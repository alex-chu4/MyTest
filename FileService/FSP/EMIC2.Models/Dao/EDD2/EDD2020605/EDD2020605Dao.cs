using Dapper;
using EMIC2.Models.Dao.Dto.EDD2.EDD2020605;
using EMIC2.Models.Helper;
using EMIC2.Models.Interface.EDD2.EDD2020605;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMIC2.Models.Dao.EDD2.EDD2020605
{
    public class EDD2020605Dao : IEDD2020605Dao
    {

        /// <summary>
        ///  得到 api 要的資料
        /// </summary>
        /// <returns>
        ///     得到api 要的資料
        /// </returns>
        public List<EDD2020605Dto> EDD2_GET_EMIC_RESOURCE()
        {
            List<EDD2020605Dto> result = new List<EDD2020605Dto>();
            using (var conn = new SqlConnection(DBHelper.GetEMIC2DBConnection()))
            {
                StringBuilder sql = new StringBuilder();

                DynamicParameters parameters = new DynamicParameters();

                sql.Append("SELECT  B.LOCATION_NAME, B.CITY_NAME, B.TOWN_NAME, B.LOCATION_ADDRESS, B.LOCATION_LATITUDE AS LAT, B.LOCATION_LONGITUDE AS LON, C.RESOURCE_NAME, A.CURRENT_QTY, B.UNIT_NAME, A.MODIFIED_TIME FROM EDD2_RESOURCE_STOCK_DATA A ");
                sql.Append(" JOIN ( SELECT A.UNIT_LOCATION_ID, B.UNIT_ID, B.UNIT_NAME, C.LOCATION_ID, C.LOCATION_NAME, C.CITY_NAME, C.TOWN_NAME, C.LOCATION_ADDRESS, C.LOCATION_LATITUDE, C.LOCATION_LONGITUDE FROM EDD2_UNIT_LOCATION A JOIN EDD2_UNIT_MASTER B ON A.UNIT_ID = B.UNIT_ID JOIN EDD2_LOCATION_MASTER C ON A.LOCATION_ID = C.LOCATION_ID ) B ON A.UNIT_LOCATION_ID = B.UNIT_LOCATION_ID ");
                sql.Append(" JOIN EDD2_RESOURCE_ITEM C ON A.RESOURCE_ID = C.RESOURCE_ID ");
                sql.Append(" ORDER BY B.LOCATION_ID, B.UNIT_ID, A.RESOURCE_ID");

                result = conn.Query<EDD2020605Dto>(sql.ToString(), parameters).ToList();

                return result;
            }
        }
    }
}
