using EMIC2.Models.Dao.Dto.ERA.ERA2030123;
using EMIC2.Result;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMIC2.Models.Interface.ERA2.ERA2030123
{
    public interface IERA2030123Dao
    {
        /// <summary>
        /// 匯入外部資料，呼叫 Stored Procedure 回傳成功或失敗
        /// </summary>
        /// <returns>int 1 成功 0 失敗</returns>
        IResult ImportJsonToRpt(ERA2030123SearchModelDto data, DataTable P_DATA_TBL);
    }
}
