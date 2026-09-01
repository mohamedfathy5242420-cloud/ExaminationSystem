using ExaminationSystem.API.Authorization;
using ExaminationSystem.Application.Features.Admin.Diplomas.CreateDiploma;
using ExaminationSystem.Application.Features.Admin.Diplomas.CreateDiploma.Requests;
using ExaminationSystem.Application.Features.Admin.Diplomas.DeleteDiploma;
using ExaminationSystem.Application.Features.Admin.Diplomas.GetDiplomas;
using ExaminationSystem.Application.Features.Admin.Diplomas.UpdateDiploma;
using ExaminationSystem.Application.Features.Admin.Diplomas.UpdateDiploma.Requests;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ExaminationSystem.API.Controllers;

[ApiController]
[Authorize(Policy = UserTypePolicies.AdminOnly)]
[Route("api/admin/diplomas")]
public class AdminDiplomasController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IValidator<CreateDiplomaCommand> _createDiplomaValidator;
    private readonly IValidator<DeleteDiplomaCommand> _deleteDiplomaValidator;
    private readonly IValidator<UpdateDiplomaCommand> _updateDiplomaValidator;

    public AdminDiplomasController(
        IMediator mediator,
        IValidator<CreateDiplomaCommand> createDiplomaValidator,
        IValidator<DeleteDiplomaCommand> deleteDiplomaValidator,
        IValidator<UpdateDiplomaCommand> updateDiplomaValidator)
    {
        _mediator = mediator;
        _createDiplomaValidator = createDiplomaValidator;
        _deleteDiplomaValidator = deleteDiplomaValidator;
        _updateDiplomaValidator = updateDiplomaValidator;
    }

    [HttpGet]
    public async Task<IActionResult> GetDiplomas(CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new GetDiplomasQuery(), cancellationToken);

        return Ok(result.Value);
    }

    [HttpPost]
    public async Task<IActionResult> CreateDiploma(
        CreateDiplomaRequest request,
        CancellationToken cancellationToken)
    {
        var command = new CreateDiplomaCommand(
            request.Title,
            request.Description,
            request.InstructorId);

        var validationResult = await _createDiplomaValidator.ValidateAsync(command, cancellationToken);
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
    public async Task<IActionResult> UpdateDiploma(
        Guid id,
        UpdateDiplomaRequest request,
        CancellationToken cancellationToken)
    {
        var command = new UpdateDiplomaCommand(
            id,
            request.Title,
            request.Description,
            request.InstructorId);

        var validationResult = await _updateDiplomaValidator.ValidateAsync(command, cancellationToken);
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
    public async Task<IActionResult> DeleteDiploma(
        Guid id,
        CancellationToken cancellationToken)
    {
        var command = new DeleteDiplomaCommand(id);

        var validationResult = await _deleteDiplomaValidator.ValidateAsync(command, cancellationToken);
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
