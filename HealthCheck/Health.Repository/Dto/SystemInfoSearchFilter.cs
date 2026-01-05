using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Health.Repository.Dto
{
    public class SystemInfoSearchFilter
    {
        public string ID { get; set; }

        public string Name { get; set; }

        public IEnumerable<string> IDs { get; set; }

        public Nullable<bool> KEY_FUNCTION { get; set; }
    }
}
