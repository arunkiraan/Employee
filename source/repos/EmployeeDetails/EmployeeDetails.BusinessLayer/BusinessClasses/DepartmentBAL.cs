using AutoMapper;
using EmployeeDetails.BusinessLayer.Interfaces;
using EmployeeDetails.DataAccessLayer.Interfaces;
using EmployeeDetails.Entities.DTO;
using EmployeeDetails.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeDetails.BusinessLayer.BusinessClasses
{
    public class DepartmentBAL : IDepartmentBAL
    {
        private readonly IDepartmentRepository _departmentRepository;
        private readonly IMapper _mapper;
        public DepartmentBAL(IDepartmentRepository departmentRepository, IMapper mapper)
        {
            _departmentRepository = departmentRepository;
            _mapper = mapper;
        }
        public Task<IEnumerable<DepartmentDTO>> GetDepartmentList()
        {
            try
            {
                var departments = _departmentRepository.GetDepartmentList();
                return Task.Run(() => (_mapper.Map<IEnumerable<DepartmentDTO>>(departments)));
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task AddDepartment(string departmentName)
        {
            try
            {
                await _departmentRepository.AddDepartment(departmentName);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool CheckIfDepartmentExists(string departmentName)
        {
            try
            {
                var department = _departmentRepository.GetDepartmentList()?.Where(x => x.DepartmentName.ToLower() == departmentName.ToLower()).ToList();
                return department != null && department.Count > 0 ? true : false;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
