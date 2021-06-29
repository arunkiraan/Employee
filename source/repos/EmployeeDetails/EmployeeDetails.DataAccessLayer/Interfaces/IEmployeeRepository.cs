using EmployeeDetails.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeDetails.DataAccessLayer.Interfaces
{
  
    public interface IEmployeeRepository : IGenericRepository<Employee>
    {
        Task<List<Employee>> GetAllEmployees();
        Task<long> AddEmployee(Employee employee);
        Task<long> UpdateEmployee(Employee employeeInput);
        void DeleteEmployeeInformation(Employee employeeData);
    }
}
