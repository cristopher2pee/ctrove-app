using CTrove.Api.Azure;
using CTrove.Api.Exceptions;
using CTrove.Api.Middleware;
using CTrove.Core.Common;
using CTrove.Core.DTO.Request;
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
    public class RaceController : ControllerBase
    {

        private readonly IRaceServices _raceServices;
        private readonly AuthService _authService;

        public RaceController(IRaceServices raceServices, AuthService auth)
        {
            _raceServices = raceServices;
            _authService = auth;
        }

        [HttpGet]
        //[CtroveAuthorize(new string[] { CtroveRoles.User, CtroveRoles.Admin })]
        public async Task<IActionResult> GetAll([FromQuery] BaseFilters filters)
        {
            var entities = await _raceServices.GetAll(filters);
            if (entities is null) throw new BadRequestException("Error in requesting entities.");
            return Ok(entities);
        }

        [HttpGet("{id}")]
        //[CtroveAuthorize(new string[] { CtroveRoles.Admin })]
        public async Task<IActionResult> GetbyId([FromRoute] Guid id)
        {
            var entity = await _raceServices.GetbyId(id, _authService.GetUserObjectId());
            if (entity == null) throw new NotFoundException("Entity not found.");
            return Ok(entity);
        }

        [HttpPost]
       // [CtroveAuthorize(new string[] { CtroveRoles.Admin })]
        public async Task<IActionResult> Add([FromBody] RaceRequest param)
        {
            var entity = await _raceServices.Save(param, _authService.GetUserObjectId());
            if (entity is null) throw new BadRequestException("Error in saving entity.");
            return Ok(entity);

        }

        [HttpPut]
       // [CtroveAuthorize(new string[] { CtroveRoles.Admin })]
        public async Task<IActionResult> Update([FromBody] RaceRequest param)
        {
            var entity = await _raceServices.Update(param, _authService.GetUserObjectId());
            if (entity is null) throw new BadRequestException("Error in updating entity.");
            return Ok(entity);
        }

        [HttpDelete]
        //[CtroveAuthorize(new string[] { CtroveRoles.Admin })]
        public async Task<IActionResult> Delete([FromBody] DeactivateRequest req)
        {
            var result = await _raceServices.Deactivate(req, _authService.GetUserObjectId());
            if (!result) throw new BadRequestException($"Error in deleting entity id : {req.Id}");
            return Ok();
        }

        [HttpGet("exist")]
        public async Task<IActionResult> IsExist([FromQuery] string param)
        {
            var result = await _raceServices.IsExist(param);
            return Ok(result);
        }

    }
}
