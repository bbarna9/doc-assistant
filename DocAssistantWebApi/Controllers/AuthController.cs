using System;
using System.Threading.Tasks;
using System.Web.Mvc;
using DocAssistant_Common.Models;
using DocAssistantWebApi.Database.DbModels;
using DocAssistantWebApi.Services.Auth;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ActionResult = Microsoft.AspNetCore.Mvc.ActionResult;
using ControllerBase = Microsoft.AspNetCore.Mvc.ControllerBase;
using Doctor = DocAssistantWebApi.Database.DbModels.Doctor;

namespace DocAssistantWebApi.Controllers
{
    [RoutePrefix("api")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            this._authService = authService;
        }
        
        [Microsoft.AspNetCore.Mvc.Route("/api/auth")]
        [Produces("application/json")]
        [Microsoft.AspNetCore.Mvc.HttpPost]
        public async Task<ActionResult> Auth([FromBody] Credentials credentials)
        {
            var doc = await this._authService.Auth(credentials.Username, credentials.Password);
            if (doc == null) return Unauthorized();

            var accessToken = await this._authService.GenerateAccessToken(doc);

            return Ok(accessToken);
        }

        [Microsoft.AspNetCore.Mvc.Route("/api/logout")]
        [Produces("application/json")]
        [Microsoft.AspNetCore.Mvc.HttpPost]
        public async Task<ActionResult> Logout([FromHeader(Name = "Authorization")] string accessToken)
        {
            if (!await this._authService.Logout(accessToken)) return Unauthorized();
            
            return Ok();
        }
    }
}