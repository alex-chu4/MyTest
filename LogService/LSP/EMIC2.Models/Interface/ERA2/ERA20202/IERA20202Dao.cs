using System.Collections.Generic;

namespace EMIC2.Models.Interface.ERA
{
    public interface IERA20202Dao
    {
        /// <summary>
        /// 依條件查詢報表資料
        /// </summary>
        /// <param name="p_EOC_ID"></param>
        /// <param name="p_PRJ_NO"></param>
        /// <param name="p_ORG_ID"></param>
        /// <returns>資料集</returns>
        List<List<object>> ERA2_0202_M(string p_EOC_ID, long p_PRJ_NO, int p_ORG_ID);
    }
}
