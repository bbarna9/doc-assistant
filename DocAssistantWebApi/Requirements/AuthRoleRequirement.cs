using System;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using Microsoft.AspNetCore.Authorization;

namespace DocAssistantWebApi.Filters
{
    public class AuthRoleRequirement : AuthorizationHandler<AuthRoleRequirement> , IAuthorizationRequirement
    {
        public readonly Roles[] AllowedRoles;
        public AuthRoleRequirement(Roles[] allowedRoles)
        {
            AllowedRoles = allowedRoles;
        }
        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, AuthRoleRequirement requirement)
        {
            foreach (var role in AllowedRoles)
            {
                if (context.User.IsInRole(nameof(role)))
                {
                    context.Succeed(requirement);
                    return;
                }
            }
            
            context.Fail();
        }
        
    }
}