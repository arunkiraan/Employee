using System.ComponentModel.DataAnnotations;

namespace EmployeeDetails.Entities.DTO
{
    public class FilterEmployeeByIdDTO
    {
        public long EmployeeId { get; set; }
        
        public string FirstName { get; set; }
        
        public string LastName { get; set; }
        
        public string EmailId { get; set; }
        public string DepartmentName { get; set; }
        public string EmployeeStatus { get; set; }
        public long? ManagerId { get; set; }
    }
}
