using System.Text.Json.Serialization;

namespace Tester.Shared.DTOs.UserDTOs;

public class UserResponse
{
    [JsonPropertyName("userId")]
    public int UserId { get; set; }
    [JsonPropertyName("fullName")]
    public string FullName { get; set; }
    [JsonPropertyName("login")]
    public string Login { get; set; }
    [JsonPropertyName("role")]
    public string Role { get; set; }
}
