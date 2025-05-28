using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Tester.Data.Entities;

namespace Tester.Data.TableConfiguration;

public class UserAnswerConfiguration : IEntityTypeConfiguration<UserAnswer>
{
    public void Configure(EntityTypeBuilder<UserAnswer> builder)
    {
        builder.HasKey(n => n.UserAnswerId);

        builder.Property(n => n.UserAnswerId)
               .ValueGeneratedOnAdd();

        builder.HasOne(u => u.User)
        .WithMany(ua => ua.UserAnswers)
        .HasForeignKey(u => u.UserId)
        .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(q => q.Question)
        .WithMany(ua => ua.UserAnswers)
        .HasForeignKey(q => q.QuestionId)
        .OnDelete(DeleteBehavior.NoAction);

        builder.HasOne(ao => ao.AnswerOption)
        .WithMany(ua => ua.UserAnswers)
        .HasForeignKey(ao => ao.AnswerOptionId)
        .OnDelete(DeleteBehavior.NoAction);
    }
}
