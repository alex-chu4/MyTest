using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMIC2.Models.Dao.Dto.EDD2.EDD2020406
{
    public class SearchModelDto
    {
        public int UnitID { get; set; }

        public string UnitName { get; set; }

        public int MasterType { get; set; }

        public int SecondaryType { get; set; }

        public int DetailType { get; set; }

        public int ResourceID { get; set; }

        public string ResourceName { get; set; }

        public string CityName { get; set; }

        public string TownName { get; set; }

        // 共用
        public int Limit { get; set; }

        public int Offset { get; set; }

        public string SortName { get; set; }

        public string SortOrder { get; set; }

        public int Total { get; set; }

        public string FILE_TYPE { get; set; }
    }
}
