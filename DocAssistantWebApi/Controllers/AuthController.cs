using System;
using System.Threading.Tasks;
using System.Web.Mvc;
using DocAssistant_Common.Models;
using DocAssistantWebApi.Services.Auth;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ActionResult = Microsoft.AspNetCore.Mvc.ActionResult;
using ControllerBase = Microsoft.AspNetCore.Mvc.ControllerBase;

namespace DocAssistantWebApi.Controllers
{
    [RoutePrefix("api")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService<Doctor> _doctorAuthService;
        private readonly IAuthService<Assistant> _assistantAuthService;

        public AuthController(IAuthService<Doctor> doctorAuthService,IAuthService<Assistant> assistantAuthService)
        {
            this._doctorAuthService = doctorAuthService;
            this._assistantAuthService = assistantAuthService;
        }
        
        [Microsoft.AspNetCore.Mvc.Route("/api/auth")]
        [Produces("application/json")]
        [Microsoft.AspNetCore.Mvc.HttpPost]
        public async Task<ActionResult> Auth([FromQuery(Name = "type")] string type,[FromBody] Credentials credentials)
        {
            string accessToken = null;
            switch (type)
            {
                case "doctor":

                    var doc = await _doctorAuthService.Auth(credentials.Username, credentials.Password);
                    if (doc == null) return Unauthorized();
                    
                    accessToken = await this._doctorAuthService.GenerateAccessToken(doc);
                    return Ok(accessToken);
                    
                    break;
                case "assistant":
                    
                    var assistant = await _doctorAuthService.Auth(credentials.Username, credentials.Password);
                    if (assistant == null) return Unauthorized();
                    
                    accessToken = await this._doctorAuthService.GenerateAccessToken(assistant);
                    return Ok(accessToken);
                    
                    break;
                default:
                    return BadRequest();
            }
        }

        [Microsoft.AspNetCore.Mvc.Route("/api/logout")]
        [Produces("application/json")]
        [Microsoft.AspNetCore.Mvc.HttpPost]
        public async Task<ActionResult> Logout([FromHeader(Name = "Authorization")] string accessToken)
        {
            //if (!await this._authService.Logout(accessToken)) return Unauthorized();
            
            return Ok();
        }
    }
}