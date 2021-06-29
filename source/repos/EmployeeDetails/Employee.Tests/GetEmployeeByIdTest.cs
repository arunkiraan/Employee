using EmployeeDetails.DataAccessLayer.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using EmployeeDetails.Entities.Entities;
using System.Threading.Tasks;
using EmployeeDetails.BusinessLayer.BusinessClasses;
using EmployeeDetails.Common;
using EmployeeDetails.BusinessLayer.Interfaces;
using AutoMapper;
using EmployeeDetails.Mapper;

namespace EmployeeDetails.Tests
{
    [TestClass]
    public class GetEmployeeByIdTest
    {
        Mock<IEmployeeRepository> employeeRepoMock = new Mock<IEmployeeRepository>();
        Mock<IDepartmentRepository> departmentRepoMock = new Mock<IDepartmentRepository>();
        Mock<IEmailService> eMailServiceMock = new Mock<IEmailService>();
        Mock<IManagerBAL> managerBALMock = new Mock<IManagerBAL>();
        private static IMapper _mapper;
        public GetEmployeeByIdTest()
        {
            if (_mapper == null)
            {
                var mapperConfig = new MapperConfiguration(mc =>
                {
                    mc.AddProfile(new MappingProfile());
                });

                IMapper mapper = mapperConfig.CreateMapper();
                _mapper = mapper;
            }
        }
        [TestMethod]
        public void GetEmployeeDetailsById_TestIfEmployeeNotAvaialbe()
        {

            EmployeeResponse employeeResponse = new EmployeeResponse();
            employeeResponse.ErrorMessage = Constants.EmployeeNotAvaialbe;

            List<Entities.Entities.Employee> employees = new List<Entities.Entities.Employee>();
            employees.Add(new Entities.Entities.Employee { DepartmentId = 11, EmailId = "arun@gmail.com", FirstName = "FN", LastName = "LN", EmployeeId = 1 });
            employees.Add(new Entities.Entities.Employee { DepartmentId = 22, EmailId = "arun@gmail.com", FirstName = "FN", LastName = "LN", EmployeeId = 3 });

            employeeRepoMock.Setup(p => p.GetAllEmployees()).Returns(Task.FromResult(employees));

            EmployeeBAL employeeBAL = new EmployeeBAL(employeeRepoMock.Object, _mapper, departmentRepoMock.Object, eMailServiceMock.Object, managerBALMock.Object);
            var response = employeeBAL.GetEmployeeDetailsById(2);
            Assert.AreEqual(employeeResponse.ErrorMessage, response.ErrorMessage);
        }

        [TestMethod]
        public void GetEmployeeDetailsById_HEADStatus()
        {
            EmployeeResponse employeeResponse = new EmployeeResponse();
            employeeResponse.ErrorMessage = Constants.EmployeeNotAvaialbe;

            List<Entities.Entities.Employee> employees = new List<Entities.Entities.Employee>();
            employees.Add(new Entities.Entities.Employee { DepartmentId = 11, EmailId = "arun@gmail.com", FirstName = "FN", LastName = "LN", EmployeeId = 1, ManagerId = null });
            employees.Add(new Entities.Entities.Employee { DepartmentId = 22, EmailId = "arun@gmail.com", FirstName = "FN", LastName = "LN", EmployeeId = 3 });

            employeeRepoMock.Setup(p => p.GetAllEmployees()).Returns(Task.FromResult(employees));

            EmployeeBAL employeeBAL = new EmployeeBAL(employeeRepoMock.Object, _mapper, departmentRepoMock.Object, eMailServiceMock.Object, managerBALMock.Object);
            var response = employeeBAL.GetEmployeeDetailsById(1);
            Assert.AreEqual(Constants.Head, response.filterEmployee.EmployeeStatus);
        }

        [TestMethod]
        public void GetEmployeeDetailsById_AssociateStatus()
        {
            EmployeeResponse employeeResponse = new EmployeeResponse();
            employeeResponse.ErrorMessage = Constants.EmployeeNotAvaialbe;

            List<Entities.Entities.Employee> employees = new List<Entities.Entities.Employee>();
            employees.Add(new Employee { DepartmentId = 11, EmailId = "arun@gmail.com", FirstName = "FN", LastName = "LN", EmployeeId = 1, ManagerId = null });
            employees.Add(new Employee { DepartmentId = 22, EmailId = "arun@gmail.com", FirstName = "FN", LastName = "LN", EmployeeId = 3, ManagerId = 1 });

            employeeRepoMock.Setup(p => p.GetAllEmployees()).Returns(Task.FromResult(employees));

            EmployeeBAL employeeBAL = new EmployeeBAL(employeeRepoMock.Object, _mapper, departmentRepoMock.Object, eMailServiceMock.Object, managerBALMock.Object);
            var response = employeeBAL.GetEmployeeDetailsById(3);
            Assert.AreEqual(Constants.Associate, response.filterEmployee.EmployeeStatus);
        }

        [TestMethod]
        public void GetEmployeeDetailsById_ManagerStatus()
        {
            EmployeeResponse employeeResponse = new EmployeeResponse();
            employeeResponse.ErrorMessage = Constants.EmployeeNotAvaialbe;

            List<Entities.Entities.Employee> employees = new List<Entities.Entities.Employee>();
            employees.Add(new Employee { DepartmentId = 11, EmailId = "arun@gmail.com", FirstName = "FN", LastName = "LN", EmployeeId = 1, ManagerId = null });
            employees.Add(new Employee { DepartmentId = 22, EmailId = "arun@gmail.com", FirstName = "FN", LastName = "LN", EmployeeId = 2, ManagerId = 1 });
            employees.Add(new Employee { DepartmentId = 22, EmailId = "arun@gmail.com", FirstName = "FN", LastName = "LN", EmployeeId = 3, ManagerId = 2 });

            employeeRepoMock.Setup(p => p.GetAllEmployees()).Returns(Task.FromResult(employees));

            EmployeeBAL employeeBAL = new EmployeeBAL(employeeRepoMock.Object, _mapper, departmentRepoMock.Object, eMailServiceMock.Object, managerBALMock.Object);
            var response = employeeBAL.GetEmployeeDetailsById(2);
            Assert.AreEqual(Constants.Manager, response.filterEmployee.EmployeeStatus);
        }
    }
}
