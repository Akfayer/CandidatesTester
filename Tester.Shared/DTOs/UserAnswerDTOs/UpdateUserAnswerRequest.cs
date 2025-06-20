namespace Tester.Shared.DTOs.UserAnswerDTOs;

public class UpdateUserAnswerRequest
{
    public int UserAnswerId { get; set; }
    public int? AnswerOptionId { get; set; }
    public string? TextAnswer { get; set; }
}
