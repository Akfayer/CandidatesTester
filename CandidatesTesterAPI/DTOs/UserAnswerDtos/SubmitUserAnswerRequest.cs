using System.ComponentModel.DataAnnotations;

namespace CandidatesTesterAPI.DTOs.UserAnswerDtos;

public class SubmitUserAnswerRequest
{
    [Required]
    public int UserId { get; set; }
    [Required]
    public int QuestionId { get; set; }
    public int? AnswerOptionId { get; set; }
    public string? TextAnswer { get; set; }
}
