using EmployeeAPI.Common;
using EmployeeAPI.Model.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeAPI.Data.DBRepositry.Profile
{
    public interface IProfileRepository
    {
        Task<EmployeeWithDepartmentModel> GetProfile(int id);
        Task<List<HierarchyTreeDto>> GetHierarchyTree();
    }
}
