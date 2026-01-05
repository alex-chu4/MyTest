using EMIC2.Models.Dao.Dto.EEM2.EEM2010303;
using EMIC2.Models.Dao.EEM2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMIC2.Models.Interface.EEM2.EEM2010303
{
    public interface IEEM2010303Dao
    {
        List<string> GET_PRJ_GROUP_UID();

        List<EEM2010303Dto> EEM2010303_Result(List<string> eocID, decimal EOC_GROUP_UID = 0);
    }
}
