using ExaminationSystem.Domain.Entities.Attempt;
using ExaminationSystem.Domain.Entities.Identity;
using ExaminationSystem.Domain.Entities.Learning;
using ExaminationSystem.Domain.Entities.Quiz;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ExaminationSystem.Persistence.Context;

public class ApplicationDbContext
    : IdentityDbContext<User, IdentityRole<Guid>, Guid>
{
    public ApplicationDbContext(
        DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    // Identity
    public DbSet<OTP> OTPs => Set<OTP>();
    public DbSet<RefreshToken> RefreshTokens => Set<RefreshToken>();

    // Learning
    public DbSet<Diploma> Diplomas => Set<Diploma>();
    public DbSet<Enrollment> Enrollments => Set<Enrollment>();

    // Quiz
    public DbSet<Quizes> Quizzes => Set<Quizes>();
    public DbSet<Question> Questions => Set<Question>();
    public DbSet<QuestionOption> QuestionOptions => Set<QuestionOption>();

    // Attempts
    public DbSet<QuizAttempt> QuizAttempts => Set<QuizAttempt>();
    public DbSet<AttemptAnswer> AttemptAnswers => Set<AttemptAnswer>();

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.ApplyConfigurationsFromAssembly(
            typeof(ApplicationDbContext).Assembly);
    }
}