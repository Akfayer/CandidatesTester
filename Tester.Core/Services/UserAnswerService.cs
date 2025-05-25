using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Tester.Core.Models;
using Tester.Core.Services.Interfaces;
using Tester.Data.Entities;
using Tester.Data.Repositories.Interfaces;

namespace Tester.Core.Services;

public class UserAnswerService : IUserAnswerService
{
    private readonly IRepository<UserAnswer> _userAnswerRepository;
    private readonly IMapper _mapper;

    public UserAnswerService(IRepository<UserAnswer> userAnswerRepository,
                             IMapper mapper)
    {
        _userAnswerRepository = userAnswerRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<UserAnswerModel>> GetUserAnswersAsync(int userId)
    {
        List<UserAnswer> answers = await _userAnswerRepository
                     .GetQueryable()
                     .Where(ua=>ua.UserId==userId)
                     .Include(ua=>ua.Question)
                     .Include(ua=>ua.AnswerOption)
                     .ToListAsync();

        return _mapper.Map<List<UserAnswerModel>>(answers);
    }

    public async Task SaveUserAnswerAsync(UserAnswerModel userAnswerModel)
    {
        UserAnswer userAnswer = _mapper.Map<UserAnswer>(userAnswerModel);

        await _userAnswerRepository.CreateAsync(userAnswer);
    }
}
