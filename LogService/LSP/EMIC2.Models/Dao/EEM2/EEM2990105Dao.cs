using Dapper;
using EMIC2.Models.Dao.Dto.EEM2;
using EMIC2.Models.Helper;
using EMIC2.Models.Interface.EEM2.EEM2990105;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMIC2.Models.Dao.EEM2
{
    public class EEM2990105Dao : IEEM2990105Dao
    {
        /// <summary>
        ///  得到 api 要的資料
        /// </summary>
        /// <returns>
        ///     得到api 要的資料
        /// </returns>
        public IEnumerable<EEM2990105Dto> EEM2_GET_EMIC_RESOURCE()
        {
            IEnumerable<EEM2990105Dto> result = null;
            using (var conn = new SqlConnection(DBHelper.GetEMIC2DBConnection()))
            {
                StringBuilder sql = new StringBuilder();

                sql.Append("select PRJ_GROUP_UID as caseCode, CASE_NAME as caseName, PRJ_STIME as caseStartTime, EEM2_DIS_DATA.DIS_NAME as disName, EEM2_EOC_DATA.EOC_NAME as eocName, OPEN_LV as openTier from EEM2_EOC_PRJ ");
                sql.Append("left join EEM2_EOC_DATA ON EEM2_EOC_DATA.EOC_ID=EEM2_EOC_PRJ.EOC_ID ");
                sql.Append("left join EEM2_DIS_DATA on EEM2_DIS_DATA.DIS_DATA_UID=EEM2_EOC_PRJ.DIS_DATA_UID ");
                sql.Append("where EEM2_EOC_PRJ.EOC_ID='00000' AND PRJ_ETIME IS NULL ");
                sql.Append("order by PRJ_STIME");

                result = conn.Query<EEM2990105Dto>(sql.ToString());

                return result;
            }
        }
    }
}
