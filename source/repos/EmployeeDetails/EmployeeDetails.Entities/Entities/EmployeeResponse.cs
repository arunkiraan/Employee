using EmployeeDetails.Entities.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace EmployeeDetails.Entities.Entities
{
   public class EmployeeResponse
    {
        public long EmployeeId { get; set; }
        public string ErrorMessage { get; set; }
        public FilterEmployeeByIdDTO filterEmployee { get; set; }
    }
}
