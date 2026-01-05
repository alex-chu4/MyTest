using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using MSP.Service.Models;

namespace MSP.Service.Interfaces
{
    public interface IMailService
    {
        ResultModel Send(string apId, string funcName, string oid, string subject, string content, IEnumerable<string> emailList);

        ResultModel Send(string apId, string funcName, string oid, string subject, string content, IEnumerable<string> emailList, string attFileName);

        ResultModel Send(string apId, string funcName, string oid, string subject, string content, IEnumerable<string> emailList, Stream stream, string fileNameExtension, string userType);

        ResultModel Send(string apId, string funcName, string oid, string subject, string content, IEnumerable<string> emailList, string fileBase, string fileNameExtension, string userType);

        Task<ResultModel> SendAsync(string apId, string funcName, string oid, string subject, string content, IEnumerable<string> emailList);

        Task<ResultModel> SendAsync(string apId, string funcName, string oid, string subject, string content, IEnumerable<string> emailList, string attFileName);

        Task<ResultModel> SendAsync(string apId, string funcName, string oid, string subject, string content, IEnumerable<string> emailList, Stream stream, string fileNameExtension, string userType);

        Task<ResultModel> SendAsync(string apId, string funcName, string oid, string subject, string content, IEnumerable<string> emailList, string fileBase, string fileNameExtension, string userType);
    }
}
