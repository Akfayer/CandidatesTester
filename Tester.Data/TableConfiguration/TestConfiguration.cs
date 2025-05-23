
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Tester.Data.Entities;

namespace Tester.Data.TableCondiguration;

public class TestConfiguration : IEntityTypeConfiguration<Test>
{
    public void Configure(EntityTypeBuilder<Test> builder)
    {
        builder.HasKey(n => n.TestId);

        builder.Property(n => n.TestId)
               .ValueGeneratedOnAdd();

        builder.Property(n => n.TestTitle)
               .HasMaxLength(100);
        
        builder.Property(n => n.TestDescription)
               .HasMaxLength(255);
    }
}
