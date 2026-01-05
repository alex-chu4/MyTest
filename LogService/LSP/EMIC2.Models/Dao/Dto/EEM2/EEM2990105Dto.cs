using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMIC2.Models.Dao.Dto.EEM2
{
    public class EEM2990105Dto
    {
        public string CASECODE { get; set; }
        
        public string CASENAME { get; set; }

        public DateTime? CASESTARTTIME { get; set; }
        //public string CASESTARTTIME { get; set; }

        public string DISNAME { get; set; }

        public string EOCNAME { get; set; }

        public int OPENTIER { get; set; }
    }
}
