using DocumentFormat.OpenXml.Bibliography;
using EmployeeAPI.Common;
using EmployeeAPI.Common.Export;
using EmployeeAPI.Model.Model;
using EmployeeAPI.Service.Services.Department;
using EmployeeAPI.Service.Services.Position;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PositionController : ControllerBase
    {
        private readonly IPositionServices _positionService;
        private readonly ILogger<PositionController> _logger;
        private readonly IExcelExportService _excelService;

        public PositionController(IPositionServices positionService, ILogger<PositionController> logger, IExcelExportService excelService)
        {
            _positionService = positionService;
            _logger = logger;
            _excelService = excelService;
        }

        [HttpPost("/SavePosition")]
        public async Task<ApiResponseModel> SavePosition(PositionModel position)
        {
            ApiResponseModel response = new();
            var result = await _positionService.SavePosition(position);
            if (result.Code==(int)DbResponseCode.Success)
            {
                response.Success = true;
                response.Message = result.Message;
                _logger.LogInformation(result.Message, position.PositionName);
            }
            else if(result.Code==(int)DbResponseCode.AlreadyExists)
            {
                response.Success = false;
                response.Message = result.Message;
                _logger.LogWarning(result.Message, position.PositionName);
            }
            else
            {
                response.Success = false;
                response.Message = result.Message;
                _logger.LogError(result.Message, position.PositionName);
            }
            return response;
        }
        [HttpPost("/GetAllPosition")]
        public async Task<ApiList<PositionModel>> GetAllPosition([FromBody] CommonPaginationModel model)
        {
            ApiList<PositionModel> response = new() { Data = [] };
            var result = await _positionService.GetAllPosition(model);
            if (result != null && result.Count > 0)
            {
                response.Success = true;
                response.Message = "Positions retrieved successfully.";
                response.Data = result;
                _logger.LogInformation("Positions retrieved successfully. Count: {Count}", result.Count);
            }
            else
            {
                response.Success = false;
                response.Message = "No positions found.";
                _logger.LogWarning("No positions found.");
            }
            return response;
        }
        [HttpDelete("/DeletePosition/{id}")]
        public async Task<ApiResponseModel> DeletePosition(int id)
        {
            ApiResponseModel response = new();
            var result = await _positionService.DeletePosition(id);
            if (result.Code == (int)DbResponseCode.Success)
            {
                response.Success = true;
                response.Message = result.Message;
                _logger.LogInformation(result.Message, id);
            }
            else
            {
                response.Success = false;
                response.Message = result.Message;
                _logger.LogWarning(result.Message, id);
            }
            return response;
        }
        [HttpGet("/GetPositionActive")]
        public async Task<ApiList<PositionModel>> GetPositionActive()
        {
            ApiList<PositionModel> response = new() { Data = [] };
            var result = await _positionService.GetPositionActive();
            if (result != null && result.Count > 0)
            {
                response.Success = true;
                response.Message = "Active positions retrieved successfully.";
                response.Data = result;
                _logger.LogInformation("Active positions retrieved successfully.");
            }
            else
            {
                response.Success = false;
                response.Message = "No active positions found.";
                _logger.LogWarning("No active positions found.");
            }
            return response;
        }
        [HttpPost("/UpdatePositionStatus")]
        public async Task<ApiResponseModel> UpdatePositionStatus(int positionId, bool isActive, int updatedBy)
        {
            ApiResponseModel response = new();
            var result = await _positionService.UpdatePositionStatus(positionId, isActive, updatedBy);
            if (result.Code == (int)DbResponseCode.Success)
            {
                response.Success = true;
                response.Message = result.Message;
                _logger.LogInformation(result.Message, positionId);
            }
            else
            {
                response.Success = false;
                response.Message = result.Message;
                _logger.LogWarning(result.Message, positionId);
            }
            return response;
        }
    }
}