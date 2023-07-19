using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.AutoMapper;
using Abp.Domain.Repositories;
using Abp.Linq.Extensions;
using Abp.UI;
using AutoMapper;
using AutoMapper.Internal.Mappers;
using MyProject.Authorization;
using MyProject.Students.Dto;
using System.Collections.Generic;
using System.Linq;

namespace MyProject.Students
{
    [AbpAuthorize(PermissionNames.Pages_Students)]
    public class StudentAppService : MyProjectAppServiceBase, IStudentAppService
    {
        private readonly StudentManager _studentManager;
        private readonly IRepository<Student> _studentRepository;

        public StudentAppService(StudentManager studentManager, IRepository<Student> studentRepository)
        {
            _studentManager = studentManager;
            _studentRepository = studentRepository;

        }

        public void CreateStudent(CreateStudentDto input)
        {
            var student = _studentRepository.FirstOrDefault(p => p.Email == input.Email);
            if (student != null)
            {
                throw new UserFriendlyException("There is already a person with given email address");
            }
            else
            {
                student = ObjectMapper.Map<Student>(input);
                _studentRepository.Insert(student);
            }
        }

        public void DeleteStudent(DeleteStudentDto input)
        {
            var student = ObjectMapper.Map<Student>(input);
            if (student != null)
            {
                _studentRepository.Delete(student);
            }
            else
            {
                throw new UserFriendlyException("There is no such student");
            }
        }

        public void UpdateStudent(UpdateStudentDto input)
        {
            var student = ObjectMapper.Map<Student>(input);
            if (student != null)
            {
                _studentRepository.Update(student);
            }
            else
            {
                throw new UserFriendlyException("There is no such student");
            }
        }
        public StudentDto Get(int id)
        {
            var student = _studentRepository.Get(id);
            return ObjectMapper.Map<StudentDto>(student);
        }

        public PagedResultDto<StudentDto> GetAll(PagedStudentResultRequestDto input)
        {
            var studentCount = _studentRepository.Count();

            var t = _studentRepository
                    .GetAll()
                    .PageBy(input) // Page by SkipCount and MaxResultCount
                    .ToList();
            var studentDtos = ObjectMapper.Map<List<StudentDto>>(t);
            return new PagedResultDto<StudentDto>
            {
                TotalCount = studentCount,
                Items = studentDtos
            };
        }






    }
}

