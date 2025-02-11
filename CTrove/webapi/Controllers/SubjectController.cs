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
    public class SubjectController : ControllerBase
    {
        private readonly ISubjectServices _subjectServices;
        private readonly AuthService _authService;

        public SubjectController(ISubjectServices subjectServices, AuthService auth)
        {
            _subjectServices = subjectServices;
            _authService = auth;
        }

        [HttpPost]
       // [CtroveAuthorize(new string[] { CtroveRoles.Admin })]
        public async Task<IActionResult> Add([FromBody] SubjectRequest param)
        {
            var entity = await _subjectServices.Save(param, _authService.GetUserObjectId());
            if (entity is null) throw new BadRequestException("Error in saving entity.");
            return Ok(entity);

        }

        [HttpPut]
        //[CtroveAuthorize(new string[] { CtroveRoles.Admin })]
        public async Task<IActionResult> Uodate([FromBody] SubjectRequest param)
        {
            var entity = await _subjectServices.Update(param, _authService.GetUserObjectId());
            if (entity is null) throw new BadRequestException("Error in updating entity.");
            return Ok(entity);

        }

        [HttpGet]
        //[CtroveAuthorize(new string[] { CtroveRoles.Admin })]
        public async Task<IActionResult> GetAll([FromQuery] SubjectFilters filters)
        {
            var entities = await _subjectServices.GetAll(filters);
            if (entities is null) throw new BadRequestException("Error in requesting entities.");
            return Ok(entities);
        }

        [HttpGet("{id}")]
       // [CtroveAuthorize(new string[] { CtroveRoles.Admin })]

        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var entity = await _subjectServices.GetById(id, _authService.GetUserObjectId());
            if (entity == null) throw new NotFoundException("Entity not found.");
            return Ok(entity);
        }

        [HttpDelete]
       // [CtroveAuthorize(new string[] { CtroveRoles.Admin })]
        public async Task<IActionResult> Deactivate([FromBody] DeactivateRequest req)
        {
            var result = await _subjectServices.Deactivate(req, _authService.GetUserObjectId());
            if (!result) throw new BadRequestException($"Error in deleting entity id : {req.Id}");
            return Ok();
        }

        [HttpDelete("subject-phases")]
       // [CtroveAuthorize(new string[] { CtroveRoles.Admin })]
        public async Task<IActionResult> DeactivateStudyPhase([FromBody] DeactivateRequest req)
        {
            var result = await _subjectServices.DeactivateSubjectPhase(req, _authService.GetUserObjectId());
            if (!result) throw new BadRequestException($"Error in deleting entity id : {req.Id}");
            return Ok();
        }

        [HttpPost("subject-phases")]
       // [CtroveAuthorize(new string[] { CtroveRoles.Admin })]
        public async Task<IActionResult> AddStudyPhases([FromBody] SubjectPhasesRequest param)
        {
            var entity = await _subjectServices.AddSubjectPhase(param, _authService.GetUserObjectId());
            if (entity is null) throw new BadRequestException("Error in saving entity.");
            return Ok(entity);

        }

        [HttpPut("subject-phases")]
        //[CtroveAuthorize(new string[] { CtroveRoles.Admin })]
        public async Task<IActionResult> UpdateStudyPhases([FromBody] SubjectPhasesRequest param)
        {
            var entity = await _subjectServices.UpdateSubjectPhase(param, _authService.GetUserObjectId());
            if (entity is null) throw new BadRequestException("Error in updating entity.");
            return Ok(entity);

        }

        [HttpGet("subject-phases/{id}")]
      //  [CtroveAuthorize(new string[] { CtroveRoles.Admin })]
        public async Task<IActionResult> GetStudyPhasesById([FromRoute] Guid id)
        {
            var entity = await _subjectServices.GetSubjectPhasebyId(id, _authService.GetUserObjectId());
            if (entity == null) throw new NotFoundException("Entity not found.");
            return Ok(entity);
        }
    }
}
