using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Dapper;
using Health.Repository.Dto;
using Health.Repository.Interfaces;

namespace Health.Repository.Repositories
{
    public class NotificationInfoRepository : BaseRepository, INotificationInfoRepository
    {
        public async Task<IEnumerable<NotificationInfoDto>> SearchAsync(NotificationInfoSearchFilter filter)
        {
            DynamicParameters parameters = new DynamicParameters();
            StringBuilder sql = new StringBuilder();

            sql.Append("SELECT OID,[TYPE],EMAIL,SMS,FAX,ACTIVE,MEMO,CREATE_TIME,CREATE_USER,UPDATE_TIME,UPDATE_USER " +
                "FROM NOTIFICATION_INFO " +
                "WHERE 1=1 ");

            if (filter.ACTIVE.HasValue)
            {
                sql.Append("AND ACTIVE=@ACTIVE ");
                parameters.Add("@ACTIVE", filter.ACTIVE.Value);
            }

            using (SqlConnection connection = new SqlConnection(HealthConnectionString))
            {
                try
                {
                    IEnumerable<NotificationInfoDto> result = await connection.QueryAsync<NotificationInfoDto>(sql.ToString(), parameters);
                    return result;
                }
                catch (Exception ex)
                {
                    throw;
                }
            }
        }

        public async Task<int> Create(NotificationInfoDto notificationInfo)
        {
            string sql = "INSERT INTO NOTIFICATION_INFO(TYPE,EMAIL,SMS,FAX,ACTIVE,MEMO,CREATE_TIME) " +
                         "VALUES(@TYPE,@EMAIL,@SMS,@FAX,@ACTIVE,@MEMO,GETDATE()) ";
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@TYPE", notificationInfo.TYPE);
            parameters.Add("@EMAIL", notificationInfo.EMAIL);
            parameters.Add("@SMS", notificationInfo.SMS);
            parameters.Add("@FAX", notificationInfo.FAX);
            parameters.Add("@ACTIVE", notificationInfo.ACTIVE);
            parameters.Add("@MEMO", notificationInfo.MEMO);

            using (SqlConnection connection = new SqlConnection(HealthConnectionString))
            {
                return await connection.ExecuteAsync(sql, parameters);
            }
        }

        public async Task<int> Delete(NotificationInfoDto notificationInfo)
        {
            string sql = "DELETE NOTIFICATION_INFO WHERE OID=@OID ";
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@OID", notificationInfo.OID);

            using (SqlConnection connection = new SqlConnection(HealthConnectionString))
            {
                return await connection.ExecuteAsync(sql, parameters);
            }
        }

        public async Task<int> Update(NotificationInfoDto notificationInfo)
        {
            string sql = "UPDATE NOTIFICATION_INFO SET " +
                         "TYPE=@TYPE," +
                         "EMAIL=@EMAIL," +
                         "SMS=@SMS," +
                         "FAX=@FAX," +
                         "ACTIVE=@ACTIVE," +
                         "MEMO=@MEMO," +
                         "UPDATE_TIME=GETDATE() " +
                         "WHERE OID=@OID ";
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@TYPE", notificationInfo.TYPE);
            parameters.Add("@EMAIL", notificationInfo.EMAIL);
            parameters.Add("@SMS", notificationInfo.SMS);
            parameters.Add("@FAX", notificationInfo.FAX);
            parameters.Add("@ACTIVE", notificationInfo.ACTIVE);
            parameters.Add("@MEMO", notificationInfo.MEMO);
            parameters.Add("@OID", notificationInfo.OID);

            using (SqlConnection connection = new SqlConnection(HealthConnectionString))
            {
                return await connection.ExecuteAsync(sql, parameters);
            }
        }
    }
}
