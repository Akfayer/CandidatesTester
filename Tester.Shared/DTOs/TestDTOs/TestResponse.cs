using System.Text.Json.Serialization;

namespace Tester.Shared.DTOs.TestDTOs;

public class TestResponse
{
    [JsonPropertyName("testId")]
    public int TestId { get; set; }
    [JsonPropertyName("testTitle")]
    public string? TestTitle { get; set; }
    [JsonPropertyName("testDescription")]
    public string? TestDescription { get; set; }
}
