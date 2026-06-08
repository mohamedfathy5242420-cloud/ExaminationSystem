namespace ExaminationSystem.Application.Features.Auth.Register.Requests;

public sealed record RegisterUserRequest(
    string FullName,
    string Email,
    string Password,
    string UserType);
