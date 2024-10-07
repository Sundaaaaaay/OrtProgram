using Microsoft.EntityFrameworkCore;
using OrtProgram.Server.Entities;
using OrtProgram.Server.Interfaces.Repositories;
using OrtProgram.Server.Data;

namespace OrtProgram.Server.Repositories;

public class TestRepository : ITestRepository
{
    private readonly ApplicationDbContext _dbContext;

    public TestRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task<IEnumerable<Test>> GetAllTestsAsync()
    {
        return await _dbContext.Tests.ToListAsync();
    }
}