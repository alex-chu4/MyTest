
using EMIC2.Models.Dao.Dto.EDD2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMIC2.Models.Interface.EDD2
{
    public interface IEDD2Dao
    {
        /// <summary>
        /// 資源項目
        /// </summary>
        /// <returns>List<EEA2_EVACUATE></returns>
        RESOURCE_ITEM_ALLDto ERA2_RESOURCE_ITEM_ALL(ResourceItemModelDto data);

        /// <summary>
        /// 資源項目列表
        /// </summary>
        /// <returns>List<EEA2_EVACUATE></returns>
        List<RESOURCE_ITEM_ALLDto> ERA2_RESOURCE_ITEM_ALL_LIST(ResourceItemModelDto data);

        /// <summary>
        /// Dapper大量資料寫入
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql"></param>
        /// <param name="list"></param>
        void DapperToBulkInsert<T>(string sql, List<T> list);
    }
}
