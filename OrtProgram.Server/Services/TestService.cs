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
    
    public async Task<IEnumerable<Test?>> GetAllTestsAsync()
    {
        try
        {
            IEnumerable<Test> tests = await _testRepository.GetAllTestsAsync();
            
            _logger.LogInformation($"Found {tests.Count()} tests");

            return tests;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            _logger.LogError($"Failed to get test data in TestService -> TestRepository.GetAllTestsAsync {ex.Message}");
            
            throw new Exception(ex.Message);
        }
    }
}