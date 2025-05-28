using System.Text.Json.Serialization;
using Tester.Core.Enums;

namespace Tester.Shared.DTOs.QuestionDTOs;

public class QuestionResponse
{
    [JsonPropertyName("questionId")]
    public int QuestionId { get; set; }
    [JsonPropertyName("testId")]
    public int TestId { get; set; }
    [JsonPropertyName("questionText")]
    public string QuestionText { get; set; }
    [JsonPropertyName("typeOfQuestion")]
    public QuestionType TypeOfQuestion { get; set; }
}
