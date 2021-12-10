using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Agenda.Application.Exceptions;
using Agenda.Application.Interfaces;
using Agenda.Application.ViewModels;
using Agenda.Application.ViewModels.Contact;
using Agenda.Application.ViewModels.ContactTelephone;
using Agenda.Application.ViewModels.Exceptions;
using Agenda.Application.ViewModels.Exceptions.Base;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Agenda.API.Controllers.StandardUser
{
    [Route("api/agenda")]
    [ApiController]
    [Authorize(Roles = "STANDARD USER")]
    public class PhonebookController : ControllerBase
    {
        
        private readonly IPhonebookService _phonebookService;

        public PhonebookController(IPhonebookService phonebookService)
        {
            _phonebookService = phonebookService;
        }

        [HttpGet("tipos-telefone")]
        public ActionResult<IEnumerable<EnumerationResponse>> GetAllContactTelephoneTypes()
        {
            return Ok(_phonebookService.GetAllContactTelephoneTypes());
        }

        [HttpPost]
        [ProducesResponseType(typeof(ContactResponse), 201)]
        [ProducesResponseType(typeof(BadRequestExceptionViewModel), 400)]
        [ProducesResponseType(typeof(ConflictExceptionViewModel), 409)]
        public async Task<ActionResult<ContactResponse>> AddContact(ContactRequest contactRequest)
        {
            return CreatedAtAction("AddContact", await _phonebookService.AddContact(contactRequest));
        }

        [HttpGet("{id:int}")]
        [ProducesResponseType(typeof(ContactAdminResponse), 200)]
        [ProducesResponseType(typeof(ExceptionViewModel), 404)]
        public async Task<ActionResult<ContactResponse>> GetContact(int id)
        {
            return Ok(await _phonebookService.GetContactResponse(id));
        }

        [HttpGet("buscar")]
        public async Task<ActionResult<ContactResponse>> Search([FromQuery] ContactSearchParams searchQuery)
        {
            return Ok(await _phonebookService.Search(searchQuery));
        }

        [HttpPut("{id:int}")]
        [ProducesResponseType(typeof(ContactResponse), 200)]
        [ProducesResponseType(typeof(BadRequestExceptionViewModel), 400)]
        [ProducesResponseType(typeof(ExceptionViewModel), 403)]
        [ProducesResponseType(typeof(ExceptionViewModel), 404)]
        [ProducesResponseType(typeof(ConflictExceptionViewModel), 409)]
        public async Task<ActionResult<ContactResponse>> UpdateContact(int id, ContactRequest contactRequest)
        {
            return Ok(await _phonebookService.UpdateContact(id, contactRequest));
        }

        [HttpDelete("{id:int}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(typeof(ExceptionViewModel), 403)]
        [ProducesResponseType(typeof(ExceptionViewModel), 404)]
        public async Task<ActionResult> RemoveContact(int id)
        {
            await _phonebookService.RemoveContact(id);

            return NoContent();
        }

        [HttpGet("telefone-ja-registrado")]
        [ProducesResponseType(200)]
        public async Task<ActionResult<bool>> IsPhoneNumberAlreadyRegistedInUserPhonebook([FromQuery] string telefone)
        {
            int userId = int.Parse(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
            bool exists = await _phonebookService.IsPhoneNumberAlreadySavedInUserPhonebook(userId, telefone);
            return Ok(exists);
        }

    }
}
