using System.IdentityModel.Tokens.Jwt;
using ExaminationSystem.API.Authorization;
using ExaminationSystem.Application.Features.Student.Diplomas.BrowseDiplomas;
using ExaminationSystem.Application.Features.Student.Diplomas.EnrollInDiploma;
using ExaminationSystem.Application.Features.Student.Diplomas.EnrollInDiploma.Requests;
using ExaminationSystem.Application.Features.Student.Diplomas.GetDiplomaQuizzes;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ExaminationSystem.API.Controllers;

[ApiController]
[Authorize(Policy = UserTypePolicies.StudentOnly)]
[Route("api/student/diplomas")]
public class StudentDiplomasController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IValidator<EnrollInDiplomaCommand> _enrollInDiplomaValidator;

    public StudentDiplomasController(
        IMediator mediator,
        IValidator<EnrollInDiplomaCommand> enrollInDiplomaValidator)
    {
        _mediator = mediator;
        _enrollInDiplomaValidator = enrollInDiplomaValidator;
    }

    [HttpGet]
    public async Task<IActionResult> BrowseDiplomas(CancellationToken cancellationToken)
    {
        var studentId = GetStudentId();
        if (studentId is null)
        {
            return Forbid();
        }

        var result = await _mediator.Send(
            new BrowseDiplomasQuery(studentId.Value),
            cancellationToken);

        return Ok(result.Value);
    }

    [HttpPost("enroll")]
    public async Task<IActionResult> EnrollInDiploma(
        EnrollInDiplomaRequest request,
        CancellationToken cancellationToken)
    {
        var studentId = GetStudentId();
        if (studentId is null)
        {
            return Forbid();
        }

        var command = new EnrollInDiplomaCommand(
            studentId.Value,
            request.DiplomaId);

        var validationResult = await _enrollInDiplomaValidator.ValidateAsync(command, cancellationToken);
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

    [HttpGet("{diplomaId:guid}/quizzes")]
    public async Task<IActionResult> GetDiplomaQuizzes(
        Guid diplomaId,
        CancellationToken cancellationToken)
    {
        var studentId = GetStudentId();
        if (studentId is null)
        {
            return Forbid();
        }

        var result = await _mediator.Send(
            new GetDiplomaQuizzesForStudentQuery(studentId.Value, diplomaId),
            cancellationToken);

        if (result.IsFailure)
        {
            return BadRequest(new
            {
                error = result.Error
            });
        }

        return Ok(result.Value);
    }

    private Guid? GetStudentId()
    {
        var studentIdClaim = User.FindFirst(JwtRegisteredClaimNames.Sub)?.Value;
        var userType = User.FindFirst("user_type")?.Value;

        return Guid.TryParse(studentIdClaim, out var studentId) && userType == "Student"
            ? studentId
            : null;
    }
}
