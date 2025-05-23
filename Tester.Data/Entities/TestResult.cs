namespace Tester.Data.Entities;

public class TestResult
{
    public int TestResultId { get; set; }
    public int UserId { get; set; }
    public int TestId { get; set; }
    public int Score { get; set; }
    public int MaxScore { get; set; }
    public DateTime CompletedAt { get; set; }
    public User User { get; set; }
    public Test Test { get; set; }
}
