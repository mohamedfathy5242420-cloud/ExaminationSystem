using ExaminationSystem.Application.Common;
using ExaminationSystem.Application.Features.Student.Quizzes.GetQuizTimer;
using ExaminationSystem.Application.Features.Student.Quizzes.GetQuizTimer.ViewModels;

namespace ExaminationSystem.Application.Interfaces.Orchestrators;

public interface IGetQuizTimerOrchestrator
{
    Task<Result<QuizTimerViewModel>> GetAsync(
        GetQuizTimerQuery query,
        CancellationToken cancellationToken = default);
}
