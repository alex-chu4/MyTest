using System;
using System.Collections.Generic;
using System.Web.Http;

using MSP.Service.Models;

namespace MSP.API.Controllers
{
    public class EmicBaseController : ApiController
    {
        protected IList<string> IsArgumentValid(MessageModel model, Action<IList<string>> extraCheck)
        {
            List<string> messages = new List<string>();

            if (string.IsNullOrEmpty(model.Oid)) messages.Add("Oid不能為空值");
            if (string.IsNullOrEmpty(model.Content)) messages.Add("Content不能為空值");
            if (!string.IsNullOrEmpty(model.FileBase) && string.IsNullOrEmpty(model.MimeType)) messages.Add("MimeType不能為空值");

            extraCheck?.Invoke(messages);

            return messages;
        }

        protected ResultModel CreateErrorResultModel(string returnCode, string returnDesc)
        {
            DateTime now = DateTime.Now;

            return new ResultModel
            {
                Serial = now.ToString("yyyyMMddHHmmssfff"),
                Sender = "msp.emic.gov.tw",
                Sent = now.ToString("yyyy-MM-ddTHH:mm:ss.fff"),
                TicketID = "0",
                ReturnCode = returnCode,
                ReturnDesc = returnDesc
            };
        }
    }
}
