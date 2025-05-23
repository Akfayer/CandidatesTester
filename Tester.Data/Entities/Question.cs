using Tester.Data.Enums;

namespace Tester.Data.Entities;

public class Question
{
    public int QuestionId { get; set; }
    public int TestId { get; set; }
    public required string QuestionText { get; set; }
    public QuestionType TypeOfQuestion { get; set; }
    public Test Test { get; set; }
    public List<AnswerOption> AnswerOptions { get; set; }
    public List<UserAnswer> UserAnswers { get; set; }
}
