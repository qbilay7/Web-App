using Abp.Application.Services;
using Abp.Application.Services.Dto;
using MyProject.Authorization.Users;
using MyProject.Students.Dto;
using MyProject.Users.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyProject.Students
{
    public interface IStudentAppService : IApplicationService
    {

        void CreateStudent(CreateStudentDto input);
        void UpdateStudent(UpdateStudentDto input);
        void DeleteStudent(DeleteStudentDto input);
        public StudentDto Get(int id);
        public PagedResultDto<StudentDto> GetAll(PagedStudentResultRequestDto input);
    }
}
