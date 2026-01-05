using EMIC2.Result;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMIC2.Models.Interface.ERA2.ERA2020117
{
    public interface IERA2020117Dao
    {
        /// <summary>
        /// 匯入外部資料，呼叫 Stored Procedure 回傳成功或失敗
        /// </summary>
        /// <returns>int 1 成功 0 失敗</returns>
        IResult ImportXmlToRpt(string P_RPT_MAIN_ID, DataTable P_DATA_TBL);
    }
}
