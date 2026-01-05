using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

//using Utility;

namespace LSP.MVCClient.Controllers
{
    public class TestController : Controller
    {
        // GET: Test
        public ActionResult Index()
        {
            Utility.MessageQueue.LSP _MessageQueue = new Utility.MessageQueue.LSP();
            System.Text.StringBuilder _Result = new System.Text.StringBuilder();
            for (int i = 0; i < 10; i++)
            {
                var Result = _MessageQueue.SendMessage(new Utility.Model.LogQueueDataModel()
                {
                    //Time = DateTime.Now.AddYears(i % 10).ToString("yyyy-MM-dd HH:mm:ss.fff"), // old
                    //Time = DateTime.Now.AddYears(i % 10),
                    Level = Utility.Model.LogLevel.ERROR,

                    SysCode = "SSO2" ,
                    FunctionCode = "SSO20102" ,
                    ActionName = "Login" ,
                    OpType = 10 ,

                    //Content = "{ \"ErrMsg\":\"密碼錯誤\" }" ,
                    Content = (new System.Web.Script.Serialization.JavaScriptSerializer()).Serialize(new { ErrMsg = "密碼錯誤 !" }),
                    Memo = "testNew" + i.ToString() 
                    
                    //,ClientIP = "10.1.3.254" ,
                    //ClientIP = Utility.Helper.DBHelper.GetRemoteIP() ,

                    //ServerIP = "10.1.2.255"
                    //ServerIP = Utility.Helper.DBHelper.GetLocalIPv4(System.Net.NetworkInformation.NetworkInterfaceType.Ethernet)
                     ,
                    CreateUser = "CEN103"
                });
                //Console.WriteLine("Write Log(" + (i + 1) + ") --> " + (Result ? "OK !" : "Fail !"));
                _Result.Append($"Write Log({i+1}) --> {(Result?"OK":"Fail")} <br/>") ;
            }
            //Console.ReadKey();
            //return View();
            return Content(_Result.ToString());
        }
    }
}