using ExaminationSystem.Application.Common;
using ExaminationSystem.Application.Features.Admin.Quizzes.UpdateQuiz;
using ExaminationSystem.Application.Features.Admin.Quizzes.UpdateQuiz.ViewModels;

namespace ExaminationSystem.Application.Interfaces.Orchestrators;

public interface IUpdateQuizOrchestrator
{
    Task<Result<UpdateQuizViewModel>> UpdateAsync(
        UpdateQuizCommand command,
        CancellationToken cancellationToken = default);
}
