using System;
using System.Collections.Generic;
using System.Text;

namespace EmployeeDetails.Entities.DTO
{
    public class FilterEmployeeDTO
    {
        public long EmployeeId { get; set; }
        
        public string FirstName { get; set; }
        
        public string LastName { get; set; }
        
        public string DepartmentName { get; set; }
    }
}
