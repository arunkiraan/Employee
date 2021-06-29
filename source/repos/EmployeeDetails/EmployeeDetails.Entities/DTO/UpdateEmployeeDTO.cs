using System.ComponentModel.DataAnnotations;

namespace EmployeeDetails.Entities.DTO
{
    public class UpdateEmployeeDTO
    {
        [Required]
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
