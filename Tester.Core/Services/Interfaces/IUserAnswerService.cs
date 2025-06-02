using Tester.Core.Models;
using Tester.Data.Entities;

namespace Tester.Core.Services.Interfaces;

public interface IUserAnswerService
{
    Task SaveUserAnswerAsync(UserAnswerModel answer);
    Task<IEnumerable<UserAnswerModel>> GetUserAnswersAsync(int userId);
    Task UpdateUserAnswerAsync(UserAnswerModel answer);
}
