using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Tester.Data.Entities;

namespace Tester.Data.TableConfiguration;

public class TestResultConfiguration : IEntityTypeConfiguration<TestResult>
{
    public void Configure(EntityTypeBuilder<TestResult> builder)
    {
        builder.HasKey(n => n.TestResultId);

        builder.Property(n => n.TestResultId)
               .ValueGeneratedOnAdd();

        builder.HasOne(u => u.User)
        .WithMany(tr => tr.TestResults)
        .HasForeignKey(tr => tr.UserId)
        .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(u => u.Test)
        .WithMany(up => up.TestResults)
        .HasForeignKey(u => u.TestId)
        .OnDelete(DeleteBehavior.Cascade);
    }
}