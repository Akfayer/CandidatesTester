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
    private readonly IMapper _mapper;

    public TestService(IRepository<Test> testRepo, IRepository<Question> questionRepo,
        IRepository<AnswerOption> answerOptionRepo, IRepository<UserAnswer> userAnswerRepo, IMapper mapper)
    {
        _testRepository = testRepo;
        _questionRepository = questionRepo;
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
}
