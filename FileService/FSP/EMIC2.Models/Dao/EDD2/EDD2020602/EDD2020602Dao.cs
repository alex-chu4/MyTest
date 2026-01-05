///////////////////////////////////////////////////////////////////////////////////////
//  程式檔名：
//  EDD2020602Dao.cs
//  程式名稱：
//  救災資源群組及分類查詢API
///////////////////////////////////////////////////////////////////////////////////////
//  開發人員            日期             版本        備註
//  timan             2019-10-22       1.0.0.0     初始版本
///////////////////////////////////////////////////////////////////////////////////////
//  Copyright (C) NEC Taiwan Ltd. 2018-2019.
//  程式描述：
//  救災資源群組及分類查詢API
///////////////////////////////////////////////////////////////////////////////////////
using Dapper;
using EMIC2.Models.Dao.Dto.EDD2.EDD2020602;
using EMIC2.Models.Helper;
using EMIC2.Models.Interface.EDD2.EDD2020602;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMIC2.Models.Dao.EDD2.EDD2020602
{
    public class EDD2020602Dao : IEDD2020602Dao
    {
        /// <summary>
        /// 救災資源群組及分類查詢
        /// </summary>
        /// <returns>List<EDD2020602Dto></returns>
        public List<EDD2020602Dto> DisasterReliefResourceGroupAndClassification()
        {
            List<EDD2020602Dto> result = new List<EDD2020602Dto>();
            using (var conn = new SqlConnection(DBHelper.GetEMIC2DBConnection()))
            {
                StringBuilder sql = new StringBuilder();
                sql.Append(@" select 
A.ID_1, A.ITEM_GROUP_ID, A.ITEM_GROUP_NAME,
B.ID_2, B.MASTER_TYPE_ID, B.MASTER_TYPE_NAME,
C.ID_3, C.SECONDARY_TYPE_ID, C.SECONDARY_TYPE_NAME,
D.ID_4, D.DETAIL_TYPE_ID, D.DETAIL_TYPE_NAME
from ( select ROW_NUMBER() OVER(ORDER BY ITEM_GROUP_ID) as ID_1, ITEM_GROUP_ID, ITEM_GROUP_NAME
       from EDD2_ITEM_GROUP_PAR) A
join ( select ROW_NUMBER() OVER(PARTITION BY ITEM_GROUP_ID ORDER BY ITEM_GROUP_ID, MASTER_TYPE_ID) as ID_2,
       ITEM_GROUP_ID, MASTER_TYPE_ID, MASTER_TYPE_NAME
       from EDD2_MASTER_TYPE) B on A.ITEM_GROUP_ID = B.ITEM_GROUP_ID
join ( select ROW_NUMBER() OVER(PARTITION BY MASTER_TYPE_ID ORDER BY MASTER_TYPE_ID, SECONDARY_TYPE_ID) as ID_3,
       MASTER_TYPE_ID, SECONDARY_TYPE_ID, SECONDARY_TYPE_NAME
       from EDD2_SECONDARY_TYPE) C on B.MASTER_TYPE_ID = C.MASTER_TYPE_ID
join ( select ROW_NUMBER() OVER(PARTITION BY SECONDARY_TYPE_ID ORDER BY SECONDARY_TYPE_ID, DETAIL_TYPE_ID) as ID_4,
       SECONDARY_TYPE_ID, DETAIL_TYPE_ID, DETAIL_TYPE_NAME
       from EDD2_DETAIL_TYPE) D on C.SECONDARY_TYPE_ID = D.SECONDARY_TYPE_ID
order by A.ITEM_GROUP_ID, B.MASTER_TYPE_ID, C.SECONDARY_TYPE_ID, D.DETAIL_TYPE_ID ");

                result = conn.Query<EDD2020602Dto>(sql.ToString()).ToList();

                return result;
            }
        }
    }
}
