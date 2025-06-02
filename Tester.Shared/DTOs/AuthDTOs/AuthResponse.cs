using System.Text.Json.Serialization;

namespace Tester.Shared.DTOs.AuthDTOs;

public class AuthResponse

{
    [JsonPropertyName("userId")]
    public int UserId { get; set; }
    [JsonPropertyName("token")]
    public string Token { get; set; }
    [JsonPropertyName("role")]
    public string Role { get; set; }
    [JsonPropertyName("fullName")]
    public string FullName { get; set; }
}
