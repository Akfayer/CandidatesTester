using System.ComponentModel.DataAnnotations;

namespace Tester.Shared.DTOs.AuthDTOs;

public class UserRequest
{
    public int UserId { get; set; }
    [Required]
    public string FullName { get; set;}
    [Required]
    public string Login { get; set; }
    [Required]
    public string Role { get; set; }
}
