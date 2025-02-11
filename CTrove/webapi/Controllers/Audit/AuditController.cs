using CTrove.Api.Azure;
using CTrove.Api.Exceptions;
using CTrove.Api.Extensions;
using CTrove.Api.Middleware;
using CTrove.Core.Common;
using CTrove.Core.Filters;
using CTrove.Services.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CTrove.Api.Controllers.Audit
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]

    public class AuditController : ControllerBase
    {
        private readonly IAuditTrailServices _auditTrailService;
        private readonly AuthService _authService;

        public AuditController(IAuditTrailServices auditTrailServices, AuthService auth)
        {
            _auditTrailService = auditTrailServices;
            _authService = auth;
        }

        [HttpGet("{id}/list-by-record-id")]
        [CtroveAuthorize(new string[] { CtroveRoles.Admin })]
        public async Task<IActionResult> GetAuditListByRecordId([FromRoute] Guid id)
        {
            var result = await _auditTrailService.GetAuditListByRecordIdWithUser(id, _authService.GetUserObjectId());
            return Ok(result);
        }

    }
}
