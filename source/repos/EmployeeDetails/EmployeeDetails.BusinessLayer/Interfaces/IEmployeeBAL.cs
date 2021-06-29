using EmployeeDetails.Entities.DTO;
using EmployeeDetails.Entities.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EmployeeDetails.BusinessLayer.Interfaces
{
    public interface IEmployeeBAL
    {
        Task<IEnumerable<EmployeeDTO>> GetAllEmploeeDetails();
        Task<EmployeeResponse> AddEmployee(EmployeeDTO employee);
        Task<EmployeeResponse> UpdateEmployee(UpdateEmployeeDTO updateEmployeeDTO);
        EmployeeResponse DeleteEmployee(long employeeId);
        IEnumerable<EmployeeDTO> GetEmployeeDetails(FilterEmployeeDTO filterEmployeeDTO);
        EmployeeResponse GetEmployeeDetailsById(long employeeId);
    }
}