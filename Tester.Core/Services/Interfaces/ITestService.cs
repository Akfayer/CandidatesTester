using Tester.Core.Models;
using Tester.Data.Entities;

namespace Tester.Core.Services.Interfaces;

public interface ITestService
{
    Task<IEnumerable<TestModel>> GetAllTestsAsync();
    Task<TestModel?> GetTestByIdAsync(int id);
    Task CreateTestAsync(TestModel testModel);
    Task UpdateTestAsync(TestModel testModel);
    Task DeleteTestAsync(int id);
    Task<List<QuestionModel>> GetQuestionsWithOptionsAsync(int testId);
    Task<TestResultModel> CheckTestAsync(int userId, int testId, List<UserAnswer> userAnswers);
}