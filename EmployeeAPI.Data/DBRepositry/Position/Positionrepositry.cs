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

namespace EmployeeAPI.Data.DBRepositry.Position
{
    public class Positionrepositry : IPositionRepositry
    {
        private readonly IDbConnection _db;
        public Positionrepositry(IDbConnection db)
        {
            _db = db;
        }

        public async Task<ApiResponseModel> SavePosition(PositionModel position)
        {
            DynamicParameters param = new();
            param.Add("@PositionId", position.PositionId);
            param.Add("@PositionName", position.PositionName);
            param.Add("@Level", position.Level);
            param.Add("@CreatedBy", position.CreatedBy);
            param.Add("@UpdatedBy", position.UpdatedBy);

            var result = await _db.QueryFirstOrDefaultAsync<ApiResponseModel>(StoredProcedure.AddOrUpdatePosition, param, commandType: CommandType.StoredProcedure);
            return result;
        }
        public async Task<List<PositionModel>> GetAllPosition(CommonPaginationModel model)
        {
            DynamicParameters param = new();
            param.Add("@Search", model.SearchText);
            param.Add("@SortColumn", model.SortColumn);
            param.Add("@SortOrder", model.SortOrder);
            param.Add("@PageNumber", model.PageNumber);
            param.Add("@PageSize", model.PageSize);
            var result = await _db.QueryAsync<PositionModel>(StoredProcedure.GetAllPosition, param, commandType: CommandType.StoredProcedure);
            return result.ToList();
        }
        public async Task<ApiResponseModel> DeletePosition(int id)
        {
            DynamicParameters param = new();
            param.Add("@PositionId", id);
            var result = await _db.QueryFirstOrDefaultAsync<ApiResponseModel>(StoredProcedure.DeletePosition, param, commandType: CommandType.StoredProcedure);
            return result;
        }
        public async Task<List<PositionModel>> GetPositionActive()
        {
            var result = await _db.QueryAsync<PositionModel>(StoredProcedure.GetAllActivePosition, commandType: CommandType.StoredProcedure);
            return result.ToList();
        }
        public async Task<ApiResponseModel> UpdatePositionStatus(int positionId, bool isActive, int updatedBy)
        {
            DynamicParameters param = new();
            param.Add("@PositionId", positionId);
            param.Add("@IsActive", isActive);
            param.Add("@UpdatedBy", updatedBy);
            var result = await _db.QueryFirstOrDefaultAsync<ApiResponseModel>(StoredProcedure.UpdatePositionStatus, param, commandType: CommandType.StoredProcedure);
            return result;
        }
    }
}
