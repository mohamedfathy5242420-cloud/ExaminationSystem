using System.IdentityModel.Tokens.Jwt;
using ExaminationSystem.Application.Features.Student.Quizzes.AnswerQuestion;
using ExaminationSystem.Application.Features.Student.Quizzes.AnswerQuestion.Requests;
using ExaminationSystem.Application.Features.Student.Quizzes.StartQuiz;
using ExaminationSystem.Application.Features.Student.Quizzes.StartQuiz.Requests;
using ExaminationSystem.Application.Features.Student.Quizzes.SubmitQuiz;
using ExaminationSystem.Application.Features.Student.Quizzes.SubmitQuiz.Requests;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ExaminationSystem.API.Controllers;

[ApiController]
[Authorize]
[Route("api/student/quizzes")]
public class StudentQuizzesController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IValidator<AnswerQuestionCommand> _answerQuestionValidator;
    private readonly IValidator<StartQuizCommand> _startQuizValidator;
    private readonly IValidator<SubmitQuizCommand> _submitQuizValidator;

    public StudentQuizzesController(
        IMediator mediator,
        IValidator<AnswerQuestionCommand> answerQuestionValidator,
        IValidator<StartQuizCommand> startQuizValidator,
        IValidator<SubmitQuizCommand> submitQuizValidator)
    {
        _mediator = mediator;
        _answerQuestionValidator = answerQuestionValidator;
        _startQuizValidator = startQuizValidator;
        _submitQuizValidator = submitQuizValidator;
    }

    [HttpPost("start")]
    public async Task<IActionResult> StartQuiz(
        StartQuizRequest request,
        CancellationToken cancellationToken)
    {
        var studentIdClaim = User.FindFirst(JwtRegisteredClaimNames.Sub)?.Value;
        var userType = User.FindFirst("user_type")?.Value;

        if (!Guid.TryParse(studentIdClaim, out var studentId) || userType != "Student")
        {
            return Forbid();
        }

        var command = new StartQuizCommand(
            studentId,
            request.QuizId);

        var validationResult = await _startQuizValidator.ValidateAsync(command, cancellationToken);
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

    [HttpPost("answer")]
    public async Task<IActionResult> AnswerQuestion(
        AnswerQuestionRequest request,
        CancellationToken cancellationToken)
    {
        var studentIdClaim = User.FindFirst(JwtRegisteredClaimNames.Sub)?.Value;
        var userType = User.FindFirst("user_type")?.Value;

        if (!Guid.TryParse(studentIdClaim, out var studentId) || userType != "Student")
        {
            return Forbid();
        }

        var command = new AnswerQuestionCommand(
            studentId,
            request.AttemptId,
            request.QuestionId,
            request.SelectedOptionId);

        var validationResult = await _answerQuestionValidator.ValidateAsync(command, cancellationToken);
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

    [HttpPost("submit")]
    public async Task<IActionResult> SubmitQuiz(
        SubmitQuizRequest request,
        CancellationToken cancellationToken)
    {
        var studentIdClaim = User.FindFirst(JwtRegisteredClaimNames.Sub)?.Value;
        var userType = User.FindFirst("user_type")?.Value;

        if (!Guid.TryParse(studentIdClaim, out var studentId) || userType != "Student")
        {
            return Forbid();
        }

        var command = new SubmitQuizCommand(
            studentId,
            request.AttemptId);

        var validationResult = await _submitQuizValidator.ValidateAsync(command, cancellationToken);
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
