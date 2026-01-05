using Dapper;
using EMIC2.Models.Helper;
using System.Collections.Generic;
using static Utility.Helper.ConnectionHelper;
using Utility.Model;

namespace Utility.Dao
{
    public class RoleAndFunctionDao
    {
        public IEnumerable<RoleFunctionModel> getRolsFuncsByUser(string userID)
        {
            string sqlStatement = @"
DECLARE @temp_ROLE TABLE
(
    [USER_ID] NVARCHAR(20),
    ROLE_CODE NVARCHAR(20)
)
INSERT INTO @temp_ROLE
	SELECT * FROM (
		SELECT USER_ID
			 , ROLE_CODE
		FROM dbo.SSO2_USER_AUTH
		WHERE ROLE_CODE IS NOT NULL
        AND ROLE_CODE != ''
        AND USER_ID = @UserID
		UNION 
		SELECT uAuth.USER_ID
			 , sGr.ROLE_CODE
		FROM dbo.SSO2_USER_AUTH uAuth
		INNER JOIN dbo.SSO2_GR sGr ON uAuth.GROUP_CODE = sGr.GROUP_CODE
		WHERE uAuth.GROUP_CODE IS NOT NULL 
            AND uAuth.USER_ID = @UserID) AS UR
	GROUP BY UR.USER_ID
		   , UR.ROLE_CODE
    ORDER BY UR.USER_ID

DECLARE @temp_USER_ROLE TABLE
(
    [USER_ID] NVARCHAR(20),
    ROLES NVARCHAR(4000)
)
INSERT INTO @temp_USER_ROLE
SELECT USER_ID, LEFT(t.ROLE_CODE,len(t.ROLE_CODE)-1) as ROLES
FROM 
(
	SELECT USER_ID ,
	(
		SELECT CAST(ROLE_CODE AS NVARCHAR ) + ',' from @temp_ROLE
		WHERE USER_ID = tmpR.USER_ID	--把name一樣的加起來
		FOR XML PATH('')
	) AS ROLE_CODE
	FROM @temp_ROLE tmpR
) t
GROUP BY ROLE_CODE, USER_ID 
ORDER BY ROLE_CODE 

--------------- 人 -> 功能 組合
DECLARE @temp_FUN TABLE
(
    [USER_ID] NVARCHAR(20),
    FUNCTION_CODE NVARCHAR(20)
)
INSERT INTO @temp_FUN
SELECT F.USER_ID
     , F.FUNCTION_CODE
FROM(
SELECT T.USER_ID
     , T.ROLE_CODE
	 , RF.FUNCTION_CODE
FROM @temp_ROLE T
INNER JOIN SSO2_RF RF ON RF.ROLE_CODE = T.ROLE_CODE) F
GROUP BY F.USER_ID
       , F.FUNCTION_CODE
ORDER BY F.USER_ID

DECLARE @temp_USER_FUN TABLE
(
    [USER_ID] NVARCHAR(20),
    FUNCTIONS NVARCHAR(4000)
)
INSERT INTO @temp_USER_FUN
SELECT USER_ID, LEFT(t.FUNCTION_CODE,len(t.FUNCTION_CODE)-1) as FUNCTIONS
FROM 
(
	SELECT USER_ID ,
	(
		SELECT CAST(FUNCTION_CODE AS NVARCHAR ) + ',' from @temp_FUN
		WHERE USER_ID = tmpF.USER_ID	--把name一樣的加起來
		FOR XML PATH('')
	) AS FUNCTION_CODE
	FROM @temp_FUN tmpF
) t
GROUP BY FUNCTION_CODE, USER_ID 
ORDER BY FUNCTION_CODE 

--SELECT * FROM @temp_USER_ROLE
--SELECT * FROM @temp_USER_FUN

SELECT UR.ROLES
     , UF.FUNCTIONS
FROM @temp_USER_ROLE UR
INNER JOIN @temp_USER_FUN UF ON UF.USER_ID = UR.USER_ID
GROUP BY UR.ROLES
       , UF.FUNCTIONS
ORDER BY UR.ROLES
       , UF.FUNCTIONS
            ";
            return Connect(DBHelper.GetEMIC2DBConnection(), c =>
                 c.Query<RoleFunctionModel>(sqlStatement, new { UserID = userID }));
        }
    }

}
