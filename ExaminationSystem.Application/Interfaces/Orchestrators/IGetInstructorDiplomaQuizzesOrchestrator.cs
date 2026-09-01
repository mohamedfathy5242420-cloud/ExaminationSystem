using ExaminationSystem.Application.Common;
using ExaminationSystem.Application.Features.Instructor.Quizzes.GetInstructorDiplomaQuizzes;
using ExaminationSystem.Application.Features.Instructor.Quizzes.GetInstructorDiplomaQuizzes.ViewModels;

namespace ExaminationSystem.Application.Interfaces.Orchestrators;

public interface IGetInstructorDiplomaQuizzesOrchestrator
{
    Task<Result<IReadOnlyList<InstructorQuizListItemViewModel>>> GetAsync(
        GetInstructorDiplomaQuizzesQuery query,
        CancellationToken cancellationToken = default);
}
