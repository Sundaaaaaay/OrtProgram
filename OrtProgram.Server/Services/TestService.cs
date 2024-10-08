using OrtProgram.Server.Entities;
using OrtProgram.Server.Interfaces.Repositories;
using OrtProgram.Server.Interfaces.Services;

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
    
    public async Task<IEnumerable<Test?>> GetAllAsync()
    {
        try
        {
            IEnumerable<Test> tests = await _testRepository.GetAllAsync();
            
            _logger.LogInformation($"Found {tests.Count()} tests");

            return tests;
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