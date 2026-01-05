using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

using MSP.Repository.Interfaces.Emic;
using MSP.Service.Interfaces;
using MSP.Service.Models;

namespace MSP.Service.Services.Emic
{
    public class MailService : BaseService, IMailService
    {
        public MailService(ICommonParameterRepository commonParameterRepository, IMspLicenseRepository mspLicenseRepository) : base(commonParameterRepository, mspLicenseRepository)
        {
        }

        public ResultModel Send(string apId, string funcName, string oid, string subject, string content, IEnumerable<string> emailList)
        {
            return base.Send(apId, funcName, oid, subject, content, emailList, (string)null, (string)null);
        }

        public ResultModel Send(string apId, string funcName, string oid, string subject, string content, IEnumerable<string> emailList, Stream stream, string fileNameExtension, string userType)
        {
            if (string.Compare(userType, AccountType.GEN, true) != 0) return InvalidUserTypeResultModel;

            return base.Send(apId, funcName, oid, subject, content, emailList, stream, fileNameExtension);
        }

        public ResultModel Send(string apId, string funcName, string oid, string subject, string content, IEnumerable<string> emailList, string fileBase, string fileNameExtension, string userType)
        {
            if (string.Compare(userType, AccountType.GEN, true) != 0) return InvalidUserTypeResultModel;

            return base.Send(apId, funcName, oid, subject, content, emailList, fileBase, fileNameExtension);
        }

        public async Task<ResultModel> SendAsync(string apId, string funcName, string oid, string subject, string content, IEnumerable<string> emailList)
        {
            return await base.SendAsync(apId, funcName, oid, subject, content, emailList, (string)null, (string)null);
        }

        public async Task<ResultModel> SendAsync(string apId, string funcName, string oid, string subject, string content, IEnumerable<string> emailList, Stream stream, string fileNameExtension, string userType)
        {
            if (string.Compare(userType, AccountType.GEN, true) != 0) return InvalidUserTypeResultModel;

            return await base.SendAsync(apId, funcName, oid, subject, content, emailList, stream, fileNameExtension);
        }

        public async Task<ResultModel> SendAsync(string apId, string funcName, string oid, string subject, string content, IEnumerable<string> emailList, string fileBase, string fileNameExtension, string userType)
        {
            if (string.Compare(userType, AccountType.GEN, true) != 0) return InvalidUserTypeResultModel;

            return await base.SendAsync(apId, funcName, oid, subject, content, emailList, fileBase, fileNameExtension);
        }

        protected override string CategoryID => "12";

        protected override string ChannelID => "5";

        protected override string ComposeAudience(IEnumerable<string> toList)
        {
            StringBuilder xml = new StringBuilder();

            foreach (string mail in toList)
            {
                xml.Append("<audience>");
                xml.Append("<attentionTo></attentionTo>");
                xml.Append("<mobileNo></mobileNo>");
                xml.Append("<telNo></telNo>");
                xml.Append("<faxNo></faxNo>");
                xml.Append("<email>" + mail + "</email>");
                xml.Append("</audience>");
            }

            return xml.ToString();
        }
    }
}
