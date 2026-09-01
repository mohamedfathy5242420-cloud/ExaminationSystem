using ExaminationSystem.Application.Common;
using ExaminationSystem.Application.Features.Admin.Questions.DeleteQuestion.ViewModels;
using MediatR;

namespace ExaminationSystem.Application.Features.Admin.Questions.DeleteQuestion;

public sealed record DeleteQuestionCommand(
    Guid Id) : IRequest<Result<DeleteQuestionViewModel>>;
