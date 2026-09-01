using ExaminationSystem.Application.Common;
using ExaminationSystem.Application.Features.Admin.Quizzes.PublishQuiz.ViewModels;
using MediatR;

namespace ExaminationSystem.Application.Features.Admin.Quizzes.UnpublishQuiz;

public sealed record UnpublishQuizCommand(
    Guid Id) : IRequest<Result<PublishQuizViewModel>>;
