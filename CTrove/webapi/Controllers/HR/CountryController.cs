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
    public class CountryController : ControllerBase
    {
        private readonly ICountryServices _countryServices;
        private readonly AuthService _authService;

        public CountryController(ICountryServices countryServices, AuthService authService)
        {
            _authService = authService;
            _countryServices = countryServices;
        }

        [HttpGet]
        public async Task<IActionResult> GetPageList([FromQuery] BaseFilters filters)
        {
            var entities = await _countryServices.GetPagedList(filters);
            return Ok(entities);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get([FromRoute] Guid id)
        {
            var entity = await _countryServices.Get(id, _authService.GetUserObjectId());
            if (entity is null) throw new NotFoundException(MessageShow.ERROR_NOTFOUND);
            return Ok(entity);
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] CountryRequest req)
        {
            var result = await _countryServices.Add(req, _authService.GetUserObjectId());
            if (result is null) throw new BadRequestException(MessageShow.ERROR_SAVING);
            return Ok(result);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] CountryRequest req)
        {
            var result = await _countryServices.Update(req, _authService.GetUserObjectId());
            if (result is null) throw new BadRequestException(MessageShow.ERROR_UPDATING);
            return Ok(result);
        }

        [HttpDelete]
        public async Task<IActionResult> Deactivate([FromQuery] DeactivateRequest req)
        {
            var result = await _countryServices.Deactivate(req, _authService.GetUserObjectId());
            return Ok(new { isSuccess = result });
        }

        [HttpGet("country-exist")]
        public async Task<IActionResult> isEmailExist([FromQuery] string param)
        {
            var result = await _countryServices.isCountryExist(param);
            return Ok(new { isExist = result });
        }
    }
}
