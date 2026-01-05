
namespace EMIC2.Models.Dao.EDD2
{
    using EMIC2.Models.Helper;
    using EMIC2.Models.Interface.EDD;
    using EMIC2.Result;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Dynamic;
    using System.Linq;
    using System.Text;

    public class EDD2020101Dao : EDD2020Dao, IEDD2020101Dao
    {
        public IResult EDD2_020101_M(out List<List<dynamic>> data, string p_OID_ID)
        {
            List<string> inputParas = new List<string>() {
                "@P_OID_ID",
            };
            List<object> inputParaVals = new List<object>() {
                p_OID_ID
            };

            string query = ConcatSelectQuery("[dbo].[EDD2_020101_M]", inputParas, useEnd: true);

            IResult result = GetTableCollectionWithParameter(out data, query, inputParas, inputParaVals);
            return result;
        }
    }
}
