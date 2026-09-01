using ExaminationSystem.Application.Common;
using ExaminationSystem.Application.Features.Student.Quizzes.GetQuizResult;
using ExaminationSystem.Application.Features.Student.Quizzes.GetQuizResult.ViewModels;
using ExaminationSystem.Application.Interfaces;
using ExaminationSystem.Application.Interfaces.Orchestrators;
using ExaminationSystem.Domain.Entities.Attempt;
using ExaminationSystem.Domain.Entities.Enums;
using ExaminationSystem.Domain.Entities.Quiz;
using QuizEntity = ExaminationSystem.Domain.Entities.Quiz.Quiz;

namespace ExaminationSystem.Infrastructure.Orchestrators;

public class GetQuizResultOrchestrator : IGetQuizResultOrchestrator
{
    private readonly IUnitOfWork _unitOfWork;

    public GetQuizResultOrchestrator(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public Task<Result<QuizResultViewModel>> GetAsync(
        GetQuizResultQuery query,
        CancellationToken cancellationToken = default)
    {
        var attempt = _unitOfWork.Repository<QuizAttempt>()
            .Query()
            .FirstOrDefault(x => x.Id == query.AttemptId && x.StudentId == query.StudentId);

        if (attempt is null)
        {
            return Task.FromResult(Result<QuizResultViewModel>.Failure("Attempt was not found."));
        }

        if (attempt.Status == AttemptStatus.InProgress)
        {
            return Task.FromResult(Result<QuizResultViewModel>.Failure("Attempt is still in progress."));
        }

        var quiz = _unitOfWork.Repository<QuizEntity>()
            .Query()
            .FirstOrDefault(x => x.Id == attempt.QuizId);

        if (quiz is null)
        {
            return Task.FromResult(Result<QuizResultViewModel>.Failure("Quiz was not found."));
        }

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

        var answerViewModels = questions
            .Select(question =>
            {
                var answer = answers.FirstOrDefault(x => x.QuestionId == question.Id);
                var selectedOption = answer?.SelectedOptionId is null
                    ? null
                    : options.FirstOrDefault(x => x.Id == answer.SelectedOptionId.Value);
                var correctOption = options.First(x => x.QuestionId == question.Id && x.IsCorrect);

                return new QuizResultAnswerViewModel(
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

        var viewModel = new QuizResultViewModel(
            attempt.Id,
            quiz.Id,
            quiz.Title,
            attempt.Score,
            questions.Sum(x => x.Score),
            quiz.PassScore,
            attempt.IsPassed,
            attempt.Status.ToString(),
            attempt.StartTime,
            attempt.EndTime,
            answerViewModels);

        return Task.FromResult(Result<QuizResultViewModel>.Success(viewModel));
    }
}
