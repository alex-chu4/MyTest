///////////////////////////////////////////////////////////////////////////////////////
//  程式名稱：
//  程式描述：Log Socket Server
///////////////////////////////////////////////////////////////////////////////////////
//  開發人員            日期             版本       備註
//  David             2019-11-08       0.0.1.0     初始版本
///////////////////////////////////////////////////////////////////////////////////////
//  Copyright (C) NEC Taiwan Ltd. 2018-2019.
///////////////////////////////////////////////////////////////////////////////////////
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Configuration;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Utility.Model;

namespace LSP.API
{
    public class mySocketServer
    {
        public static bool pbStop = false;
        private Socket tcpLstn;
        private static Thread LstnTheard;
        private IPEndPoint myServer;
        private const int m_nSocketBuffersSize = 512;

        public void Start(string strIPAddress, int plngLocalPort)
        {
            myServer = new IPEndPoint(IPAddress.Parse(strIPAddress), plngLocalPort);
            tcpLstn = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            LstnTheard = new Thread(new ThreadStart(Listener));
            LstnTheard.Start();
            pbStop = false;
        }

        private void Listener()
        {
            try
            {
                tcpLstn.Bind(myServer);
                tcpLstn.Listen(int.MaxValue);

                while (!pbStop)
                {
                    Socket tcpclient = tcpLstn.Accept();
                    if (tcpclient.Connected)
                    {
                        Task.Factory.StartNew((x) =>
                        {
                            HandleClientComm(x);
                        }, tcpclient);
                    }
                }
            }
            catch(Exception eListener)
            {
                if (eListener.Message.IndexOf("位址") >= 0)
                {
                    //Console.WriteLine(eListener.Message);
                    MessageBox.Show($"{myServer.Address}:{myServer.Port} ---> {eListener.Message}", "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    //Application.Exit();
                    Environment.Exit(-1);
                }            
            }
        }

        public void Stop()
        {
            pbStop = true;

            if (tcpLstn != null)
            {
                tcpLstn.Close();
                LstnTheard.Abort();
            }
            
        }

        private void HandleClientComm(object client)
        {
            Socket tcpClient = (Socket)client;
            NetworkStream clientStream = null;
            try
            {
                clientStream = new NetworkStream(tcpClient);

                string RcvData = string.Empty;

                if (clientStream.CanRead)
                {
                    byte[] lenB = new byte[4];
                    int len = clientStream.Read(lenB, 0, 4);
                    if (BitConverter.IsLittleEndian)
                        Array.Reverse(lenB);
                    int nLen = BitConverter.ToInt32(lenB, 0);

                    int byteRead;
                    byte[] byteFullMsg = new byte[nLen + m_nSocketBuffersSize];
                    Int32 nOffset = 0;
                    do
                    {
                        byte[] message = new byte[m_nSocketBuffersSize];
                        byteRead = 0;
                        try
                        {
                            byteRead = clientStream.Read(message, 0, m_nSocketBuffersSize);
                        }
                        catch
                        {
                            // A socket error has occured
                            break;
                        }

                        message.CopyTo(byteFullMsg, nOffset);
                        nOffset += byteRead;
                        message = null;
                        if (nOffset >= nLen) break;
                    } while (clientStream.DataAvailable);

                    byte[] byteMsg = new byte[nLen];
                    Array.Copy(byteFullMsg, 0, byteMsg, 0, nLen);
                    byteFullMsg = null;

                    // receive data
                    String recvData = System.Text.Encoding.Default.GetString(byteMsg);
                    
                    LogQueueDataModel qData = JsonConvert.DeserializeObject<LogQueueDataModel>(recvData);
                    LogQueue.logQueue.Enqueue(qData);
                    // release data memory
                    byteMsg = null;
                }
   
                if (clientStream.CanWrite)
                {
                    byte[] buffer = System.Text.Encoding.Default.GetBytes("OK");
                    clientStream.Write(buffer, 0, buffer.Length);
                    buffer = null;
                }
            }
            catch (Exception ex)
            {
                try
                {
                    if (clientStream.CanWrite)
                    {
                        byte[] buffer = System.Text.Encoding.Default.GetBytes("FAIL");
                        clientStream.Write(buffer, 0, buffer.Length);
                        buffer = null;
                    }
                }
                catch
                {

                }
            }
            finally
            {
                // release resource
                if (clientStream != null)
                    clientStream.Close();

                if (tcpClient != null)
                {
                    try
                    {
                        tcpClient.Shutdown(SocketShutdown.Both);
                    }
                    catch (Exception)
                    { }
                    tcpClient.Close();
                }

                GC.Collect();
                Thread.CurrentThread.Abort();
            }
        }
    }
}
