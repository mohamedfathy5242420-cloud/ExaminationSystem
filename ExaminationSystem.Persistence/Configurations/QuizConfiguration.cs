using ExaminationSystem.Domain.Entities.Quiz;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ExaminationSystem.Persistence.Configurations.Quiz;

public class QuizConfiguration : IEntityTypeConfiguration<Quizes>
{
    public void Configure(EntityTypeBuilder<Quizes> builder)
    {
        builder.Property(x => x.Title)
            .HasMaxLength(200)
            .IsRequired();

        builder.Property(x => x.Duration)
            .IsRequired();

        builder.Property(x => x.PassScore)
            .IsRequired();

        builder.Property(x => x.MaxAttempts)
            .IsRequired();

        builder.HasMany(x => x.Questions)
            .WithOne(x => x.Quiz)
            .HasForeignKey(x => x.QuizId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasIndex(x => x.DiplomaId);
    }
}