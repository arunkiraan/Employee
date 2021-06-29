using AutoMapper;
using EmployeeDetails.Entities.DTO;
using EmployeeDetails.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeDetails.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Employee, EmployeeDTO>().ForMember(dest => dest.DepartmentName, src => src.MapFrom(x => x.Departments.DepartmentName)).ReverseMap();
            CreateMap<Employee, UpdateEmployeeDTO>().ForMember(dest => dest.DepartmentName, src => src.MapFrom(x => x.Departments.DepartmentName)).ReverseMap();
            CreateMap<Employee, FilterEmployeeDTO>().ForMember(dest => dest.DepartmentName, src => src.MapFrom(x => x.Departments.DepartmentName)).ReverseMap();
            CreateMap<Employee, FilterEmployeeByIdDTO>().ForMember(dest => dest.DepartmentName, src => src.MapFrom(x => x.Departments.DepartmentName)).ReverseMap();
            CreateMap<Department, DepartmentDTO>().ReverseMap();
        }
    }
}
