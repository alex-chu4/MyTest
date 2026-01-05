using EMIC2.Models.Dao.Dto.EDD2.EDD2020303;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMIC2.Models.Interface.EDD2.EDD2020303
{
    public interface IEDD2020303Dao
    {
        List<EDD2020303_Create_Result_Dto> EDD2_020303_RANDOM(string typeCode, int num, List<int> resourceIdList, List<int> UnitIdList);
    }
}
