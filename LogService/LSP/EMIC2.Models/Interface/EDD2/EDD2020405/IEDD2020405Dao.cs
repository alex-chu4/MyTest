using EMIC2.Models.Dao.Dto.EDD2.EDD2020405;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMIC2.Models.Interface.EDD2.EDD2020405
{
    public interface IEDD2020405Dao
    {
        /// <summary>
        /// 單位填報紀錄列表
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        List<EDD2_020405_MDto> EDD2_020405_M(EDD2020405SearchModelDto data);

        /// <summary>
        /// 檢視異動狀態列表
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        List<EDD2_020405_DDto> EDD2_020405_D(EDD2020405SearchModelDto data);
    }
}
