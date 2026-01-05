using EMIC2.Result;
using System.Collections.Generic;
using System.Data;

namespace EMIC2.Models.Interface.EDD
{
    public interface IEDD2020101Dao
    {
        IResult EDD2_020101_M(out List<List<dynamic>> data, string p_OID_ID);
    }
}
