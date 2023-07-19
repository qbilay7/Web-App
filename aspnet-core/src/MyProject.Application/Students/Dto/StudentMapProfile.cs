using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyProject.Students.Dto
{
    public class StudentMapProfile : Profile
    {
        public StudentMapProfile()
        {
            CreateMap<CreateStudentDto, Student>();
            CreateMap<Student, StudentDto>();
            CreateMap<DeleteStudentDto, Student>();
            CreateMap<UpdateStudentDto, Student>();
        }
    }
}
