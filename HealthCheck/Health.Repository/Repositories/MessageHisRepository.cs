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
    public class MessageHisRepository : BaseRepository, IMessageHisRepository
    {
        public async Task<int> Create(MessageHisDto messageHis)
        {
            DynamicParameters parameters = new DynamicParameters();
            string sql = "INSERT INTO MESSAGE_HIS(MESSAGE_TYPE,MESSAGE_CONTENT,MESSAGE_HASH,CREATE_TIME) " +
                         "VALUES(@MESSAGE_TYPE,@MESSAGE_CONTENT,@MESSAGE_HASH,GETDATE()) ";
            parameters.Add("@MESSAGE_TYPE", messageHis.MESSAGE_TYPE);
            parameters.Add("@MESSAGE_CONTENT", messageHis.MESSAGE_CONTENT);
            parameters.Add("@MESSAGE_HASH", messageHis.MESSAGE_HASH);

            using (SqlConnection connection = new SqlConnection(HealthConnectionString))
            {
                return await connection.ExecuteAsync(sql, parameters);
            }
        }

        public async Task<MessageHisDto> GetLatestMessage(string messageType)
        {
            DynamicParameters parameters = new DynamicParameters();
            string sql = "SELECT TOP 1 MESSAGE_TYPE,MESSAGE_CONTENT,MESSAGE_HASH,CREATE_TIME " +
                         "FROM MESSAGE_HIS " +
                         "WHERE MESSAGE_TYPE=@MESSAGE_TYPE " +
                         "ORDER BY CREATE_TIME DESC ";
            parameters.Add("@MESSAGE_TYPE", messageType);

            using (SqlConnection connection = new SqlConnection(HealthConnectionString))
            {
                return (await connection.QueryAsync<MessageHisDto>(sql, parameters)).FirstOrDefault();
            }
        }
    }
}
