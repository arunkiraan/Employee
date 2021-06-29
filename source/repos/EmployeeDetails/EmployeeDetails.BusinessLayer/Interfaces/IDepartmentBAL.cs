using EmployeeDetails.Entities.DTO;
using EmployeeDetails.Entities.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeDetails.BusinessLayer.Interfaces
{
    public interface IDepartmentBAL
    {
        Task<IEnumerable<DepartmentDTO>> GetDepartmentList();
        Task AddDepartment(string departmentName);
        bool CheckIfDepartmentExists(string departmentName);
    }
}