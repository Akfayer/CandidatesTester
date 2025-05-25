namespace CandidatesTesterAPI.DTOs.UserAnswerDtos;

public class UserAnswerResponse
{
    public int UserAnswerId { get; set; }
    public int UserId { get; set; }
    public int QuestionId { get; set; }
    public int? AnswerOptionId { get; set; }
    public string? TextAnswer { get; set; }
    public DateTime SubmittedDate { get; set; }
}
