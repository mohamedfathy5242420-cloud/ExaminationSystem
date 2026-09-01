namespace ExaminationSystem.Application.Features.Auth.Login.Requests;

public sealed record LoginRequest(
    string Email,
    string Password);
