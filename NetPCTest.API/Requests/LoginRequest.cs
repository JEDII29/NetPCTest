using System.ComponentModel.DataAnnotations;

namespace NetPCTest.API.Requests
{
    public class LoginRequest
    {
        [Required]
        public string Username { get; set; } = default!;
        [Required]
        public string Password { get; set; } = default!;
    }
}
