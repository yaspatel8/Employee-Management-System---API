using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeAPI.Common
{
    public class ApiResponseModel
    {
        public int Code { get; set; }
        public bool Success { get; set; }
        public string Message { get; set; } = string.Empty;
    }
    public class ApiResponse<T> : ApiResponseModel
    {
        public T? Data { get; set; }
    }

    public class ApiList<T> : ApiResponseModel
    {
        public IList<T>? Data { get; set; }
    }
    public class DbResponseModel : ApiResponseModel
    {
        public int Code { get; set; }
        public string Message { get; set; } = string.Empty;
        public string? OldFileName { get; set; }
    }
    public class BulkDbResponseModel : ApiResponseModel
    {
        public int InsertedCount { get; set; }
        public int SkippedCount { get; set; }
        public string? DuplicateEmails { get; set; }
    }
}
