using AutoMapper;
using Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Model.Dtos;

namespace LMS.App_Start
{

    public class AutomapperConfig
    {
        public static void Initialize()
        {
            Mapper.Initialize(config =>
            {
                //config.CreateMap<Student, StudentDto>()
                //.ForMember(d => d.FullName, opt => opt.MapFrom(src => src.FirstName + " " + src.LastName)); 

                config.CreateMap<Student, StudentDto>().ForMember(d => d.FullName, opt => opt.MapFrom(src => src.FirstName + " " + src.LastName));
                config.CreateMap<Student, StudentCreatedDto>();
                config.CreateMap<Lecturer, LecturerCreatedDto>();
                config.CreateMap<Course, CourseDisplayDto>();
            });
        }
    }


}