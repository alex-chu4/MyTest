using EMIC2.Models.Dao.Dto.ERA;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMIC2.Models.Interface.ERA2.ERA
{
    public interface IERA20402Dao
    {
        /// <summary>
        /// 下層應變中心最新填報狀況，呼叫 Stored Procedure 回傳資料
        /// </summary>
        /// <returns> IEnumerable<ERA20402Dto></returns>
        IEnumerable<ERA20402Dto> GetData(ERA20402Dto data);
    }
}
