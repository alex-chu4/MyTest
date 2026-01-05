using Dapper;
using EMIC2.Models.Dao.Dto.EDD2.EDD2020406;
using EMIC2.Models.Helper;
using EMIC2.Models.Interface.EDD2.EDD2020406;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMIC2.Models.Dao.EDD2.EDD2020406
{
    public class EDD2020406Dao : IEDD2020406Dao
    {

        /// <summary>
        ///  查到 Index 頁的資料
        /// </summary>
        /// <param name="searchModelDto">searchModelDto</param>
        /// <returns>
        ///  List<ResultModelDto>
        /// </returns>
        public List<ResultModelDto> EDD2020406_Result(SearchModelDto searchModelDto)
        {
            List<ResultModelDto> result = new List<ResultModelDto>();
            using (var conn = new SqlConnection(DBHelper.GetEMIC2DBConnection()))
            {
                StringBuilder sql = new StringBuilder();

                DynamicParameters parameters = new DynamicParameters();

                searchModelDto.UnitID = searchModelDto.UnitID <= 0 ? -1 : searchModelDto.UnitID;

                sql.Append("SELECT B.UNIT_LOCATION_ID, C.UNIT_ID, C.UNIT_NAME, C.CONTACT_NAME, C.CONTACT_TEL, D.LOCATION_NAME, D.CITY_NAME, D.TOWN_NAME, D.LOCATION_ADDRESS, E.MODIFIED_TIME, CASE WHEN E.MODIFIED_TIME IS NULL THEN 0 ELSE 1 END AS RESOURCE_STATUS");
                sql.Append(" FROM ( SELECT * FROM EDD2_QRY_UNIT_OWN( @UNIT_ID , 1) WHERE ");
                parameters.Add("UNIT_ID", searchModelDto.UnitID);

                // 填報機關名稱
                if (!string.IsNullOrEmpty(searchModelDto.UnitName))
                {
                    sql.Append(" UNIT_NAME LIKE N'%' + @UnitName + '%' ");
                    parameters.Add("UnitName", searchModelDto.UnitName);
                }
                else
                {
                    sql.Append(" 1 = 1  ");
                }

                sql.Append(" ) A JOIN( SELECT UNIT_LOCATION_ID, UNIT_ID, LOCATION_ID FROM EDD2_UNIT_LOCATION) B ON A.UNIT_ID = B.UNIT_ID JOIN EDD2_UNIT_MASTER C ON A.UNIT_ID = C.UNIT_ID  ");

                // 有用 縣市去查的
                if (!string.IsNullOrEmpty(searchModelDto.CityName) || !string.IsNullOrEmpty(searchModelDto.TownName))
                {
                    string c = string.IsNullOrEmpty(searchModelDto.CityName) ? string.Empty : " CITY_NAME = @CityName ";
                    string t = string.IsNullOrEmpty(searchModelDto.TownName) ? string.Empty : " TOWN_NAME = @TownName ";
                    string a = (!string.IsNullOrEmpty(c) && !string.IsNullOrEmpty(t)) ? " AND" : string.Empty;
                    sql.Append($" JOIN ( SELECT * FROM EDD2_LOCATION_MASTER WHERE {c} {a} {t} ) D on B.LOCATION_ID = D.LOCATION_ID");

                    if (!string.IsNullOrEmpty(c)) { parameters.Add("CityName", searchModelDto.CityName); }
                    if (!string.IsNullOrEmpty(t)) { parameters.Add("TownName", searchModelDto.TownName); }
                }
                else
                {
                    sql.Append(" JOIN EDD2_LOCATION_MASTER D on B.LOCATION_ID = D.LOCATION_ID");
                }

                sql.Append(" LEFT JOIN ( SELECT T1.UNIT_LOCATION_ID, MAX(T1.MODIFIED_TIME) AS MODIFIED_TIME FROM EDD2_RESOURCE_STOCK_DATA T1 JOIN ( SELECT RESOURCE_ID FROM EDD2_RESOURCE_ITEM_ALL WHERE 1 = 1 ");

                // 資源相關
                if (searchModelDto.MasterType > 0)
                {
                    sql.Append(" AND ");
                    sql.Append(" MASTER_TYPE_ID = @MasterType ");
                    parameters.Add("MasterType", searchModelDto.MasterType);
                }

                if (searchModelDto.SecondaryType > 0)
                {
                    sql.Append(" AND ");
                    sql.Append(" SECONDARY_TYPE_ID = @SecondaryType ");
                    parameters.Add("SecondaryType", searchModelDto.SecondaryType);
                }

                if (searchModelDto.DetailType > 0)
                {
                    sql.Append(" AND ");
                    sql.Append(" DETAIL_TYPE_ID = @DetailType ");
                    parameters.Add("DetailType", searchModelDto.DetailType);
                }

                if (searchModelDto.ResourceID > 0)
                {
                    sql.Append(" AND ");
                    sql.Append(" RESOURCE_ID = @ResourceID ");
                    parameters.Add("ResourceID", searchModelDto.ResourceID);
                }

                if (!string.IsNullOrEmpty(searchModelDto.ResourceName))
                {
                    sql.Append(" AND ");
                    sql.Append(" RESOURCE_NAME LIKE N'%' + @ResourceName + '%' ");
                    parameters.Add("ResourceName", searchModelDto.ResourceName);
                }

                sql.Append(" ) T2 ON T1.RESOURCE_ID = T2.RESOURCE_ID GROUP BY T1.UNIT_LOCATION_ID) E ON B.UNIT_LOCATION_ID = E.UNIT_LOCATION_ID  ORDER BY A.UNIT_LEVEL, C.UNIT_NAME, D.LOCATION_NAME");

                result = conn.Query<ResultModelDto>(sql.ToString(), parameters).ToList();

                return result;
            }
        }

        /// <summary>
        ///  查到 Detail 頁 匯出 的資料
        /// </summary>
        /// <param name="searchModelDto">searchModelDto</param>
        /// <returns>
        ///  List<ResultModelDto>
        /// </returns>
        public List<DetailModelDto> EDD2020406_Detail(SearchModelDto searchModelDto, string locationName)
        {
            List<DetailModelDto> result = new List<DetailModelDto>();
            using (var conn = new SqlConnection(DBHelper.GetEMIC2DBConnection()))
            {
                StringBuilder sql = new StringBuilder();

                DynamicParameters parameters = new DynamicParameters();

                searchModelDto.UnitID = searchModelDto.UnitID <= 0 ? -1 : searchModelDto.UnitID;

                sql.Append("SELECT B.UNIT_LOCATION_ID, C.UNIT_ID, C.UNIT_NAME, C.CONTACT_NAME, C.CONTACT_TEL, D.LOCATION_NAME, D.CITY_NAME, D.TOWN_NAME, D.LOCATION_ADDRESS, E.MODIFIED_TIME, E.MASTER_TYPE_NAME, E.SECONDARY_TYPE_NAME, E.DETAIL_TYPE_NAME, E.RESOURCE_NAME, E.AVAILABLE_QTY, E.STD_UOM, CASE WHEN E.MODIFIED_TIME IS NULL THEN 0 ELSE 1 END AS RESOURCE_STATUS");
                sql.Append(" FROM ( SELECT * FROM EDD2_QRY_UNIT_OWN( @UNIT_ID , 1) WHERE ");
                parameters.Add("UNIT_ID", searchModelDto.UnitID);

                // 填報機關名稱
                if (!string.IsNullOrEmpty(searchModelDto.UnitName))
                {
                    sql.Append(" UNIT_NAME LIKE N'%' + @UnitName + '%' ");
                    parameters.Add("UnitName", searchModelDto.UnitName);
                }
                else
                {
                    sql.Append(" 1 = 1  ");
                }

                sql.Append(" ) A JOIN( SELECT UNIT_LOCATION_ID, UNIT_ID, LOCATION_ID FROM EDD2_UNIT_LOCATION) B ON A.UNIT_ID = B.UNIT_ID JOIN EDD2_UNIT_MASTER C ON A.UNIT_ID = C.UNIT_ID  ");

                // 有用 縣市去查的
                if (!string.IsNullOrEmpty(searchModelDto.CityName) || !string.IsNullOrEmpty(searchModelDto.TownName))
                {
                    string c = string.IsNullOrEmpty(searchModelDto.CityName) ? string.Empty : " CITY_NAME = @CityName ";
                    string t = string.IsNullOrEmpty(searchModelDto.TownName) ? string.Empty : " TOWN_NAME = @TownName ";
                    string a = (!string.IsNullOrEmpty(c) && !string.IsNullOrEmpty(t)) ? " AND" : string.Empty;
                    sql.Append($" JOIN ( SELECT * FROM EDD2_LOCATION_MASTER WHERE {c} {a} {t} ) D on B.LOCATION_ID = D.LOCATION_ID");

                    if (!string.IsNullOrEmpty(c)) { parameters.Add("CityName", searchModelDto.CityName); }
                    if (!string.IsNullOrEmpty(t)) { parameters.Add("TownName", searchModelDto.TownName); }
                }
                else
                {
                    sql.Append(" JOIN EDD2_LOCATION_MASTER D on B.LOCATION_ID = D.LOCATION_ID");
                }

                sql.Append(" LEFT JOIN ( SELECT T1.UNIT_LOCATION_ID, MAX(T1.MODIFIED_TIME) AS MODIFIED_TIME , T2.MASTER_TYPE_NAME, T2.SECONDARY_TYPE_NAME, T2.DETAIL_TYPE_NAME, T2.RESOURCE_NAME, SUM(T1.AVAILABLE_QTY) AS AVAILABLE_QTY, T2.STD_UOM FROM EDD2_RESOURCE_STOCK_DATA T1 JOIN ( SELECT * FROM EDD2_RESOURCE_ITEM_ALL RI INNER JOIN EDD2_UOM U ON RI.STANDARD_UOM = U.UOM_ID WHERE 1 = 1 ");

                // 資源相關
                if (searchModelDto.MasterType > 0)
                {
                    sql.Append(" AND ");
                    sql.Append(" MASTER_TYPE_ID = @MasterType ");
                    parameters.Add("MasterType", searchModelDto.MasterType);
                }

                if (searchModelDto.SecondaryType > 0)
                {
                    sql.Append(" AND ");
                    sql.Append(" SECONDARY_TYPE_ID = @SecondaryType ");
                    parameters.Add("SecondaryType", searchModelDto.SecondaryType);
                }

                if (searchModelDto.DetailType > 0)
                {
                    sql.Append(" AND ");
                    sql.Append(" DETAIL_TYPE_ID = @DetailType ");
                    parameters.Add("DetailType", searchModelDto.DetailType);
                }

                if (searchModelDto.ResourceID > 0)
                {
                    sql.Append(" AND ");
                    sql.Append(" RESOURCE_ID = @ResourceID ");
                    parameters.Add("ResourceID", searchModelDto.ResourceID);
                }

                if (!string.IsNullOrEmpty(searchModelDto.ResourceName))
                {
                    sql.Append(" AND ");
                    sql.Append(" RESOURCE_NAME LIKE N'%' + @ResourceName + '%' ");
                    parameters.Add("ResourceName", searchModelDto.ResourceName);
                }

                sql.Append(" ) T2 ON T1.RESOURCE_ID = T2.RESOURCE_ID GROUP BY T1.UNIT_LOCATION_ID , T2.MASTER_TYPE_NAME, T2.SECONDARY_TYPE_NAME, T2.DETAIL_TYPE_NAME, T2.RESOURCE_NAME, T2.STD_UOM ) E ON B.UNIT_LOCATION_ID = E.UNIT_LOCATION_ID WHERE E.MASTER_TYPE_NAME IS NOT NULL  ");

                if (!string.IsNullOrEmpty(locationName))
                {
                    sql.Append(" AND D.LOCATION_NAME = @LocationName ");
                    parameters.Add("LocationName", locationName);
                }

                sql.Append(" ORDER BY A.UNIT_LEVEL, C.UNIT_NAME, D.LOCATION_NAME ");

                result = conn.Query<DetailModelDto>(sql.ToString(), parameters).ToList();

                return result;
            }
        }


        /// <summary>
        ///  查到 Detail 頁的資料
        /// </summary>
        /// <param name="unitLocationID">unitLocationID</param>
        /// <returns>
        ///  List<DetailViewModelDto>
        /// </returns>
        public List<DetailViewModelDto> EDD2020406_Detail_View(int unitLocationID)
        {
            List<DetailViewModelDto> result = new List<DetailViewModelDto>();
            using (var conn = new SqlConnection(DBHelper.GetEMIC2DBConnection()))
            {
                StringBuilder sql = new StringBuilder();

                DynamicParameters parameters = new DynamicParameters();

                if (unitLocationID > 0)
                {
                    sql.Append(" SELECT UM.UNIT_ID, LM.LOCATION_ID, UM.UNIT_NAME, LM.LOCATION_NAME, UM.CONTACT_NAME, MAX(S.MODIFIED_TIME) AS MODIFIED_TIME, UM.CONTACT_TEL, LM.CITY_NAME, LM.TOWN_NAME, ");
                    sql.Append(" LM.LOCATION_ADDRESS, LM.LOCATION_COORDINATE_X, LM.LOCATION_COORDINATE_Y, LM.LOCATION_LONGITUDE, LM.LOCATION_LATITUDE, LM.CONTACT_NAME AS L_CONTACT_NAME, ");
                    sql.Append(" LM.CONTACT_TEL AS L_CONTACT_TEL, LM.CONTACT_EXT, LM.CONTACT_MOBILE, LM.CONTACT_EMAIL, LM.CONTACT_FAX ");
                    sql.Append(" FROM EDD2_UNIT_LOCATION UL ");
                    sql.Append(" INNER JOIN EDD2_UNIT_MASTER UM ON UL.UNIT_ID = UM.UNIT_ID ");
                    sql.Append(" INNER JOIN EDD2_LOCATION_MASTER LM ON UL.LOCATION_ID = LM.LOCATION_ID ");
                    sql.Append(" INNER JOIN EDD2_RESOURCE_STOCK_DATA S ON UL.UNIT_LOCATION_ID = S.UNIT_LOCATION_ID ");

                    sql.Append(" WHERE UL.UNIT_LOCATION_ID =  @UnitLocationID ");
                    parameters.Add("UnitLocationID", unitLocationID);

                    sql.Append(" GROUP BY UM.UNIT_ID, LM.LOCATION_ID, UM.UNIT_NAME, LM.LOCATION_NAME, UM.CONTACT_NAME, UM.CONTACT_TEL, LM.CITY_NAME, LM.TOWN_NAME, ");
                    sql.Append(" LM.LOCATION_ADDRESS, LM.LOCATION_COORDINATE_X, LM.LOCATION_COORDINATE_Y, LM.LOCATION_LONGITUDE, LM.LOCATION_LATITUDE, LM.CONTACT_NAME, ");
                    sql.Append(" LM.CONTACT_TEL, LM.CONTACT_EXT, LM.CONTACT_MOBILE, LM.CONTACT_EMAIL, LM.CONTACT_FAX, UM.UNIT_LEVEL ");
                    sql.Append(" ORDER BY UM.UNIT_LEVEL, UM.UNIT_NAME, LM.LOCATION_NAME; ");
                }

                result = conn.Query<DetailViewModelDto>(sql.ToString(), parameters).ToList();
            }

            foreach (var item in result)
            {
                item.MODIFIED_TIME_TXT = item.MODIFIED_TIME == null ? string.Empty : ((DateTime)item.MODIFIED_TIME).ToString("yyyy-MM-dd HH:mm");
                item.L_CONTACT_TEL = string.IsNullOrEmpty(item.CONTACT_EXT) ? item.L_CONTACT_TEL : $"{item.L_CONTACT_TEL}#{item.CONTACT_EXT}";

                if (!string.IsNullOrEmpty(item.CITY_NAME) && !string.IsNullOrEmpty(item.TOWN_NAME))
                {
                    item.LOCATION_ADDRESS = item.CITY_NAME + item.TOWN_NAME + item.LOCATION_ADDRESS.Replace(item.CITY_NAME, string.Empty).Replace(item.TOWN_NAME, string.Empty);
                }
            }

            return result;
        }

        /// <summary>
        ///  查到 Detail 頁 Table 的資料
        /// </summary>
        /// <param name="unitLocationID">unitLocationID</param>
        /// <param name="resourceName">resourceName</param>
        /// <returns>
        ///  List<DetailTableModelDto>
        /// </returns>
        public List<DetailTableModelDto> EDD2020406_Detail_Table(DetailSearchModelDto model)
        {
            List<DetailTableModelDto> result = new List<DetailTableModelDto>();
            using (var conn = new SqlConnection(DBHelper.GetEMIC2DBConnection()))
            {
                StringBuilder sql = new StringBuilder();

                DynamicParameters parameters = new DynamicParameters();

                if (model.unitLocationID > 0)
                {
                    sql.Append(" SELECT C.RESOURCE_ID, C.RESOURCE_NAME, ");
                    sql.Append(" (CAST(ISNULL(A.CURRENT_QTY,0) as nvarchar) + C.STANDARD_UOM_NAME) as CURRENT_QTY, ");
                    sql.Append(" (CAST(ISNULL(A.AVAILABLE_QTY,0) as nvarchar) + C.STANDARD_UOM_NAME) as AVAILABLE_QTY,  ");
                    sql.Append(" (CAST(ISNULL(A.SUPPORT_QTY,0) as nvarchar) + C.STANDARD_UOM_NAME) as SUPPORT_QTY,  ");
                    sql.Append(" (CAST((ISNULL(A.AVAILABLE_QTY,0) - ISNULL(A.SUPPORT_QTY,0)) as nvarchar) + C.STANDARD_UOM_NAME) as USEFUL_QTY, ");
                    sql.Append(" A.MODIFIED_TIME FROM EDD2_RESOURCE_STOCK_DATA A ");
                    sql.Append(" JOIN ( SELECT UNIT_LOCATION_ID, UNIT_ID, LOCATION_ID FROM EDD2_UNIT_LOCATION ");

                    sql.Append(" WHERE UNIT_LOCATION_ID =  @UnitLocationID ");
                    parameters.Add("UnitLocationID", model.unitLocationID);

                    sql.Append(" ) B on A.UNIT_LOCATION_ID = B.UNIT_LOCATION_ID ");
                    sql.Append(" join EDD2_RESOURCE_ITEM_ALL C on A.RESOURCE_ID = C.RESOURCE_ID WHERE 1 = 1 ");

                    if (!string.IsNullOrEmpty(model.resourceName))
                    {
                        sql.Append("  AND C.RESOURCE_NAME LIKE N'%' + @ResourceName + '%' ");
                        parameters.Add("ResourceName", model.resourceName);
                    }

                    if (model.MASTER_TYPE_ID > 0)
                    {
                        sql.Append("  AND C.MASTER_TYPE_ID = @MASTER_TYPE_ID ");
                        parameters.Add("MASTER_TYPE_ID", model.MASTER_TYPE_ID);
                    }

                    if (model.SECONDARY_TYPE_ID > 0)
                    {
                        sql.Append("  AND C.SECONDARY_TYPE_ID = @SECONDARY_TYPE_ID ");
                        parameters.Add("SECONDARY_TYPE_ID", model.SECONDARY_TYPE_ID);
                    }

                    if (model.DETAIL_TYPE_ID > 0)
                    {
                        sql.Append("  AND C.DETAIL_TYPE_ID = @DETAIL_TYPE_ID ");
                        parameters.Add("DETAIL_TYPE_ID", model.DETAIL_TYPE_ID);
                    }

                    if (model.RESOURCE_ID > 0)
                    {
                        sql.Append("  AND C.RESOURCE_ID = @RESOURCE_ID ");
                        parameters.Add("RESOURCE_ID", model.RESOURCE_ID);
                    }
                }

                result = conn.Query<DetailTableModelDto>(sql.ToString(), parameters).ToList();
            }

            foreach (var item in result)
            {
                item.MODIFIED_TIME_TXT = item.MODIFIED_TIME == null ? string.Empty : ((DateTime)item.MODIFIED_TIME).ToString("yyyy-MM-dd HH:mm");
            }

            return result;
        }
    }
}
