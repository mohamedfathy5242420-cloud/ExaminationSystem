using ExaminationSystem.Application.Common;
using ExaminationSystem.Application.Features.Student.Diplomas.GetDiplomaQuizzes.ViewModels;
using MediatR;

namespace ExaminationSystem.Application.Features.Student.Diplomas.GetDiplomaQuizzes;

public sealed record GetDiplomaQuizzesForStudentQuery(
    Guid StudentId,
    Guid DiplomaId) : IRequest<Result<IReadOnlyList<StudentDiplomaQuizViewModel>>>;
