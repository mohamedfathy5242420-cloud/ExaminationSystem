using ExaminationSystem.Application.Common;
using ExaminationSystem.Application.Features.Student.Quizzes.GetQuizTimer.ViewModels;
using MediatR;

namespace ExaminationSystem.Application.Features.Student.Quizzes.GetQuizTimer;

public sealed record GetQuizTimerQuery(
    Guid StudentId,
    Guid AttemptId) : IRequest<Result<QuizTimerViewModel>>;
