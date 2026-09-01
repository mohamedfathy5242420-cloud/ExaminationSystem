using ExaminationSystem.Application.Common;
using ExaminationSystem.Application.Features.Student.Quizzes.SubmitQuiz.ViewModels;
using MediatR;

namespace ExaminationSystem.Application.Features.Student.Quizzes.SubmitQuiz;

public sealed record SubmitQuizCommand(
    Guid StudentId,
    Guid AttemptId) : IRequest<Result<SubmitQuizViewModel>>;
