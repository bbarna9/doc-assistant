using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DocAssistant_Common.Models;
using DocAssistantWebApi.Database.Repositories;
using DocAssistantWebApi.Errors;
using DocAssistantWebApi.Filters;
using DocAssistantWebApi.Services.Auth;
using DocAssistantWebApi.Utils;
using Microsoft.AspNetCore.Authorization;
using Newtonsoft.Json.Linq;

namespace DocAssistantWebApi.Controllers
{
    [ApiController]
    public class StaffController : ControllerBase
    {

        private readonly IRepository<Doctor> _repository;
        private readonly IRepository<Patient> _patientRepository;
       // private readonly IAuthService _authService;
        
        public StaffController(IRepository<Doctor> repository,IRepository<Patient> patientRepository/*,IAuthService authService*/)
        {
            this._repository = repository;
            this._patientRepository = patientRepository;
          //  this._authService = authService;
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

        [Authorize(Policy = "DoctorRequirement")]
        [Route("/api/doc/")]
        [Produces("application/json")]
        [HttpPatch]
        public async Task<ActionResult> UpdateDoctorData([FromBody] Doctor data)
        {

            data.Id = (long) HttpContext.Items["Id"];

            if (!await this._repository.UpdateChangedProperties(data))
            {
                throw new GenericRequestException
                {
                    Title = "Failed to update account data",
                    Error = "No changes were made",
                    StatusCode = 400
                };
            }
            
            return Ok();
        }
    }
}