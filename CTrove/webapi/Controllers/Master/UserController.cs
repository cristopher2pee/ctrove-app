using CTrove.Api.Exceptions;
using CTrove.Api.Extensions;
using CTrove.Core.Entity;
using CTrove.Services.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using CTrove.Core.DTO.Request;
using CTrove.Api.Middleware;
using CTrove.Core.Common;
using CTrove.Core.Filters;
using CTrove.Api.Azure;

namespace CTrove.Api.Controllers.Master
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly IUserServices _userServices;
        private readonly AuthService _authService;
        public UserController(IUserServices userServices, AuthService auth)
        {
            _userServices = userServices;
            _authService = auth;
        }

        [HttpPut]
        [CtroveAuthorize(new string[] { CtroveRoles.User, CtroveRoles.Admin })]
        public async Task<IActionResult> UpdateUser([FromBody] UserRequest req)
        {
            var entity = await _userServices.Update(_authService.GetUserObjectId(), req);
            if (entity == null) { throw new BadRequestException("Error in updating user profile"); }
            return Ok(entity);
        }


        [HttpDelete]
        [CtroveAuthorize(new string[] { CtroveRoles.User, CtroveRoles.Admin })]
        public async Task<IActionResult> DeleteUser([FromBody] DeactivateRequest userId)
        {
            var result = await _userServices.Deactivate(userId, _authService.GetUserObjectId());
            if (!result) throw new BadRequestException("Error in deleting user.");
            return Ok();
        }

        [HttpPost]
        [CtroveAuthorize(new string[] { CtroveRoles.User, CtroveRoles.Admin })]
        public async Task<IActionResult> AddUser([FromBody] UserRequest req)
        {
            var result = await _userServices.Add(_authService.GetUserObjectId(), req);
            if (result == null) throw new BadRequestException("Error in saving user.");
            return Ok(result);
        }

        [HttpGet]
        [CtroveAuthorize(new string[] { CtroveRoles.Admin })]
        public async Task<IActionResult> GetAll([FromQuery] UserFilters filters)
        {
            var entities = await _userServices.GetAll(filters);
            if (entities is null) throw new BadRequestException("Error in requesting entities.");
            return Ok(entities);
        }

        [HttpGet("{id}")]
        [CtroveAuthorize(new string[] { CtroveRoles.Admin })]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var entity = await _userServices.GetById(_authService.GetUserObjectId(), id);
            if (entity == null) throw new NotFoundException("Entity not found.");
            return Ok(entity);
        }


        [HttpGet("{id}/access-rights")]
        [CtroveAuthorize(new string[] { CtroveRoles.Admin })]
        public async Task<IActionResult> GetAccessRightsById([FromRoute] Guid id)
        {
            var entity = await _userServices.UserAccessRights(id);
            if (entity is null) throw new BadRequestException("Error in requesting entities.");
            return Ok(entity);
        }

        [HttpGet("user-email-exist")]
        [CtroveAuthorize(new string[] { CtroveRoles.Admin })]
        public async Task<IActionResult> IsUserEmailExist([FromQuery]string email)
        {
            var isExist = await _userServices.IsUserEmailExist(email);
            return Ok(isExist);
        }
    }
}
