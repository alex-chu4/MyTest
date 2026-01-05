using Dapper;
using EMIC2.Models.Helper;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Utility.Model.MenuModel;

namespace Utility.Dao
{
    public class MenuTreeDao
    {
        public IEnumerable<MainMenu> GetParentList(string MenuIndex, bool bCheckIsONOFF)
        {
            string sqlStatement = @"
                    SELECT EMP.MENU_ID AS ID
                         , EMP.MENU_NAME AS Name
                         , (FUN.DOMAIN + FUN.URL + '') AS Url
                         , EMP.ICON_NAME as Icon
                         , EMP.CATEGORY
                    FROM EMP2_MENU_TREE EMP
                    LEFT JOIN SSO2_FUNCTION FUN ON FUN.FUNCTION_CODE = EMP.FUNCTION_CODE
                    WHERE EMP.PARENT_MENU_ID IS NULL ";
            if (bCheckIsONOFF)
            {
                sqlStatement += @" and EMP.IS_ON_OFF = 'Y' ";
                //sqlStatement += @" and FUN.IS_ON_OFF = 'Y' "; //SSO2_FUNCTION中的is_on_off幾乎都是null，先不判斷
            }

            sqlStatement += @"  AND MENU_INDEX = @MenuIndex ORDER BY EMP.SHOW_ORDER ";

            using (var conn = new SqlConnection(DBHelper.GetEMIC2DBConnection()))
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@MenuIndex", MenuIndex);

                var result = conn.Query<MainMenu>(sqlStatement, parameters);
                return result;
            }
        }

        public IEnumerable<SubMenu> GetSubList(string MenuIndex, bool bCheckIsONOFF)
        {
            string sqlStatement = @"
                SELECT EMP.MENU_ID AS ID
                     , EMP.MENU_NAME AS Name
                     , EMP.SHOW_ORDER AS Level
                     , EMP.PARENT_MENU_ID AS ParentID
                     , (FUN.DOMAIN + FUN.URL + '') AS Url
	                 , FUN.FUNCTION_CODE AS FunctionCode
                     , EMP.CATEGORY
                FROM EMP2_MENU_TREE EMP
                LEFT JOIN SSO2_FUNCTION FUN ON FUN.FUNCTION_CODE = EMP.FUNCTION_CODE
                WHERE EMP.PARENT_MENU_ID IS NOT NULL ";
            if (bCheckIsONOFF)
            {
                sqlStatement += @" and EMP.IS_ON_OFF = 'Y' ";
                //sqlStatement += @" and FUN.IS_ON_OFF = 'Y' "; //SSO2_FUNCTION中的is_on_off幾乎都是null，先不判斷
            }
            sqlStatement += @" AND MENU_INDEX = @MenuIndex  ORDER BY EMP.PARENT_MENU_ID, SHOW_ORDER ";

            using (var conn = new SqlConnection(DBHelper.GetEMIC2DBConnection()))
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@MenuIndex", MenuIndex);

                var result = conn.Query<SubMenu>(sqlStatement, parameters);
                return result;
            }
        }
    }



}
