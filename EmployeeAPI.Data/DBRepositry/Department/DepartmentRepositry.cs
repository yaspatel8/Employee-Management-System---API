using Dapper;
using EmployeeAPI.Common;
using EmployeeAPI.Common.Helper;
using EmployeeAPI.Model.Model;
using EmployeeAPI.Model.Model.Export;
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
           
                DynamicParameters param = new();
                param.Add("@DepartmentId", department.DepartmentId);
                param.Add("@DepartmentName", department.DepartmentName);
                param.Add("@UpdatedBy", department.UpdatedBy);
                param.Add("@CreatedBy", department.CreatedBy);
                var result = await _db.QueryFirstOrDefaultAsync<DbResponseModel>(StoredProcedure.SaveDepartment, param, commandType: CommandType.StoredProcedure);
                return result;
           
        }


        public async Task<List<DepartmentModel>> GetAllDepartment(CommonPaginationModel model)
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
            
                DynamicParameters param = new();
                param.Add("@DepartmentId", id);
                var result = await _db.QueryFirstOrDefaultAsync<DbResponseModel>(StoredProcedure.DeleteDepartment, param, commandType: CommandType.StoredProcedure);
                return result;
           
        }
        public async Task<List<DepartmentModel>> GetDepartment()
        {
            
                var result = await _db.QueryAsync<DepartmentModel>(StoredProcedure.SP_Department_Get, commandType: CommandType.StoredProcedure);
                return result.ToList();
            
        }
        public async Task<ApiResponseModel> UpdateDepartmentStatus(int departmentId, bool isActive, int updatedBy)
        {
            
                DynamicParameters param = new();
                param.Add("@DepartmentId", departmentId);
                param.Add("@IsActive", isActive);
                param.Add("@UpdatedBy", updatedBy);
                var result = await _db.QueryFirstOrDefaultAsync<ApiResponseModel>(StoredProcedure.UpdateDepartmentStatus, param, commandType: CommandType.StoredProcedure);
                return result;

        }
        public async Task<List<DepartmentExportModel>> ExportDepartments(List<int> ids)
        {
            DataTable dt = new();
            dt.Columns.Add("Id", typeof(int));
            foreach (var id in ids)
            {
                dt.Rows.Add(id);
            }
            DynamicParameters param = new();
            param.Add("@DepartmentIds", dt.AsTableValuedParameter("dbo.IdListType"));
            var result = await _db.QueryAsync<DepartmentExportModel>(StoredProcedure.ExportDepartments, param, commandType: CommandType.StoredProcedure);
            return result.ToList();
        }
    }
}
