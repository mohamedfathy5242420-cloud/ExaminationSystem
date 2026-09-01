using ExaminationSystem.Application.Common;
using ExaminationSystem.Application.Features.Instructor.Quizzes.GetInstructorDiplomaQuizzes.ViewModels;
using MediatR;

namespace ExaminationSystem.Application.Features.Instructor.Quizzes.GetInstructorDiplomaQuizzes;

public sealed record GetInstructorDiplomaQuizzesQuery(
    Guid InstructorId,
    Guid DiplomaId) : IRequest<Result<IReadOnlyList<InstructorQuizListItemViewModel>>>;
