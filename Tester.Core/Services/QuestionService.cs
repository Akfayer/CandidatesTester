using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Tester.Core.Models;
using Tester.Core.Services.Interfaces;
using Tester.Data.Entities;
using Tester.Data.Enums;
using Tester.Data.Repositories.Interfaces;

namespace Tester.Core.Services;

public class QuestionService:IQuestionService
{
    private readonly IRepository<Question> _questionRepository;
    private readonly IMapper _mapper;

    public QuestionService(IRepository<Question> questionRepository, IMapper mapper)
    {
        _questionRepository = questionRepository;
        _mapper = mapper;
    }

    public async Task CreateQuestionAsync(QuestionModel questionModel)
    {
        Question question = _mapper.Map<Question>(questionModel);

        await _questionRepository.CreateAsync(question);
    }

    public async Task ChangeQuestionTypeAsync(QuestionModel questionModel)
    {
        Question question = await _questionRepository.GetByIdAsync(questionModel.QuestionId);
        if (question is null)
            throw new Exception("Question haven't been found");

        _mapper.Map(questionModel, question);
        await _questionRepository.UpdateAsync(question);
    }

    public async Task<List<QuestionModel>> GetQuestionsByTestIdAsync(int testId)
    {
        List<Question> questions = await _questionRepository
            .GetQueryable()
            .Where(q => q.TestId == testId)
            .Include(q => q.AnswerOptions)
            .ToListAsync();

        return _mapper.Map<List<QuestionModel>>(questions);
    }
}
