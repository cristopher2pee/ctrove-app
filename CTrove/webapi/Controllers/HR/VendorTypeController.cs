using Ctrove.HR.Common;
using CTrove.Api.Azure;
using CTrove.Api.Exceptions;
using CTrove.Core.DTO.Request;
using CTrove.Core.DTO;
using CTrove.Services.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using CTrove.Core.Filters;

namespace CTrove.Api.Controllers.HR
{
    [Route("api/[controller]")]
    [ApiController]
    public class VendorTypeController : ControllerBase
    {
        private readonly IVendorTypeServices _vendorTypeServices;
        private readonly AuthService _authService;

        public VendorTypeController(IVendorTypeServices vendorTypeServices, AuthService authService)
        {
            _authService = authService;
            _vendorTypeServices = vendorTypeServices;
        }

        [HttpGet]
        public async Task<IActionResult> GetPagedList([FromQuery] BaseFilters filters)
        {
            var entities = await _vendorTypeServices.GetPagedList(filters);
            return Ok(entities);
        }

        [HttpGet("list-resources")]
        public async Task<IActionResult> GetList()
        {
            var entities = await _vendorTypeServices.GetList();
            return Ok(entities);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get([FromRoute] Guid id)
        {
            var entity = await _vendorTypeServices.Get(id, _authService.GetUserObjectId());
            if (entity is null) throw new NotFoundException(MessageShow.ERROR_NOTFOUND);
            return Ok(entity);
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] VendorTypeRequest req)
        {
            var result = await _vendorTypeServices.Add(req, _authService.GetUserObjectId());
            if (result is null) throw new BadRequestException(MessageShow.ERROR_SAVING);
            return Ok(result);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] VendorTypeRequest req)
        {
            var result = await _vendorTypeServices.Update(req, _authService.GetUserObjectId());
            if (result is null) throw new BadRequestException(MessageShow.ERROR_UPDATING);
            return Ok(result);
        }

        [HttpDelete]
        public async Task<IActionResult> Deactivate([FromQuery] DeactivateRequest req)
        {
            var result = await _vendorTypeServices.Deactivate(req, _authService.GetUserObjectId());
            return Ok(new { isSuccess = result });
        }
    }
}
