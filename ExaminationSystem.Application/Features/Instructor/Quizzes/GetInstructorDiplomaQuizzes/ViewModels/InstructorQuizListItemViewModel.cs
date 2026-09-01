namespace ExaminationSystem.Application.Features.Instructor.Quizzes.GetInstructorDiplomaQuizzes.ViewModels;

public sealed record InstructorQuizListItemViewModel(
    Guid Id,
    Guid DiplomaId,
    string Title,
    int Duration,
    int PassScore,
    int MaxAttempts,
    string Instructions,
    bool IsPublished,
    int QuestionsCount,
    int AttemptsCount);
