using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeAPI.Model.Model
{
    public class CommonModel
    {
        public DateTime CreatedAt { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsActive { get; set; }
        public DateTime? UpdateAt { get; set; }
        public long? TotalRecords { get; set; }
    }
    public class CommonPaginationModel : CommonModel
    {
        public int? PageNumber { get; set; } 
        public int? PageSize { get; set; }
        public string? SearchText { get; set; } = string.Empty;
        public string? SortColumn { get; set; }
        public string? SortOrder { get; set; }

    }
}
