using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Tester.Data.Entities;

namespace Tester.Data.TableCondiguration;

public class QuestionConfiguration : IEntityTypeConfiguration<Question>
{
    public void Configure(EntityTypeBuilder<Question> builder)
    {
        builder.HasKey(n => n.QuestionId);

        builder.Property(n => n.QuestionId)
               .ValueGeneratedOnAdd();

        builder.Property(n => n.QuestionText)
               .HasMaxLength(500);

        builder.HasOne(u => u.Test)
        .WithMany(up => up.Questions)
        .HasForeignKey(u => u.TestId)
        .OnDelete(DeleteBehavior.Cascade);
    }
}
