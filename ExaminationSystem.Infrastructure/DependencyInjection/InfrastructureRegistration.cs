using ExaminationSystem.Application.Common.Events;
using ExaminationSystem.Application.Features.Admin.Diplomas.CreateDiploma;
using ExaminationSystem.Application.Features.Admin.Diplomas.DeleteDiploma;
using ExaminationSystem.Application.Features.Admin.Diplomas.UpdateDiploma;
using ExaminationSystem.Application.Features.Admin.Analytics.GetPerformanceAnalytics;
using ExaminationSystem.Application.Features.Admin.Monitoring.GetStudentAttemptDetails;
using ExaminationSystem.Application.Features.Admin.Monitoring.GetStudentAttempts;
using ExaminationSystem.Application.Features.Admin.Questions.CreateQuestion;
using ExaminationSystem.Application.Features.Admin.Questions.DeleteQuestion;
using ExaminationSystem.Application.Features.Admin.Questions.UpdateQuestion;
using ExaminationSystem.Application.Features.Admin.Quizzes.CreateQuiz;
using ExaminationSystem.Application.Features.Admin.Quizzes.DeleteQuiz;
using ExaminationSystem.Application.Features.Admin.Quizzes.PublishQuiz;
using ExaminationSystem.Application.Features.Admin.Quizzes.UnpublishQuiz;
using ExaminationSystem.Application.Features.Admin.Quizzes.UpdateQuiz;
using ExaminationSystem.Application.Features.Auth.ForgotPassword;
using ExaminationSystem.Application.Features.Auth.Login;
using ExaminationSystem.Application.Features.Auth.Register;
using ExaminationSystem.Application.Features.Auth.RefreshToken;
using ExaminationSystem.Application.Features.Auth.ResetPassword;
using ExaminationSystem.Application.Features.Auth.VerifyAccount;
using ExaminationSystem.Application.Features.Instructor.Dashboard.GetInstructorDashboard;
using ExaminationSystem.Application.Features.Instructor.Diplomas.GetInstructorDiplomas;
using ExaminationSystem.Application.Features.Instructor.Monitoring.GetInstructorStudentAttempts;
using ExaminationSystem.Application.Features.Instructor.Quizzes.GetInstructorDiplomaQuizzes;
using ExaminationSystem.Application.Features.Student.Dashboard.GetStudentDashboard;
using ExaminationSystem.Application.Features.Student.Diplomas.BrowseDiplomas;
using ExaminationSystem.Application.Features.Student.Diplomas.EnrollInDiploma;
using ExaminationSystem.Application.Features.Student.Diplomas.GetDiplomaQuizzes;
using ExaminationSystem.Application.Features.Student.Quizzes.AnswerQuestion;
using ExaminationSystem.Application.Features.Student.Quizzes.GetQuizHistory;
using ExaminationSystem.Application.Features.Student.Quizzes.GetQuizResult;
using ExaminationSystem.Application.Features.Student.Quizzes.GetQuizTimer;
using ExaminationSystem.Application.Features.Student.Quizzes.StartQuiz;
using ExaminationSystem.Application.Features.Student.Quizzes.SubmitQuiz;
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

        services.AddScoped<IAnswerQuestionOrchestrator, AnswerQuestionOrchestrator>();
        services.AddScoped<IBrowseDiplomasOrchestrator, BrowseDiplomasOrchestrator>();
        services.AddScoped<ICreateDiplomaOrchestrator, CreateDiplomaOrchestrator>();
        services.AddScoped<ICreateQuestionOrchestrator, CreateQuestionOrchestrator>();
        services.AddScoped<ICreateQuizOrchestrator, CreateQuizOrchestrator>();
        services.AddScoped<IDeleteDiplomaOrchestrator, DeleteDiplomaOrchestrator>();
        services.AddScoped<IDeleteQuestionOrchestrator, DeleteQuestionOrchestrator>();
        services.AddScoped<IDeleteQuizOrchestrator, DeleteQuizOrchestrator>();
        services.AddScoped<IForgotPasswordOrchestrator, ForgotPasswordOrchestrator>();
        services.AddScoped<IEnrollInDiplomaOrchestrator, EnrollInDiplomaOrchestrator>();
        services.AddScoped<IGetDiplomaQuizzesForStudentOrchestrator, GetDiplomaQuizzesForStudentOrchestrator>();
        services.AddScoped<IGetPerformanceAnalyticsOrchestrator, GetPerformanceAnalyticsOrchestrator>();
        services.AddScoped<IGetQuizHistoryOrchestrator, GetQuizHistoryOrchestrator>();
        services.AddScoped<IGetQuizResultOrchestrator, GetQuizResultOrchestrator>();
        services.AddScoped<IGetQuizTimerOrchestrator, GetQuizTimerOrchestrator>();
        services.AddScoped<IGetInstructorDashboardOrchestrator, GetInstructorDashboardOrchestrator>();
        services.AddScoped<IGetInstructorDiplomasOrchestrator, GetInstructorDiplomasOrchestrator>();
        services.AddScoped<IGetInstructorDiplomaQuizzesOrchestrator, GetInstructorDiplomaQuizzesOrchestrator>();
        services.AddScoped<IGetInstructorStudentAttemptsOrchestrator, GetInstructorStudentAttemptsOrchestrator>();
        services.AddScoped<IGetStudentAttemptDetailsOrchestrator, GetStudentAttemptDetailsOrchestrator>();
        services.AddScoped<IGetStudentAttemptsOrchestrator, GetStudentAttemptsOrchestrator>();
        services.AddScoped<IGetStudentDashboardOrchestrator, GetStudentDashboardOrchestrator>();
        services.AddScoped<IRegisterUserOrchestrator, RegisterUserOrchestrator>();
        services.AddScoped<ILoginOrchestrator, LoginOrchestrator>();
        services.AddScoped<IRefreshTokenOrchestrator, RefreshTokenOrchestrator>();
        services.AddScoped<IResetPasswordOrchestrator, ResetPasswordOrchestrator>();
        services.AddScoped<IStartQuizOrchestrator, StartQuizOrchestrator>();
        services.AddScoped<ISubmitQuizOrchestrator, SubmitQuizOrchestrator>();
        services.AddScoped<IPublishQuizOrchestrator, PublishQuizOrchestrator>();
        services.AddScoped<IUpdateDiplomaOrchestrator, UpdateDiplomaOrchestrator>();
        services.AddScoped<IUpdateQuestionOrchestrator, UpdateQuestionOrchestrator>();
        services.AddScoped<IUpdateQuizOrchestrator, UpdateQuizOrchestrator>();
        services.AddScoped<IUnpublishQuizOrchestrator, UnpublishQuizOrchestrator>();
        services.AddScoped<IVerifyAccountOrchestrator, VerifyAccountOrchestrator>();
        services.AddScoped<JwtTokenBuilder>();
        services.AddScoped<IEventDispatcher, InProcessEventDispatcher>();
        services.AddScoped<IEventHandler<DiplomaCreatedEvent>, DiplomaCreatedEventHandler>();
        services.AddScoped<IEventHandler<QuestionAnsweredEvent>, QuestionAnsweredEventHandler>();
        services.AddScoped<IEventHandler<QuestionCreatedEvent>, QuestionCreatedEventHandler>();
        services.AddScoped<IEventHandler<QuestionDeletedEvent>, QuestionDeletedEventHandler>();
        services.AddScoped<IEventHandler<QuestionUpdatedEvent>, QuestionUpdatedEventHandler>();
        services.AddScoped<IEventHandler<QuizSubmittedEvent>, QuizSubmittedEventHandler>();
        services.AddScoped<IEventHandler<QuizTimerExpiredEvent>, QuizTimerExpiredEventHandler>();
        services.AddScoped<IEventHandler<StudentEnrolledInDiplomaEvent>, StudentEnrolledInDiplomaEventHandler>();
        services.AddScoped<IEventHandler<QuizCreatedEvent>, QuizCreatedEventHandler>();
        services.AddScoped<IEventHandler<QuizDeletedEvent>, QuizDeletedEventHandler>();
        services.AddScoped<IEventHandler<QuizPublishedEvent>, QuizPublishedEventHandler>();
        services.AddScoped<IEventHandler<QuizUnpublishedEvent>, QuizUnpublishedEventHandler>();
        services.AddScoped<IEventHandler<QuizUpdatedEvent>, QuizUpdatedEventHandler>();
        services.AddScoped<IEventHandler<DiplomaDeletedEvent>, DiplomaDeletedEventHandler>();
        services.AddScoped<IEventHandler<DiplomaUpdatedEvent>, DiplomaUpdatedEventHandler>();
        services.AddScoped<IEventHandler<UserRegisteredEvent>, UserRegisteredEventHandler>();
        services.AddScoped<IEventHandler<UserLoggedInEvent>, UserLoggedInEventHandler>();
        services.AddScoped<IEventHandler<RefreshTokenRotatedEvent>, RefreshTokenRotatedEventHandler>();
        services.AddScoped<IEventHandler<QuizStartedEvent>, QuizStartedEventHandler>();
        services.AddScoped<IEventHandler<PasswordResetRequestedEvent>, PasswordResetRequestedEventHandler>();
        services.AddScoped<IEventHandler<PasswordResetCompletedEvent>, PasswordResetCompletedEventHandler>();
        services.AddScoped<IEventHandler<AccountVerifiedEvent>, AccountVerifiedEventHandler>();

        return services;
    }
}
