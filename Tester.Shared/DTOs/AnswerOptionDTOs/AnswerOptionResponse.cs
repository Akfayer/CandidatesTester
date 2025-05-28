using System.Text.Json.Serialization;

namespace Tester.Shared.DTOs.AnswerOptionDTOs;

public class AnswerOptionResponse
{
    [JsonPropertyName("answerOptionId")]
    public int AnswerOptionId { get; set; }
    [JsonPropertyName("questionId")]
    public int QuestionId { get; set; }
    [JsonPropertyName("answerText")]
    public string AnswerText { get; set; }
    [JsonPropertyName("isCorrect")]
    public bool IsCorrect { get; set; }
}
