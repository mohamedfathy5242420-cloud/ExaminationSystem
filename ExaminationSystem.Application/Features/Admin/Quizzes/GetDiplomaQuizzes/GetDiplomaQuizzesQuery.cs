using ExaminationSystem.Application.Common;
using ExaminationSystem.Application.Features.Admin.Quizzes.GetDiplomaQuizzes.ViewModels;
using MediatR;

namespace ExaminationSystem.Application.Features.Admin.Quizzes.GetDiplomaQuizzes;

public sealed record GetDiplomaQuizzesQuery(
    Guid DiplomaId) : IRequest<Result<IReadOnlyList<QuizListItemViewModel>>>;
