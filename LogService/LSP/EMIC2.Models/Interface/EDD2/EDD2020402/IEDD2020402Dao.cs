using EMIC2.Models.Dao.Dto.EDD2.EDD2020402;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMIC2.Models.Interface.EDD2.EDD2020402
{
    public interface IEDD2020402Dao
    {
        /// <summary>
        /// 稽催填報進度查詢
        /// </summary>
        /// <returns>List<EDD2_020402_MDto></returns>
        List<EDD2_020402_MDto> EDD2_020402_M(EDD2020402SearchModelDto data);
    }
}
