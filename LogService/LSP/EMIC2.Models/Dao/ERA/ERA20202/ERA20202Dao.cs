using EMIC2.Models.Interface.ERA;
using EMIC2.Result;
using System.Collections.Generic;

namespace EMIC2.Models.Dao.ERA
{
    public class ERA20202Dao : ERA203Dao, IERA20202Dao
    {
        /// <summary>
        /// 依條件查詢報表資料
        /// </summary>
        /// <param name="p_EOC_ID"></param>
        /// <param name="p_PRJ_NO"></param>
        /// <param name="p_ORG_ID"></param>
        /// <returns>資料集</returns>
        public List<List<object>> ERA2_0202_M(string p_EOC_ID, long p_PRJ_NO, int p_ORG_ID)
        {
            List<string> inputParas = new List<string>()
            {
                "@P_EOC_ID",
                "@P_PRJ_NO",
                "@P_ORG_ID"
            };

            List<object> inputParaValues = new List<object>()
            {
                p_EOC_ID,
                p_PRJ_NO,
                p_ORG_ID
            };

            string query = ConcatSelectQuery("[dbo].[ERA2_0202_M]", inputParas, useEnd: true);

            IResult result = GetTableDataWithParameter(out List<List<object>> tbData, query, inputParas, inputParaValues);

            return tbData;
        }
    }
}
