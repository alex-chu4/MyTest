using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System;
using System.Configuration;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Net.NetworkInformation;

namespace LSP.Client
{
    public enum LogLevel : short
    {
        INFO = 1,
        ERROR = 9
    }

    public class LogQueueDataModel
    {
        /// <summary>
        /// Log發生時間
        /// </summary>
        //public DateTime Time { get; set; }
        public DateTime? Time { get; set; }

        /// <summary>
        /// 處理狀態
        /// </summary>
        public LogLevel Level { get; set; }

        /// <summary>
        /// 系統代號
        /// </summary>
        public string SysCode { get; set; }

        /// <summary>
        /// 功能代號
        /// </summary>
        public string FunctionCode { get; set; }

        /// <summary>
        /// 動作名稱
        /// </summary>
        public string ActionName { get; set; }

        /// <summary>
        /// 操作
        /// </summary>
        public int OpType { get; set; }

        /// <summary>
        /// 內容
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// 註記
        /// </summary>
        public string Memo { get; set; }

        /// <summary>
        /// 使用者IP
        /// </summary>
        public string ClientIP { get; set; }

        /// <summary>
        /// 主機IP
        /// </summary>
        public string ServerIP { get; set; }

        /// <summary>
        /// 操作使用者
        /// </summary>
        public string CreateUser { get; set; }
    }

    /*
    /// <summary>
    /// OPTYPE.
    /// </summary>
    public enum ENUM_OPTYPE
    {
        PAGELOAD = 0,   // 頁面載入
        FIND = 10,      // 查詢
        CREATE = 11,    // 新增
        UPDATE = 12,    // 編輯
        DELETE = 13,    // 刪除
        UPLOAD = 14,    // 上傳
        EXPORT = 15,    // 匯出
        ASSIGN = 30,    // 指派
        REPLAY = 60,    // 回覆
        OTHERS = 99,    // 其他
    }
    */

    public class LSP
    {
        public static string GetLocalIPv4(NetworkInterfaceType _type)
        {
            string output = "";
            foreach (NetworkInterface item in NetworkInterface.GetAllNetworkInterfaces())
            {
                if (item.NetworkInterfaceType == _type && item.OperationalStatus == OperationalStatus.Up)
                {
                    foreach (UnicastIPAddressInformation ip in item.GetIPProperties().UnicastAddresses)
                    {
                        if (ip.Address.AddressFamily == AddressFamily.InterNetwork)
                        {
                            output = ip.Address.ToString();
                        }
                    }
                }
            }

            return output;
        }
        /*
        public static string GetRemoteIP()
        {
            try
            {
                if (HttpContext.Current.Request.ServerVariables["HTTP_VIA"] != null)
                {
                    // using proxy
                    return HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"].ToString(); // Return real client IP.
                }
                else
                {
                    // not using proxy or can’t get the Client IP
                    return HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"].ToString(); //While it can’t get the Client IP, it will return proxy IP.
                }
            }
            catch (Exception) { }

            return "LocalHost";
        }
        */

        /// <summary>
        /// 送封包到log server
        /// </summary>
        /// <param name="log">Log Queue Data Model</param>
        /// <returns>boolean</returns>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public bool SendMessage(LogQueueDataModel log)
        {
            //return false;

            bool bRtn = false;
            // check log is on/off
            //if (IsWriteLog(log.SysCode, log.Level) == false)
            //    return false;

            TcpClient tcpClient = default(TcpClient);
            NetworkStream stream = default(NetworkStream);
            string socketServerIP = ConfigurationManager.AppSettings["LogSocketServerIP"];
            int port = ConfigurationManager.AppSettings["LogSocketServerPort"] == null ? 0 : int.Parse(ConfigurationManager.AppSettings["LogSocketServerPort"]);
            if (string.IsNullOrEmpty(socketServerIP) || port == 0)
            {
                return false;
            }

            try
            {
                //log.Time = string.IsNullOrEmpty(log.Time) ? DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff") : log.Time.Trim();
                log.Time = log.Time.HasValue ? log.Time.Value : DateTime.Now;
                string _ServerIP =GetLocalIPv4(NetworkInterfaceType.Ethernet);
                log.ServerIP = string.IsNullOrEmpty(log.ServerIP) ? (string.IsNullOrEmpty(_ServerIP) ?GetLocalIPv4(NetworkInterfaceType.Wireless80211) : _ServerIP) : log.ServerIP.Trim();
                //log.ClientIP = DBHelper.GetRemoteIP();

                tcpClient = new TcpClient();
                tcpClient.Connect(socketServerIP, port); // 1.設定 IP:Port 2.連線至伺服器
                stream = tcpClient.GetStream();
                // Set a 3000 millisecond timeout for reading.
                stream.ReadTimeout = 3000;

                byte[] message = GetMessage(log);
                if (stream.CanWrite)
                {
                    stream.Write(message, 0, message.Length);
                    stream.Flush();
                }

                if (stream.CanRead)
                {
                    byte[] bytes = new byte[tcpClient.ReceiveBufferSize];
                    //byte[] bytes = new byte[512];
                    int ReadCnt = stream.Read(bytes, 0, bytes.Length);
                    byte[] _RtnMsg = new byte[ReadCnt];
                    Array.Copy(bytes, _RtnMsg, ReadCnt);
                    bytes = null;
                    string RtnMsg = System.Text.Encoding.UTF8.GetString(_RtnMsg);
                    _RtnMsg = null;
                    if (!RtnMsg.ToUpper().StartsWith("OK"))
                    {
                        throw new Exception(RtnMsg);
                    }
                }
                bRtn = true;
            }
            catch (Exception ex)
            {
                bRtn = false;
            }
            finally
            {
                try
                {
                    stream.Close();
                }
                catch { }

                try
                {
                    tcpClient.Close();
                }
                catch { }                
            }

            return bRtn;
        }


        /// <summary>
        /// send message to log server
        /// </summary>
        /// <returns>log queue data model</returns>
        private byte[] GetMessage(LogQueueDataModel log)
        {
            // 將log queue data model 轉成json string
            string jsonString = JsonConvert.SerializeObject(log);
            byte[] dataB = System.Text.Encoding.Default.GetBytes(jsonString);

            // 計算訊息長度
            Int32 length = dataB.Length;

            byte[] lenB = new byte[4];

            lenB[0] = (byte)(length >> 24);
            lenB[1] = (byte)(length >> 16);
            lenB[2] = (byte)(length >> 8);
            lenB[3] = (byte)length;

            // 產生封包
            byte[] rtnB = new byte[dataB.Length + 4];
            lenB.CopyTo(rtnB, 0);
            dataB.CopyTo(rtnB, 4);

            lenB = null;
            dataB = null;

            return rtnB;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            bool Result = false;
            //for (int i = 0; i < 500; i++)
            while (true)
            {
                Console.WriteLine("請輸入寫入次數 (0: exit)");
                string input = Console.ReadLine();
                int TestCnt = int.Parse(input);
                if (TestCnt == 0) break;
                for (int i = 0; i < TestCnt; i++)
                {
                    Result = new LSP().SendMessage(new LogQueueDataModel()
                    {
                        //Time = DateTime.Now.AddYears(i % 10).ToString("yyyy-MM-dd HH:mm:ss.fff"), // old
                        Time = DateTime.Now.AddYears(i % 10),

                        Level = LogLevel.ERROR
                         ,
                        SysCode = "SSO"
                         ,
                        FunctionCode = "SSO20102"
                        ,
                        ActionName = "Login"
                         ,
                        OpType = 10
                         ,
                        Content = "{ \"ErrMsg\":\"密碼錯誤\" }"
                         ,
                        Memo = "Consoletest" + i.ToString()
                         ,
                        ClientIP = "10.1.3.159"
                         // ,
                         //ServerIP = "10.1.2.164"
                         ,
                        CreateUser = "CEN103"
                    });
                    Console.WriteLine("Write Log(" + (i + 1) + ") --> " + (Result ? "OK !" : "Fail !"));
                }
            }
            //Console.ReadKey();
        }


 
    }
}
