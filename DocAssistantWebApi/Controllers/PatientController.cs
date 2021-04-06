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
    [Route("api/patient")]
    public class PatientController : ControllerBase
    {

        private readonly IRepository<Patient> _patientRepository;
        private readonly IRepository<Doctor> _doctorRepository;
        private readonly IAuthService _authService;
        
        public PatientController(IRepository<Patient> repository,IRepository<Doctor> doctorRepository,IAuthService authService)
        {
            this._patientRepository = repository;
            this._doctorRepository = doctorRepository;
            this._authService = authService;
        }
        
        [HttpGet]
        public ActionResult<Patient> Get()
        {
            return Ok(
                new Patient()
            );
        }

      //  [Authorize(Policy = "DoctorAuth")]
        [Produces("application/json")]
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

       /* public async Task<ActionResult> UpdatePatientData()
        {
            throw new NotImplementedException();
        }*/
    }
}