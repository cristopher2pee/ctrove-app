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
    public class StudyCountryController : ControllerBase
    {
        private readonly IStudyCountryServices _services;
        private readonly AuthService _authService;
        public StudyCountryController(IStudyCountryServices services, AuthService auth)
        {
            _services = services;
            _authService = auth;
        }

        [HttpGet]
       // [CtroveAuthorize(new string[] { CtroveRoles.User, CtroveRoles.Admin })]
        public async Task<IActionResult> GetAll([FromQuery] BaseFilters filters)
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
        public async Task<IActionResult> Add([FromBody] DefaultRequest param)
        {
            var entity = await _services.Save(param, _authService.GetUserObjectId());
            if (entity is null) throw new BadRequestException("Error in saving entity.");
            return Ok(entity);

        }

        [HttpPut]
       // [CtroveAuthorize(new string[] { CtroveRoles.Admin })]
        public async Task<IActionResult> Update([FromBody] DefaultRequest param)
        {
            var entity = await _services.Update(param, _authService.GetUserObjectId());
            if (entity is null) throw new BadRequestException("Error in updating entity.");
            return Ok(entity);
        }

        [HttpDelete("{id}")]
      //  [CtroveAuthorize(new string[] { CtroveRoles.Admin })]
        public async Task<IActionResult> Delete([FromBody] DeactivateRequest id)
        {
            var result = await _services.Delete(id, _authService.GetUserObjectId());
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
