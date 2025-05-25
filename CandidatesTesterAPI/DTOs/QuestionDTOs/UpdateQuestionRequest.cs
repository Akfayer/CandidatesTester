using System.ComponentModel.DataAnnotations;
using Tester.Core.Enums;

namespace CandidatesTesterAPI.DTOs.QuestionDTOs;

public class UpdateQuestionRequest
{
    [Required]
    [MaxLength(500)]
    public string QuestionText { get; set; }

    [Required]
    public QuestionType TypeOfQuestion { get; set; }
}
