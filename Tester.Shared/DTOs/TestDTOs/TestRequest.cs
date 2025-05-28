using System.ComponentModel.DataAnnotations;

namespace Tester.Shared.DTOs.TestDTOs;

public class TestRequest
{
    [Required]
    public string TestTitle { get; set; }
    [Required]
    public string TestDescription { get; set; }
}
