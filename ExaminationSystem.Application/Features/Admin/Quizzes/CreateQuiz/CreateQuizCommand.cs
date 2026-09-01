using ExaminationSystem.Application.Common;
using ExaminationSystem.Application.Features.Admin.Quizzes.CreateQuiz.ViewModels;
using MediatR;

namespace ExaminationSystem.Application.Features.Admin.Quizzes.CreateQuiz;

public sealed record CreateQuizCommand(
    Guid DiplomaId,
    string Title,
    int Duration,
    int PassScore,
    int MaxAttempts,
    string Instructions) : IRequest<Result<CreateQuizViewModel>>;
