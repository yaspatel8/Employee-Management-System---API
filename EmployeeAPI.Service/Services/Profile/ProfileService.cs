using EmployeeAPI.Common;
using EmployeeAPI.Data.DBRepositry.Profile;
using EmployeeAPI.Model.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeAPI.Service.Services.Profile
{
    public class ProfileService : IProfileServices
    {
        private readonly IProfileRepository _profileRepository;

        public ProfileService(IProfileRepository profileRepository)
        {
            _profileRepository = profileRepository;
        }

        public async Task<EmployeeWithDepartmentModel> GetProfile(int id)
        {
            return await _profileRepository.GetProfile(id);
        }
    }
}
