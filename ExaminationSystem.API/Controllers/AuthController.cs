using ExaminationSystem.Application.Features.Auth.ForgotPassword;
using ExaminationSystem.Application.Features.Auth.ForgotPassword.Requests;
using ExaminationSystem.Application.Features.Auth.Login;
using ExaminationSystem.Application.Features.Auth.Login.Requests;
using ExaminationSystem.Application.Features.Auth.Register;
using ExaminationSystem.Application.Features.Auth.Register.Requests;
using ExaminationSystem.Application.Features.Auth.RefreshToken;
using ExaminationSystem.Application.Features.Auth.RefreshToken.Requests;
using ExaminationSystem.Application.Features.Auth.ResetPassword;
using ExaminationSystem.Application.Features.Auth.ResetPassword.Requests;
using ExaminationSystem.Application.Features.Auth.VerifyAccount;
using ExaminationSystem.Application.Features.Auth.VerifyAccount.Requests;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ExaminationSystem.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IValidator<ForgotPasswordCommand> _forgotPasswordValidator;
    private readonly IValidator<RegisterUserCommand> _registerValidator;
    private readonly IValidator<LoginCommand> _loginValidator;
    private readonly IValidator<RefreshTokenCommand> _refreshTokenValidator;
    private readonly IValidator<ResetPasswordCommand> _resetPasswordValidator;
    private readonly IValidator<VerifyAccountCommand> _verifyAccountValidator;

    public AuthController(
        IMediator mediator,
        IValidator<ForgotPasswordCommand> forgotPasswordValidator,
        IValidator<RegisterUserCommand> registerValidator,
        IValidator<LoginCommand> loginValidator,
        IValidator<RefreshTokenCommand> refreshTokenValidator,
        IValidator<ResetPasswordCommand> resetPasswordValidator,
        IValidator<VerifyAccountCommand> verifyAccountValidator)
    {
        _mediator = mediator;
        _forgotPasswordValidator = forgotPasswordValidator;
        _registerValidator = registerValidator;
        _loginValidator = loginValidator;
        _refreshTokenValidator = refreshTokenValidator;
        _resetPasswordValidator = resetPasswordValidator;
        _verifyAccountValidator = verifyAccountValidator;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(
        RegisterUserRequest request,
        CancellationToken cancellationToken)
    {
        var command = new RegisterUserCommand(
            request.FullName,
            request.Email,
            request.Password,
            request.UserType);

        var validationResult = await _registerValidator.ValidateAsync(command, cancellationToken);
        if (!validationResult.IsValid)
        {
            return BadRequest(new
            {
                errors = validationResult.Errors.Select(error => error.ErrorMessage)
            });
        }

        var result = await _mediator.Send(command, cancellationToken);
        if (result.IsFailure)
        {
            return BadRequest(new
            {
                error = result.Error
            });
        }

        return Ok(result.Value);
    }

    [HttpPost("forgot-password")]
    public async Task<IActionResult> ForgotPassword(
        ForgotPasswordRequest request,
        CancellationToken cancellationToken)
    {
        var command = new ForgotPasswordCommand(request.Email);

        var validationResult = await _forgotPasswordValidator.ValidateAsync(command, cancellationToken);
        if (!validationResult.IsValid)
        {
            return BadRequest(new
            {
                errors = validationResult.Errors.Select(error => error.ErrorMessage)
            });
        }

        var result = await _mediator.Send(command, cancellationToken);
        if (result.IsFailure)
        {
            return BadRequest(new
            {
                error = result.Error
            });
        }

        return Ok(result.Value);
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(
        LoginRequest request,
        CancellationToken cancellationToken)
    {
        var command = new LoginCommand(
            request.Email,
            request.Password);

        var validationResult = await _loginValidator.ValidateAsync(command, cancellationToken);
        if (!validationResult.IsValid)
        {
            return BadRequest(new
            {
                errors = validationResult.Errors.Select(error => error.ErrorMessage)
            });
        }

        var result = await _mediator.Send(command, cancellationToken);
        if (result.IsFailure)
        {
            return BadRequest(new
            {
                error = result.Error
            });
        }

        return Ok(result.Value);
    }

    [HttpPost("refresh-token")]
    public async Task<IActionResult> RefreshToken(
        RefreshTokenRequest request,
        CancellationToken cancellationToken)
    {
        var command = new RefreshTokenCommand(request.RefreshToken);

        var validationResult = await _refreshTokenValidator.ValidateAsync(command, cancellationToken);
        if (!validationResult.IsValid)
        {
            return BadRequest(new
            {
                errors = validationResult.Errors.Select(error => error.ErrorMessage)
            });
        }

        var result = await _mediator.Send(command, cancellationToken);
        if (result.IsFailure)
        {
            return BadRequest(new
            {
                error = result.Error
            });
        }

        return Ok(result.Value);
    }

    [HttpPost("reset-password")]
    public async Task<IActionResult> ResetPassword(
        ResetPasswordRequest request,
        CancellationToken cancellationToken)
    {
        var command = new ResetPasswordCommand(
            request.Email,
            request.OtpCode,
            request.NewPassword);

        var validationResult = await _resetPasswordValidator.ValidateAsync(command, cancellationToken);
        if (!validationResult.IsValid)
        {
            return BadRequest(new
            {
                errors = validationResult.Errors.Select(error => error.ErrorMessage)
            });
        }

        var result = await _mediator.Send(command, cancellationToken);
        if (result.IsFailure)
        {
            return BadRequest(new
            {
                error = result.Error
            });
        }

        return Ok(result.Value);
    }

    [HttpPost("verify-account")]
    public async Task<IActionResult> VerifyAccount(
        VerifyAccountRequest request,
        CancellationToken cancellationToken)
    {
        var command = new VerifyAccountCommand(
            request.Email,
            request.OtpCode);

        var validationResult = await _verifyAccountValidator.ValidateAsync(command, cancellationToken);
        if (!validationResult.IsValid)
        {
            return BadRequest(new
            {
                errors = validationResult.Errors.Select(error => error.ErrorMessage)
            });
        }

        var result = await _mediator.Send(command, cancellationToken);
        if (result.IsFailure)
        {
            return BadRequest(new
            {
                error = result.Error
            });
        }

        return Ok(result.Value);
    }
}
