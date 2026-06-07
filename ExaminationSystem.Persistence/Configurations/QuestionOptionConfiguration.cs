using ExaminationSystem.Domain.Entities.Quiz;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ExaminationSystem.Persistence.Configurations.Quiz;

public class QuestionOptionConfiguration : IEntityTypeConfiguration<QuestionOption>
{
    public void Configure(EntityTypeBuilder<QuestionOption> builder)
    {
        builder.Property(x => x.Text)
            .HasMaxLength(1000)
            .IsRequired();

        builder.Property(x => x.IsCorrect)
            .IsRequired();

        builder.HasIndex(x => x.QuestionId);
    }
}