using System.Collections.Generic;
using System.Threading.Tasks;
using Agenda.Application.Exceptions;
using Agenda.Application.Interfaces;
using Agenda.Application.ViewModels;
using Agenda.Application.ViewModels.Exceptions;
using Agenda.Application.ViewModels.Exceptions.Base;
using Agenda.Application.ViewModels.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Agenda.API.Controllers.Admin
{
    [Route("api/admin/usuarios")]
    [ApiController]
    [Authorize(Roles = "ADMIN")]
    public class UserManagementController : ControllerBase
    {

        private readonly IUserManagementService _userService;

        public UserManagementController(IUserManagementService userService)
        {
            _userService = userService;
        }

        [HttpGet("tipos")]
        public ActionResult<IEnumerable<EnumerationResponse>> GetAllUserTypes()
        {
            return Ok(_userService.GetAllUserTypes());
        }

        [HttpPost]
        [ProducesResponseType(typeof(UserResponse), 201)]
        [ProducesResponseType(typeof(BadRequestExceptionViewModel), 400)]
        [ProducesResponseType(typeof(ConflictExceptionViewModel), 409)]
        public async Task<ActionResult<UserResponse>> CreateUser(UserRequest userPostRequest)
        {
            return CreatedAtAction("CreateUser", await _userService.CreateUser(userPostRequest));
        }

        [HttpGet("{id:int}")]
        [ProducesResponseType(typeof(UserResponse), 200)]
        [ProducesResponseType(typeof(ExceptionViewModel), 404)]
        public async Task<ActionResult<UserResponse>> GetUser(int id)
        {
            return Ok(await _userService.GetUserResponse(id));
        }

        [HttpGet("buscar")]
        public async Task<ActionResult<UserResponse>> Search([FromQuery] UserSearchParams searchQuery)
        {
            return Ok(await _userService.Search(searchQuery));
        }

        [HttpPut("{id}")]
        [ProducesResponseType(typeof(UserResponse), 200)]
        [ProducesResponseType(typeof(BadRequestExceptionViewModel), 400)]
        [ProducesResponseType(typeof(ExceptionViewModel), 404)]
        [ProducesResponseType(typeof(ConflictExceptionViewModel), 409)]
        public async Task<ActionResult<UserResponse>> UpdateUser(int id, UserRequest updatedUser)
        {
            return Ok(await _userService.UpdateUser(id, updatedUser));
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(typeof(ExceptionViewModel), 404)]
        public async Task<ActionResult> DeleteUser(int id)
        {
            await _userService.DeleteUser(id);

            return NoContent();
        }

        [HttpGet("username-ja-registrado")]
        [ProducesResponseType(200)]
        public async Task<ActionResult<bool>> IsUsernameAlreadyRegistered([FromQuery] string username)
        {
            return Ok(await _userService.IsUsernameAlreadyRegistered(username));
        }

        [HttpGet("email-ja-registrado")]
        [ProducesResponseType(200)]
        public async Task<ActionResult<bool>> IsEmailAlreadyRegistered([FromQuery] string email)
        {
            return Ok(await _userService.IsEmailAlreadyRegistered(email));
        }

    }
}
