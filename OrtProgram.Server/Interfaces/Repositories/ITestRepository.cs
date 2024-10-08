using OrtProgram.Server.Entities;

namespace OrtProgram.Server.Interfaces.Repositories;

public interface ITestRepository
{
    Task<IEnumerable<Test>> GetAllAsync();
    Task<Test?> GetByIdAsync(int id);
}