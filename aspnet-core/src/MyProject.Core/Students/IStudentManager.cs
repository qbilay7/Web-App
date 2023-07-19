using Abp.Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyProject.Students
{
    public interface IStudentManager:IDomainService
    {
        IEnumerable<Student> GetAll();
        Student Get(int id);
        Task<Student> Create(Student entity);
        void Update(Student entity);
        void Delete(int id);
    }
}
