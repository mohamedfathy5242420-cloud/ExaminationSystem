using ExaminationSystem.Application.Common;
using ExaminationSystem.Application.Features.Admin.Quizzes.CreateQuiz;
using ExaminationSystem.Application.Features.Admin.Quizzes.CreateQuiz.ViewModels;

namespace ExaminationSystem.Application.Interfaces.Orchestrators;

public interface ICreateQuizOrchestrator
{
    Task<Result<CreateQuizViewModel>> CreateAsync(
        CreateQuizCommand command,
        CancellationToken cancellationToken = default);
}
