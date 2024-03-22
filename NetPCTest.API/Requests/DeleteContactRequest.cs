using System.ComponentModel.DataAnnotations;

namespace NetPCTest.API.Requests
{
    public class DeleteContactRequest
    {
        [Required]
        public string Id { get; set; }
    }
}
