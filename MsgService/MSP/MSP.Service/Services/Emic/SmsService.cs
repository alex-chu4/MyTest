using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MSP.Service.Interfaces;
using MSP.Service.Models;

using MSP.Repository.Interfaces.Emic;

namespace MSP.Service.Services.Emic
{
    public class SmsService : BaseService, ISmsService
    {
        public SmsService(ICommonParameterRepository commonParameterRepository, IMspLicenseRepository mspLicenseRepository) : base(commonParameterRepository, mspLicenseRepository)
        {
        }

        public new ResultModel Send(string apId, string funcName, string oid, string subject, string content, IEnumerable<string> mobileList, string userType)
        {
            if (string.Compare(userType, AccountType.GEN, true) != 0) return InvalidUserTypeResultModel;

            return base.Send(apId, funcName, oid, subject, content, mobileList, (string)null, (string)null);
        }

        public async new Task<ResultModel> SendAsync(string apId, string funcName, string oid, string subject, string content, IEnumerable<string> mobileList, string userType)
        {
            if (string.Compare(userType, AccountType.GEN, true) != 0) return InvalidUserTypeResultModel;

            return await base.SendAsync(apId, funcName, oid, subject, content, mobileList, (string)null, (string)null);
        }

        protected override string CategoryID => "2";

        protected override string ChannelID => "2";

        protected override string ComposeAudience(IEnumerable<string> toList)
        {
            StringBuilder xml = new StringBuilder();

            foreach (string mobile in toList)
            {
                xml.Append("<audience>");
                xml.Append("<attentionTo></attentionTo>");
                xml.Append("<mobileNo>" + mobile + "</mobileNo>");
                xml.Append("<telNo></telNo>");
                xml.Append("<faxNo></faxNo>");
                xml.Append("<email></email>");
                xml.Append("</audience>");
            }

            return xml.ToString();
        }
    }
}
