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
        Task<Student> Update(UpdateStudentRequest model);
        Task BulkInsert(List<CreateStudentRequest> model);
        Task DeleteAllStudents(); 
    }
    public class StudentService : BaseService<Student>, IStudentService
    {
        readonly IUserService _userService;
        public StudentService(AppDbContext appDbContext, IUserService userService) : base(appDbContext)
        {
            _userService = userService;
        }
        public async Task BulkInsert(List<CreateStudentRequest> excel)
        {
            List<Student> students = new();
            foreach (var row in excel)
            {
                Student student = row.MapToEntity<Student>();
                student.User = row.MapToEntity<User>();
                student.User.Role = "Student";
                students.Add(student);
            }

            await _appDbContext.Set<Student>().AddRangeAsync(students);
            await _appDbContext.SaveChangesAsync();
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
        public async Task<Student> Update(UpdateStudentRequest model)
        {
            Student student = await base.Update(model);
            model.Id = student.IdUser;
            await _userService.Update(model);
            return student;
        }
        public async Task DeleteAllStudents()
        {
            List<User> users = _appDbContext.Set<User>().Where(u => u.Role == "Student").AsNoTracking().ToList();
            _appDbContext.Set<User>().RemoveRange(users);
            await _appDbContext.SaveChangesAsync();
        }
    }
}

