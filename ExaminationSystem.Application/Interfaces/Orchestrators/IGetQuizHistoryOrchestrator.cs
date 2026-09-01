using ExaminationSystem.Application.Common;
using ExaminationSystem.Application.Features.Student.Quizzes.GetQuizHistory;
using ExaminationSystem.Application.Features.Student.Quizzes.GetQuizHistory.ViewModels;

namespace ExaminationSystem.Application.Interfaces.Orchestrators;

public interface IGetQuizHistoryOrchestrator
{
    Task<Result<IReadOnlyList<QuizHistoryItemViewModel>>> GetAsync(
        GetQuizHistoryQuery query,
        CancellationToken cancellationToken = default);
}
