using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DocAssistant_Common.Models;
using DocAssistantWebApi.Database.Repositories;
using DocAssistantWebApi.Filters;
using DocAssistantWebApi.Services.Auth;
using Microsoft.AspNetCore.Authorization;

namespace DocAssistantWebApi.Controllers
{
    [ApiController]
    public class PatientController : ControllerBase
    {

       /* private readonly IRepository<Patient> _patientRepository;
        private readonly IRepository<Doctor> _doctorRepository;
       // private readonly IAuthService _authService;
        
        public PatientController(IRepository<Patient> repository,IRepository<Doctor> doctorRepository,IAuthService authService)
        {
            this._patientRepository = repository;
            this._doctorRepository = doctorRepository;
           // this._authService = authService;
        }

        [Produces("application/json")]
        [Route("api/patient/load")]
        [HttpPost]
        public async Task<ActionResult> LoadPatient([FromHeader(Name = "Authorization")] string token,[FromQuery(Name = "type")] string type, [FromQuery(Name = "id")] long? id)
        {
            var result = await this._authService.Authorize(token);

            if (!result.Item1)
            {
                return Unauthorized();
            }
            
            switch (type)
            {
                case "single":
                {
                    var patient = await this._patientRepository.Where(patient => patient.Id == id);
                    if (patient == null || patient.DoctorId != result.Item2)
                    {
                        return BadRequest(
                            "Patient with id does not exist or the patient does not belong to this doctor account");
                    }
                    return Ok(patient);
                }
                case "all":
                    var patients =
                        await this._patientRepository.WhereMulti(patient => patient.DoctorId == result.Item2);
                    return Ok(patients);
                default:
                    return BadRequest();
            }
        }

        //  [Authorize(Policy = "DoctorAuth")]
        [Produces("application/json")]
        [Route("api/patient")]
        [HttpPost]
        public async Task<ActionResult> AddPatient([FromHeader(Name = "Authorization")] string token,[FromBody] Patient patient)
        {
            var result = await this._authService.Authorize(token);

            if (!result.Item1)
            {
                return Unauthorized();
            }

            var doctor = await this._doctorRepository.Where(doctor => doctor.Id == result.Item2);
            doctor.Patients.Add(patient);
            await this._doctorRepository.Update(doctor);

            return Ok();
        }

        [Produces("application/json")]
        [Route("api/patient")]
        [HttpPatch]
        public async Task<ActionResult> UpdateData([FromHeader(Name = "Authorization")] string token,[FromBody] Patient patient)
        {
            var result = await this._authService.Authorize(token);

            if (!result.Item1)
            {
                return Unauthorized();
            }

            await this._patientRepository.Update(patient);
            
            return Ok();
        }

        [Produces("application/json")]
        [Route("api/patient")]
        [HttpDelete]
        public async Task<ActionResult> DeletePatient([FromHeader(Name = "Authorization")] string token,[FromQuery(Name = "id")] long id)
        {
            var result = await this._authService.Authorize(token);

            if (!result.Item1)
            {
                return Unauthorized();
            }

            await this._patientRepository.DeleteWhere(patient => patient.Id == id);

            return Ok();
        }*/
    }
}