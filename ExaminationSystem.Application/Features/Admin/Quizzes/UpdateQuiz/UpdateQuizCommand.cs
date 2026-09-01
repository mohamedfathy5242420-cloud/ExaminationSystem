using ExaminationSystem.Application.Common;
using ExaminationSystem.Application.Features.Admin.Quizzes.UpdateQuiz.ViewModels;
using MediatR;

namespace ExaminationSystem.Application.Features.Admin.Quizzes.UpdateQuiz;

public sealed record UpdateQuizCommand(
    Guid Id,
    string Title,
    int Duration,
    int PassScore,
    int MaxAttempts,
    string Instructions) : IRequest<Result<UpdateQuizViewModel>>;
