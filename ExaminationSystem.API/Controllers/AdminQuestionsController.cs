using ExaminationSystem.Application.Features.Admin.Questions.CreateQuestion;
using ExaminationSystem.Application.Features.Admin.Questions.CreateQuestion.Requests;
using ExaminationSystem.Application.Features.Admin.Questions.GetQuizQuestions;
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

    public AdminQuestionsController(
        IMediator mediator,
        IValidator<CreateQuestionCommand> createQuestionValidator)
    {
        _mediator = mediator;
        _createQuestionValidator = createQuestionValidator;
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
}
