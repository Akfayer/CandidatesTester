using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Tester.Core.Models;
using Tester.Core.Services.Interfaces;
using Tester.Data.Entities;
using Tester.Data.Repositories.Interfaces;

namespace Tester.Core.Services;

public class AnswerOptionService : IAnswerOptionService
{
    private readonly IRepository<AnswerOption> _answerOptionRepository;
    private readonly IMapper _mapper;

    public AnswerOptionService(IRepository<AnswerOption> answerOptionRepository, IMapper mapper)
    {
        _answerOptionRepository = answerOptionRepository;
        _mapper = mapper;
    }

    public async Task CreateAnswerOptionAsync(AnswerOptionModel answerOptionModel)
    {
        AnswerOption answerOption = _mapper.Map<AnswerOption>(answerOptionModel);

        await _answerOptionRepository.CreateAsync(answerOption);
    }

    public async Task DeleteAnswerOptionAsync(int answerOptionId)
    {
        var option = await _answerOptionRepository.GetByIdAsync(answerOptionId);
        if (option is null)
            throw new Exception("Answer haven't been found");

        await _answerOptionRepository.DeleteAsync(option);
    }

    public async Task<List<AnswerOptionModel>> GetAllAnswerOptionsByQuestionIdAsync(int questionId)
    {
        List<AnswerOption> allOptions = await _answerOptionRepository.GetQueryable()
            .Where(a => a.QuestionId == questionId)
            .ToListAsync();
        return _mapper.Map<List<AnswerOptionModel>>(allOptions);
    }

    public async Task<List<AnswerOptionModel>> GetCorrectAnswerOptionsAsync(int questionId)
    {
        List<AnswerOption> correctOptions = await _answerOptionRepository.GetQueryable()
            .Where(a => a.QuestionId == questionId && a.IsCorrect)
            .ToListAsync();
        return _mapper.Map<List<AnswerOptionModel>>(correctOptions);
    }

    public async Task UpdateAnswerOptionAsync(AnswerOptionModel answerOptionModel)
    {
        AnswerOption option = await _answerOptionRepository.GetByIdAsync(answerOptionModel.AnswerOptionId);
        if (option is null)
            throw new Exception("Answer haven't been found");

        _mapper.Map(answerOptionModel, option);

        await _answerOptionRepository.UpdateAsync(option);
    }
}
