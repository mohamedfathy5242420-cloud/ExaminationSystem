using ExaminationSystem.Application.Features.Admin.Questions.CreateQuestion;
using ExaminationSystem.Application.Features.Admin.Questions.CreateQuestion.Requests;
using ExaminationSystem.Application.Features.Admin.Questions.DeleteQuestion;
using ExaminationSystem.Application.Features.Admin.Questions.GetQuizQuestions;
using ExaminationSystem.Application.Features.Admin.Questions.UpdateQuestion;
using ExaminationSystem.Application.Features.Admin.Questions.UpdateQuestion.Requests;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ExaminationSystem.API.Controllers;

[ApiController]
[Authorize]
[Route("api/admin/questions")]
public class AdminQuestionsController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IValidator<CreateQuestionCommand> _createQuestionValidator;
    private readonly IValidator<DeleteQuestionCommand> _deleteQuestionValidator;
    private readonly IValidator<UpdateQuestionCommand> _updateQuestionValidator;

    public AdminQuestionsController(
        IMediator mediator,
        IValidator<CreateQuestionCommand> createQuestionValidator,
        IValidator<DeleteQuestionCommand> deleteQuestionValidator,
        IValidator<UpdateQuestionCommand> updateQuestionValidator)
    {
        _mediator = mediator;
        _createQuestionValidator = createQuestionValidator;
        _deleteQuestionValidator = deleteQuestionValidator;
        _updateQuestionValidator = updateQuestionValidator;
    }

    [HttpGet("by-quiz/{quizId:guid}")]
    public async Task<IActionResult> GetByQuiz(
        Guid quizId,
        CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new GetQuizQuestionsQuery(quizId), cancellationToken);

        return Ok(result.Value);
    }

    [HttpPost]
    public async Task<IActionResult> CreateQuestion(
        CreateQuestionRequest request,
        CancellationToken cancellationToken)
    {
        var command = new CreateQuestionCommand(
            request.QuizId,
            request.Text,
            request.Explanation,
            request.Order,
            request.Score,
            request.Options
                .Select(option => new CreateQuestionOptionCommandItem(
                    option.Text,
                    option.IsCorrect))
                .ToList());

        var validationResult = await _createQuestionValidator.ValidateAsync(command, cancellationToken);
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
    public async Task<IActionResult> UpdateQuestion(
        Guid id,
        UpdateQuestionRequest request,
        CancellationToken cancellationToken)
    {
        var command = new UpdateQuestionCommand(
            id,
            request.Text,
            request.Explanation,
            request.Order,
            request.Score,
            request.Options
                .Select(option => new UpdateQuestionOptionCommandItem(
                    option.Text,
                    option.IsCorrect))
                .ToList());

        var validationResult = await _updateQuestionValidator.ValidateAsync(command, cancellationToken);
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
    public async Task<IActionResult> DeleteQuestion(
        Guid id,
        CancellationToken cancellationToken)
    {
        var command = new DeleteQuestionCommand(id);

        var validationResult = await _deleteQuestionValidator.ValidateAsync(command, cancellationToken);
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
