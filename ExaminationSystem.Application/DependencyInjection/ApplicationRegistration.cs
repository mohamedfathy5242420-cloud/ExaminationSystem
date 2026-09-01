using ExaminationSystem.Application.Features.Admin.Diplomas.CreateDiploma;
using ExaminationSystem.Application.Features.Admin.Diplomas.DeleteDiploma;
using ExaminationSystem.Application.Features.Admin.Diplomas.UpdateDiploma;
using ExaminationSystem.Application.Features.Admin.Questions.CreateQuestion;
using ExaminationSystem.Application.Features.Admin.Quizzes.CreateQuiz;
using ExaminationSystem.Application.Features.Admin.Quizzes.DeleteQuiz;
using ExaminationSystem.Application.Features.Admin.Quizzes.PublishQuiz;
using ExaminationSystem.Application.Features.Admin.Quizzes.UnpublishQuiz;
using ExaminationSystem.Application.Features.Admin.Quizzes.UpdateQuiz;
using ExaminationSystem.Application.Features.Auth.ForgotPassword;
using ExaminationSystem.Application.Features.Auth.Register;
using ExaminationSystem.Application.Features.Auth.Login;
using ExaminationSystem.Application.Features.Auth.RefreshToken;
using ExaminationSystem.Application.Features.Auth.ResetPassword;
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
        services.AddScoped<IValidator<CreateDiplomaCommand>, CreateDiplomaCommandValidator>();
        services.AddScoped<IValidator<CreateQuestionCommand>, CreateQuestionCommandValidator>();
        services.AddScoped<IValidator<CreateQuizCommand>, CreateQuizCommandValidator>();
        services.AddScoped<IValidator<DeleteQuizCommand>, DeleteQuizCommandValidator>();
        services.AddScoped<IValidator<PublishQuizCommand>, PublishQuizCommandValidator>();
        services.AddScoped<IValidator<UnpublishQuizCommand>, UnpublishQuizCommandValidator>();
        services.AddScoped<IValidator<UpdateQuizCommand>, UpdateQuizCommandValidator>();
        services.AddScoped<IValidator<DeleteDiplomaCommand>, DeleteDiplomaCommandValidator>();
        services.AddScoped<IValidator<UpdateDiplomaCommand>, UpdateDiplomaCommandValidator>();
        services.AddScoped<IValidator<ForgotPasswordCommand>, ForgotPasswordCommandValidator>();
        services.AddScoped<IValidator<RegisterUserCommand>, RegisterUserCommandValidator>();
        services.AddScoped<IValidator<LoginCommand>, LoginCommandValidator>();
        services.AddScoped<IValidator<RefreshTokenCommand>, RefreshTokenCommandValidator>();
        services.AddScoped<IValidator<ResetPasswordCommand>, ResetPasswordCommandValidator>();
        services.AddScoped<IValidator<VerifyAccountCommand>, VerifyAccountCommandValidator>();

        return services;
    }
}
