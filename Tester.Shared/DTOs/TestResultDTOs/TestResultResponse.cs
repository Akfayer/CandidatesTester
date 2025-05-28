using System.Text.Json.Serialization;

namespace Tester.Shared.DTOs.TestResultDTOs;

public class TestResultResponse
{
    [JsonPropertyName("questionId")]
    public int TestResultId { get; set; }
    [JsonPropertyName("userId")]
    public int UserId { get; set; }
    [JsonPropertyName("testId")]
    public int TestId { get; set; }
    [JsonPropertyName("score")]
    public int Score { get; set; }
    [JsonPropertyName("maxScore")]
    public int MaxScore { get; set; }
    [JsonPropertyName("completedAt")]
    public DateTime CompletedAt { get; set; }
}
