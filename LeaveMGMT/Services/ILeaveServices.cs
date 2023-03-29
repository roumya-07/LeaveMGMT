﻿using LeaveMGMT.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LeaveMGMT.Services
{
    public interface ILeaveServices
    {
        public Task<List<LeaveEntity>> EmpDetailsByEmpCode(int EmpCode);
        public Task<int> InsertLeave(LeaveEntity LE);
        public Task<List<LeaveEntity>> GetEmployeeCode();
    }
}
