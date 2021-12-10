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

namespace Agenda.API.Controllers.Admin
{
    [Route("api/admin/contatos")]
    [ApiController]
    [Authorize(Roles = "ADMIN")]
    public class ContactManagementController : ControllerBase
    {

        private readonly IContactManagementService _contactManagementService;

        public ContactManagementController(IContactManagementService contactManagementService)
        {
            _contactManagementService = contactManagementService;
        }

        [HttpGet("tipos-telefone")]
        public ActionResult<IEnumerable<EnumerationResponse>> GetAllContactTelephoneTypes()
        {
            return Ok(_contactManagementService.GetAllContactTelephoneTypes());
        }

        [HttpPost]
        [ProducesResponseType(typeof(ContactAdminResponse), 201)]
        [ProducesResponseType(typeof(BadRequestExceptionViewModel), 400)]
        [ProducesResponseType(typeof(ExceptionViewModel), 404)]
        [ProducesResponseType(typeof(ConflictExceptionViewModel), 409)]
        public async Task<ActionResult<ContactAdminResponse>> AddContact(ContactAdminRequest contactRequest)
        {
            return CreatedAtAction("AddContact", await _contactManagementService.Create(contactRequest));
        }

        [HttpGet("{id:int}")]
        [ProducesResponseType(typeof(ContactAdminResponse), 200)]
        [ProducesResponseType(typeof(ExceptionViewModel), 404)]
        public async Task<ActionResult<ContactAdminResponse>> GetContact(int id)
        {
            return Ok(await _contactManagementService.GetContactAdminResponse(id));
        }

        [HttpGet("buscar")]
        public async Task<ActionResult<ContactAdminResponse>> Search([FromQuery] ContactSearchParams searchQuery)
        {
            return Ok(await _contactManagementService.Search(searchQuery));
        }

        [HttpPut("{id:int}")]
        [ProducesResponseType(typeof(ContactAdminResponse), 200)]
        [ProducesResponseType(typeof(BadRequestExceptionViewModel), 400)]
        [ProducesResponseType(typeof(ExceptionViewModel), 404)]
        [ProducesResponseType(typeof(ConflictExceptionViewModel), 409)]
        public async Task<ActionResult<ContactAdminResponse>> UpdateContact(int id, ContactAdminRequest contactRequest)
        {
            return Ok(await _contactManagementService.Update(id, contactRequest));
        }

        [HttpDelete("{id:int}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(typeof(ExceptionViewModel), 404)]
        public async Task<ActionResult> RemoveContact(int id)
        {
            await _contactManagementService.Delete(id);

            return NoContent();
        }

        [HttpGet("telefone-ja-registrado")]
        [ProducesResponseType(200)]
        public async Task<ActionResult<bool>> IsPhoneNumberAlreadyRegistedInUserPhonebook([FromQuery] int usuarioId, [FromQuery] string telefone)
        {
            bool exists = await _contactManagementService.IsPhoneNumberAlreadySavedInUserPhonebook(usuarioId, telefone);
            return Ok(exists);
        }


    }

}
