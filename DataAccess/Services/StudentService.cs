using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SealabAPI.Base;
using SealabAPI.DataAccess.Entities;
using SealabAPI.DataAccess.Models;
using SealabAPI.Helpers;

namespace SealabAPI.DataAccess.Services
{
    public interface IStudentService : IBaseService<Student>
    {
        Task<Student> Create(CreateStudentRequest model);
        Task<List<Student>> BulkInsert(List<CreateStudentRequest> model);
    }
    public class StudentService : BaseService<Student>, IStudentService
    {
        public StudentService(AppDbContext appDbContext) : base(appDbContext) { }
        public async Task<List<Student>> BulkInsert(List<CreateStudentRequest> excel)
        {
            List<Student> Students = new();
            foreach (var row in excel)
            {
                Student Student = row.MapToEntity<Student>();
                Student.User = row.MapToEntity<User>();
                Student.User.Role = "Student";
                Students.Add(Student);
                await _appDbContext.Set<Student>().AddAsync(Student);
            }

            await _appDbContext.SaveChangesAsync();
            return Students;
        }
        public async Task<Student> Create(CreateStudentRequest model)
        {
            Student Student = model.MapToEntity<Student>();
            Student.User = model.MapToEntity<User>();
            Student.User.Role = "Student";

            await _appDbContext.Set<Student>().AddAsync(Student);
            await _appDbContext.SaveChangesAsync();

            return Student;
        }
    }
}

