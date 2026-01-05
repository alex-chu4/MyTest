using EMIC2.Models.Dao.Dto.EDD2.EDD2020302;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMIC2.Models.Interface.EDD2.EDD2020302
{
    public interface IEDD2020302Dao
    {
        /// <summary>
        /// 稽催填報進度查詢
        /// </summary>
        /// <returns>List<EDD2_020302_M></returns>
        List<EDD2_020302_MDto> EDD2_020302_M(EDD2_020302_M_SearchModelDto data);

        /// <summary>
        /// 稽催填報進度查詢
        /// </summary>
        /// <returns>List<EDD2_020302_M></returns>
        List<EDD2_020302_DDto> EDD2_020302_M_Detail(EDD2_020302_M_SearchModelDto data);

        /// <summary>
        /// 稽催填報進度，稽催作業下拉選單
        /// </summary>
        /// <returns>List<EDD2_020302_M></returns>
        List<EDD2_AUDITING_OPERATION> EDD2_AUDITING_OPERATION(EDD2_020302_M_SearchModelDto data);
    }
}
