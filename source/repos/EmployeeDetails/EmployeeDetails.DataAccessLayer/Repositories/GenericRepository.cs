using EmployeeDetails.DataAccessLayer.Interfaces;
using EmployeeDetails.EF;
using EmployeeDetails.Entities.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeDetails.DataAccessLayer.Repositories
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class, new()
    {
        protected readonly EmployeeDbContext _employeeDbContext;

        public GenericRepository(EmployeeDbContext employeeDbContext)
        {
            _employeeDbContext = employeeDbContext;
        }

        public IQueryable<TEntity> GetAll()
        {
            try
            {
                return _employeeDbContext.Set<TEntity>();
            }
            catch (Exception ex)
            {
                throw new Exception($"Couldn't retrieve entities: {ex.Message}");
            }
        }

        public async Task<TEntity> AddAsync(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException($"{nameof(AddAsync)} entity must not be null");
            }

            try
            {
                await _employeeDbContext.AddAsync(entity);
                await _employeeDbContext.SaveChangesAsync();

                return entity;
            }
            catch (Exception ex)
            {
                throw new Exception($"{nameof(entity)} could not be saved: {ex.Message}");
            }
        }

        public async Task<TEntity> UpdateAsync(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException($"{nameof(AddAsync)} entity must not be null");
            }

            try
            {
                _employeeDbContext.Update(entity);
                await _employeeDbContext.SaveChangesAsync();

                return entity;
            }
            catch (Exception ex)
            {
                throw new Exception($"{nameof(entity)} could not be updated: {ex.Message}");
            }
        }

        public void Delete(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException($"{nameof(Delete)} entity must not be null");
            }

            _employeeDbContext.Remove(entity);
            _employeeDbContext.SaveChanges();
        }

        public Task<List<Employee>> GetEmployeeWithDepartment()
        {
            return Task.Run(() => (_employeeDbContext.Employee.Include(x => x.Departments)).ToList());
        }
    }
}
