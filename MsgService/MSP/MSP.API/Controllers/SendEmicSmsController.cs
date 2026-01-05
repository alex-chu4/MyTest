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
    public class SendEmicSmsController : EmicBaseController
    {
        private readonly ILogger logger = LogManager.GetCurrentClassLogger();
        private readonly ISmsService smsService;

        public SendEmicSmsController(ISmsService smsService)
        {
            this.smsService = smsService;
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
                    if ((model.MobileList == null) || (model.MobileList.Count() == 0)) m.Add("MobileList不能為空值");
                    if (model.Content.Length > 70)
                        model.Content = model.Content.Substring(0, 70);
                });

                if (messages.Count() > 0)
                    resultModel = CreateErrorResultModel("E001", string.Join(";", messages));
                else
                    resultModel = smsService.Send(model.ApId, model.FuncName, model.Oid, model.Subject, model.Content, model.MobileList.Select(m => m.Mobile).ToArray(), model.UserType);
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
                    if ((model.MobileList == null) || (model.MobileList.Count() == 0)) m.Add("MobileList不能為空值");
                    if (model.Content.Length > 70)
                        model.Content = model.Content.Substring(0, 70);
                });

                if (messages.Count() > 0)
                    resultModel = CreateErrorResultModel("E001", string.Join(";", messages));
                else
                    resultModel = await smsService.SendAsync(model.ApId, model.FuncName, model.Oid, model.Subject, model.Content, model.MobileList.Select(m => m.Mobile).ToArray(), model.UserType);
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
