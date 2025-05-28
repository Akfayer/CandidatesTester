using System.ComponentModel.DataAnnotations;

namespace Tester.Shared.DTOs.TestResultDTOs
{
    public class CreateTestResultRequest
    {
        [Required]
        public int UserId { get; set; }

        [Required]
        public int TestId { get; set; }

        [Required]
        public int Score { get; set; }

        [Required]
        public int MaxScore { get; set; }
    }
}
