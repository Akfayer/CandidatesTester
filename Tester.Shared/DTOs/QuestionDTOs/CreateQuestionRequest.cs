using System.ComponentModel.DataAnnotations;
using Tester.Core.Enums;

namespace Tester.Shared.DTOs.QuestionDTOs;

public class CreateQuestionRequest
{
    [Required]
    public int TestId { get; set; }

    [Required]
    [MaxLength(500)]
    public string QuestionText { get; set; }

    [Required]
    public QuestionType TypeOfQuestion { get; set; }
}
