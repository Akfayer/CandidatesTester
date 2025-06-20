using System.Text.Json.Serialization;

namespace Tester.Shared.DTOs.UserAnswerDTOs;

public class UserAnswerResponse
{
    [JsonPropertyName("userAnswerId")]
    public int UserAnswerId { get; set; }
    [JsonPropertyName("userId")]
    public int UserId { get; set; }
    [JsonPropertyName("questionId")]
    public int QuestionId { get; set; }
    [JsonPropertyName("answerOptionId")]
    public int? AnswerOptionId { get; set; }
    [JsonPropertyName("textAnswer")]
    public string? TextAnswer { get; set; }
    [JsonPropertyName("submittedDate")]
    public DateTime SubmittedDate { get; set; }
}
