using Dapper;
using EmployeeAPI.Common;
using EmployeeAPI.Common.Helper;
using EmployeeAPI.Model.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeAPI.Data.DBRepositry.Department
{
    public class DepartmentRepositry : IDepartmentRepositry
    {
        private readonly IDbConnection _db;

        public DepartmentRepositry(IDbConnection db)
        {
            _db = db;
        }

        public async Task<DbResponseModel> SaveDepartment(DepartmentModel department)
        {
            try
            {
                DynamicParameters param = new();
                param.Add("@DepartmentId", department.DepartmentId);
                param.Add("@DepartmentName", department.DepartmentName);
                var result = await _db.QueryFirstOrDefaultAsync<DbResponseModel>(StoredProcedure.SaveDepartment, param, commandType: CommandType.StoredProcedure);
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }



        public async Task<List<DepartmentModel>> GetAllDepartment(CommonPaginationModel model)
        {
            try
            {
                DynamicParameters param = new();
                param.Add("@SearchText", model.SearchText);
                param.Add("@PageNumber", model.PageNumber);
                param.Add("@PageSize", model.PageSize);
                param.Add("@SortColumn", model.SortColumn);
                param.Add("@SortOrder", model.SortOrder);
                var result = await _db.QueryAsync<DepartmentModel>(StoredProcedure.GetAllDepartment, param, commandType: CommandType.StoredProcedure);
                return result.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        //public async Task<DepartmentModel?> GetDepartmentById(int id)
        //{
        //    try
        //    {
        //        DynamicParameters param = new();
        //        param.Add("@DepartmentId", id);
        //        var result = await _db.QueryFirstOrDefaultAsync<DepartmentModel>(StoredProcedure.GetDepartmentById, param, commandType: CommandType.StoredProcedure);
        //        return result;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception(ex.Message);
        //    }
        //}

        public async Task<DbResponseModel> DeleteDepartment(int id)
        {
            try
            {
                DynamicParameters param = new();
                param.Add("@DepartmentId", id);
                var result = await _db.QueryFirstOrDefaultAsync<DbResponseModel>(StoredProcedure.DeleteDepartment, param, commandType: CommandType.StoredProcedure);
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<List<DepartmentModel>> GetDepartment()
        {
            try
            {
                var result = await _db.QueryAsync<DepartmentModel>(StoredProcedure.SP_Department_Get, commandType: CommandType.StoredProcedure);
                return result.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
