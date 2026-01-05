using EMIC2.Models.Dao.Dto.EEM2.EEM2990104;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMIC2.Models.Interface.EEM2.EEM2990104
{
    public interface IEEM2990104Dao
    {
        //IEnumerable<EEM2990104PartDto> EEM2_GET_PRJNO();
        //IEnumerable<EEM2990104Dto> EEM2_GET_EMIC_BOARD();
        List<IEnumerable<EEM2990104Dto>> EEM2_GET_EMIC_BOARD();
    }
}
