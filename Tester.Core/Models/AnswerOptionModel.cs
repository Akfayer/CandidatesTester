namespace Tester.Core.Models;

public class AnswerOptionModel
{
    public int AnswerOptionId { get; set; }
    public int QuestionId { get; set; }
    public required string AnswerText { get; set; }
    public bool IsCorrect { get; set; }
}
