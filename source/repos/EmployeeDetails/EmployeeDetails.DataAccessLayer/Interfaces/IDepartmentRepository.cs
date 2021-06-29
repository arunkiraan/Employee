using EmployeeDetails.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeDetails.DataAccessLayer.Interfaces
{

    public interface IDepartmentRepository : IGenericRepository<Department>
    {
        IQueryable<Department> GetDepartmentList();
        Task AddDepartment(string department);
    }
}
