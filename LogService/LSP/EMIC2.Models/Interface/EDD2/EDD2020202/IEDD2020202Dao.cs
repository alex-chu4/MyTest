using EMIC2.Models.Dao.Dto.EDD2.EDD2020202;
using EMIC2.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMIC2.Models.Interface.EDD2.EDD2020202
{
    public interface IEDD2020202Dao
    {
        /// <summary>
        /// 稽催填報查詢
        /// </summary>
        /// <returns>List<EDD2020602Dto></returns>
        List<EDD2020202Dto> EDD2_020202_M(EDD2020202SearchModelDto model);

        /// <summary>
        /// 匯入外部資料，呼叫 Stored Procedure 回傳成功或失敗
        /// </summary>
        /// <returns>int 1 成功 0 失敗</returns>
        IResult EDD2_UPDATE_STOCK(EDD2020202SearchModelDto model);
    }
}
