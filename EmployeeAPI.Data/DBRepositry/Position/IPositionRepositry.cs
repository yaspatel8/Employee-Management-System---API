using EmployeeAPI.Common;
using EmployeeAPI.Model.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeAPI.Data.DBRepositry.Position
{
    public interface IPositionRepositry
    {
        Task<ApiResponseModel> SavePosition(PositionModel position);
        Task<List<PositionModel>> GetAllPosition(CommonPaginationModel model);
        Task<ApiResponseModel> DeletePosition(int id);
        Task<List<PositionModel>> GetPositionActive();
        Task<ApiResponseModel> UpdatePositionStatus(int positionId, bool isActive, int updatedBy);
    }
}
