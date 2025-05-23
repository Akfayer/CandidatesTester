using Tester.Core.Enums;

namespace Tester.Core.Models;

public class QuestionModel
{
    public int QuestionId { get; set; }
    public int TestId { get; set; }
    public required string QuestionText { get; set; }
    public QuestionType TypeOfQuestion { get; set; }
}
