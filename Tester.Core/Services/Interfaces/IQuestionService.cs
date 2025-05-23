using Tester.Core.Models;
using Tester.Data.Enums;

namespace Tester.Core.Services.Interfaces;

public interface IQuestionService
{
    Task CreateQuestionAsync(QuestionModel question);
    Task<List<QuestionModel>> GetQuestionsByTestIdAsync(int testId);
    Task ChangeQuestionTypeAsync(QuestionModel questionModel);
}
