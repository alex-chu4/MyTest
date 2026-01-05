using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utility.Model
{
    public class HeaderUserInfoViewModel
    {
        public string USERID { get; set; }

        public string NAME { get; set; }

        public string COMPANY { get; set; }

        public string USER_OID { get; set; }

        public string DEPARTMENT { get; set; }

        public string TITLE { get; set; }

        public DateTime LastLoginTime { get; set; }

        public bool IsLogin { get; set; }

        public string Exp_Timer { get; set; }
        //add by david
        public int Expires { get; set; }

        public string EOC_NAME { get; set; }

        public string PRJ_NAME { get; set; }

        public string PRJ_LEVEL { get; set; }

        public string EOC_ID { get; set; }

        public string PRJ_NO { get; set; }

        public string EOC_USER_NAME { get; set; }

        public string ACCOUNT_TYPE { get; set; }
    }
}
