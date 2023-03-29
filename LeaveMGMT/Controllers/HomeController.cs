using LeaveMGMT.Models;
using LeaveMGMT.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace LeaveMGMT.Controllers
{
    public class HomeController : Controller
    {
        private IHostingEnvironment _hostingEnvironment;
        private readonly ILeaveServices _leaveServices;
        public IConfiguration Configuration { get; }
        public HomeController(ILeaveServices leaveServices, IHostingEnvironment hostingEnvironment, IConfiguration configuration)
        {
            _hostingEnvironment = hostingEnvironment;
            _leaveServices = leaveServices;
        }

        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult EmpDetailsByEmpCode(int EmpCode)
        {
            var Data = _leaveServices.EmpDetailsByEmpCode(EmpCode).Result;
            return Ok(JsonConvert.SerializeObject(Data));
        }

        public IActionResult GetEmployeeCode()
        {
            var Data = _leaveServices.GetEmployeeCode().Result;
            return Ok(JsonConvert.SerializeObject(Data));
        }


        [HttpPost]
        public async Task<JsonResult> AddLeave(LeaveEntity entity)
        {
            try
            {
                string flodername = "LeaveDoc";
                string webrootpath = _hostingEnvironment.WebRootPath;
                string ProcDocPath = Path.Combine(webrootpath, flodername);
                if (!Directory.Exists(ProcDocPath))
                    Directory.CreateDirectory(ProcDocPath);
                if (entity.DocFile != null)
                {
                    var filename = Path.GetExtension(ContentDispositionHeaderValue.Parse(entity.DocFile.ContentDisposition).FileName.Trim('"'));
                    var timestamp = DateTime.Now.ToString("yyyyMMddHHmmssfff");
                    entity.Doc = "LeaveDoc" + timestamp + "" + filename;
                    using (var stream = new FileStream(Path.Combine(ProcDocPath, entity.Doc), FileMode.Create))
                    {
                        entity.DocFile.CopyTo(stream);
                    }
                }
                int retMsg = _leaveServices.InsertLeave(entity).Result;

                if (retMsg == 1)
                {
                    return Json("Inserted");
                }
                else
                {
                    return Json("Some Error Occured");
                }
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
