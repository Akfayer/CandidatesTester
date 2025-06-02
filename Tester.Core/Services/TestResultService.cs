using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Tester.Data.Enums;
using Tester.Core.Models;
using Tester.Core.Services.Interfaces;
using Tester.Data.Entities;
using Tester.Data.Repositories.Interfaces;

namespace Tester.Core.Services;

public class TestResultService : ITestResultService
{
    private readonly IRepository<TestResult> _testResultRepository;
    private readonly IRepository<Question> _questionRepository;
    private readonly IMapper _mapper;

    public TestResultService(IRepository<TestResult> testResultRepository,
        IRepository<Question> questionRepository, IMapper mapper)
    {
        _testResultRepository = testResultRepository;
        _questionRepository = questionRepository;
        _mapper = mapper;
    }

    public async Task<TestResultModel?> GetResultByIdAsync(int resultId)
    {
        TestResult testResult = await _testResultRepository.GetByIdAsync(resultId);
        if (testResult is null)
            throw new Exception("Test haven't been found");

        return _mapper.Map<TestResultModel>(testResult);
    }

    public async Task<IEnumerable<TestResultModel>> GetResultsByUserAsync(int userId)
    {
        IEnumerable<TestResult> testResults = await _testResultRepository
            .GetQueryable()
            .Where(r => r.UserId == userId)
            .ToListAsync();

        return _mapper.Map<IEnumerable<TestResultModel>>(testResults);
    }

    public async Task<double> GetAverageScoreAsync(int userId)
    {
        var results = await GetResultsByUserAsync(userId);
        if (!results.Any())
            return 0;

        return results.Average(r => (double)r.Score / r.MaxScore * 100);
    }

    public async Task SaveTestResultAsync(TestResultModel resultModel)
    {
        TestResult testResult = _mapper.Map<TestResult>(resultModel);

        await _testResultRepository.CreateAsync(testResult);
    }

    public async Task<TestResultModel> CheckTestAsync(int userId, int testId, List<UserAnswerModel> userAnswerModels)
    {
        var questions = await _questionRepository
                              .GetQueryable()
                              .Where(q => q.TestId == testId)
                              .Include(q => q.AnswerOptions)
                              .ToListAsync();

        int score = 0;
        int maxScore = questions.Count;
        var userAnswers = _mapper.Map<List<UserAnswer>>(userAnswerModels);

        foreach (var question in questions)
        {
            var userAnswer = userAnswers.FirstOrDefault(ua => ua.QuestionId == question.QuestionId);
            if (userAnswer == null)
                continue;

            if (question.TypeOfQuestion == QuestionType.SingleChoice)
            {
                var correctAnswerId = question.AnswerOptions.FirstOrDefault(a => a.IsCorrect)?.AnswerOptionId;
                if (correctAnswerId == userAnswer.AnswerOptionId)
                    score++;
            }
            else if (question.TypeOfQuestion == QuestionType.MultipleChoice)
            {
                var correctAnswersIds = question.AnswerOptions.Where(a => a.IsCorrect).ToList();
                var userSelectedAnswers = userAnswers
                            .Where(ua => ua.QuestionId == question.QuestionId && ua.AnswerOptionId.HasValue)
                            .Select(ua => ua.AnswerOptionId.Value)
                            .ToList();
                if (correctAnswersIds.Equals(userSelectedAnswers))
                {
                    score++;
                }
            }
            else if (question.TypeOfQuestion == QuestionType.OpenText)
            {
                var correctAnswer = question.AnswerOptions.FirstOrDefault(a => a.IsCorrect)?
                                                           .AnswerText?.Trim().ToLower();
                var userText = userAnswer.TextAnswer?.Trim().ToLower();
                if (!string.IsNullOrEmpty(userText) && correctAnswer == userText)
                    score++;
            }
        }

        TestResult testResult = new TestResult
        {
            UserId = userId,
            TestId = testId,
            Score = score,
            MaxScore = maxScore,
            CompletedAt = DateTime.UtcNow
        };

        await _testResultRepository.CreateAsync(testResult);

        return _mapper.Map<TestResultModel>(testResult);
    }
}
