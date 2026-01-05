using Dapper;
using EMIC2.Models.Helper;
using System.Linq;
using static Utility.Helper.ConnectionHelper;

namespace Utility.Dao
{
    public class UserWithRoleDao
    {
        //public static string GetSSO2RoleList(string userId)
        //    => Connect(DBHelper.GetEMIC2DBConnection(), c =>
        //        c.Query<string>(
        //         "select * from SSO2_QRY_ROLE_CODE(@UserId)",
        //            new { UserId = userId })).FirstOrDefault() ?? null;

        //public static string GetEMIC2RoleList(string userId, string eoccode)
        //    => Connect(DBHelper.GetEMIC2DBConnection(), c =>
        //        c.Query<string>(
        //            "select * from EMIC2_QRY_ROLE_CODE(@UserId,@Eoc)",
        //                new { UserId = userId, Eoc = eoccode })).FirstOrDefault() ?? null;

        //public static(string ROLE_LIST, string MAIN_MENU) GetSSO2ROLE_MENU(string userId)
        //    => Connect(DBHelper.GetEMIC2DBConnection(), c =>
        //        c.Query<(string ROLE_LIST, string MAIN_MENU)>(
        //         "select * from SSO2_QRY_ROLE_MENU(@UserId)",
        //            new { UserId = userId })).FirstOrDefault();

        //public static (string ROLE_LIST, string MAIN_MENU) GetEMIC2ROLE_MENU(string userId, string eoccode)
        //   => Connect(DBHelper.GetEMIC2DBConnection(), c =>
        //       c.Query<(string ROLE_LIST, string MAIN_MENU)>(
        //           "select * from EMIC2_QRY_ROLE_MENU(@UserId,@Eoc)",
        //               new { UserId = userId, Eoc = eoccode })).FirstOrDefault();

        public static (string ROLE_LIST, string MAIN_MENU) GetCOM2ROLE_MENU(string userId, string eoccode, string menuindex)
           => Connect(DBHelper.GetEMIC2DBConnection(), c =>
               c.Query<(string ROLE_LIST, string MAIN_MENU)>(
                   "select * from COM2_QRY_ROLE_MENU(@P_USER_ID,@P_EOC_ID,@P_MENU_INDEX)",
                       new { P_USER_ID = userId, P_EOC_ID = eoccode, P_MENU_INDEX = menuindex })).FirstOrDefault();
    }
}
