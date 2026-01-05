using EMIC2.Models.Dao.Dto.EDD2.EDD2020604;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMIC2.Models.Interface.EDD2.EDD2020604
{
    public interface IEDD2020604Dao
    {
        /// <summary>
        /// 救災資源查詢
        /// </summary>
        /// <returns>List<EDD2020602Dto></returns>
        List<EDD2020604Dto> EDD2_020604_M(EDD2020604SearchModelDto model);
    }
}
