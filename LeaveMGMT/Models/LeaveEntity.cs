using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LeaveMGMT.Models
{
    public class LeaveEntity
    {
        public int EmpId { get; set; } = 0;
        public int EmpCode { get; set; } = 0;
        public string EmpName { get; set; } = null;
        public string Designation { get; set; } = null;
        public string LeaveType { get; set; } = null;
        public string Doc { get; set; } = null;
        public IFormFile DocFile { get; set; } = null;
        public string FromDate { get; set; } = null;
        public string ToDate { get; set; } = null;
        public string Reason { get; set; } = null;
        public int TML { get; set; } = 0;
        public int TCL { get; set; } = 0;
        public int MLApp { get; set; } = 0;
        public int CLApp { get; set; } = 0;
    }
}
