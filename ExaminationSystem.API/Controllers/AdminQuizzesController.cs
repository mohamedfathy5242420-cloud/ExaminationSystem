using ExaminationSystem.API.Authorization;
using ExaminationSystem.Application.Features.Admin.Quizzes.CreateQuiz;
using ExaminationSystem.Application.Features.Admin.Quizzes.CreateQuiz.Requests;
using ExaminationSystem.Application.Features.Admin.Quizzes.DeleteQuiz;
using ExaminationSystem.Application.Features.Admin.Quizzes.GetDiplomaQuizzes;
using ExaminationSystem.Application.Features.Admin.Quizzes.PublishQuiz;
using ExaminationSystem.Application.Features.Admin.Quizzes.UnpublishQuiz;
using ExaminationSystem.Application.Features.Admin.Quizzes.UpdateQuiz;
using ExaminationSystem.Application.Features.Admin.Quizzes.UpdateQuiz.Requests;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ExaminationSystem.API.Controllers;

[ApiController]
[Authorize(Policy = UserTypePolicies.AdminOnly)]
[Route("api/admin/quizzes")]
public class AdminQuizzesController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IValidator<CreateQuizCommand> _createQuizValidator;
    private readonly IValidator<DeleteQuizCommand> _deleteQuizValidator;
    private readonly IValidator<PublishQuizCommand> _publishQuizValidator;
    private readonly IValidator<UnpublishQuizCommand> _unpublishQuizValidator;
    private readonly IValidator<UpdateQuizCommand> _updateQuizValidator;

    public AdminQuizzesController(
        IMediator mediator,
        IValidator<CreateQuizCommand> createQuizValidator,
        IValidator<DeleteQuizCommand> deleteQuizValidator,
        IValidator<PublishQuizCommand> publishQuizValidator,
        IValidator<UnpublishQuizCommand> unpublishQuizValidator,
        IValidator<UpdateQuizCommand> updateQuizValidator)
    {
        _mediator = mediator;
        _createQuizValidator = createQuizValidator;
        _deleteQuizValidator = deleteQuizValidator;
        _publishQuizValidator = publishQuizValidator;
        _unpublishQuizValidator = unpublishQuizValidator;
        _updateQuizValidator = updateQuizValidator;
    }

    [HttpGet("by-diploma/{diplomaId:guid}")]
    public async Task<IActionResult> GetByDiploma(
        Guid diplomaId,
        CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new GetDiplomaQuizzesQuery(diplomaId), cancellationToken);

        return Ok(result.Value);
    }

    [HttpPost]
    public async Task<IActionResult> CreateQuiz(
        CreateQuizRequest request,
        CancellationToken cancellationToken)
    {
        var command = new CreateQuizCommand(
            request.DiplomaId,
            request.Title,
            request.Duration,
            request.PassScore,
            request.MaxAttempts,
            request.Instructions);

        var validationResult = await _createQuizValidator.ValidateAsync(command, cancellationToken);
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

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> UpdateQuiz(
        Guid id,
        UpdateQuizRequest request,
        CancellationToken cancellationToken)
    {
        var command = new UpdateQuizCommand(
            id,
            request.Title,
            request.Duration,
            request.PassScore,
            request.MaxAttempts,
            request.Instructions);

        var validationResult = await _updateQuizValidator.ValidateAsync(command, cancellationToken);
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

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteQuiz(
        Guid id,
        CancellationToken cancellationToken)
    {
        var command = new DeleteQuizCommand(id);

        var validationResult = await _deleteQuizValidator.ValidateAsync(command, cancellationToken);
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

    [HttpPost("{id:guid}/publish")]
    public async Task<IActionResult> PublishQuiz(
        Guid id,
        CancellationToken cancellationToken)
    {
        var command = new PublishQuizCommand(id);

        var validationResult = await _publishQuizValidator.ValidateAsync(command, cancellationToken);
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

    [HttpPost("{id:guid}/unpublish")]
    public async Task<IActionResult> UnpublishQuiz(
        Guid id,
        CancellationToken cancellationToken)
    {
        var command = new UnpublishQuizCommand(id);

        var validationResult = await _unpublishQuizValidator.ValidateAsync(command, cancellationToken);
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
