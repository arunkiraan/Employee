using EmployeeDetails.Entities.Entities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace EmployeeDetails.DataAccessLayer.Interfaces
{
    public interface IGenericRepository<TEntity> where TEntity : class, new()
    {
        IQueryable<TEntity> GetAll();

        Task<TEntity> AddAsync(TEntity entity);

        Task<TEntity> UpdateAsync(TEntity entity);
        Task<List<Employee>> GetEmployeeWithDepartment();

    }

}