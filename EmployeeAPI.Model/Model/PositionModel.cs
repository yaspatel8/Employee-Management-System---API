using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeAPI.Model.Model
{
    public class PositionModel :CommonModel
    {
        public int PositionId { get; set; }
        public string PositionName { get; set; } = string.Empty;
        public int Level { get; set; }
    }
}
