using OrtProgram.Server.DTO.Test;
using OrtProgram.Server.Entities;
using OrtProgram.Server.Interfaces.Repositories;
using OrtProgram.Server.Interfaces.Services;
using OrtProgram.Server.Mappers;

namespace OrtProgram.Server.Services;

public class TestService : ITestService
{
    private readonly ITestRepository _testRepository;
    private readonly ILogger<TestService> _logger;

    public TestService(ITestRepository testRepository, ILogger<TestService> logger)
    {
        _testRepository = testRepository;
        _logger = logger;
    }
    
    public async Task<IEnumerable<ResponseTestDto?>> GetAllAsync()
    {
        try
        {
            IEnumerable<Test> tests = await _testRepository.GetAllAsync();
            var testDtos = tests.Select(s => s.ToResponseTestDto());
            
            _logger.LogInformation($"Found {tests.Count()} tests");

            return testDtos;
        }
        catch (Exception ex)
        {
            _logger.LogError($"Failed to get test data in TestService -> TestRepository.GetAllAsync {ex.Message}");
            
            throw new Exception(ex.Message);
        }
    }

    public async Task<Test?> GetByIdAsync(int id)
    {
        try
        {
            Test? test = await _testRepository.GetByIdAsync(id);
            _logger.LogInformation($"Found {test?.Id} test");

            return test;
        }
        catch (Exception ex)
        {
            _logger.LogError($"Failed to get test data in TestService -> TestRepository.GetByIdAsync {ex.Message}");
            throw new Exception(ex.Message);
        }
    }
}