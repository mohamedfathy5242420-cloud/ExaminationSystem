using ExaminationSystem.Application.Common;
using ExaminationSystem.Application.Features.Admin.Questions.UpdateQuestion.ViewModels;
using MediatR;

namespace ExaminationSystem.Application.Features.Admin.Questions.UpdateQuestion;

public sealed record UpdateQuestionCommand(
    Guid Id,
    string Text,
    string Explanation,
    int Order,
    int Score,
    IReadOnlyList<UpdateQuestionOptionCommandItem> Options) : IRequest<Result<UpdateQuestionViewModel>>;
