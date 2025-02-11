using Microsoft.AspNetCore.Mvc.Filters;
using CTrove.Core.Common;
using Microsoft.AspNetCore.Mvc;
using CTrove.Services.Interface;
using CTrove.Api.Extensions;
using CTrove.Core.Entity;
using Microsoft.AspNetCore.Server.IIS.Core;
using CTrove.Api.Exceptions;
using Microsoft.AspNetCore.Http.HttpResults;
using CTrove.Api.Azure;

namespace CTrove.Api.Middleware
{
    public class CtroveAuthorizationFilter : IAuthorizationFilter
    {
        private readonly string[] _roles;
        public CtroveAuthorizationFilter(string[] roles)
        {
            if (roles.Length == 0 || roles == null)
            {
                roles = new string[] { CtroveRoles.User };
            }
            this._roles = roles;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var authService = context.HttpContext.RequestServices.GetRequiredService<AuthService>();
            var permService = context.HttpContext.RequestServices.GetRequiredService<IPermissionServices>();
            // string accessRoles = permService.AccessUserRoles(context.HttpContext.User.GetUserObjectId()).Result;
            string accessRoles = permService.AccessUserRoles(authService.GetUserObjectId()).Result;

            // if(!(_roles.Length == 1 && _roles.Contains(CtroveRoles.User)))
            // {
            if (!string.IsNullOrEmpty(accessRoles))
            {
                bool hasAccess = false;
                foreach (string role in _roles)
                {
                    if (role.ToUpper().Trim() == accessRoles.ToUpper().Trim())
                    {
                        hasAccess = true; return;
                    }
                }

                if (!hasAccess)
                {
                    context.Result = new UnauthorizedObjectResult("User doesn't have access to this page");
                }
            }
            else
            {
                context.Result = new UnauthorizedObjectResult("User doesn't have access to this page");
            }
            // }
            return;
        }
    }

    public class CtroveAuthorizeAttribute : TypeFilterAttribute
    {
        public CtroveAuthorizeAttribute(string[] roles) : base(typeof(CtroveAuthorizationFilter))
        {
            Arguments = new[] { roles };
        }

        public CtroveAuthorizeAttribute() : base(typeof(CtroveAuthorizationFilter))
        {
            Arguments = new[] { new string[] { CtroveRoles.User } };
        }
    }
}
