using AutoMapper;
using Tester.Shared.DTOs.AnswerOptionDTOs;
using Tester.Shared.DTOs.QuestionDTOs;
using Tester.Shared.DTOs.TestDTOs;
using Tester.Shared.DTOs.UserAnswerDTOs;
using Tester.Core.Models;
using Tester.Shared.DTOs.AuthDTOs;

namespace Tester.Shared;

public class ApiMappingProfile : Profile
{
    public ApiMappingProfile()
    {
        CreateMap<TestModel, TestResponse>().ReverseMap();
        CreateMap<TestRequest, TestModel>().ReverseMap();

        CreateMap<QuestionModel, QuestionResponse>().ReverseMap();
        CreateMap<CreateQuestionRequest, QuestionModel>().ReverseMap();
        CreateMap<UpdateQuestionRequest, QuestionModel>().ReverseMap();

        CreateMap<AnswerOptionModel, AnswerOptionResponse>().ReverseMap();
        CreateMap<CreateAnswerOptionRequest, AnswerOptionModel>().ReverseMap();
        CreateMap<UpdateAnswerOptionRequest, AnswerOptionModel>().ReverseMap();

        CreateMap<UserAnswerModel, UserAnswerResponse>().ReverseMap();
        CreateMap<SubmitUserAnswerRequest, UserAnswerModel>().ReverseMap();

        CreateMap<RegisterRequest, UserModel>().ReverseMap();
        CreateMap<LoginRequest, LoginModel>().ReverseMap();
    }        
}
