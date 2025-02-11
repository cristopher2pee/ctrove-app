using CTrove.Api.Azure;
using CTrove.Api.Exceptions;
using CTrove.Api.Extensions;
using CTrove.Api.Middleware;
using CTrove.Core.Common;
using CTrove.Core.DTO.Request;
using CTrove.Core.Entity;
using CTrove.Core.Filters;
using CTrove.Services.Interface;
using CTrove.Services.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CTrove.Api.Controllers.Master
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class RolesController : ControllerBase
    {
        private readonly IRolesServices _services;
        private readonly IRolesPagesServices _rolesPagesServices;
        private readonly AuthService _authService;
        public RolesController(IRolesServices services, AuthService auth, IRolesPagesServices rolesPagesServices)
        {
            _services = services;
            _authService = auth;
            _rolesPagesServices = rolesPagesServices;
        }

        [HttpGet]
        //[CtroveAuthorize(new string[] { CtroveRoles.User, CtroveRoles.Admin })]
        public async Task<IActionResult> GetAll([FromQuery] RolesFilters filters)
        {
            var entities = await _services.GetAll(filters);
            if (entities is null) throw new BadRequestException("Error in requesting entities.");
            return Ok(entities);
        }

        [HttpGet("{id}")]
        //[CtroveAuthorize(new string[] { CtroveRoles.Admin })]
        public async Task<IActionResult> GetbyId([FromRoute] Guid id)
        {
            var entity = await _services.GetById(id, _authService.GetUserObjectId());
            if (entity == null) throw new NotFoundException("Entity not found.");
            return Ok(entity);
        }

        [HttpPost]
        //[CtroveAuthorize(new string[] { CtroveRoles.Admin })]
        public async Task<IActionResult> Add([FromBody] RolesRequest param)
        {
            var entity = await _services.Save(param, _authService.GetUserObjectId());
            if (entity is null) throw new BadRequestException("Error in saving entity.");
            return Ok(entity);

        }

        [HttpPut]
       // [CtroveAuthorize(new string[] { CtroveRoles.Admin })]
        public async Task<IActionResult> Update([FromBody] RolesRequest param)
        {
            var entity = await _services.Update(param, _authService.GetUserObjectId());
            if (entity is null) throw new BadRequestException("Error in updating entity.");
            return Ok(entity);
        }

        [HttpDelete]
        //[CtroveAuthorize(new string[] { CtroveRoles.Admin })]
        public async Task<IActionResult> Delete([FromBody] DeactivateRequest id)
        {
            var result = await _services.Deactivate(id, _authService.GetUserObjectId());
            if (!result) throw new BadRequestException($"Error in deleting entity id : {id}");
            return Ok();
        }

        [HttpGet("list-resources")]
        public async Task<IActionResult> GetList()
        {
            var result = await _services.GetList();
            if (result == null) throw new BadRequestException("Error in requesting entities.");
            return Ok(result);
        }

        [HttpPost("roles-pages")]
       // [CtroveAuthorize(new string[] { CtroveRoles.Admin })]
        public async Task<IActionResult> AddRolesPages(RolesPagesRequest req)
        {
            var result = await _rolesPagesServices.Save(req, _authService.GetUserObjectId());
            if (result is null) throw new BadRequestException("Error in saving entity.");
            return Ok(result);
        }

        [HttpPut("roles-pages")]
       // [CtroveAuthorize(new string[] { CtroveRoles.Admin })]
        public async Task<IActionResult> UpdateRolesPages(RolesPagesRequest req)
        {
            var result = await _rolesPagesServices.Update(req, _authService.GetUserObjectId());
            if (result is null) throw new BadRequestException("Error in updating entity.");
            return Ok(result);
        }

        [HttpDelete("roles-pages")]
       // [CtroveAuthorize(new string[] { CtroveRoles.Admin })]
        public async Task<IActionResult> DeleteRolesPages([FromBody] DeactivateRequest id)
        {
            var result = await _rolesPagesServices.Deactivate(id, _authService.GetUserObjectId());
            if (!result) throw new BadRequestException($"Error in deleting entity id : {id}");
            return Ok();
        }

        [HttpGet("exist")]
        public async Task<IActionResult> IsExist([FromQuery] string param)
        {
            var result = await _services.IsExist(param);
            return Ok(result);
        }
    }
}
