using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace EmployeeDetails.Entities.DTO
{
    public class DepartmentDTO
    {
        public long DepartmentId { get; set; }
        public string DepartmentName { get; set; }
    }
}
