///////////////////////////////////////////////////////////////////////////////////////
//  程式名稱：
//  程式描述：Log Socket Server
///////////////////////////////////////////////////////////////////////////////////////
//  開發人員            日期             版本       備註
//  David             2019-11-08       0.0.1.0     初始版本
///////////////////////////////////////////////////////////////////////////////////////
//  Copyright (C) NEC Taiwan Ltd. 2018-2019.
///////////////////////////////////////////////////////////////////////////////////////
using Dapper;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Utility.EMIC2;
using Utility.MessageQueue;
using Utility.Model;

namespace LSP.API
{
    public partial class Form1 : Form
    {
        private static string serverIP = ConfigurationManager.AppSettings["ServerIP"];
        private static int plngLocalPort = 9600;
        private static mySocketServer mySS = new mySocketServer();
        private static int m_sleepTime = Convert.ToInt32(ConfigurationManager.AppSettings["SleepMilliSecond"]);
        //private static string conn = CryptographyEx.DecryptString(System.Configuration.ConfigurationManager.ConnectionStrings["EMICLOG2DB"].ToString(), "EMIC_SSO2");
        private static string conn = System.Configuration.ConfigurationManager.ConnectionStrings["EMICLOG2DB"].ToString();
        private static bool doqueueFlag = true;

        public Form1()
        {
            InitializeComponent();
        }

        private void notifyIcon1_Click(object sender, EventArgs e)
        {
            //取消再通知欄顯示Icon
            notifyIcon1.Visible = false;
            //顯示在工具列
            this.ShowInTaskbar = true;
            //顯示程式的視窗
            this.Show();
            this.WindowState = FormWindowState.Normal;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            plngLocalPort = int.Parse(ConfigurationManager.AppSettings["ListenPort"]);
            lblConfig.Text = "Port : " + plngLocalPort;

            Task.Factory.StartNew(() => {
                doQueue();
            }, TaskCreationOptions.LongRunning);

            btnStart_Click(sender, e);
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            try
            {
                btnStart.Enabled = false;
                mySS.Start(serverIP, plngLocalPort);
            }
            catch(Exception eStartClick)
            {
                //Console.WriteLine(eStartClick.Message);
                MessageBox.Show($"{eStartClick.Message}", "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //Application.Exit();
                Environment.Exit(-1);
            }
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            mySS.Stop();
            btnStart.Enabled = true;
        }

        private void btnHide_Click(object sender, EventArgs e)
        {
            // 隱藏程式
            this.Hide();
            this.notifyIcon1.Visible = true;
        }

        public void doQueue()
        {
            while (doqueueFlag)
            {
                try
                {
                    if (LogQueue.logQueue.Count > 0)
                    {
                        // write log to db
                        // get Queue data
                        LogQueueDataModel data = null;
                        // remove data from Queue
                        LogQueue.logQueue.TryDequeue(out data);
                        if (data != null)
                        {
                            bool rtn = WriteQueue2DB(data);
                        }
                    }
                }
                catch (Exception e_doQueue)
                {

                }

                // sleep
                SpinWait.SpinUntil(() => false, m_sleepTime);
            }
        }

        /// <summary>
        /// 將log寫入DB(only for log socket server use)
        /// </summary>
        /// <param name="message">log queue data model</param>
        /// <returns>boolean</returns>
        private bool WriteQueue2DB(LogQueueDataModel log)
        {
            string sql = "insert into EMIC2_LOG(ID, TIME, LEVEL, SYS_CODE, FUNCTION_CODE, ACTION_NAME, OP_TYPE, CONTENT, MEMO, CLIENT_IP, SERVER_IP, CREATE_USER) " +
                                    "values(@ID, @TIME, @LEVEL, @SYS_CODE, @FUNCTION_CODE, @ACTION_NAME, @OP_TYPE, @CONTENT, @MEMO, @CLIENT_IP, @SERVER_IP, @CREATE_USER)";
            try
            {
                string id = DateTime.Now.ToString("yyyyMMddHHmmssfff") + Guid.NewGuid();
                using (SqlConnection cn = new SqlConnection(conn))
                {
                    cn.Open();
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.Connection = cn;
                        using (SqlTransaction tran = cn.BeginTransaction(IsolationLevel.ReadCommitted))
                        {
                            try
                            {
                                cmd.Transaction = tran;
                                cmd.CommandText = sql;
                                cmd.Parameters.AddWithValue("@ID", id);
                                //cmd.Parameters.AddWithValue("@TIME", log.Time);
                                cmd.Parameters.AddWithValue("@TIME", log.Time.HasValue ? log.Time.Value : DateTime.Now);
                                cmd.Parameters.AddWithValue("@LEVEL", log.Level);
                                cmd.Parameters.AddWithValue("@SYS_CODE", string.IsNullOrEmpty(log.SysCode) ? "" : log.SysCode);
                                cmd.Parameters.AddWithValue("@FUNCTION_CODE", string.IsNullOrEmpty(log.FunctionCode) ? "" : log.FunctionCode);
                                cmd.Parameters.AddWithValue("@ACTION_NAME", string.IsNullOrEmpty(log.ActionName) ? "" : log.ActionName);
                                cmd.Parameters.AddWithValue("@OP_TYPE", log.OpType);
                                cmd.Parameters.AddWithValue("@CONTENT", string.IsNullOrEmpty(log.Content) ? "" : log.Content);
                                cmd.Parameters.AddWithValue("@MEMO", string.IsNullOrEmpty(log.Memo) ? "" : log.Memo);
                                cmd.Parameters.AddWithValue("@CLIENT_IP", string.IsNullOrEmpty(log.ClientIP) ? "" : log.ClientIP);
                                cmd.Parameters.AddWithValue("@SERVER_IP", string.IsNullOrEmpty(log.ServerIP) ? "" : log.ServerIP);
                                cmd.Parameters.AddWithValue("@CREATE_USER", string.IsNullOrEmpty(log.CreateUser) ? "" : log.CreateUser);
                                cmd.ExecuteNonQuery();
                                tran.Commit();
                                Console.WriteLine(log.Memo);
                            }
                            catch (Exception ex)
                            {
                                tran.Rollback();
                                return false;
                            }
                        }
                    }
                }
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            mySS.Stop();
            doqueueFlag = false;
        }
    }
}
