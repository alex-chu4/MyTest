using EMIC2.Result;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Configuration;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Utility.Dao;

namespace Utility.EMIC2
{
    public class SendEMail
    {
        public IResult SendMail(EmailModelDao data, bool bWait = false)
        {
            IResult result = new Result(false);
            string emailUrl = ConfigurationManager.AppSettings["SendEmicMailUrl"];
            if (string.IsNullOrEmpty(emailUrl))
            {
                result.Message = "Web.Config沒有設定SendEmicMailUrl";
                return result;
            }

            MessageModelDao msg = new MessageModelDao();
            msg.EmailList = new List<EMAIL>();
            msg.Oid = data.Oid;  // this.TokenUserInfo.ORG_OID_P;    //TODO 民眾確認OID
            msg.Subject = data.Subject;  // 主旨
            msg.Content = $" <![CDATA[{data.Content}]]>";   // 內容
            msg.UserType = "GEN";   // this.TokenUserInfo.ACCOUNT_TYPE;  //GEN固定寫死
            foreach (string mailStr in data.EmailList)
            {
                msg.EmailList.Add(new EMAIL { Email = mailStr });
            }

            string jsonString = JsonConvert.SerializeObject(msg);
            if (bWait)
            {
                Task.Run(async () =>
                {
                    using (HttpClient httpClient = new HttpClient())
                    {
                        using (HttpContent httpContent = new StringContent(jsonString, Encoding.UTF8, "application/json"))
                        {
                            HttpResponseMessage response = await httpClient.PostAsync(emailUrl, httpContent);
                            var res = await response.Content.ReadAsStringAsync();
                            var resMsg = JsonConvert.DeserializeObject<ResponseMessage>(res);
                            result.Success = (resMsg.ReturnCode == "0") ? true : false;
                        }
                    }
                }).Wait();
            }
            else
            {
                Task.Run(async () =>
                {
                    using (HttpClient httpClient = new HttpClient())
                    {
                        using (HttpContent httpContent = new StringContent(jsonString, Encoding.UTF8, "application/json"))
                        {
                            HttpResponseMessage response = await httpClient.PostAsync(emailUrl, httpContent);
                            var res = await response.Content.ReadAsStringAsync();
                            var resMsg = JsonConvert.DeserializeObject<ResponseMessage>(res);
                            result.Success = (resMsg.ReturnCode == "0") ? true : false;
                        }
                    }
                });
            }

            return result;
        }

        // ************************************************************************
        public static string GetSubString(string str, int beginIndex, int length)
        {
            if (beginIndex < 0)
            {
                return string.Empty;
            }

            if (length < 0)
            {
                return string.Empty;
            }

            if (str.Length > beginIndex)
            {
                if (beginIndex + length <= str.Length)
                {
                    return str.Substring(beginIndex, length);
                }
                else
                {
                    return str.Substring(beginIndex, str.Length - beginIndex);
                }
            }

            return string.Empty;
        }

        // ************************************************************************
        public static string GetSubString(string str, int beginIndex)
        {
            if (str.Length > beginIndex)
            {
                return str.Substring(beginIndex);
            }

            return string.Empty;
        }

        // ************************************************************************
        public static int GetPercent(long val, int total)
        {
            int rtn = (int)(val * 100 / total);
            rtn = rtn > 100 ? 100 : rtn;
            return rtn;
        }
    }
}
