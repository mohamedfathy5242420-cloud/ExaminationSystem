using ExaminationSystem.Domain.Entities.Learning;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ExaminationSystem.Persistence.Configurations.Learning;

public class EnrollmentConfiguration : IEntityTypeConfiguration<Enrollment>
{
    public void Configure(EntityTypeBuilder<Enrollment> builder)
    {
        builder.HasKey(x => x.Id);

        builder.HasIndex(x => new { x.StudentId, x.DiplomaId })
            .IsUnique();

        builder.Property(x => x.Progress)
            .HasPrecision(5, 2);

        builder.HasOne(x => x.Diploma)
            .WithMany()
            .HasForeignKey(x => x.DiplomaId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(x => x.Student)
            .WithMany(x => x.Enrollments)
            .HasForeignKey(x => x.StudentId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}