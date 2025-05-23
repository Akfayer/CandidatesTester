using Microsoft.EntityFrameworkCore;
using Tester.Data.Enums;
using Tester.Core.Services.Interfaces;
using Tester.Data.Entities;
using Tester.Data.Repositories.Interfaces;
using AutoMapper;
using Tester.Core.Models;

namespace Tester.Core.Services;

public class TestService : ITestService
{
    private readonly IRepository<Test> _testRepository;
    private readonly IRepository<Question> _questionRepository;
    private readonly IRepository<TestResult> _testResultRepository;
    private readonly IMapper _mapper;

    public TestService(IRepository<Test> testRepo, IRepository<Question> questionRepo,
        IRepository<AnswerOption> answerOptionRepo, IRepository<UserAnswer> userAnswerRepo,
        IRepository<TestResult> testResultRepo, IMapper mapper)
    {
        _testRepository = testRepo;
        _questionRepository = questionRepo;
        _testResultRepository = testResultRepo;
        _mapper = mapper;
    }

    public async Task<TestModel?> GetTestByIdAsync(int id)
    {
        Test test = await _testRepository.GetByIdAsync(id);
        if (test is null)
            throw new Exception("Test haven't been found");

        return _mapper.Map<TestModel>(test);
    }

    public async Task<IEnumerable<TestModel>> GetAllTestsAsync()
    {
        IEnumerable<Test> tests = await _testRepository.GetAllAsync();
        return _mapper.Map<List<TestModel>>(tests);
    }

    public async Task CreateTestAsync(TestModel testModel)
    {
        Test test = _mapper.Map<Test>(testModel);
        await _testRepository.CreateAsync(test);
    }

    public async Task DeleteTestAsync(int id)
    {
        var test = await _testRepository.GetByIdAsync(id);
        if (test is null)
            throw new Exception("Test haven't been found");

        await _testRepository.DeleteAsync(test);
    }

    public async Task UpdateTestAsync(TestModel testModel)
    {
        Test test = await _testRepository.GetByIdAsync(testModel.TestId);
        if (test is null)
            throw new Exception("Test haven't been found");

        _mapper.Map(testModel, test);
        await _testRepository.UpdateAsync(test);
    }

    public async Task<List<QuestionModel>> GetQuestionsWithOptionsAsync(int testId)
    {
        List<Question> questions = await _questionRepository
            .GetQueryable()
            .Where(q => q.TestId == testId)
            .Include(q => q.AnswerOptions)
            .ToListAsync();

        return _mapper.Map<List<QuestionModel>>(questions);
    }

    public async Task<TestResultModel> CheckTestAsync(int userId, int testId, List<UserAnswer> userAnswers)
    {
        var questions = await _questionRepository
                              .GetQueryable()
                              .Where(q => q.TestId == testId)
                              .Include(q => q.AnswerOptions)
                              .ToListAsync();

        int score = 0;
        int maxScore = questions.Count;

        foreach (var question in questions)
        {
            var userAnswer = userAnswers.FirstOrDefault(ua => ua.QuestionId == question.QuestionId);
            if (userAnswer == null)
                continue;
            
            if(question.TypeOfQuestion == QuestionType.SingleChoice)
            {
                var correctAnswerId = question.AnswerOptions.FirstOrDefault(a => a.IsCorrect)?.AnswerOptionId;
                if (correctAnswerId == userAnswer.AnswerOptionId)
                    score++;
            }
            else if(question.TypeOfQuestion == QuestionType.MultipleChoice)
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
            else if(question.TypeOfQuestion == QuestionType.OpenText)
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
