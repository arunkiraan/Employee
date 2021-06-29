using EmployeeDetails.DataAccessLayer.Interfaces;
using EmployeeDetails.EF;
using EmployeeDetails.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeDetails.DataAccessLayer.Repositories
{

    public class DepartmentRepository : GenericRepository<Department>, IDepartmentRepository
    {
        public DepartmentRepository(EmployeeDbContext dbContext) : base(dbContext)
        {
        }

        public IQueryable<Department> GetDepartmentList()
        {
            try
            {
                return GetAll();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task AddDepartment(string department)
        {
            try
            {
                Department dept = new Department();
                dept.DepartmentName = department;
                await AddAsync(dept);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
