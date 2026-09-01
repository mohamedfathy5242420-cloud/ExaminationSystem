using ExaminationSystem.Application.Common;
using ExaminationSystem.Application.Features.Student.Quizzes.StartQuiz;
using ExaminationSystem.Application.Features.Student.Quizzes.StartQuiz.ViewModels;

namespace ExaminationSystem.Application.Interfaces.Orchestrators;

public interface IStartQuizOrchestrator
{
    Task<Result<StartQuizViewModel>> StartAsync(
        StartQuizCommand command,
        CancellationToken cancellationToken = default);
}
