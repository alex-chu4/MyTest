///////////////////////////////////////////////////////////////////////////////////////
//  程式名稱：
//  程式描述：Log Queue Data Model
///////////////////////////////////////////////////////////////////////////////////////
//  開發人員            日期             版本       備註
//  David             2019-11-09       0.0.1.0     初始版本
//  David             2019-11-20       0.0.2.0     新增檢查LOG_Config，決定要不要送log
///////////////////////////////////////////////////////////////////////////////////////
//  Copyright (C) NEC Taiwan Ltd. 2018-2019.
///////////////////////////////////////////////////////////////////////////////////////
using EMIC2.Models.Interface;
using EMIC2.Models.Repository;
using Newtonsoft.Json;
using System;
using System.Configuration;
using System.Net.Sockets;
using Utility.Model;
using EMIC2.Models;
using System.Data.SqlClient;
using System.Data;
using Utility.EMIC2;
using System.Net.NetworkInformation;
using Utility.Helper;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace Utility.MessageQueue
{
    public class LSP
    {
        private IRepository<LOG_CONFIG> logconfigRepository = new GenericRepository<LOG_CONFIG>();

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
            if (IsWriteLog(log.SysCode, log.Level) == false)
                return false;

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
                string _ServerIP = DBHelper.GetLocalIPv4(NetworkInterfaceType.Ethernet);
                log.ServerIP = string.IsNullOrEmpty(log.ServerIP) ? (string.IsNullOrEmpty(_ServerIP) ? DBHelper.GetLocalIPv4(NetworkInterfaceType.Wireless80211) : _ServerIP) : log.ServerIP.Trim();
                log.ClientIP = string.IsNullOrEmpty(log.ClientIP) ? DBHelper.GetRemoteIP() : log.ClientIP.Trim();

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
                    //byte[] bytes = new byte[tcpClient.ReceiveBufferSize];
                    byte[] bytes = new byte[512];
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

                //bRtn = true;
            }

            return bRtn;
        }

        private bool IsWriteLog(string sysCode, LogLevel level)
        {
            int max_level = 0;
            try
            {
                List<LOG_CONFIG> config_all = logconfigRepository.GetAll().Where(x => x.ID.ToUpper().Equals("ALL") || x.ID.ToUpper().Equals(sysCode.ToUpper())).ToList();
                foreach (LOG_CONFIG config in config_all)
                {
                    // 取最大值
                    max_level = Math.Max(max_level, Convert.ToInt16(config.LOG_LEVEL));
                }

                if (max_level >= 99 || Convert.ToInt16(level) < max_level)
                {
                    return false;
                }
            }
            catch (Exception)
            {
                return false;
            }

            return true;
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
}
