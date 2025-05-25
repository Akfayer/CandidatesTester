using System.ComponentModel.DataAnnotations;

namespace CandidatesTesterAPI.DTOs.AnswerOptionDTOs;

public class UpdateAnswerOptionRequest
{
    [Required]
    public int QuestionId { get; set; }

    [Required]
    [MaxLength(255)]
    public string AnswerText { get; set; }

    public bool IsCorrect { get; set; }
}
