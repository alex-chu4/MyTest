using EMIC2.Models.Dao.Dto.FIS2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMIC2.Models.Interface.FIS2
{
    public interface IFIS2990104Dao
    {
        FIS2990104Dto Get_QryEvaReport(FIS2990104Dto model);

        FIS2990104Dto Get_QryShReport(FIS2990104Dto model);

        FIS2990104Dto Get_QryMaxPeopleMissing(List<FIS2990104Dto> model);
    }
}
