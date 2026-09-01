using ExaminationSystem.Application.Common;
using ExaminationSystem.Application.Features.Student.Quizzes.AnswerQuestion.ViewModels;
using MediatR;

namespace ExaminationSystem.Application.Features.Student.Quizzes.AnswerQuestion;

public sealed record AnswerQuestionCommand(
    Guid StudentId,
    Guid AttemptId,
    Guid QuestionId,
    Guid SelectedOptionId) : IRequest<Result<AnswerQuestionViewModel>>;
