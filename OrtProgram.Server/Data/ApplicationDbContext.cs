using Microsoft.EntityFrameworkCore;
using OrtProgram.Server.Entities;

namespace OrtProgram.Server.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
        
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Test>()
            .HasMany(q => q.Questions)
            .WithOne(t => t.Test)
            .HasForeignKey(t => t.TestId)
            .OnDelete(DeleteBehavior.Cascade); 
        
        base.OnModelCreating(modelBuilder);
    }

    public DbSet<Test> Tests { get; set; }
    public DbSet<Question> Questions { get; set; }
}