using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DocAssistant_Common.Models;
using DocAssistantWebApi.Database.Repositories;
using DocAssistantWebApi.Errors;
using DocAssistantWebApi.Services.Auth;
using DocAssistantWebApi.Utils;
using Newtonsoft.Json.Linq;

namespace DocAssistantWebApi.Controllers
{
    [ApiController]
    public class DoctorController : ControllerBase
    {

        private readonly IRepository<Doctor> _repository;
        private readonly IAuthService _authService;
        
        public DoctorController(IRepository<Doctor> repository,IAuthService authService)
        {
            this._repository = repository;
            this._authService = authService;
        }
        
        [Route("/api/doc/register")]
        [Produces("application/json")]
        [HttpPost]
        public async Task<ActionResult> Register([Microsoft.AspNetCore.Mvc.FromBody] Credentials credentials)
        {

            if (await this._repository.Where(doctor => doctor.Username.Equals(credentials.Username)) != null)
            {
                var errors = new Dictionary<string, string[]>()
                {
                    {"Username",new []{"Username is already in use"}}
                };
                
                return BadRequest(
                    new BadRequestProblemDetails(errors)
                    {
                        Title = "Failed to create a new doctor account",
                        StatusCode = 400
                    }
                );
            }

            var doctor = new Doctor
            {
                Username = credentials.Username,
                Password = SecurityUtils.CreatePasswordHash(credentials.Password),
                
            };

            await _repository.Save(doctor);
            
            return Ok();
        }

        [Route("/api/doc/update")]
        [Produces("application/json")]
        [HttpPatch]
        public async Task<ActionResult> UpdateData([FromHeader(Name = "Authorization")] string accessToken,[FromBody] Doctor data)
        {
            var result = await this._authService.Authorize(accessToken);
            
            if (!result.Item1)
                return Unauthorized();

            data.Id = result.Item2;
            
            await this._repository.Update(data);

            return Ok();
        }
    }
}