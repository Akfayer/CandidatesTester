using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Tester.Data.Entities;

namespace Tester.Data.TableConfiguration;

public class AnswerOptionConfiguration : IEntityTypeConfiguration<AnswerOption>
{
    public void Configure(EntityTypeBuilder<AnswerOption> builder)
    {
        builder.HasKey(n => n.AnswerOptionId);

        builder.Property(n => n.AnswerOptionId)
               .ValueGeneratedOnAdd();

        builder.Property(n => n.AnswerText)
               .HasMaxLength(255);

        builder.HasOne(u => u.Question)
        .WithMany(up => up.AnswerOptions)
        .HasForeignKey(u => u.QuestionId)
        .OnDelete(DeleteBehavior.Cascade);
    }
}