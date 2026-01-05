using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

using Health.Repository.Dto;
using Health.Service.Interfaces;
using MSP.Service.Models;
using Newtonsoft.Json;
using NLog;

namespace Health.Notify
{
    internal enum ContentTypeEnum
    {
        NONE,
        TEXT,
        HTML
    }

    public class Alert
    {
        private readonly ILogger logger = LogManager.GetCurrentClassLogger();
        private readonly IEnumerable<NotificationInfoDto> _NotificationInfos;
        private readonly IEnumerable<HealthCurrentViewDto> _HealthCurrentViews;
        private readonly IEnumerable<EventDto> _Events;
        private readonly IHealthService _HealthService;

        private const int HTML_MAX = 900;
        private const int TEXT_MAX = 70;

        private string SendEmicMailUrl
        {
            get
            {
                return ConfigurationManager.AppSettings["SendEmicMailUrl"];
            }
        }

        private string SendEmicSmsUrl
        {
            get
            {
                return ConfigurationManager.AppSettings["SendEmicSmsUrl"];
            }
        }

        public Alert(IHealthService healthService,
                     IEnumerable<NotificationInfoDto> notificationInfos,
                     IEnumerable<HealthCurrentViewDto> healthCurrentViews,
                     IEnumerable<EventDto> events)
        {
            _HealthService = healthService;
            _NotificationInfos = notificationInfos;
            _HealthCurrentViews = healthCurrentViews;
            _Events = events;
        }

        public async Task SendAsync()
        {
            MessageModel message =
                new MessageModel()
                {
                    Oid = "OP",
                    Subject = string.Format("APM警示 {0:yyyy-MM-dd HH:mm}", DateTime.Now),
                    UserType = "GEN"
                };

            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("Cache-Control", "no-cache");
                foreach (NotificationInfoDto notificationInfo in _NotificationInfos)
                {
                    if (!string.IsNullOrEmpty(notificationInfo.EMAIL))
                    {
                        IEnumerable<string> contentCollection = ComposeContent(ContentTypeEnum.HTML, notificationInfo.TYPE);
                        if (await IsSameContent(contentCollection, "EMAIL")) break;

                        for (int index = 0; index < contentCollection.Count(); index++)
                        {
                            if (!string.IsNullOrEmpty(contentCollection.ElementAt(index)))
                            {
                                message.Content = string.Format("<![CDATA[<div style='font-size:14pt'>{0}</div>]]>", contentCollection.ElementAt(index));
                                message.EmailList = new EmailModel[] { new EmailModel { Email = notificationInfo.EMAIL } };
                                message.MobileList = null;
                                string jsonString = JsonConvert.SerializeObject(message);
                                HttpResponseMessage responseMessage;

                                using (HttpContent content = new StringContent(jsonString, Encoding.UTF8, "application/json"))
                                {
                                    logger.Info(jsonString);
                                    responseMessage = await client.PostAsync(SendEmicMailUrl, content);
                                    logger.Info(await responseMessage.Content.ReadAsStringAsync());
                                }
                            }
                        }
                    }

                    if (!string.IsNullOrEmpty(notificationInfo.SMS))
                    {
                        IEnumerable<string> contentCollection = ComposeContent(ContentTypeEnum.TEXT, notificationInfo.TYPE);
                        if (await IsSameContent(contentCollection, "SMS")) break;

                        for (int index = 0; index < contentCollection.Count(); index++)
                        {
                            if (!string.IsNullOrEmpty(contentCollection.ElementAt(index)))
                            {
                                message.Content = contentCollection.ElementAt(index);
                                message.EmailList = null;
                                message.MobileList = new MobileModel[] { new MobileModel { Mobile = notificationInfo.SMS } };
                                string jsonString = JsonConvert.SerializeObject(message);
                                HttpResponseMessage responseMessage;

                                using (HttpContent content = new StringContent(jsonString, Encoding.UTF8, "application/json"))
                                {
                                    logger.Info(jsonString);
                                    responseMessage = await client.PostAsync(SendEmicSmsUrl, content);
                                    logger.Info(await responseMessage.Content.ReadAsStringAsync());
                                }
                            }
                        }
                    }
                }
            }
        }

        public void Send()
        {
            SendAsync().Wait();
        }

        private IList<string> ComposeSystem(ContentTypeEnum contentType)
        {
            IList<string> systemContent = new List<string>();

            switch (contentType)
            {
                case ContentTypeEnum.HTML:
                    foreach (HealthCurrentViewDto healthCurrentView in _HealthCurrentViews)
                    {
                        if (!healthCurrentView.STATUS)
                        {
                            systemContent.Add(string.Format("&nbsp;&nbsp;&nbsp;&nbsp;<span style='color:red'>{0}:{1} 離線</span><br />",
                                healthCurrentView.SYSTEM_NAME, healthCurrentView.VM_IPv4));
                        }
                    }
                    break;
                case ContentTypeEnum.TEXT:
                    foreach (HealthCurrentViewDto healthCurrentView in _HealthCurrentViews)
                    {
                        if (!healthCurrentView.STATUS)
                        {
                            systemContent.Add(string.Format("{0}:{1} 離線",
                                healthCurrentView.SYSTEM_NAME, healthCurrentView.VM_IPv4));
                        }
                    }
                    break;
            }

            return systemContent;
        }

        private IList<string> ComposeEvent(ContentTypeEnum contentType)
        {
            IList<string> eventContent = new List<string>();

            switch (contentType)
            {
                case ContentTypeEnum.HTML:
                    foreach (EventDto @event in _Events)
                    {
                        eventContent.Add(string.Format("&nbsp;&nbsp;&nbsp;&nbsp;<span style='color:red'>{0}的{1}:{2} 超過 Threshold({3})</span><br />",
                            @event.SYSTEM_NAME, @event.FUNCTION_NAME, @event.IPv4, @event.THRESHOLD));
                    }
                    break;
                case ContentTypeEnum.TEXT:
                    foreach (EventDto @event in _Events)
                    {
                        eventContent.Add(string.Format("{0}的{1}:{2} 超過 Threshold({3})",
                            @event.SYSTEM_NAME, @event.FUNCTION_NAME, @event.IPv4, @event.THRESHOLD));
                    }
                    break;
            }

            return eventContent;
        }

        private IEnumerable<string> ComposeContent(ContentTypeEnum contentType, int notificationType)
        {
            IList<string> systemContent = ComposeSystem(contentType);
            IList<string> eventContent = ComposeEvent(contentType);
            StringBuilder content = new StringBuilder();
            IList<string> contentList = new List<string>();

            if ((systemContent.Count > 0) && !string.IsNullOrEmpty(systemContent[0]))
            {
                switch (contentType)
                {
                    case ContentTypeEnum.HTML:
                        content.Append("警示內容：<br /><br />");
                        content.Append("以下為系統警示：<br />");
                        for (int index = 0; index < systemContent.Count; index++)
                        {
                            if (content.Length + systemContent[index].Length > HTML_MAX)
                            {
                                contentList.Add(content.ToString());
                                content.Clear();
                            }
                            content.Append(systemContent[index]);
                        }

                        break;
                    case ContentTypeEnum.TEXT:
                        //content.Append("警示內容：");
                        content.Append("以下為系統警示：");
                        for (int index = 0; index < systemContent.Count; index++)
                        {
                            if (content.Length + systemContent[index].Length > TEXT_MAX)
                            {
                                contentList.Add(content.ToString());
                                content.Clear();
                            }
                            content.Append(systemContent[index]);
                        }

                        break;
                }
            }

            if ((eventContent.Count > 0) && !string.IsNullOrEmpty(eventContent[0]))
            {
                switch (contentType)
                {
                    case ContentTypeEnum.HTML:
                        content.Append("以下為事件警示：<br />");
                        for (int index = 0; index < eventContent.Count; index++)
                        {
                            if (content.Length + eventContent[index].Length > HTML_MAX)
                            {
                                contentList.Add(content.ToString());
                                content.Clear();
                            }
                            content.Append(eventContent[index]);
                        }

                        break;
                    case ContentTypeEnum.TEXT:
                        content.Append("以下為事件警示：");
                        for (int index = 0; index < eventContent.Count; index++)
                        {
                            if (content.Length + eventContent[index].Length > TEXT_MAX)
                            {
                                contentList.Add(content.ToString());
                                content.Clear();
                            }
                            content.Append(eventContent[index]);
                        }

                        break;
                }
            }

            if (content.Length > 0)
            {
                contentList.Add(content.ToString());
            }

            return contentList;
        }

        private async Task<bool> IsSameContent(IEnumerable<string> contentCollection, string messageType)
        {
            bool result = true;
            string content = string.Join(string.Empty, contentCollection);
            StringBuilder hashContent = new StringBuilder();
            MessageHisDto messageHis = await _HealthService.GetLatestMessageHisAsync(messageType);

            if (content.Length > 0)
            {
                using (SHA256 sha256 = SHA256.Create())
                {
                    byte[] hashByte = sha256.ComputeHash(Encoding.UTF8.GetBytes(content));
                    for (int i = 0; i < hashByte.Length; i++)
                    {
                        hashContent.Append(hashByte[i].ToString("x2"));
                    }
                }

                if ((messageHis == null) || (messageHis.MESSAGE_HASH != hashContent.ToString()))
                {
                    if (messageHis == null)
                    {
                        messageHis = new MessageHisDto();
                    }

                    messageHis.MESSAGE_CONTENT = content;
                    messageHis.MESSAGE_HASH = hashContent.ToString();
                    messageHis.MESSAGE_TYPE = messageType;
                    await _HealthService.CreateMessageHisAsync(messageHis);
                    result = false;
                }
            }

            return result;
        }
    }
}
