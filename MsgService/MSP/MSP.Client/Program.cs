using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using MSP.Service.Models;
using Newtonsoft.Json;

namespace MSP.Client
{
    class Program
    {
        static char[] SEPERATOR = new char[] { ',', ';' };

        static void Main(string[] args)
        {
            MessageModel message = new MessageModel();
            message.Oid = ConfigurationManager.AppSettings["Oid"];
            message.Subject = ConfigurationManager.AppSettings["Subject"];
            message.Content = ConfigurationManager.AppSettings["Content"];
            message.FileBase = ConfigurationManager.AppSettings["FileBase"];
            message.MimeType = ConfigurationManager.AppSettings["MimeType"];
            message.UserType = "GEN";

            if (!string.IsNullOrEmpty(message.FileBase))
            {
                if (File.Exists(message.FileBase))
                {
                    FileInfo fileInfo = new FileInfo(message.FileBase);
                    byte[] buffer = File.ReadAllBytes(message.FileBase);
                    message.FileBase = Convert.ToBase64String(buffer);
                    message.MimeType = fileInfo.Extension.Substring(1);
                }
            }

            if (!string.IsNullOrEmpty(ConfigurationManager.AppSettings["EmailList"]))
            {
                IList<EmailModel> emailList = new List<EmailModel>();
                string[] emailArray = ConfigurationManager.AppSettings["EmailList"].Split(SEPERATOR);
                Array.ForEach(emailArray, p => emailList.Add(new EmailModel { Email = p }));
                message.EmailList = emailList;
            }

            if (!string.IsNullOrEmpty(ConfigurationManager.AppSettings["MobileList"]))
            {
                IList<MobileModel> mobileList = new List<MobileModel>();
                string[] mobileArray = ConfigurationManager.AppSettings["MobileList"].Split(SEPERATOR);
                Array.ForEach(mobileArray, p => mobileList.Add(new MobileModel { Mobile = p }));
                message.MobileList = mobileList;
            }

            if (!string.IsNullOrEmpty(ConfigurationManager.AppSettings["FaxList"]))
            {
                IList<FaxModel> faxList = new List<FaxModel>();
                string[] faxArray = ConfigurationManager.AppSettings["FaxList"].Split(SEPERATOR);
                Array.ForEach(faxArray, p => faxList.Add(new FaxModel { Fax = p }));
                message.FaxList = faxList;
            }

            string jsonString = JsonConvert.SerializeObject(message);

            Task.Run(async () =>
                {
                    using (HttpClient httpClient = new HttpClient())
                    {
                        if (!string.IsNullOrEmpty(ConfigurationManager.AppSettings["SendEmicMailUrl"]))
                        {
                            using (HttpContent httpContent = new StringContent(jsonString, Encoding.UTF8, "application/json"))
                            {
                                HttpResponseMessage response = await httpClient.PostAsync(ConfigurationManager.AppSettings["SendEmicMailUrl"], httpContent);
                                Console.WriteLine(await response.Content.ReadAsStringAsync());
                            }
                        }

                        if (!string.IsNullOrEmpty(ConfigurationManager.AppSettings["SendEmicSmsUrl"]))
                        {
                            using (HttpContent httpContent = new StringContent(jsonString, Encoding.UTF8, "application/json"))
                            {
                                HttpResponseMessage response = await httpClient.PostAsync(ConfigurationManager.AppSettings["SendEmicSmsUrl"], httpContent);
                                Console.WriteLine(await response.Content.ReadAsStringAsync());
                            }
                        }

                        if (!string.IsNullOrEmpty(ConfigurationManager.AppSettings["SendEmicFaxUrl"]))
                        {
                            using (HttpContent httpContent = new StringContent(jsonString, Encoding.UTF8, "application/json"))
                            {
                                HttpResponseMessage response = await httpClient.PostAsync(ConfigurationManager.AppSettings["SendEmicFaxUrl"], httpContent);
                                Console.WriteLine(await response.Content.ReadAsStringAsync());
                            }
                        }
                    }
                }).Wait();
        }
    }
}
