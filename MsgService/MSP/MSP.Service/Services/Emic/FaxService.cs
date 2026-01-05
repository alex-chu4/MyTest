using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using MSP.Service.Interfaces;
using MSP.Service.Models;

using MSP.Repository.Interfaces.Emic;

namespace MSP.Service.Services.Emic
{
    public class FaxService : BaseService, IFaxService
    {
        public FaxService(ICommonParameterRepository commonParameterRepository, IMspLicenseRepository mspLicenseRepository) : base(commonParameterRepository, mspLicenseRepository)
        {
        }

        public new ResultModel Send(string apId, string funcName, string oid, string subject, string content, IEnumerable<string> faxList, string attFileName, string userType)
        {
            if (string.Compare(userType, AccountType.GEN, true) != 0) return InvalidUserTypeResultModel;

            return base.Send(apId, funcName, oid, subject, content, faxList, attFileName);
        }

        public ResultModel Send(string apId, string funcName, string oid, string subject, string content, IEnumerable<string> faxList, Stream stream, string fileNameExtension, string userType)
        {
            if (string.Compare(userType, AccountType.GEN, true) != 0) return InvalidUserTypeResultModel;

            return base.Send(apId, funcName, oid, subject, content, faxList, stream, fileNameExtension);
        }

        public ResultModel Send(string apId, string funcName, string oid, string subject, string content, IEnumerable<string> faxList, string fileBase, string fileNameExtension, string userType)
        {
            if (string.Compare(userType, AccountType.GEN, true) != 0) return InvalidUserTypeResultModel;

            return base.Send(apId, funcName, oid, subject, content, faxList, fileBase, fileNameExtension);
        }

        public async new Task<ResultModel> SendAsync(string apId, string funcName, string oid, string subject, string content, IEnumerable<string> faxList, string attFileName, string userType)
        {
            if (string.Compare(userType, AccountType.GEN, true) != 0) return InvalidUserTypeResultModel;

            return await base.SendAsync(apId, funcName, oid, subject, content, faxList, attFileName);
        }

        public async Task<ResultModel> SendAsync(string apId, string funcName, string oid, string subject, string content, IEnumerable<string> faxList, Stream stream, string fileNameExtension, string userType)
        {
            if (string.Compare(userType, AccountType.GEN, true) != 0) return InvalidUserTypeResultModel;

            return await base.SendAsync(apId, funcName, oid, subject, content, faxList, stream, fileNameExtension);
        }

        public async Task<ResultModel> SendAsync(string apId, string funcName, string oid, string subject, string content, IEnumerable<string> faxList, string fileBase, string fileNameExtension, string userType)
        {
            if (string.Compare(userType, AccountType.GEN, true) != 0) return InvalidUserTypeResultModel;

            return await base.SendAsync(apId, funcName, oid, subject, content, faxList, fileBase, fileNameExtension);
        }

        protected override string CategoryID => "9";

        protected override string ChannelID => "50";

        protected override string ComposeAudience(IEnumerable<string> toList)
        {
            StringBuilder xml = new StringBuilder();

            foreach (string fax in toList)
            {
                xml.Append("<audience>");
                xml.Append("<attentionTo></attentionTo>");
                xml.Append("<mobileNo></mobileNo>");
                xml.Append("<telNo></telNo>");
                xml.Append("<faxNo>" + fax + "</faxNo>");
                xml.Append("<email></email>");
                xml.Append("</audience>");
            }

            return xml.ToString();
        }
    }
}
