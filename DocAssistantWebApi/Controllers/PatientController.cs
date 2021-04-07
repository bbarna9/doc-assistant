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

        private readonly IRepository<Patient> _patientRepository;
        private readonly IRepository<Doctor> _doctorRepository;

        public PatientController(IRepository<Patient> repository,IRepository<Doctor> doctorRepository)
        {
            this._patientRepository = repository;
            this._doctorRepository = doctorRepository;
        }

        [Authorize(Policy = "DoctorRequirement")]
        [Produces("application/json")]
        [Route("api/patient/load")]
        [HttpPost]
        public async Task<ActionResult> LoadPatient([FromQuery(Name = "type")] string type, [FromQuery(Name = "id")] long? id)
        {

            long docId = (long) HttpContext.Items["Id"];
            
            switch (type)
            {
                case "single":
                {
                    var patient = await this._patientRepository.Where(patient => patient.Id == id);
                    if (patient == null || patient.DoctorId != docId)
                    {
                        return BadRequest(
                            "Patient with id does not exist or the patient does not belong to this doctor account");
                    }
                    return Ok(patient);
                }
                case "all":
                    var patients =
                        await this._patientRepository.WhereMulti(patient => patient.DoctorId == docId);
                    return Ok(patients);
                default:
                    return BadRequest();
            }
        }

        [Authorize(Policy = "AssistantRequirement")]
        [Produces("application/json")]
        [Route("api/patient")]
        [HttpPost]
        public async Task<ActionResult> AddPatient([FromHeader(Name = "Authorization")] string token,[FromBody] Patient patient)
        {
            long docId = (long)HttpContext.Items["Id"];
            
            var doctor = await this._doctorRepository.Where(doctor => doctor.Id == docId);
            doctor.Patients.Add(patient);
            await this._doctorRepository.Update(doctor);

            return Ok();
        }

        [Authorize(Policy = "DoctorRequirement")]
        [Produces("application/json")]
        [Route("api/patient")]
        [HttpPatch]
        public async Task<ActionResult> UpdateData([FromBody] Patient patient)
        {
            await this._patientRepository.Update(patient);
            
            return Ok();
        }

        [Authorize(Policy = "DoctorRequirement")]
        [Produces("application/json")]
        [Route("api/patient")]
        [HttpDelete]
        public async Task<ActionResult> DeletePatient([FromQuery(Name = "id")] long id)
        {
            await this._patientRepository.DeleteWhere(patient => patient.Id == id);

            return Ok();
        }
    }
}