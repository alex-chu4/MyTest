using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utility.Dao
{
    public class EmailModelDao
    {
        public string Oid { get; set; }

        public string Subject { get; set; }

        public string Content { get; set; }

        public List<string> EmailList { get; set; }

        public string UserType { get; set; }
    }
}
