using Microsoft.AspNetCore.Mvc;
using DocAssistant_Common.Models;
using DocAssistantWebApi.Filters;
using Microsoft.AspNetCore.Authorization;

namespace DocAssistantWebApi.Controllers
{
    [ApiController]
    [Route("api/patient")]
    public class PatientController : ControllerBase
    {
        [HttpGet]
        public ActionResult<Patient> Get()
        {
            return Ok(
                new Patient()
            );
        }

        [Authorize(Policy = "DoctorAuth")]
        [Produces("application/json")]
        [HttpPost]
        public ActionResult Save(/*[FromBody] Patient patient*/)
        {
            return Ok();
        }
    }
}