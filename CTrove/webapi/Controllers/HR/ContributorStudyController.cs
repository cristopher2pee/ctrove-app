using Ctrove.HR.Common;
using Ctrove.HR.Services;
using CTrove.Api.Azure;
using CTrove.Api.Exceptions;
using CTrove.Core.DTO;
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
    public class ContributorStudyController : ControllerBase
    {
        private readonly IContributorStudyServices _contributorStudyServices;
        private readonly AuthService _authService;
        public ContributorStudyController(IContributorStudyServices contributorStudyServices, AuthService authService)
        {
            _authService = authService;
            _contributorStudyServices = contributorStudyServices;
        }

        [HttpGet]
        public async Task<IActionResult> GetPagedList([FromQuery] ContributorStudyFilters filters)
        {
            var entities = await _contributorStudyServices.GetPagedList(filters);
            return Ok(entities);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get([FromRoute] Guid id)
        {
            var entity = await _contributorStudyServices.Get(id, _authService.GetUserObjectId());
            if (entity is null) throw new NotFoundException(MessageShow.ERROR_NOTFOUND);
            return Ok(entity);
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] ContributorStudyRequest req)
        {
            var result = await _contributorStudyServices.Add(req, _authService.GetUserObjectId());
            if (result is null) throw new BadRequestException(MessageShow.ERROR_SAVING);
            return Ok(result);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] ContributorStudyRequest req)
        {
            var result = await _contributorStudyServices.Update(req, _authService.GetUserObjectId());
            if (result is null) throw new BadRequestException(MessageShow.ERROR_UPDATING);
            return Ok(result);
        }
    }
}
