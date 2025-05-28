using System.ComponentModel.DataAnnotations;

namespace Tester.Shared.DTOs.AnswerOptionDTOs;

public class UpdateAnswerOptionRequest
{
    [Required]
    public int QuestionId { get; set; }

    [Required]
    [MaxLength(255)]
    public string AnswerText { get; set; }

    public bool IsCorrect { get; set; }
}