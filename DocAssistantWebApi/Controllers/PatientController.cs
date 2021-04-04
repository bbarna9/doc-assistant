using Microsoft.AspNetCore.Mvc;
using DocAssistant_Common.Models;

namespace DocAssistantWebApi.Controllers
{
    [ApiController]
    [Route("api/patient")]
    public class PatientController : ControllerBase
    {
        [HttpGet]
        public ActionResult<Patient> Get()
        {
            return Ok();
        }

        [HttpPost]
        public ActionResult<Patient> Save()
        {
            return Ok();
        }
    }
}