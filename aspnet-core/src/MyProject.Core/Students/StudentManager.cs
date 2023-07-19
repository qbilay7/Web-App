
using Abp.Domain.Repositories;
using Abp.Domain.Services;
using Abp.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace MyProject.Students
{
    public class StudentManager : DomainService, IStudentManager
    {
        private readonly IRepository<Student> _studentRepository;
        public StudentManager(IRepository<Student> studentRepository)
        {
            _studentRepository = studentRepository;
        }

        public async Task<Student> Create(Student entity)
        {
            var student = _studentRepository.FirstOrDefault(x => x.Id == entity.Id);

            var hasEmail = _studentRepository.GetAll().Any(p => p.Email == entity.Email);
            if(hasEmail)
                {
                throw new UserFriendlyException("Emaıl Already Exists");

            }
            if (student == null)
            {
                throw new UserFriendlyException("Already Exists");
            }
            else
            {
                return await _studentRepository.InsertAsync(entity);
            }
        }

        public void Delete(int id)
        {
            var student=_studentRepository.FirstOrDefault(_x => _x.Id == id);
            if (student == null)
            {
                throw new UserFriendlyException("Already Exists");
            }
            else
            {
                _studentRepository.Delete(student);
            }
        }

        public Student Get(int id)
        {
            return _studentRepository.Get(id);
        }

        public IEnumerable<Student> GetAll()
        {
            return _studentRepository.GetAll();
        }

        public void Update(Student student)
        {
            _studentRepository.Update(student);
        }
    }
}
