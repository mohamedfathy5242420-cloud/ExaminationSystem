using ExaminationSystem.Application.Common;
using ExaminationSystem.Application.Features.Student.Quizzes.GetQuizResult.ViewModels;
using MediatR;

namespace ExaminationSystem.Application.Features.Student.Quizzes.GetQuizResult;

public sealed record GetQuizResultQuery(
    Guid StudentId,
    Guid AttemptId) : IRequest<Result<QuizResultViewModel>>;
