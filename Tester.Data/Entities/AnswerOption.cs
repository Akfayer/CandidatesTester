namespace Tester.Data.Entities;

public class AnswerOption
{
    public int AnswerOptionId { get; set; } 
    public int QuestionId { get; set; }
    public required string AnswerText { get; set; }
    public bool IsCorrect { get; set; }
    public Question Question { get; set; }
    public List<UserAnswer> UserAnswers { get; set; }
}
