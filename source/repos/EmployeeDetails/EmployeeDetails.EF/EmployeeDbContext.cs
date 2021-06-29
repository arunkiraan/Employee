using EmployeeDetails.Entities.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeDetails.EF
{
    public class EmployeeDbContext : DbContext
    {
        public EmployeeDbContext(DbContextOptions<EmployeeDbContext> options) : base(options)
        {

        }
        public DbSet<Employee> Employee { get; set; }
        public DbSet<Department> Department { get; set; }
       
    }
}
