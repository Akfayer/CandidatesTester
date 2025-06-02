using Tester.Core.Models;
using Tester.Data.Entities;

namespace Tester.Core.Services.Interfaces;

public interface ITestResultService
{
    Task SaveTestResultAsync(TestResultModel result);
    Task<TestResultModel?> GetResultByIdAsync(int resultId);
    Task<IEnumerable<TestResultModel>> GetResultsByUserAsync(int userId);
    Task<double> GetAverageScoreAsync(int userId);
    Task<TestResultModel> CheckTestAsync(int userId, int testId, List<UserAnswerModel> userAnswers);
}
