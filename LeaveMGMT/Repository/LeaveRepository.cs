using Dapper;
using LeaveMGMT.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace LeaveMGMT.Repository
{
    public class LeaveRepository : BaseRepository, ILeaveRepository
    {
        public LeaveRepository(IConfiguration configuration) : base(configuration)
        {

        }
        public async Task<List<LeaveEntity>> EmpDetailsByEmpCode(int EmpCode)
        {
            try
            {
                var cn = CreateConnection();
                if (cn.State == ConnectionState.Closed) cn.Open();
                DynamicParameters param = new DynamicParameters();
                param.Add("@EmpCode", EmpCode);
                param.Add("@action", "SelectEmpDetailsByEmpCode");
                var lstste = await cn.QueryAsync<LeaveEntity>("USP_LeaveManagement", param, commandType: CommandType.StoredProcedure);
                return lstste.ToList();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<List<LeaveEntity>> GetEmployeeCode()
        {
            try
            {
                var cn = CreateConnection();
                if (cn.State == ConnectionState.Closed) cn.Open();
                DynamicParameters param = new DynamicParameters();
                param.Add("@action", "GetEmployeeCode");
                var lstste = await cn.QueryAsync<LeaveEntity>("USP_LeaveManagement", param, commandType: CommandType.StoredProcedure);
                return lstste.ToList();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<int> InsertLeave(LeaveEntity LE)
        {
            try
            {
                var cn = CreateConnection();
                if (cn.State == ConnectionState.Closed) cn.Open();
                DynamicParameters param = new DynamicParameters();
                param.Add("@EmpId", LE.EmpCode);
                param.Add("@LeaveType", LE.LeaveType);
                param.Add("@Doc", LE.Doc);
                param.Add("@FromDate", LE.FromDate);
                param.Add("@ToDate", LE.ToDate);
                param.Add("@Reason", LE.Reason);
                param.Add("@Action", "InsertLeave");
                var x = await cn.ExecuteAsync("USP_LeaveManagement", param, commandType: CommandType.StoredProcedure);
                cn.Close();
                return x;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
