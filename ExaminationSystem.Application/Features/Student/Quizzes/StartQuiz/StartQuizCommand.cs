using ExaminationSystem.Application.Common;
using ExaminationSystem.Application.Features.Student.Quizzes.StartQuiz.ViewModels;
using MediatR;

namespace ExaminationSystem.Application.Features.Student.Quizzes.StartQuiz;

public sealed record StartQuizCommand(
    Guid StudentId,
    Guid QuizId) : IRequest<Result<StartQuizViewModel>>;
