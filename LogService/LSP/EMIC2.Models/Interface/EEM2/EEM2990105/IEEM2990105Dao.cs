using EMIC2.Models.Dao.Dto.EEM2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMIC2.Models.Interface.EEM2.EEM2990105
{
    public interface IEEM2990105Dao
    {
        IEnumerable<EEM2990105Dto> EEM2_GET_EMIC_RESOURCE();
    }
}
