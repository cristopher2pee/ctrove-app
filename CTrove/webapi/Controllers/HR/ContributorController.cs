
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
    public class ContributorController : ControllerBase
    {
        private readonly IContributorServices _contributorServices;
        private readonly IHrContributorServices _hrContributorServices;
        private readonly AuthService _authService;

        public ContributorController(IContributorServices contributorServices, 
            AuthService auth,
            IHrContributorServices hrContributorServices)
        {
            _contributorServices = contributorServices;
            _authService = auth;
            _hrContributorServices = hrContributorServices;
        }



        [HttpGet]
        public async Task<IActionResult> GetPagedList([FromQuery]ContributorFiltes filters)
        {
            var entities = await _contributorServices.GetPagedList(filters);
            return Ok(entities);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get([FromRoute] Guid id)
        {
            var entity = await _contributorServices.Get(id,_authService.GetUserObjectId());
            if (entity is null) throw new NotFoundException(MessageShow.ERROR_NOTFOUND);
            return Ok(entity);
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] ContributorRequest req)
        {
            var result = await _contributorServices.Add(req,_authService.GetUserObjectId());
            if(result is null) throw new BadRequestException(MessageShow.ERROR_SAVING);
            return Ok(result);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] ContributorRequest req)
        {
            var result = await _contributorServices.Update(req, _authService.GetUserObjectId());
            if (result is null) throw new BadRequestException(MessageShow.ERROR_UPDATING);
            return Ok(result);
        }

        [HttpDelete]
        public async Task<IActionResult> Deactivate([FromQuery] DeactivateRequest req)
        {
            var result = await _contributorServices.Deactivate(req,_authService.GetUserObjectId());
            return Ok( new { isSuccess = result });
        }

        [HttpGet("name-exist")]
        public async Task<IActionResult> isNameExist([FromQuery] string firstname, [FromQuery] string lastname)
        {
            var result = await _contributorServices.isContributorNameExist(firstname, lastname);
            return Ok(new { isExist = result });
        }

        [HttpGet("email-exist")]
        public async Task<IActionResult> isEmailExist([FromQuery] string email)
        {
            var result = await _contributorServices.isContributorEmailExist(email);
            return Ok(new { isExist = result });
        }

        [HttpGet("hrdb-search-contributor")]
        public async Task<IActionResult> SearchHrContributor([FromQuery] string searchParam)
        {
            var entities = await _hrContributorServices.SearchContributor(searchParam);
            return Ok(entities);
        }

        [HttpGet("search-contributor")]
        public async Task<IActionResult> SearchContributor([FromQuery] string searchParam)
        {
            var entities = await _contributorServices.SearchContributor(searchParam);
            return Ok(entities);
        }

    }
}
