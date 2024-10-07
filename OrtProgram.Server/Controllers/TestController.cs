using Microsoft.AspNetCore.Mvc;
using NLog;
using OrtProgram.Server.Interfaces.Repositories;
using OrtProgram.Server.Interfaces.Services;
using ILogger = Microsoft.Extensions.Logging.ILogger;

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
        return Ok(await _testService.GetAllTestsAsync());
    }
}