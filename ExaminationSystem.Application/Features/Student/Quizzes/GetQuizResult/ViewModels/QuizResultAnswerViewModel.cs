namespace ExaminationSystem.Application.Features.Student.Quizzes.GetQuizResult.ViewModels;

public sealed record QuizResultAnswerViewModel(
    Guid QuestionId,
    string QuestionText,
    string Explanation,
    int Score,
    Guid? SelectedOptionId,
    string? SelectedOptionText,
    Guid CorrectOptionId,
    string CorrectOptionText,
    bool IsCorrect);
