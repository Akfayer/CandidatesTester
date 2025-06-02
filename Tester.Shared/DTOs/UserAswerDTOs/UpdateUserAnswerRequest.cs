namespace Tester.Shared.DTOs.UserAswerDTOs;

public class UpdateUserAnswerRequest
{
    public int UserAnswerId { get; set; }
    public int? AnswerOptionId { get; set; }
    public string? TextAnswer { get; set; }
}
