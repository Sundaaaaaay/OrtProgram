using OrtProgram.Server.Entities;

namespace OrtProgram.Server.Interfaces.Services;

public interface ITestService
{
    Task<IEnumerable<Test?>> GetAllAsync();
    Task<Test?> GetByIdAsync(int id);
}