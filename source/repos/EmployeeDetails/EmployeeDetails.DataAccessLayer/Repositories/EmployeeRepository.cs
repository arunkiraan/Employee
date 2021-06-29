using EmployeeDetails.DataAccessLayer.Interfaces;
using EmployeeDetails.EF;
using EmployeeDetails.Entities.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeDetails.DataAccessLayer.Repositories
{
    public class EmployeeRepository : GenericRepository<Employee>, IEmployeeRepository
    {
        public EmployeeRepository(EmployeeDbContext dbContext) : base(dbContext)
        {
        }

        public Task<List<Employee>> GetAllEmployees()
        {
            try
            {
                return GetEmployeeWithDepartment();
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<long> AddEmployee(Employee employee)
        {
            try
            {
                var emp = await AddAsync(employee);
                if (emp != null && emp.EmployeeId > 0)
                {
                    return emp.EmployeeId;
                }
                return 0;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<long> UpdateEmployee(Employee employee)
        {
            try
            {
                var emp = await UpdateAsync(employee);
                if (emp != null && emp.EmployeeId > 0)
                {
                    return emp.EmployeeId;
                }
                return 0;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void DeleteEmployeeInformation(Employee employeeData)
        {
            try
            {
                Delete(employeeData);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
