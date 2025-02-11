using CTrove.Api.Azure;
using CTrove.Api.Middleware;
using CTrove.Core.Common;
using CTrove.Services.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using CTrove.Core.Filters;
using CTrove.Api.Exceptions;
using CTrove.Core.DTO.Request;

namespace CTrove.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class SitesController : ControllerBase
    {
        private readonly AuthService _authService;
        private readonly ISitesServices _sitesService;

        public SitesController(AuthService authService, ISitesServices sitesServices)
        {
            _authService = authService;
            _sitesService = sitesServices;
        }

        [HttpGet]
      //  [CtroveAuthorize(new string[] { CtroveRoles.Admin })]
        public async Task<IActionResult> GetAllPagedList([FromQuery] SitesFilters filters)
        {
            var entities = await _sitesService.GetAllPagedList(filters, _authService.GetUserObjectId());
            if (entities is null) throw new BadRequestException("Error in requesting entities.");
            return Ok(entities);
        }

        [HttpGet("list-resources")]
        //[CtroveAuthorize(new string[] { CtroveRoles.Admin })]
        public async Task<IActionResult> GetList([FromQuery] SitesFilters filters)
        {
            var entities = await _sitesService.GetList(_authService.GetUserObjectId());
            if (entities is null) throw new BadRequestException("Error in requesting entities.");
            return Ok(entities);
        }

        [HttpGet("{id}")]
       // [CtroveAuthorize(new string[] { CtroveRoles.Admin })]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var entity = await _sitesService.GetbyId(_authService.GetUserObjectId(), id);
            if (entity is null) throw new NotFoundException("Entity not found.");
            return Ok(entity);
        }

        [HttpPost]
       // [CtroveAuthorize(new string[] { CtroveRoles.Admin })]
        public async Task<IActionResult> AddSites([FromBody] SitesRequest req)
        {
            var entity = await _sitesService.Save(_authService.GetUserObjectId(), req);
            if (entity is null) throw new BadRequestException("Error in saving entity.");
            return Ok(entity);
        }

        [HttpPut]
       // [CtroveAuthorize(new string[] { CtroveRoles.Admin })]
        public async Task<IActionResult> UpdateSites([FromBody] SitesRequest req)
        {
            var entity = await _sitesService.Update(_authService.GetUserObjectId(), req);
            if (entity is null) throw new BadRequestException("Error in updating entity.");
            return Ok(entity);
        }

        [HttpDelete]
       // [CtroveAuthorize(new string[] { CtroveRoles.Admin })]
        public async Task<IActionResult> Delete([FromBody] DeactivateRequest req)
        {
            var result = await _sitesService.Deactivate(_authService.GetUserObjectId(), req);
            if (!result) throw new BadRequestException($"Error in deleting entity id : {req.Id}");
            return Ok();
        }

        [HttpDelete("site-phases")]
       // [CtroveAuthorize(new string[] { CtroveRoles.Admin })]
        public async Task<IActionResult> DeleteSitePhases([FromBody] DeactivateRequest req)
        {
            var result = await _sitesService.DeactivateSitePhases(_authService.GetUserObjectId(), req);
            if (!result) throw new BadRequestException($"Error in deleting entity id : {req.Id}");
            return Ok();
        }

        [HttpGet("site-phases/{id}")]
        //[CtroveAuthorize(new string[] { CtroveRoles.Admin })]
        public async Task<IActionResult> GetSitePhasesById([FromRoute] Guid id)
        {
            var entity = await _sitesService.GetSitePhasesById(id, _authService.GetUserObjectId());
            if (entity is null) throw new NotFoundException("Entity not found.");
            return Ok(entity);
        }

        [HttpPost("site-phases")]
        //[CtroveAuthorize(new string[] { CtroveRoles.Admin })]
        public async Task<IActionResult> AddSitePhases([FromBody] SitePhasesRequest req)
        {
            var entity = await _sitesService.SaveSitePhases(req, _authService.GetUserObjectId());
            if (entity is null) throw new BadRequestException("Error in saving entity.");
            return Ok(entity);
        }

        [HttpPut("site-phases")]
       // [CtroveAuthorize(new string[] { CtroveRoles.Admin })]
        public async Task<IActionResult> UpdateSitePhases([FromBody] SitePhasesRequest req)
        {
            var entity = await _sitesService.UpdateSitePhases(req, _authService.GetUserObjectId());
            if (entity is null) throw new BadRequestException("Error in updating entity.");
            return Ok(entity);
        }

        [HttpGet("exist")]
        public async Task<IActionResult> IsExist([FromQuery] string param)
        {
            var result = await _sitesService.IsExist(param);
            return Ok(result);
        }
    }
}
