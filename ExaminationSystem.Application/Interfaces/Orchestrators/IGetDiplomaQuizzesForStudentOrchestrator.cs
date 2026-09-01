using ExaminationSystem.Application.Common;
using ExaminationSystem.Application.Features.Student.Diplomas.GetDiplomaQuizzes;
using ExaminationSystem.Application.Features.Student.Diplomas.GetDiplomaQuizzes.ViewModels;

namespace ExaminationSystem.Application.Interfaces.Orchestrators;

public interface IGetDiplomaQuizzesForStudentOrchestrator
{
    Task<Result<IReadOnlyList<StudentDiplomaQuizViewModel>>> GetAsync(
        GetDiplomaQuizzesForStudentQuery query,
        CancellationToken cancellationToken = default);
}
