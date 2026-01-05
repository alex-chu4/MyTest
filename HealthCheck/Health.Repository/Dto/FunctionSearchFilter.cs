using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Health.Repository.Dto
{
    public class FunctionSearchFilter
    {
        public string FUNCTION_CODE { get; set; }

        public string[] FUNCTION_CODES { get; set; }

        public string SYS_CODE { get; set; }
    }
}
