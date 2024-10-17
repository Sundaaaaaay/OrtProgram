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
            _logger.LogError($"Failed to get test data in TestService -> TestRepository.GetAllAsync: {ex.Message}");
            
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
            _logger.LogError($"Failed to get test data in TestService -> TestRepository.GetByIdAsync: {ex.Message}");
            throw new Exception(ex.Message);
        }
    }
    
    public async Task<TestResultDto> CheckAnswersAsync(int id, List<UserAnswerDto> answers)
    {
        try
        {
            var test = await _testRepository.GetByIdAsync(id);
            if (test == null)
            {
                _logger.LogWarning($"Test with id: {id} not found");
                throw new NullReferenceException($"Test with id: {id} not found");
            }

            // Подготовка вопросов для быстрого доступа
            var questionMap = test.Questions.ToDictionary(q => q.Id);

            var result = new TestResultDto { TotalQuestions = answers.Count };

            foreach (var answer in answers)
            {
                if (!questionMap.TryGetValue(answer.QuestionId, out var question))
                {
                    _logger.LogWarning($"Question with id: {answer.QuestionId} not found");
                    throw new NullReferenceException($"Question with id: {answer.QuestionId} not found");
                }
                
                var isCorrect = answer.SelectedAnswer == question.rightAnswer;

                result.Details.Add(new QuestionResultDtо
                {
                    CorrectAnswer = question.rightAnswer,
                    IsCorrect = isCorrect,
                    QuestionId = answer.QuestionId,
                });

                if (isCorrect) result.CorrectAnswers++;
            }

            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error checking answers for test {id} in TestService -> CheckAnswersAsync: {ex.Message}");
            throw;
        }
    }
}