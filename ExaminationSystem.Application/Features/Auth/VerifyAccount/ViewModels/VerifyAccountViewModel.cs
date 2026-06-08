namespace ExaminationSystem.Application.Features.Auth.VerifyAccount.ViewModels;

public sealed record VerifyAccountViewModel(
    Guid UserId,
    string Email,
    string Status,
    string Message);
