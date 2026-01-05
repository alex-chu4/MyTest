using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;

using NLog;
using Newtonsoft.Json;
using MSP.Service.Interfaces;
using MSP.Service.Models;

namespace MSP.API.Controllers
{
    public class SendEmicMailController : EmicBaseController
    {
        private readonly ILogger logger = LogManager.GetCurrentClassLogger();
        private readonly IMailService mailService;

        public SendEmicMailController(IMailService mailService)
        {
            this.mailService = mailService;
        }

#if false
        public IHttpActionResult Post(MessageModel model)
        {
            ResultModel resultModel = null;

            try
            {
                logger.Info(JsonConvert.SerializeObject(model));

                IEnumerable<string> messages = IsArgumentValid(model, (m) =>
                {
                    if (string.IsNullOrEmpty(model.Subject)) m.Add("Subject不能為空值");
                    if ((model.EmailList == null) || (model.EmailList.Count() == 0)) m.Add("EmailList不能為空值");
                });

                if (messages.Count() > 0)
                    resultModel = CreateErrorResultModel("E001", string.Join(";", messages));
                else
                    resultModel = mailService.Send(model.ApId, model.FuncName, model.Oid, model.Subject, model.Content, model.EmailList.Select(e => e.Email).ToArray(), model.FileBase, model.MimeType, model.UserType);
            }
            catch (Exception ex)
            {
                if (ex.InnerException == null)
                    resultModel = CreateErrorResultModel("E010", ex.Message);
                else
                    resultModel = CreateErrorResultModel("E011", ex.InnerException.Message);
            }
            finally
            {
                logger.Info(JsonConvert.SerializeObject(resultModel));
            }

            return Ok(resultModel);
        }
#else
        public async Task<IHttpActionResult> Post(MessageModel model)
        {
            ResultModel resultModel = null;

            try
            {
                logger.Info(JsonConvert.SerializeObject(model));

                IEnumerable<string> messages = IsArgumentValid(model, (m) =>
                {
                    if (string.IsNullOrEmpty(model.Subject)) m.Add("Subject不能為空值");
                    if ((model.EmailList == null) || (model.EmailList.Count() == 0)) m.Add("EmailList不能為空值");
                });

                if (messages.Count() > 0)
                    resultModel = CreateErrorResultModel("E001", string.Join(";", messages));
                else
                    resultModel = await mailService.SendAsync(model.ApId, model.FuncName, model.Oid, model.Subject, model.Content, model.EmailList.Select(e => e.Email).ToArray(), model.FileBase, model.MimeType, model.UserType);
            }
            catch (Exception ex)
            {
                if (ex.InnerException == null)
                {
                    logger.Error(ex);
                    resultModel = CreateErrorResultModel("E010", ex.Message);
                }
                else
                {
                    logger.Error(ex.InnerException);
                    resultModel = CreateErrorResultModel("E011", ex.InnerException.Message);
                }
            }
            finally
            {
                logger.Info(JsonConvert.SerializeObject(resultModel));
            }

            return Ok(resultModel);
        }
#endif
    }
}
