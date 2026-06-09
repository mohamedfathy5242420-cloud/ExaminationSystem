using ExaminationSystem.Application.Features.Auth.Register;
using ExaminationSystem.Application.Features.Auth.Login;
using ExaminationSystem.Application.Features.Auth.VerifyAccount;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace ExaminationSystem.Application;

public static class ApplicationRegistration
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMediatR(Assembly.GetExecutingAssembly());
        services.AddScoped<IValidator<RegisterUserCommand>, RegisterUserCommandValidator>();
        services.AddScoped<IValidator<LoginCommand>, LoginCommandValidator>();
        services.AddScoped<IValidator<VerifyAccountCommand>, VerifyAccountCommandValidator>();

        return services;
    }
}
