using Tester.Core.Models;
using Tester.Data.Entities;

namespace Tester.Core.Services.Interfaces;

public interface IAnswerOptionService
{
    Task CreateAnswerOptionAsync(AnswerOptionModel answerOptionModel);
    Task UpdateAnswerOptionAsync(AnswerOptionModel answerOptionModel);
    Task DeleteAnswerOptionAsync(int answerOptionId);
    Task<List<AnswerOptionModel>> GetCorrectAnswerOptionsAsync(int questionId);
}
