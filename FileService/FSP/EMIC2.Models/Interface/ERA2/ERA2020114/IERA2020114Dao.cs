using EMIC2.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMIC2.Models.Interface.ERA2.ERA2020114
{
    public interface IERA2020114Dao
    {
        /// <summary>
        /// 匯入外部資料，呼叫 Stored Procedure 回傳成功或失敗
        /// </summary>
        /// <returns>int 1 成功 0 失敗</returns>
        IResult ImportMoeaboeToRpt(string P_MTMP_REC_ID, string P_RPT_MAIN_ID, string P_RPT_CODE);
    }
}
