using ExaminationSystem.Application.Common;
using ExaminationSystem.Application.Features.Student.Quizzes.GetQuizHistory.ViewModels;
using MediatR;

namespace ExaminationSystem.Application.Features.Student.Quizzes.GetQuizHistory;

public sealed record GetQuizHistoryQuery(
    Guid StudentId,
    Guid? QuizId,
    Guid? DiplomaId) : IRequest<Result<IReadOnlyList<QuizHistoryItemViewModel>>>;
