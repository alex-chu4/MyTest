using EMIC2.Models.Dao.Dto.EDD2.EDD2020406;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMIC2.Models.Interface.EDD2.EDD2020406
{
    public interface IEDD2020406Dao
    {
        List<ResultModelDto> EDD2020406_Result(SearchModelDto searchModelDto);

        List<DetailModelDto> EDD2020406_Detail(SearchModelDto searchModelDto, string locationName);

        List<DetailViewModelDto> EDD2020406_Detail_View(int unitLocationID);

        List<DetailTableModelDto> EDD2020406_Detail_Table(DetailSearchModelDto model);
    }
}
