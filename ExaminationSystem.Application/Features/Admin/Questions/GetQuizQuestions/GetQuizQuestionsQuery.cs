using ExaminationSystem.Application.Common;
using ExaminationSystem.Application.Features.Admin.Questions.GetQuizQuestions.ViewModels;
using MediatR;

namespace ExaminationSystem.Application.Features.Admin.Questions.GetQuizQuestions;

public sealed record GetQuizQuestionsQuery(
    Guid QuizId) : IRequest<Result<IReadOnlyList<QuestionListItemViewModel>>>;
