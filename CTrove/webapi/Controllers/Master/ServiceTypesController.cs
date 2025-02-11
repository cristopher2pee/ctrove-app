using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using CTrove.Services.Interface;
using CTrove.Api.Exceptions;
using CTrove.Core.Entity;
using CTrove.Core.Filters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Identity.Web;
using CTrove.Api.Extensions;
using CTrove.Api.Middleware;
using CTrove.Core.Common;
using CTrove.Api.Azure;
using CTrove.Core.DTO.Request;

namespace CTrove.Api.Controllers.Master
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ServiceTypesController : ControllerBase
    {
        private readonly IServiceTypesServices _serviceTypes;
        private readonly AuthService _authService;
        public ServiceTypesController(IServiceTypesServices services, AuthService auth)
        {
            _serviceTypes = services;
            _authService = auth;
        }

        [HttpGet]
        [CtroveAuthorize(new string[] { CtroveRoles.User, CtroveRoles.Admin })]
        public async Task<IActionResult> GetServiceTypesAll([FromQuery] BaseFilters filters)
        {
            //var serviceTypes = await _serviceTypes.GetAll();
            //if (serviceTypes == null) { throw new NotFoundException("Empty"); }
            //return Ok(serviceTypes);

            var serviceTypes = await _serviceTypes.GetAllServiceType(filters);
            return Ok(serviceTypes);

        }

        [HttpGet("{id}")]
        [CtroveAuthorize(new string[] { CtroveRoles.Admin })]
        public async Task<IActionResult> GetServiceTypeById([FromRoute] Guid id)
        {
            var serviceTypes = await _serviceTypes.GetById(id, _authService.GetUserObjectId());
            if (serviceTypes == null) { throw new NotFoundException("Empty"); }
            return Ok(serviceTypes);

        }

        [HttpPost]
        [CtroveAuthorize(new string[] { CtroveRoles.Admin })]
        public async Task<IActionResult> AddServiceTypes([FromBody] DefaultRequest serviceType)
        {
            serviceType.Id = Guid.NewGuid();
            await _serviceTypes.Save(serviceType, _authService.GetUserObjectId());
            return Ok(serviceType);
        }

        [HttpPut]
        [CtroveAuthorize(new string[] { CtroveRoles.Admin })]
        public async Task<IActionResult> UpdateServiceTypes([FromBody] DefaultRequest serviceType)
        {
            await _serviceTypes.Update(serviceType, _authService.GetUserObjectId());
            return Ok(serviceType);
        }

        [HttpDelete("{id}")]
        [CtroveAuthorize(new string[] { CtroveRoles.Admin })]
        public async Task<IActionResult> DeleteServiceTypes([FromBody] DeactivateRequest id)
        {
            await _serviceTypes.Deactivate(id, _authService.GetUserObjectId());
            return Ok();
        }

        [HttpGet("exist")]
        public async Task<IActionResult> IsExist([FromQuery] string param)
        {
            var result = await _serviceTypes.IsExist(param);
            return Ok(result);
        }


    }
}
