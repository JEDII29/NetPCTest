using System.ComponentModel.DataAnnotations;

namespace NetPCTest.API.Requests
{
    public class CreateContactRequest
    {
        [Required]
        public string Firstname { get; set; }
        [Required]
        public string Surname { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
        [Required]
        public DateTime BirthDate { get; set; }
        [Required]
        public string Category { get; set; }
    }
}
