using System;
using System.Collections.Generic;
using System.Text;

namespace EmployeeDetails.Common
{
    public static class Constants
    {
        public const string DefaultEmployeeErrorMessage = "Issues in adding Exployee";
        public const string DefaultUpdateEmployeeErrorMessage = "Issues in updating Exployee";
        public const string EmployeeBadRequestResponse = "Employee Input not formed correctly!";
        public const string EmployeeSuccessMessageWhileAdd = "Employee Added Successfully! Employee ID is ";
        public const string EmployeeSuccessMessageWhileUpdate = "Employee Updated Successfully!";
        public const string AddEmployeeSubject = "Employee Added Successfully";
        public const string DeleteEmployeeSubject = "Employee Deleted Successfully";
        public const string DeleteUnavailableEmployee = "Employee Id is not avaialbe in the database.";
        public const string EmployeeDeleted = "Employee Id is deleted from the database.";
        public const string AddEmployeeMailBodyMessage = "The employee details has been added to our database successfully.";
        public const string DeleteEmployeeMailBodyMessage = "The employee details has been deleted from our database successfully.";
        public const string DepartmentNotAvaialbe = "Department Not Available. Please add the Department.";
        public const string EmployeeNotAvaialbe = "Employee Data Not Available. Please add Employee.";
        public const string DepartmentNameValidation = "Department Name should not be empty";
        public const string DepartmentSuccessMessage = "Department Added Successfully!";
        public const string DepartmentNameExists = "Department Already Exists";
        public const string ManagerDataAlreadyExists = "Manager Data Already Exists";
        public const string ManagerIdNotAvailable = "Entered Manager Id does not exists.";
        public const string Head = "Head";
        public const string Associate = "Associate";
        public const string Manager = "Manager";
    }
}
