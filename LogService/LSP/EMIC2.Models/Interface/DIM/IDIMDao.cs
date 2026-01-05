using EMIC2.Models.Dao.Dto.DIM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMIC2.Models.Interface.DIM
{
    public interface IDIMDao
    {
        /// <summary>
        /// 取得鄰近案件
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        List<DIMCaseDto> GetNearByCase(DIMCaseDto model);

        string GetCaseNo(string type);

        string GetRENEW_ID();
    }
}
