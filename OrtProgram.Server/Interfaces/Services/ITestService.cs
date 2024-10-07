using OrtProgram.Server.Entities;

namespace OrtProgram.Server.Interfaces.Services;

public interface ITestService
{
    Task<IEnumerable<Test?>> GetAllTestsAsync();
}