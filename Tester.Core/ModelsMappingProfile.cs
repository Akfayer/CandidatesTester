using AutoMapper;
using Tester.Core.Models;
using Tester.Data.Entities;

namespace Tester.Core;

public class ModelsMappingProfile:Profile
{
    public ModelsMappingProfile()
    {
        CreateMap<AnswerOption, AnswerOptionModel>().ReverseMap();
        CreateMap<Question, QuestionModel>().ReverseMap();
        CreateMap<Test, TestModel>().ReverseMap();
        CreateMap<TestResult, TestResultModel>().ReverseMap();
        CreateMap<User, UserModel>().ReverseMap();
        CreateMap<UserAnswer, UserAnswerModel>().ReverseMap();
        CreateMap<User, AuthModel>()
            .ForMember(dest => dest.Token, opt => opt.Ignore())
            .ForMember(dest => dest.Role, opt => opt.MapFrom(src => src.Role.ToString()));
    }
}
