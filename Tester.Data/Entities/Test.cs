namespace Tester.Data.Entities;

public class Test
{
    public int TestId { get; set; }
    public required string TestTitle { get; set; }
    public required string TestDescription { get; set; }
    public List<Question> Questions { get; set; }
    public List<TestResult> TestResults { get; set; }
}
