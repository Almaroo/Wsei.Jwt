using Microsoft.EntityFrameworkCore;
using Wsei.Jwt.DbContexts;
using Wsei.Jwt.Models;

namespace Wsei.Jwt.Repositories;

public class StudentRepository : IStudentRepository
{
    private readonly StudentDbContext _studentDbContext;

    public StudentRepository(StudentDbContext studentDbContext)
    {
        _studentDbContext = studentDbContext;
    }

    public async Task<IEnumerable<Student>> GetItems() =>
        await _studentDbContext.Students.ToListAsync();

    public async Task<Student?> GetItem(int itemId) =>
        await _studentDbContext.Students.FirstOrDefaultAsync(x => x.StudentId == itemId);

    public async Task UpdateItem(Student student)
    {
        var oldStudent = await _studentDbContext.Students.FirstOrDefaultAsync(
            x => x.StudentId == student.StudentId
        );
        if (oldStudent is null)
            return;

        oldStudent.Age = student.Age;
        oldStudent.Name = student.Name;
        oldStudent.LastName = student.LastName;

        await _studentDbContext.SaveChangesAsync();
    }

    public async Task CreateItem(Student item)
    {
        _studentDbContext.Students.Add(item);
        await _studentDbContext.SaveChangesAsync();
    }

    public async Task DeleteItem(int itemId)
    {
        _studentDbContext.Students.Remove(new() { StudentId = itemId });
        await _studentDbContext.SaveChangesAsync();
    }
}
