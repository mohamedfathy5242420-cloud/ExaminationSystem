namespace ExaminationSystem.Application.Features.Admin.Monitoring.GetStudentAttemptDetails.ViewModels;

public sealed record AdminStudentAttemptDetailsViewModel(
    Guid AttemptId,
    Guid StudentId,
    string StudentName,
    string StudentEmail,
    Guid QuizId,
    string QuizTitle,
    Guid DiplomaId,
    string DiplomaTitle,
    string Status,
    int Score,
    int TotalScore,
    int PassScore,
    bool IsPassed,
    DateTime StartedAt,
    DateTime? EndedAt,
    IReadOnlyList<AdminAttemptAnswerDetailViewModel> Answers);
