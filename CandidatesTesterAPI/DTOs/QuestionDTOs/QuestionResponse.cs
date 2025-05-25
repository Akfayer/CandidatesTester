using Tester.Core.Enums;

namespace CandidatesTesterAPI.DTOs.QuestionDTOs;

public class QuestionResponse
{
    public int QuestionId { get; set; }
    public int TestId { get; set; }
    public string QuestionText { get; set; }
    public QuestionType TypeOfQuestion { get; set; }
}
