using Tester.Data.Enums;

namespace Tester.Data.Entities;

public class User
{
    public int UserId { get; set; }
    public UserRole Role { get; set; }
    public required string Login { get; set; }
    public required string Password { get; set; }
    public required string FullName { get; set; }
    public List<UserAnswer> UserAnswers { get; set; }
    public List<TestResult> TestResults { get; set; }
}
