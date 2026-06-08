using ExaminationSystem.Domain.Entities.Learning;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ExaminationSystem.Persistence.Configurations.Learning;

public class DiplomaConfiguration : IEntityTypeConfiguration<Diploma>
{
    public void Configure(EntityTypeBuilder<Diploma> builder)
    {
        builder.Property(x => x.Title)
            .HasMaxLength(200)
            .IsRequired();

        builder.Property(x => x.Description)
            .HasMaxLength(2000)
            .IsRequired();

        builder.HasMany(x => x.Quizzes)
            .WithOne(x => x.Diploma)
            .HasForeignKey(x => x.DiplomaId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(x => x.Enrollments)
            .WithOne(x => x.Diploma)
            .HasForeignKey(x => x.DiplomaId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(x => x.Instructor)
            .WithMany(x => x.Diplomas)
            .HasForeignKey(x => x.InstructorId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasIndex(x => x.Title);
    }
}
