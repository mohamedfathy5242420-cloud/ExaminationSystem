namespace ExaminationSystem.Application.Features.Auth.Register.ViewModels;

public sealed record RegisterUserViewModel(
    Guid UserId,
    string Email,
    string FullName,
    string UserType,
    string Status,
    string Message);
