using AutoMapper;
using CandidatesTesterAPI.DTOs.AnswerOptionDTOs;
using CandidatesTesterAPI.DTOs.QuestionDTOs;
using CandidatesTesterAPI.DTOs.TestDTOs;
using CandidatesTesterAPI.DTOs.UserAnswerDtos;
using Tester.Core.Models;

namespace CandidatesTesterAPI;

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
    }        
}
