using OrtProgram.Server.DTO.Test;
using OrtProgram.Server.Entities;

namespace OrtProgram.Server.Interfaces.Services;

public interface ITestService
{
    Task<IEnumerable<ResponseTestDto?>> GetAllAsync();
    Task<Test?> GetByIdAsync(int id);
    Task<TestResultDto> CheckAnswersAsync(int id, List<UserAnswerDto> answers);
}