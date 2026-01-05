using EMIC2.Models.Dao.Dto.EDD2.EDD2020401;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMIC2.Models.Interface.EDD2.EDD2020401
{
    public interface IEDD2020401Dao
    {
        List<EDD2_UNIT_MASTER> EDD2_QRY_UNIT_OWN(int unitID);

        List<EDD2020401_Unit_Resource_Dto> EDD2_UNIT_RESOURCE(List<int> UnitIdList);
    }
}
