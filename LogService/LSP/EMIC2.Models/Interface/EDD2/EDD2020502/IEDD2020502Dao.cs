using EMIC2.Result;
using System.Collections.Generic;
using System.Data;

namespace EMIC2.Models.Interface.EDD
{
    public interface IEDD2020502Dao
    {
        IResult UpdateUnitLocation(List<string> insertDetailParas, List<object> insertDetailVals, List<string> insertMasterParas = null, List<object> insertMasterVals = null);

        IResult SearchUnitLocation(
            out List<List<dynamic>> data, string p_QRY_TYPE, int p_UNIT_ID, int? p_RESOURCE_ID,
            string w_CITY_NAME = "", string w_TOWN_NAME = "", string w_LOCATION_NAME = "", string w_CONTACT_NAME = "");

    }
}
