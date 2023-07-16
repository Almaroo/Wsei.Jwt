using Wsei.Jwt.DTOs;

namespace Wsei.Jwt.Services;

public interface IStudentService
{
    public Task CreateAsync(CreateStudentDto dto);
    public Task<StudentDto?> GetAsync(int id);
    public Task<IEnumerable<StudentDto>> ListAsync();
}
