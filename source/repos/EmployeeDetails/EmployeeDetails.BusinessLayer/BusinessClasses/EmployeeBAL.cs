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
    public class EmployeeBAL : IEmployeeBAL
    {
        private IEmployeeRepository _employeeRepo;
        private IManagerBAL _managerBAL;
        private IDepartmentRepository _departmentRepo;
        private IEmailService _emailService;
        private readonly IMapper _mapper;

        public EmployeeBAL(IEmployeeRepository employeeRepo, IMapper mapper, IDepartmentRepository departmentRepo, IEmailService emailService, IManagerBAL managerBAL)
        {
            _employeeRepo = employeeRepo;
            _mapper = mapper;
            _departmentRepo = departmentRepo;
            _emailService = emailService;
            _managerBAL = managerBAL;
        }

        public Task<IEnumerable<EmployeeDTO>> GetAllEmploeeDetails()
        {
            var employees = _employeeRepo.GetAllEmployees()?.Result.OrderBy(x => x.FirstName).ToList();
            return Task.Run(() => (_mapper.Map<IEnumerable<EmployeeDTO>>(employees)));
        }

        public async Task<EmployeeResponse> AddEmployee(EmployeeDTO employee)
        {
            try
            {
                EmployeeResponse employeeResponse = new EmployeeResponse();
                var employeeData = _employeeRepo.GetAllEmployees()?.Result;

                #region Manager Id Validation
                if (employee.ManagerId == null)
                {
                    var response = _managerBAL.CheckIfManagerIsNULL(employeeData);
                    if (response != null)
                    {
                        return response;
                    }
                }
                else
                {
                    //Check if Manager Id is availbe in database
                    var response = _managerBAL.CheckIfManagerIdExists(employeeData, (long)employee.ManagerId);
                    if (response != null)
                    {
                        return response;
                    }
                }
                #endregion
                // Check if Department exists
                var department = _departmentRepo.GetDepartmentList()?.Where(x => x.DepartmentName.ToLower() == employee.DepartmentName.ToLower());

                var employeeInput = _mapper.Map<Employee>(employee);
                if (department != null && department.Count() > 0)
                {
                    employeeInput.Departments = null;
                    employeeInput.DepartmentId = department.FirstOrDefault().DepartmentId;

                    // Add Employee
                    long empId = await _employeeRepo.AddEmployee(employeeInput);
                    employeeResponse.EmployeeId = empId;
                    employeeResponse.ErrorMessage = string.Empty;

                    // Send Email to User
                    _emailService.Send(employee.EmailId, Constants.AddEmployeeSubject, Constants.AddEmployeeMailBodyMessage);
                }
                else
                {
                    employeeResponse.ErrorMessage = Constants.DepartmentNotAvaialbe;
                }
                return employeeResponse;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<EmployeeResponse> UpdateEmployee(UpdateEmployeeDTO updateEmployeeDTO)
        {
            try
            {
                //EmployeeResponse employeeResponse = null ;
                EmployeeResponse employeeResponse = new EmployeeResponse();
                var employeeData = _employeeRepo.GetAllEmployees()?.Result;

                #region Manager Id Validation
                if (updateEmployeeDTO.ManagerId == null)
                {
                    var response = _managerBAL.CheckIfManagerIsNULL(employeeData);
                    if (response != null)
                    {
                        return response;
                    }
                }
                else
                {
                    //Check if Manager Id is availbe in database
                    var response = _managerBAL.CheckIfManagerIdExists(employeeData, (long)updateEmployeeDTO.ManagerId);
                    if (response != null)
                    {
                        return response;
                    }
                }
                #endregion

                // Check if Employee exists
                var employee = employeeData.Where(x => x.EmployeeId == updateEmployeeDTO.EmployeeId);
                if (employee == null || employee.Count() == 0)
                {
                    employeeResponse.ErrorMessage = Constants.EmployeeNotAvaialbe;
                    return employeeResponse;
                }

                // Check if Department exists
                var department = _departmentRepo.GetDepartmentList()?.Where(x => x.DepartmentName.ToLower() == updateEmployeeDTO.DepartmentName.ToLower());

                var employeeInput = _mapper.Map<Employee>(updateEmployeeDTO);
                if (department != null && department.Count() > 0)
                {
                    employeeInput.Departments = null;
                    employeeInput.DepartmentId = department.FirstOrDefault().DepartmentId;

                    // Update Employee
                    long empId = await _employeeRepo.UpdateEmployee(employeeInput);
                    employeeResponse.EmployeeId = empId;
                    employeeResponse.ErrorMessage = string.Empty;
                }
                else
                {
                    employeeResponse.ErrorMessage = Constants.DepartmentNotAvaialbe;
                }
                return employeeResponse;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public EmployeeResponse DeleteEmployee(long employeeId)
        {
            try
            {
                EmployeeResponse employeeResponse = new EmployeeResponse();
                var employeeData = _employeeRepo.GetAllEmployees()?.Result.Where(x => x.EmployeeId == employeeId).FirstOrDefault();
                if (employeeData == null)
                {
                    employeeResponse.ErrorMessage = Constants.DeleteUnavailableEmployee;
                }
                else
                {
                    _employeeRepo.DeleteEmployeeInformation(employeeData);
                    employeeResponse.EmployeeId = employeeId;

                    // Send Email to User
                    _emailService.Send(employeeData.EmailId, Constants.DeleteEmployeeSubject, Constants.DeleteEmployeeMailBodyMessage);
                }
                return employeeResponse;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public IEnumerable<EmployeeDTO> GetEmployeeDetails(FilterEmployeeDTO filterEmployeeDTO)
        {
            try
            {
                var employeeData = _employeeRepo.GetAllEmployees()?.Result;

                if (filterEmployeeDTO.EmployeeId > 0 && !string.IsNullOrEmpty(Convert.ToString(filterEmployeeDTO.EmployeeId)) && !string.IsNullOrWhiteSpace(Convert.ToString(filterEmployeeDTO.EmployeeId)))
                {
                    employeeData = employeeData.Where(x => x.EmployeeId == filterEmployeeDTO.EmployeeId).ToList();
                }

                if (filterEmployeeDTO.LastName != null && !string.IsNullOrEmpty(filterEmployeeDTO.LastName) && !string.IsNullOrWhiteSpace(filterEmployeeDTO.LastName))
                {
                    employeeData = employeeData.Where(x => x.LastName.ToLower() == filterEmployeeDTO.LastName.ToLower()).ToList();
                }

                if (filterEmployeeDTO.FirstName != null && !string.IsNullOrEmpty(filterEmployeeDTO.FirstName) && !string.IsNullOrWhiteSpace(filterEmployeeDTO.FirstName))
                {
                    employeeData = employeeData.Where(x => x.FirstName.ToLower() == filterEmployeeDTO.FirstName.ToLower()).ToList();
                }

                if (filterEmployeeDTO.DepartmentName != null && !string.IsNullOrEmpty(filterEmployeeDTO.DepartmentName) && !string.IsNullOrWhiteSpace(filterEmployeeDTO.DepartmentName))
                {
                    employeeData = employeeData.Where(x => x.Departments.DepartmentName.ToLower() == filterEmployeeDTO.DepartmentName.ToLower()).ToList();
                }

                var employees = _mapper.Map<IEnumerable<EmployeeDTO>>(employeeData);
                return employees.OrderBy(x => x.FirstName);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public EmployeeResponse GetEmployeeDetailsById(long employeeId)
        {
            try
            {
                EmployeeResponse response = new EmployeeResponse();
                string status = string.Empty;

                var employeeData = _employeeRepo.GetAllEmployees()?.Result;
                var employee = employeeData.Where(x => x.EmployeeId == employeeId).FirstOrDefault();

                if (employee == null)
                {
                    response.ErrorMessage = Constants.EmployeeNotAvaialbe;
                }
                else
                {
                    var responseEmployee = _mapper.Map<FilterEmployeeByIdDTO>(employee);
                    if (!employee.ManagerId.HasValue)
                    {
                        status = Constants.Head;
                    }
                    else if (!employeeData.Any(x => x.ManagerId == employee.EmployeeId))
                    {
                        status = Constants.Associate;
                    }
                    else
                    {
                        status = Constants.Manager;
                    }
                    responseEmployee.EmployeeStatus = status;
                    response.filterEmployee = responseEmployee;
                }
                return response;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
