using CTrove.Api.Exceptions;
using CTrove.Api.Extensions;
using CTrove.Api.Middleware;
using CTrove.Core.Common;
using CTrove.Core.DTO.Request;
using CTrove.Services.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using CTrove.Core.DTO;
using Microsoft.Identity.Web;
using CTrove.Api.Azure;

namespace CTrove.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AccountsController : ControllerBase
    {
        private readonly IUserServices _userServices;
        private readonly AzureAdServices _azureAdServices;
        private readonly AuthService _authService;

        public AccountsController(IUserServices userServices, AzureAdServices azureAdServices, AuthService authService)
        {
            _azureAdServices = azureAdServices;
            _userServices = userServices;
            _authService = authService;
        }

        [HttpGet("profile")]
       // [CtroveAuthorize(new string[] { CtroveRoles.User, CtroveRoles.Admin })]
        public async Task<IActionResult> GetUserProfile()
        {
            var objectId =  _authService.GetUserObjectId();
            var email = _authService.GetUserEmail();

            var entity = await _userServices.GetUserProfile(objectId, email);
            if (entity == null) throw new BadRequestException("User not found");
            return Ok(entity);
        }

        [HttpPost("onboarding")]
        // [CtroveAuthorize(new string[] { CtroveRoles.User, CtroveRoles.Admin })]
        public async Task<IActionResult> OnBoardingUser([FromBody] UserRequest request)
        {
            var entity = await _userServices.UserOnBoarding(_authService.GetUserObjectId(), request);
            if (entity == null) { throw new BadRequestException("Error in onboarding user"); }
            return Ok(entity);
        }


        [HttpGet("isOnBoarding")]
        // [CtroveAuthorize(new string[] { CtroveRoles.User, CtroveRoles.Admin })]
        public async Task<IActionResult> IsOnBoarding()
        {

            var entity = await _userServices.isUserOnBoarding(_authService.GetUserObjectId());
            if (entity == null) return Ok(false);
            return Ok(true);
        }

        [HttpPost("invite")]
        [CtroveAuthorize(new string[] { CtroveRoles.Admin })]
        public async Task<IActionResult> Invite([FromBody] AccountsRequest req)
        {
            _azureAdServices.StartService();
            var invt = await _azureAdServices.InviteUser(req.email);
            if (invt == null) throw new BadRequestException($"Error in inviting user {req.email}");

            var entity = await _userServices.InviteUserSave(req, _authService.GetUserObjectId());
            if (entity == null) { throw new BadRequestException($"Error in inviting user {req.email}");}

            return Ok(entity);
        }

        [HttpPost("invite-email")]
        [CtroveAuthorize(new string[] { CtroveRoles.Admin })]
        public async Task<IActionResult> InviteEmail([FromBody] AccountsRequest req)
        {

            _azureAdServices.StartService();
            var invt = await _azureAdServices.InviteUser(req.email);
            if (invt == null) throw new BadRequestException($"Error in inviting user {req.email}");
            return Ok(invt);
        }
    }
}
