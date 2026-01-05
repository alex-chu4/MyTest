using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSP.Service.Services.Emic
{
    internal class AccountType
    {
        //一般帳號
        /**
         * 公務人員 (一般帳號)
         */
        public const string GOV = "gov";
        public const string GEN = "GEN";
        /**
			* 自然人(一般民眾)
			*/
        public const string PEOPLE = "people";
		/**
			* 舊帳號 (一般帳號)
			*/
		public const string OLD_EMIS = "oemis";
        /**
			* 臨時帳號
			*/
        public const string TEMP = "temp";
        //群組帳號
        /**
			* 非政府組織 (群組帳號)
			*/
        public const string NGO = "ngo";
        /**
			* 營利組織 (群組帳號)
			*/
        public const string COM = "com";
        /**
			* 非營利組織 (群組帳號)
			*/
        public const string NPO = "npo";

        //機關帳號
        /**
			* 機關帳號
			*/
        public const string ORG = "org";
    }
}
