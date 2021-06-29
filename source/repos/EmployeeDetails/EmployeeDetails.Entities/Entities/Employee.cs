using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace EmployeeDetails.Entities.Entities
{
    public class Employee
    {
        public long EmployeeId { get; set; }
        [Required]
        [StringLength(100)]
        public string FirstName { get; set; }
        public string LastName { get; set; }
        [Required]
        [StringLength(100)]
        public string EmailId { get; set; }
        public long DepartmentId { get; set; }
        public long? ManagerId { get; set; }

        public virtual Department Departments { get; set; }
    }
}
