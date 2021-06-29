using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Text.Json.Serialization;

namespace EmployeeDetails.Entities.DTO
{
    public class EmployeeDTO
    {
        [JsonIgnore]
        public long EmployeeId { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        [EmailAddress]
        public string EmailId { get; set; }
        [Required]
        public string DepartmentName { get; set; }
        public long? ManagerId { get; set; }
    }
}
