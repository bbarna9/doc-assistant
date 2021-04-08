using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DocAssistant_Common.Models;
using DocAssistantWebApi.Database.Repositories;
using DocAssistantWebApi.Errors;
using DocAssistantWebApi.Utils;
using Microsoft.AspNetCore.Authorization;

namespace DocAssistantWebApi.Controllers
{
    [ApiController]
    public class DoctorController : ControllerBase
    {

        private readonly IRepository<Doctor> _repository;
        private readonly IRepository<Patient> _patientRepository;

        public DoctorController(IRepository<Doctor> repository,IRepository<Patient> patientRepository)
        {
            this._repository = repository;
            this._patientRepository = patientRepository;
        }

        
        [Route("/api/doc/")]
        [Produces("application/json")]
        [HttpPost]
        public async Task<ActionResult> Register([FromBody] Credentials credentials)
        {

            if (await this._repository.Where(doctor => doctor.Username.Equals(credentials.Username)) != null)
            {
                throw new GenericRequestException
                {
                    Title = "Failed to create a new doctor account",
                    Error =
                        "Username is already in use",
                    StatusCode = 400
                };
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