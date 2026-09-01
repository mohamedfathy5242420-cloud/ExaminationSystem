using ExaminationSystem.Application.Common;
using ExaminationSystem.Application.Features.Admin.Quizzes.PublishQuiz.ViewModels;
using MediatR;

namespace ExaminationSystem.Application.Features.Admin.Quizzes.PublishQuiz;

public sealed record PublishQuizCommand(
    Guid Id) : IRequest<Result<PublishQuizViewModel>>;
