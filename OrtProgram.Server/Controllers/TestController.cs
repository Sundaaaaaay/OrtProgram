using Microsoft.AspNetCore.Mvc;
using OrtProgram.Server.DTO.Test;
using OrtProgram.Server.Interfaces.Services;

namespace OrtProgram.Server.Controllers;

[Route("ort/tests")]
[ApiController]
public class TestController : ControllerBase
{
    private readonly ITestService _testService;
    private readonly ILogger<TestController> _logger;

    public TestController(ITestService testService, ILogger<TestController> logger)
    {
        _testService = testService;
        _logger = logger;
    }

    [Route("getall")]
    [HttpGet]
    public async Task<IActionResult> GetAllTestsInfo()
    {
        _logger.LogInformation("TestController::GetAllTestsInfo");
        return Ok(await _testService.GetAllAsync());
    }

    [Route("gettest{id:int}")]
    [HttpGet]
    public async Task<IActionResult> GetById([FromRoute] int id)
    {
        _logger.LogInformation("TestController::GetById");
        return Ok(await _testService.GetByIdAsync(id));
    }

    [Route("submitanswers")]
    [HttpPost]
    public async Task<IActionResult> SubmitAnswers([FromBody] UserAnswersSubmittionDto submission)
    {
        var results = await _testService.CheckAnswersAsync(submission.TestId, submission.Answers);
        return Ok(results);
    }
}