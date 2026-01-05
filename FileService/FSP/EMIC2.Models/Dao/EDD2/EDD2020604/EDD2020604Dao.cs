using Dapper;
using EMIC2.Models.Dao.Dto.EDD2.EDD2020604;
using EMIC2.Models.Helper;
using EMIC2.Models.Interface.EDD2.EDD2020604;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMIC2.Models.Dao.EDD2.EDD2020604
{
    public class EDD2020604Dao : IEDD2020604Dao
    {

        /// <summary>
        /// 救災資源查詢
        /// </summary>
        /// <returns>List<EDD2020602Dto></returns>
        public List<EDD2020604Dto> EDD2_020604_M(EDD2020604SearchModelDto model)
        {
            List<EDD2020604Dto> result = new List<EDD2020604Dto>();
            using (var conn = new SqlConnection(DBHelper.GetEMIC2DBConnection()))
            {
                StringBuilder sql = new StringBuilder();
                DynamicParameters parameters = new DynamicParameters();

                sql.Append("select * from [EDD2_020604_M] (@city_name, @town_name, @location_id, @item_group_id, @master_type_id, @secondary_type_id, @detail_type_id) ");
                parameters.Add("city_name", model.city_name);
                parameters.Add("town_name", model.town_name);
                parameters.Add("location_id", model.location_id);
                parameters.Add("item_group_id", model.item_group_id);
                parameters.Add("master_type_id", model.master_type_id);
                parameters.Add("secondary_type_id", model.secondary_type_id);
                parameters.Add("detail_type_id", model.detail_type_id);


                result = conn.Query<EDD2020604Dto>(sql.ToString(), parameters).ToList();

                return result;
            }
        }
    }
}
