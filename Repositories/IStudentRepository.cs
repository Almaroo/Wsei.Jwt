using Wsei.Jwt.Models;

namespace Wsei.Jwt.Repositories;

public interface IStudentRepository
{
    Task<IEnumerable<Student>> GetItems();
    Task<Student?> GetItem(int itemId);
    Task UpdateItem(Student item);
    Task CreateItem(Student item);
    Task DeleteItem(int itemId);
}