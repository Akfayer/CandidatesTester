using Microsoft.EntityFrameworkCore;
using System.Reflection;
using Tester.Data.Entities;

namespace Tester.Data;

public class TesterContext : DbContext
{
    public TesterContext(DbContextOptions<TesterContext> options) : base(options)
    {
    }

    public DbSet<User> Users { get; set; }
    public DbSet<Test> Tests { get; set; }
    public DbSet<Question> Questions { get; set; }
    public DbSet<AnswerOption> AnswerOptions { get; set; }
    public DbSet<UserAnswer> UserAnswers { get; set; }
    public DbSet<TestResult> TestResults { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}
