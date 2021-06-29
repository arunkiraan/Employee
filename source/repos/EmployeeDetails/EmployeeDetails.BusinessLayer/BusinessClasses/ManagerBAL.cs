using AutoMapper;
using EmployeeDetails.BusinessLayer.Interfaces;
using EmployeeDetails.Common;
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
    public class ManagerBAL : IManagerBAL
    {
        private readonly IEmployeeRepository _employeeRepository;
        public ManagerBAL(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public EmployeeResponse CheckIfManagerIsNULL(List<Employee> employeeData)
        {
            try
            {
                EmployeeResponse employeeResponse = null;
                //Check if Manager is NULL
                var managerData = employeeData.Where(x => x.ManagerId == null);
                if (managerData != null || managerData.Count() > 0)
                {
                    employeeResponse = new EmployeeResponse();
                    employeeResponse.ErrorMessage = Constants.ManagerDataAlreadyExists;
                }
                return employeeResponse;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public EmployeeResponse CheckIfManagerIdExists(List<Employee> employeeData, long managerId)
        {
            try
            {
                EmployeeResponse employeeResponse = null;
                //Check if Manager Id Exists
                var IsManagerAvailable = employeeData.Where(x => x.EmployeeId == managerId).FirstOrDefault();
                if (IsManagerAvailable == null)
                {
                    employeeResponse = new EmployeeResponse();
                    employeeResponse.ErrorMessage = Constants.ManagerIdNotAvailable;
                    return employeeResponse;
                }
                return employeeResponse;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
