using EmployeeDetails.BusinessLayer.Interfaces;
using EmployeeDetails.Common;
using EmployeeDetails.EF;
using EmployeeDetails.Entities.DTO;
using EmployeeDetails.Entities.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EmployeeDetails.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private IEmployeeBAL _employeeBAL;
        public EmployeeController(IEmployeeBAL employeeBAL)
        {
            this._employeeBAL = employeeBAL;
        }

        // GET: api/<EmployeeController>
        [HttpGet]
        [Route("GetEmployeeList")]
        public IActionResult GetEmployeeList()
        {
            try
            {
                return Ok(_employeeBAL.GetAllEmploeeDetails().Result);
            }
            catch (Exception)
            {
                throw;
            }
        }

        // GET api/<EmployeeController>/5
        [HttpGet]
        [Route("FilteredEmployeeList")]
        public IActionResult FilteredEmployeeList([FromQuery] FilterEmployeeDTO filterEmployeeDTO)
        {
            try
            {
                if (filterEmployeeDTO == null)
                {
                    return BadRequest(Constants.EmployeeBadRequestResponse);
                }
                if (filterEmployeeDTO != null)
                {
                    if ((filterEmployeeDTO.DepartmentName == null || string.IsNullOrEmpty(filterEmployeeDTO.DepartmentName) || string.IsNullOrWhiteSpace(filterEmployeeDTO.DepartmentName))
                        && (filterEmployeeDTO.FirstName == null || string.IsNullOrEmpty(filterEmployeeDTO.FirstName) || string.IsNullOrWhiteSpace(filterEmployeeDTO.FirstName))
                        && (filterEmployeeDTO.LastName == null || string.IsNullOrEmpty(filterEmployeeDTO.LastName) || string.IsNullOrWhiteSpace(filterEmployeeDTO.LastName))
                        && (filterEmployeeDTO.EmployeeId == 0 || string.IsNullOrEmpty(Convert.ToString(filterEmployeeDTO.EmployeeId)) || string.IsNullOrWhiteSpace(Convert.ToString(filterEmployeeDTO.EmployeeId))))
                    {
                        return BadRequest("Atlest One Field is required");
                    }

                    return Ok(_employeeBAL.GetEmployeeDetails(filterEmployeeDTO));
                }
                return null;
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpGet("{employeeId}")]
        public IActionResult GetEmployeeDetailsById(long employeeId)
        {
            try
            {
                var response = _employeeBAL.GetEmployeeDetailsById(employeeId);
                if (!string.IsNullOrEmpty(response.ErrorMessage) || !string.IsNullOrWhiteSpace(response.ErrorMessage))
                {
                    return StatusCode(500, response.ErrorMessage);
                }
                return Ok(response.filterEmployee);
            }
            catch (Exception)
            {
                throw;
            }
        }
        // POST api/<EmployeeController>
        [HttpPost]
        [Route("AddEmployeeeDetails")]
        public async Task<IActionResult> AddEmployeeeDetails([FromBody] EmployeeDTO employeeDTO)
        {
            try
            {
                if (employeeDTO == null)
                {
                    return BadRequest(Constants.EmployeeBadRequestResponse);
                }
                var employeeResponse = await _employeeBAL.AddEmployee(employeeDTO);

                if (employeeResponse == null || !string.IsNullOrEmpty(employeeResponse.ErrorMessage) || !string.IsNullOrWhiteSpace(employeeResponse.ErrorMessage))
                {
                    if (string.IsNullOrEmpty(employeeResponse.ErrorMessage) || string.IsNullOrWhiteSpace(employeeResponse.ErrorMessage))
                    {
                        return StatusCode(500, Constants.DefaultEmployeeErrorMessage);
                    }
                    return StatusCode(500, employeeResponse.ErrorMessage);
                }
                return Ok(Constants.EmployeeSuccessMessageWhileAdd + employeeResponse?.EmployeeId);
            }
            catch (Exception)
            {
                throw;
            }
        }

        // PUT api/<EmployeeController>/5
        [HttpPut]
        [Route("UpdateEmployeeeDetails")]
        public async Task<IActionResult> Put([FromBody] UpdateEmployeeDTO updateEmployeeDTO)
        {
            try
            {
                if (updateEmployeeDTO == null)
                {
                    return BadRequest(Constants.EmployeeBadRequestResponse);
                }
                var employeeResponse = await _employeeBAL.UpdateEmployee(updateEmployeeDTO);

                if (employeeResponse == null || !string.IsNullOrEmpty(employeeResponse.ErrorMessage) || !string.IsNullOrWhiteSpace(employeeResponse.ErrorMessage))
                {
                    if (string.IsNullOrEmpty(employeeResponse.ErrorMessage) || string.IsNullOrWhiteSpace(employeeResponse.ErrorMessage))
                    {
                        return StatusCode(500, Constants.DefaultUpdateEmployeeErrorMessage);
                    }
                    return StatusCode(500, employeeResponse.ErrorMessage);
                }
                return Ok(Constants.EmployeeSuccessMessageWhileUpdate);
            }
            catch (Exception)
            {
                throw;
            }
        }

        // DELETE api/<EmployeeController>/5
        [HttpDelete("{employeeId}")]
        public IActionResult Delete(long employeeId)
        {
            try
            {
                var response = _employeeBAL.DeleteEmployee(employeeId);
                if (!string.IsNullOrEmpty(response.ErrorMessage) || !string.IsNullOrWhiteSpace(response.ErrorMessage))
                {
                    return StatusCode(500, response.ErrorMessage);
                }
                return Ok(Constants.EmployeeDeleted);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
