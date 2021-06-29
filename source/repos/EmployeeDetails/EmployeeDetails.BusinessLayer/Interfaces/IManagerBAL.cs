using EmployeeDetails.Entities.Entities;
using System.Collections.Generic;

namespace EmployeeDetails.BusinessLayer.Interfaces
{
    public interface IManagerBAL
    {
        EmployeeResponse CheckIfManagerIsNULL(List<Employee> employeeData);
        EmployeeResponse CheckIfManagerIdExists(List<Employee> employeeData, long managerId);
    }
}