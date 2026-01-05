using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using MSP.Service.Models;

namespace MSP.Service.Interfaces
{
    public interface IFaxService
    {
        ResultModel Send(string apId, string funcName, string oid, string subject, string content, IEnumerable<string> faxList, string attFileName, string userType);

        ResultModel Send(string apId, string funcName, string oid, string subject, string content, IEnumerable<string> faxList, Stream stream, string fileNameExtension, string userType);

        ResultModel Send(string apId, string funcName, string oid, string subject, string content, IEnumerable<string> faxList, string fileBase, string fileNameExtension, string userType);

        Task<ResultModel> SendAsync(string apId, string funcName, string oid, string subject, string content, IEnumerable<string> faxList, string attFileName, string userType);

        Task<ResultModel> SendAsync(string apId, string funcName, string oid, string subject, string content, IEnumerable<string> faxList, Stream stream, string fileNameExtension, string userType);

        Task<ResultModel> SendAsync(string apId, string funcName, string oid, string subject, string content, IEnumerable<string> faxList, string fileBase, string fileNameExtension, string userType);
    }
}
