namespace Tester.Data.Entities;

public class UserAnswer
{
    public int UserAnswerId { get; set; }
    public int UserId { get; set; }
    public int QuestionId { get; set; }
    public int? AnswerOptionId { get; set; }
    public string? TextAnswer {  get; set; } 
    public DateTime SubmittedDate { get; set; }
    public User User {  get; set; }
    public Question Question { get; set; }
    public AnswerOption AnswerOption { get; set; }
}
