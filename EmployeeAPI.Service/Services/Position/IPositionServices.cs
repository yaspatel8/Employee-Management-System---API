using EmployeeAPI.Common;
using EmployeeAPI.Model.Model;

namespace EmployeeAPI.Service.Services.Position
{
    public interface IPositionServices
    {
        Task<ApiResponseModel> SavePosition(PositionModel position);
        Task<List<PositionModel>> GetAllPosition(CommonPaginationModel model);
        Task<ApiResponseModel> DeletePosition(int id);
        Task<List<PositionModel>> GetPositionActive();
        Task<ApiResponseModel> UpdatePositionStatus(int positionId, bool isActive, int updatedBy);
    }
}
