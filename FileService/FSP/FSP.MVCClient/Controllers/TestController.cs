using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using System.Configuration;

//using Utility;

namespace LSP.MVCClient.Controllers
{
    public class TestController : Controller
    {
        // GET: Test
        public ActionResult Index()
        {
             
            byte[] XlsSrc = System.IO.File.ReadAllBytes(Server.MapPath("~/App_Data/test.xlsx"));
            var fileService = new Utility.EMIC2.FileService();
            byte[] DestOds = fileService.ConvertTo(ConfigurationManager.AppSettings["OdsUrl"], XlsSrc).Data;

            //_Result.Append($"Write Log({i+1}) --> {(Result?"OK":"Fail")} <br/>") ;
            //return View();
            //return Content(XlsSrc.Length.ToString() +  " ~ "+ DestOds.Length.ToString());
            return File(DestOds, "application/vnd.oasis.opendocument.spreadsheet","Test.ods");
             
            
            /*
            byte[] DocSrc = System.IO.File.ReadAllBytes(Server.MapPath("~/App_Data/test.docx"));
            var fileService = new Utility.EMIC2.FileService();
            byte[] DestOdt = fileService.ConvertTo(ConfigurationManager.AppSettings["OdtUrl"], DocSrc).Data;

            //_Result.Append($"Write Log({i+1}) --> {(Result?"OK":"Fail")} <br/>") ;
            //return View();
            //return Content(XlsSrc.Length.ToString() +  " ~ "+ DestOds.Length.ToString());
            return File(DestOdt, "application/vnd.oasis.opendocument.text", "Test.odt");
            */
        }
    }
}