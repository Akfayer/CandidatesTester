using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Tester.Core.Models;
using Tester.Core.Services.Interfaces;
using Tester.Data.Entities;
using Tester.Data.Repositories.Interfaces;

namespace Tester.Core.Services;

public class TestResultService : ITestResultService
{
    private readonly IRepository<TestResult> _testResultRepository;
    private readonly IMapper _mapper;

    public TestResultService(IRepository<TestResult> testResultRepository, IMapper mapper)
    {
        _testResultRepository = testResultRepository;
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
}
