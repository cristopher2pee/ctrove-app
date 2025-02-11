using Ctrove.HR.Common;
using Ctrove.HR.Services;
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
    public class ContactTypeController : ControllerBase
    {
        private readonly IContactTypeServices _contactTypeServices;
        private readonly AuthService _authService;
        public ContactTypeController(IContactTypeServices contactTypeServices, AuthService authService)
        {
            _authService = authService;
            _contactTypeServices = contactTypeServices;
        }

        [HttpGet]
        public async Task<IActionResult> GetPagedList([FromQuery] BaseFilters filters)
        {
            var entities = await _contactTypeServices.GetPagedList(filters);
            return Ok(entities);
        }

        [HttpGet("list-resources")]
        public async Task<IActionResult> GetList()
        {
            var entities = await _contactTypeServices.GetList();
            return Ok(entities);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get([FromRoute] Guid id)
        {
            var entity = await _contactTypeServices.Get(id, _authService.GetUserObjectId());
            if (entity is null) throw new NotFoundException(MessageShow.ERROR_NOTFOUND);
            return Ok(entity);
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] ContactTypeRequest req)
        {
            var result = await _contactTypeServices.Add(req, _authService.GetUserObjectId());
            if (result is null) throw new BadRequestException(MessageShow.ERROR_SAVING);
            return Ok(result);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] ContactTypeRequest req)
        {
            var result = await _contactTypeServices.Update(req, _authService.GetUserObjectId());
            if (result is null) throw new BadRequestException(MessageShow.ERROR_UPDATING);
            return Ok(result);
        }

        [HttpDelete]
        public async Task<IActionResult> Deactivate([FromQuery] DeactivateRequest req)
        {
            var result = await _contactTypeServices.Deactivate(req, _authService.GetUserObjectId());
            return Ok(new { isSuccess = result });
        }
    }
}
