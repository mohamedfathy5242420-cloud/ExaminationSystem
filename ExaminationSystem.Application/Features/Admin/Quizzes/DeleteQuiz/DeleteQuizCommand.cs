using ExaminationSystem.Application.Common;
using ExaminationSystem.Application.Features.Admin.Quizzes.DeleteQuiz.ViewModels;
using MediatR;

namespace ExaminationSystem.Application.Features.Admin.Quizzes.DeleteQuiz;

public sealed record DeleteQuizCommand(
    Guid Id) : IRequest<Result<DeleteQuizViewModel>>;
