using Ctrove.HR.Common;
using CTrove.Api.Azure;
using CTrove.Api.Exceptions;
using CTrove.Core.DTO;
using CTrove.Core.DTO.Request;
using CTrove.Core.Filters;
using CTrove.Services.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CTrove.Api.Controllers.HR
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class OrganizationController : ControllerBase
    {
        private readonly AuthService _authService;
        private readonly IOrganizationServices _organizationServices;

        public OrganizationController(AuthService authService, IOrganizationServices organizationServices)
        {
            _authService = authService;
            _organizationServices = organizationServices;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get([FromRoute] Guid id)
        {
            var entity = await _organizationServices.Get(id,_authService.GetUserObjectId());
            if (entity == null) throw new BadRequestException(MessageShow.ERROR_NOTFOUND);
            return Ok(entity);
        }

        [HttpGet]
        public async Task<IActionResult> GetPageList([FromQuery] OrganizationFilter filters)
        {
            var entities = await _organizationServices.GetPageList(filters);
            return Ok(entities);
        }

        [HttpPost]
        public async Task<IActionResult> AddOrganization([FromBody] OrganizationRequest req)
        {
            var entity = await _organizationServices.Add(req,_authService.GetUserObjectId());
            if (entity is null) throw new BadRequestException(MessageShow.ERROR_SAVING);
            return Ok(entity);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateOrganization([FromBody] OrganizationRequest req)
        {
            var entity = await _organizationServices.Update(req, _authService.GetUserObjectId());
            if (entity is null) throw new BadRequestException(MessageShow.ERROR_UPDATING);
            return Ok(entity);
        }

        [HttpDelete]
        public async Task<IActionResult> DeactivateOrganization([FromBody] DeactivateRequest req)
        {
            var entity = await _organizationServices.Deactivate(req, _authService.GetUserObjectId());
            if (!entity) throw new BadRequestException(MessageShow.ERROR_DEACTIVATE);
            return Ok(new { isSuccess = entity });
        }

        [HttpGet("company-exist")]
        public async Task<IActionResult> IsOrganizationExist([FromQuery] string parameter)
        {
            var result = await _organizationServices.isOrganizationExist(parameter);
            return Ok(new { isExist = result });
        }
    }
}
