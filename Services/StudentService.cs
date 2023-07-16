using Wsei.Jwt.DTOs;
using Wsei.Jwt.Models;
using Wsei.Jwt.Repositories;

namespace Wsei.Jwt.Services;

public class StudentService : IStudentService
{
    private readonly IStudentRepository _studentRepository;

    public StudentService(IStudentRepository studentRepository)
    {
        _studentRepository = studentRepository;
    }

    public async Task CreateAsync(CreateStudentDto dto)
    {
        await _studentRepository.CreateItem(Map(dto));
    }

    public async Task<StudentDto?> GetAsync(int id)
    {
        var result = await _studentRepository.GetItem(id);
        return result is not null ? Map(result) : null;
    }

    public async Task<IEnumerable<StudentDto>> ListAsync()
    {
        return (await _studentRepository.GetItems()).Select(Map);
    }

    private static Student Map(CreateStudentDto dto) =>
        new()
        {
            Name = dto.Name,
            LastName = dto.LastName,
            Age = dto.Age
        };

    private static StudentDto Map(Student entity) =>
        new()
        {
            Age = entity.Age,
            Name = entity.Name,
            LastName = entity.LastName,
            Id = entity.StudentId
        };
}
