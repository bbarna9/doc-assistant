using System;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.OpenApi.Extensions;

namespace DocAssistantWebApi.Filters
{
    public class AuthRoleRequirement : AuthorizationHandler<AuthRoleRequirement> , IAuthorizationRequirement
    {
        private readonly Roles[] _allowedRoles;
        public AuthRoleRequirement(Roles[] allowedRoles)
        {
            _allowedRoles = allowedRoles;
        }
        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, AuthRoleRequirement requirement)
        {
            foreach (var role in _allowedRoles)
            {
                if (context.User.IsInRole(role.GetDisplayName()))
                {
                    context.Succeed(requirement);
                    return;
                }
            }
            
            context.Fail();
        }
        
    }
}