using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DocAssistant_Common.Models;
using DocAssistantWebApi.Errors;
using DocAssistantWebApi.Services.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;

namespace DocAssistantWebApi.Controllers
{
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
        
        [Route("/api/auth")]
        [Produces("application/json")]
        [HttpPost]
        public async Task<ActionResult> Auth([FromQuery(Name = "type")] string type,[FromBody] Credentials credentials)
        {
            object response = null;
            switch (type)
            {
                case "doctor":

                    var doc = await _doctorAuthService.Auth(credentials.Username, credentials.Password);
                    if (doc == null) return Unauthorized();
                    
                    response = new
                    {
                        accessToken = await this._doctorAuthService.GenerateAccessToken(doc)
                    };
                    
                    return Ok(response);
                case "assistant":
                    
                    var assistant = await _assistantAuthService.Auth(credentials.Username, credentials.Password);
                    if (assistant == null) return Unauthorized();
                    
                    response = new
                    {
                        accessToken = await this._assistantAuthService.GenerateAccessToken(assistant)
                    };

                    return Ok(response);
                default:
                    throw new GenericRequestException
                    {
                        Title = "Failed to auth",
                        Error =
                            "Invalid account type",
                        StatusCode = 400
                    };
            }
        }

        [Authorize(Policy = "AssistantRequirement")]
        [Route("/api/logout")]
        [Produces("application/json")]
        [HttpPost]
        public async Task<ActionResult> Logout([FromQuery(Name = "type")] string type)
        {
            long userId = (long) HttpContext.Items["Id"];
            
            switch (type)
            {
                case "doctor":
                    await this._doctorAuthService.Logout(userId);
                    break;
                case "assistant":
                    await this._assistantAuthService.Logout(userId);
                    break;
                default:
                    throw new GenericRequestException
                    {
                        Title = "Failed to log out",
                        Error =
                            "Invalid account type",
                        StatusCode = 400
                    };
            }
            
            return Ok();
        }
    }
}