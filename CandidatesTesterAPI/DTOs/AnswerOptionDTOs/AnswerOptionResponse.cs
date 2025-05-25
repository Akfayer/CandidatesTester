namespace CandidatesTesterAPI.DTOs.AnswerOptionDTOs;

public class AnswerOptionResponse
{
    public int AnswerOptionId { get; set; }
    public int QuestionId { get; set; }
    public string AnswerText { get; set; }
    public bool IsCorrect { get; set; }
}
