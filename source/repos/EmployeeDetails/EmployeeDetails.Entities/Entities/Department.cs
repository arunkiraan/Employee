using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace EmployeeDetails.Entities.Entities
{
   public class Department
    {
        public long DepartmentId { get; set; }
        [Required]
        [StringLength(100)]
        public string DepartmentName { get; set; }
        public virtual ICollection<Employee> Employees { get; set; }
    }
}
