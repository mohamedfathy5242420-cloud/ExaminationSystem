using ExaminationSystem.Application.Common;
using ExaminationSystem.Application.Features.Admin.Monitoring.GetStudentAttemptDetails;
using ExaminationSystem.Application.Features.Admin.Monitoring.GetStudentAttemptDetails.ViewModels;
using ExaminationSystem.Application.Interfaces;
using ExaminationSystem.Application.Interfaces.Orchestrators;
using ExaminationSystem.Domain.Entities.Attempt;
using ExaminationSystem.Domain.Entities.Identity;
using ExaminationSystem.Domain.Entities.Learning;
using ExaminationSystem.Domain.Entities.Quiz;
using Microsoft.AspNetCore.Identity;
using QuizEntity = ExaminationSystem.Domain.Entities.Quiz.Quiz;

namespace ExaminationSystem.Infrastructure.Orchestrators;

public class GetStudentAttemptDetailsOrchestrator : IGetStudentAttemptDetailsOrchestrator
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly UserManager<User> _userManager;

    public GetStudentAttemptDetailsOrchestrator(
        IUnitOfWork unitOfWork,
        UserManager<User> userManager)
    {
        _unitOfWork = unitOfWork;
        _userManager = userManager;
    }

    public async Task<Result<AdminStudentAttemptDetailsViewModel>> GetAsync(
        GetStudentAttemptDetailsQuery query,
        CancellationToken cancellationToken = default)
    {
        var attempt = await _unitOfWork.Repository<QuizAttempt>()
            .FirstOrDefaultAsync(x => x.Id == query.AttemptId, cancellationToken);

        if (attempt is null)
        {
            return Result<AdminStudentAttemptDetailsViewModel>.Failure("Attempt was not found.");
        }

        var quiz = await _unitOfWork.Repository<QuizEntity>()
            .FirstOrDefaultAsync(x => x.Id == attempt.QuizId, cancellationToken);

        if (quiz is null)
        {
            return Result<AdminStudentAttemptDetailsViewModel>.Failure("Quiz was not found.");
        }

        var diploma = await _unitOfWork.Repository<Diploma>()
            .FirstOrDefaultAsync(x => x.Id == quiz.DiplomaId, cancellationToken);

        var student = await _userManager.FindByIdAsync(attempt.StudentId.ToString());

        var questions = _unitOfWork.Repository<Question>()
            .Query()
            .Where(x => x.QuizId == attempt.QuizId)
            .OrderBy(x => x.Order)
            .ToList();

        var questionIds = questions.Select(x => x.Id).ToList();
        var answers = _unitOfWork.Repository<AttemptAnswer>()
            .Query()
            .Where(x => x.AttemptId == attempt.Id && questionIds.Contains(x.QuestionId))
            .ToList();

        var options = _unitOfWork.Repository<QuestionOption>()
            .Query()
            .Where(x => questionIds.Contains(x.QuestionId))
            .ToList();

        var answerDetails = questions
            .Select(question =>
            {
                var answer = answers.FirstOrDefault(x => x.QuestionId == question.Id);
                var selectedOption = answer?.SelectedOptionId is null
                    ? null
                    : options.FirstOrDefault(x => x.Id == answer.SelectedOptionId.Value);
                var correctOption = options.First(x => x.QuestionId == question.Id && x.IsCorrect);

                return new AdminAttemptAnswerDetailViewModel(
                    question.Id,
                    question.Text,
                    question.Explanation,
                    question.Score,
                    selectedOption?.Id,
                    selectedOption?.Text,
                    correctOption.Id,
                    correctOption.Text,
                    selectedOption?.Id == correctOption.Id);
            })
            .ToList();

        var viewModel = new AdminStudentAttemptDetailsViewModel(
            attempt.Id,
            attempt.StudentId,
            student?.FullName ?? "Deleted student",
            student?.Email ?? string.Empty,
            quiz.Id,
            quiz.Title,
            quiz.DiplomaId,
            diploma?.Title ?? "Deleted diploma",
            attempt.Status.ToString(),
            attempt.Score,
            questions.Sum(x => x.Score),
            quiz.PassScore,
            attempt.IsPassed,
            attempt.StartTime,
            attempt.EndTime,
            answerDetails);

        return Result<AdminStudentAttemptDetailsViewModel>.Success(viewModel);
    }
}
