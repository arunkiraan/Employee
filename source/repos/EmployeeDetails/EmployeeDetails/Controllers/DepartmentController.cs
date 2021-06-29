using EmployeeDetails.BusinessLayer.Interfaces;
using EmployeeDetails.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeDetails.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {

        private readonly IDepartmentBAL _deparmentBAL;
        public DepartmentController(IDepartmentBAL deparmentBAL)
        {
            _deparmentBAL = deparmentBAL;
        }

        [HttpGet]
        [Route("GetDepartmentList")]
        public IActionResult GetDepartmentList()
        {
            try
            {
                return Ok(_deparmentBAL.GetDepartmentList().Result);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPost]
        [Route("AddDepartment")]
        public async Task<IActionResult> AddDepartment([FromBody] string value)
        {
            try
            {
                if (string.IsNullOrEmpty(value) || string.IsNullOrWhiteSpace(value))
                {
                    return StatusCode(500, Constants.DepartmentNameValidation);
                }

                if (!_deparmentBAL.CheckIfDepartmentExists(value))
                {
                    await _deparmentBAL.AddDepartment(value);
                    return Ok(Constants.DepartmentSuccessMessage);
                }
                else
                {
                    return StatusCode(500, Constants.DepartmentNameExists);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
