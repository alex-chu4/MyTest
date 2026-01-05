using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMIC2.Models.Helper
{
    public class DBHelper
    {
        public static string GetEMIC2DBConnection()
        {
           return Helper.CryptographyEx.DecryptString(System.Configuration.ConfigurationManager.ConnectionStrings["EMIC2DB"].ToString(), "EMIC_SSO2");
        }

        public static EMIC2Entities CreateDbContext()
        {
            //設定檔之連線字串應加密
            var cnStr = ConfigurationManager.ConnectionStrings["EMIC2Entities"].ConnectionString;
            //自動偵測，支援加密及未加密的連線字串，測時不加密，上線時再加密
            //在連線字串找到metadata字樣表示為加密字串
            //cnStr = Helper.CryptographyEx.DecryptString(cnStr, "EMIC_SSO2");
            var ent = new EMIC2Entities(cnStr);
            return ent;
        }
    }
}
