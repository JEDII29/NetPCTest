using System.ComponentModel.DataAnnotations;

namespace NetPCTest.API.Requests
{
    public class GetContactsRequest
    {
        [Required]
        public Guid UserId { get; set; }
    }
}
