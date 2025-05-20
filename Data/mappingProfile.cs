using System;
using AutoMapper;
using WebApplication1.Models;
using WebApplication1.DTO;

namespace WebApplication1.Data;


public class mappingProfile : Profile
{
    public mappingProfile()
    {
        CreateMap<Course, CourseDTO>().ReverseMap();
        CreateMap<Category, CategoryDTO>().ReverseMap();
        CreateMap<Instructor, InstructorDTO>().ReverseMap();
        CreateMap<Course, CourseAddDTO>().ReverseMap();
        CreateMap<Course, CourseUpdateDTO>().ReverseMap();
        }
}
