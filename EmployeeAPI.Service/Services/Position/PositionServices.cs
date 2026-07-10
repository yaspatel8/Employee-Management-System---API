using EmployeeAPI.Common;
using EmployeeAPI.Data.DBRepositry.Position;
using EmployeeAPI.Model.Model;


namespace EmployeeAPI.Service.Services.Position
{
    public class PositionServices : IPositionServices
    {
        private readonly IPositionRepositry _positionRepository;

        public PositionServices(IPositionRepositry positionRepository)
        {
            _positionRepository = positionRepository;
        }

        public async Task<ApiResponseModel> SavePosition(PositionModel position)
        {
            return await _positionRepository.SavePosition(position);
        }

        public async Task<List<PositionModel>> GetAllPosition(CommonPaginationModel model)
        {
            return await _positionRepository.GetAllPosition(model);
        }

        public async Task<ApiResponseModel> DeletePosition(int id)
        {
            return await _positionRepository.DeletePosition(id);
        }

        public async Task<List<PositionModel>> GetPositionActive()
        {
            return await _positionRepository.GetPositionActive();
        }

        public async Task<ApiResponseModel> UpdatePositionStatus(int positionId, bool isActive, int updatedBy)
        {
            return await _positionRepository.UpdatePositionStatus(positionId, isActive, updatedBy);
        }
    }
}