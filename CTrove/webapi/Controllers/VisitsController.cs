using CTrove.Api.Azure;
using CTrove.Api.Exceptions;
using CTrove.Api.Middleware;
using CTrove.Core.Common;
using CTrove.Core.DTO.Request;
using CTrove.Core.Filters;
using CTrove.Core.Interface;
using CTrove.Services.Interface;
using CTrove.Services.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CTrove.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class VisitsController : ControllerBase
    {
        private readonly AuthService _authService;
        private readonly IVisitsServices _visitsServices;

        public VisitsController(AuthService authService, IVisitsServices visitsServices)
        {
            _authService = authService;
            _visitsServices = visitsServices;
        }

        [HttpGet]
      //  [CtroveAuthorize(new string[] { CtroveRoles.Admin })]
        public async Task<IActionResult> GetListPage([FromQuery] VisitsFilters filter)
        {
            var entities = await _visitsServices.GetList(filter);
            if (entities is null) throw new BadRequestException("Error in requesting entities.");
            return Ok(entities);
        }

        [HttpGet("{id}")]
       // [CtroveAuthorize(new string[] { CtroveRoles.Admin })]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var entity = await _visitsServices.GetById(_authService.GetUserObjectId(),id);
            if(entity is null) throw new NotFoundException("Entity not found.");
            return Ok(entity);
        }

        [HttpPost]
      //  [CtroveAuthorize(new string[] { CtroveRoles.Admin })]
        public async Task<IActionResult> AddVisits([FromBody] VisitsRequest req)
        {
            var entity = await _visitsServices.Save(_authService.GetUserObjectId(), req);
            if (entity is null) throw new BadRequestException("Error in saving entity.");
            return Ok(entity);
        }

        [HttpPut]
       // [CtroveAuthorize(new string[] { CtroveRoles.Admin })]
        public async Task<IActionResult> UpdateVisits([FromBody] VisitsRequest req)
        {
            var entity = await _visitsServices.Update(_authService.GetUserObjectId(), req);
            if (entity is null) throw new BadRequestException("Error in updating entity.");
            return Ok(entity);
        }

        [HttpDelete]
      //  [CtroveAuthorize(new string[] { CtroveRoles.Admin })]
        public async Task<IActionResult> Delete([FromBody] DeactivateRequest req)
        {
            var result = await _visitsServices.Deactivate(_authService.GetUserObjectId(), req);
            if (!result) throw new BadRequestException($"Error in deleting entity id : {req.Id}");
            return Ok();
        }

        [HttpGet("exist")]
        public async Task<IActionResult> IsExist([FromQuery] string param)
        {
            var result = await _visitsServices.IsExist(param);
            return Ok(result);
        }
    }
}
