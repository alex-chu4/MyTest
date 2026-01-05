using Dapper;
using EMIC2.Models.Dao.Dto.EEM2.EEM2990104;
using EMIC2.Models.Helper;
using EMIC2.Models.Interface.EEM2.EEM2990104;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMIC2.Models.Dao.EEM2
{
    public class EEM2990104Dao : IEEM2990104Dao
    {
        IEnumerable<EEM2990104PartDto> a;

        /// <summary>
        ///  得到 api 要的資料
        /// </summary>
        /// <returns>
        ///     得到api 要的資料
        /// </returns>
        public List<IEnumerable<EEM2990104Dto>> EEM2_GET_EMIC_BOARD()
        {
            //IEnumerable<EEM2990104Dto> a = null;
            List<IEnumerable<EEM2990104Dto>> result = new List<IEnumerable<EEM2990104Dto>>();
            using (var conn = new SqlConnection(DBHelper.GetEMIC2DBConnection()))
            {
                StringBuilder sqlPart1 = new StringBuilder();
                sqlPart1.Append("select PRJ_NO, CASE_NAME ");
                sqlPart1.Append("from EEM2_EOC_PRJ ");
                sqlPart1.Append("where EOC_ID='00000' and PRJ_ETIME is null");

                a = conn.Query<EEM2990104PartDto>(sqlPart1.ToString());

                foreach (var table in a)
                {
                    StringBuilder sqlPart2 = new StringBuilder();

                    sqlPart2.Append("select P.PRJ_GROUP_UID as caseCode,P.CASE_NAME as caseName ,B.SET_DATE as doshBoardSetTime, D.[CONTENT] ,'中央災害應變中心' as eocName ,D.ITEM_NAME as itemName ,D.SET_TIME as setTime ");
                    sqlPart2.Append("from EEM2_EOC_PRJ P ");
                    sqlPart2.Append("left join EEM2_BOARD_MAIN B on B.EOC_ID=P.EOC_ID and B.PRJ_NO=P.PRJ_NO ");
                    sqlPart2.Append("left join EEM2_BOARD_DETAIL D on B.M_UID = D.M_UID ");
                    sqlPart2.Append("where P.EOC_ID = '00000' and P.PRJ_ETIME is null ");
                    //sqlPart2.Append("and P.PRJ_NO= '"+a+"'");
                    sqlPart2.Append("and P.PRJ_NO='" + table.PRJ_NO + "'");
                    //sqlpart2.("@P.PRJ_NO", table.PRJ_NO)
                    result.Add(conn.Query<EEM2990104Dto>(sqlPart2.ToString()));
                }
                return result;
            }
        }
    }
}
