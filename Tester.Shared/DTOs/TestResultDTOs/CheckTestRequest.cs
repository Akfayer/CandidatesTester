using System.ComponentModel.DataAnnotations;
using Tester.Shared.DTOs.UserAnswerDTOs;

namespace Tester.Shared.DTOs.TestResultDTOs;

public class CheckTestRequest
{
    [Required]
    public int UserId { get; set; }

    [Required]
    public int TestId { get; set; }

    [Required]
    public List<SubmitUserAnswerRequest> Answers { get; set; }
}
