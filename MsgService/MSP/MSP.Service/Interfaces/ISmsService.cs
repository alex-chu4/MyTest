using System.Collections.Generic;
using System.Threading.Tasks;
using MSP.Service.Models;

namespace MSP.Service.Interfaces
{
    public interface ISmsService
    {
        ResultModel Send(string apId, string funcName, string oid, string subject, string content, IEnumerable<string> mobileList, string userType);

        Task<ResultModel> SendAsync(string apId, string funcName, string oid, string subject, string content, IEnumerable<string> mobileList, string userType);
    }
}
