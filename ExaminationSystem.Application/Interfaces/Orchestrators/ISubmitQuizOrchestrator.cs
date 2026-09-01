using ExaminationSystem.Application.Common;
using ExaminationSystem.Application.Features.Student.Quizzes.SubmitQuiz;
using ExaminationSystem.Application.Features.Student.Quizzes.SubmitQuiz.ViewModels;

namespace ExaminationSystem.Application.Interfaces.Orchestrators;

public interface ISubmitQuizOrchestrator
{
    Task<Result<SubmitQuizViewModel>> SubmitAsync(
        SubmitQuizCommand command,
        CancellationToken cancellationToken = default);
}
