namespace ExaminationSystem.Application.Features.Admin.Monitoring.GetStudentAttemptDetails.ViewModels;

public sealed record AdminAttemptAnswerDetailViewModel(
    Guid QuestionId,
    string QuestionText,
    string Explanation,
    int Score,
    Guid? SelectedOptionId,
    string? SelectedOptionText,
    Guid CorrectOptionId,
    string CorrectOptionText,
    bool IsCorrect);
