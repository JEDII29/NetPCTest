using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NetPCTest.API.Requests;
using NetPCTest.Domain.Abstract;
using NetPCTest.Data.Entities;
using Microsoft.AspNetCore.Authorization;
using System.Data;
using System.Security.Claims;

namespace NetPCTest.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactController : ControllerBase
    {
        private readonly IContactService _contacService;
        public ContactController(IContactService contactservice)
        {
            _contacService = contactservice;
        }

        [HttpGet("GetBasicUserContacts")]
        public async Task<IActionResult> GetBsicUserContacts(Guid userId)
        {
            var result = await _contacService.GetUserBasicContacts(userId);
            if (result != null)
                return Ok(result);
            else
                return Ok();
        }
        [Authorize]
        [HttpGet("GetUserContacts")]
        public async Task<IActionResult> GetUserContacts()
        {
            var userId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

            var result = await _contacService.GetUserContacts(userId);
            if (result != null)
                return Ok(result);
            else
                return Ok();
        }
        [Authorize]
        [HttpPut("EditUserContacts")]
        public IActionResult EditUserContact(EditContactRequest editRequest) 
        {
            var userId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            ContactEntity contact = new ContactEntity(
                editRequest.Id, editRequest.Firstname, editRequest.Surname,
                editRequest.Email, editRequest.PhoneNumber, editRequest.BirthDate,
                editRequest.Category, userId);
            var result = _contacService.EditUserContact(contact);
            if (result == Task.CompletedTask)
                return Ok();
            else 
                return BadRequest();

        }
        [Authorize]
        [HttpPost("CreateContact")]
        public async Task<IActionResult> CreateContact(CreateContactRequest createRequest)
        {
            var userId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            Guid id = new Guid();
            ContactEntity contact = new ContactEntity(
            id, createRequest.Firstname, createRequest.Surname,
            createRequest.Email, createRequest.PhoneNumber, createRequest.BirthDate,
            createRequest.Category, userId);
            await _contacService.SaveNewContact(contact);
            return Ok();
        }
        [Authorize]
        [HttpDelete("DeleteContact")]
        public async Task<IActionResult> DeleteContact(DeleteContactRequest deleteRequest)
        {
            Guid id = Guid.Parse(deleteRequest.Id);
            await _contacService.DeleteUserContact(id);
            return Ok();
        }
    }
}
