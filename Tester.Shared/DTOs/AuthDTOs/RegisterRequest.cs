using System.ComponentModel.DataAnnotations;
using Tester.Shared.Enums;

namespace Tester.Shared.DTOs.AuthDTOs;

public class RegisterRequest
{
    public UserRole Role { get; set; } = UserRole.Candidate;
    [Required]
    public required string Login { get; set; }
    [Required]
    public required string Password { get; set; }
    [Required]
    public required string FullName { get; set; }
}
