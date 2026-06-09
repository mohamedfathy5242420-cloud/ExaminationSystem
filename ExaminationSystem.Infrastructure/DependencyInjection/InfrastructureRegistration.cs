using ExaminationSystem.Application.Common.Events;
using ExaminationSystem.Application.Features.Auth.Login;
using ExaminationSystem.Application.Features.Auth.Register;
using ExaminationSystem.Application.Features.Auth.VerifyAccount;
using ExaminationSystem.Application.Interfaces.Orchestrators;
using ExaminationSystem.Infrastructure.Email;
using ExaminationSystem.Infrastructure.Events;
using ExaminationSystem.Infrastructure.Jwt;
using ExaminationSystem.Infrastructure.Orchestrators;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ExaminationSystem.Infrastructure;

public static class InfrastructureRegistration
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.Configure<SmtpOptions>(configuration.GetSection("Smtp"));
        services.Configure<JwtOptions>(configuration.GetSection("Jwt"));

        services.AddScoped<IRegisterUserOrchestrator, RegisterUserOrchestrator>();
        services.AddScoped<ILoginOrchestrator, LoginOrchestrator>();
        services.AddScoped<IVerifyAccountOrchestrator, VerifyAccountOrchestrator>();
        services.AddScoped<JwtTokenBuilder>();
        services.AddScoped<IEventDispatcher, InProcessEventDispatcher>();
        services.AddScoped<IEventHandler<UserRegisteredEvent>, UserRegisteredEventHandler>();
        services.AddScoped<IEventHandler<UserLoggedInEvent>, UserLoggedInEventHandler>();
        services.AddScoped<IEventHandler<AccountVerifiedEvent>, AccountVerifiedEventHandler>();

        return services;
    }
}
