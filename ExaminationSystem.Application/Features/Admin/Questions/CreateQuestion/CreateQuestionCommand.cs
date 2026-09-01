using ExaminationSystem.Application.Common;
using ExaminationSystem.Application.Features.Admin.Questions.CreateQuestion.ViewModels;
using MediatR;

namespace ExaminationSystem.Application.Features.Admin.Questions.CreateQuestion;

public sealed record CreateQuestionCommand(
    Guid QuizId,
    string Text,
    string Explanation,
    int Order,
    int Score,
    IReadOnlyList<CreateQuestionOptionCommandItem> Options) : IRequest<Result<CreateQuestionViewModel>>;
