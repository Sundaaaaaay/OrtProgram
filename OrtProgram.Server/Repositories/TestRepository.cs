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
    
    public async Task<IEnumerable<Test>> GetAllAsync()
    {
        return await _dbContext.Tests.ToListAsync();
    }

    public async Task<Test?> GetByIdAsync(int id)
    {
        return await _dbContext.Tests
            .Include(q => q.Questions)    
            .FirstOrDefaultAsync(x => x.Id == id);
    }
}