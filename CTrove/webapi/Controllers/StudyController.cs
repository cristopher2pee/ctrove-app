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

namespace CTrove.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class StudyController : ControllerBase
    {

        private readonly AuthService _authService;
        private readonly IStudyServices _studyServices;

        public StudyController(AuthService authService, IStudyServices studyServices)
        {
            _authService = authService;
            _studyServices = studyServices;
        }

        [HttpGet("{id}")]
       // [CtroveAuthorize(new string[] { CtroveRoles.Admin })]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var entity = await _studyServices.GetById(_authService.GetUserObjectId(), id);
            if (entity == null) { throw new BadRequestException("Error in requesting entities."); }
            return Ok(entity);
        }

        [HttpGet]
       // [CtroveAuthorize(new string[] { CtroveRoles.Admin })]
        public async Task<IActionResult> GetAll([FromQuery] BaseFilters filters)
        {
            var entities = await _studyServices.GetAll(filters);
            if (entities == null) throw new BadRequestException("Error in requesting entities.");
            return Ok(entities);
        }

        [HttpPost]
       // [CtroveAuthorize(new string[] { CtroveRoles.Admin })]
        public async Task<IActionResult> AddStudy([FromBody] StudyRequest req)
        {
            var entity = await _studyServices.Save(_authService.GetUserObjectId(), req);
            if(entity == null) throw new BadRequestException("Error in saving entity.");
            return Ok(entity);
        }

        [HttpPut]
        //[CtroveAuthorize(new string[] { CtroveRoles.Admin })]
        public async Task<IActionResult> UpdateStudy([FromBody] StudyRequest req)
        {
            var entity = await _studyServices.Update(_authService.GetUserObjectId(), req);
            if (entity is null) throw new BadRequestException("Error in updating entity.");
            return Ok(entity);
        }

        [HttpDelete]
        //[CtroveAuthorize(new string[] { CtroveRoles.Admin })]
        public async Task<IActionResult> Delete([FromBody] DeactivateRequest req)
        {
            var result = await _studyServices.Deactivate(_authService.GetUserObjectId(), req);
            if (!result) throw new BadRequestException($"Error in deleting entity id : {req.Id}");
            return Ok();
        }

        [HttpGet("exist")]
        public async Task<IActionResult> IsExist([FromQuery] string param)
        {
            var result = await _studyServices.IsExist(param);
            return Ok(result);
        }
    }
}
