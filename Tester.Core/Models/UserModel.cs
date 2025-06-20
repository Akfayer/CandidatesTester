using System.Text.Json.Serialization;
using Tester.Core.Enums;

namespace Tester.Core.Models
{
    public class UserModel
    {
        public int UserId { get; set; }
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public UserRole Role { get; set; }
        public required string Login { get; set; }
        public required string Password { get; set; }
        public required string FullName { get; set; }
    }
}
