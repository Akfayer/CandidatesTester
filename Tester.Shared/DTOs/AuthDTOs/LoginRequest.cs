using System.ComponentModel.DataAnnotations;

namespace Tester.Shared.DTOs.AuthDTOs;

public class LoginRequest
{
    [Required]
    public string Login { get; set; }
    [Required]
    public string Password { get; set; }
}
