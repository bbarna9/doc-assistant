using System;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using Microsoft.AspNetCore.Authorization;

namespace DocAssistantWebApi.Filters
{
    public class DoctorAuthRequirement : AuthorizationHandler<DoctorAuthRequirement> , IAuthorizationRequirement
    {
        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, DoctorAuthRequirement requirement)
        {
            
        }
        
    }
}