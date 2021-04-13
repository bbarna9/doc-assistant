using System.Threading.Tasks;
using DocAssistant_Common.Models;
using DocAssistantWebApi.Database.Repositories;
using DocAssistantWebApi.Errors;
using DocAssistantWebApi.Filters;
using DocAssistantWebApi.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DocAssistantWebApi.Controllers
{
    [ApiController]
    public class AssistantController : ControllerBase
    {

       private readonly IRepository<Assistant> _assistantRepository;
       
       public AssistantController(IRepository<Assistant> assistantRepository)
        {
            this._assistantRepository = assistantRepository;
        }
        
        [Authorize(Policy = "DoctorRequirement")]
        [Route("/api/assistant/")]
        [Produces("application/json")]
        [HttpPost]
        public async Task<ActionResult> RegisterAssistant([FromBody] Credentials data)
        {
            if (await this._assistantRepository.Where(assistant => assistant.Username.Equals(data.Username)) != null)
            {
                throw new GenericRequestException
                {
                    Title = "Failed to create a new assistant account",
                    Error =
                        "Username is already in use",
                    StatusCode = 400
                };
            }

            var doctorId = (long) HttpContext.Items["Id"];

            var assistant = new Assistant
            {
                Username = data.Username,
                Password = SecurityUtils.CreatePasswordHash(data.Password),
                DoctorId = doctorId
            };
            
            
            await this._assistantRepository.Save(assistant);

            return Ok();
        }
        
        [Authorize(Policy = "AssistantRequirement")]
        [Route("/api/assistant/")]
        [Produces("application/json")]
        [HttpPatch]
        public async Task<ActionResult> UpdateAssistantData([FromBody] Assistant data)
        {

            Roles accountType = (Roles) HttpContext.Items["AccountType"];
            long assistantId;

            if (accountType == Roles.Doctor)
            {
                // Doctors can update assistant data
                long doctorId = (long) HttpContext.Items["Id"];

                var assistant = await this._assistantRepository.Where(assistant => assistant.Id == data.Id);
                if(assistant == null)
                    throw new GenericRequestException
                    {
                        Title = "Failed to update data",
                        Error = "Assistant does not exist",
                        StatusCode = 400
                    };
                
                if(assistant.DoctorId != doctorId)
                    throw new GenericRequestException
                    {
                        Title = "Failed to update data",
                        Error = "Assistant works for a different doctor",
                        StatusCode = 400
                    };
                
                assistantId = assistant.Id;
            }
            else 
                assistantId = (long) HttpContext.Items["Id"];

            data.Id = assistantId;

            if(!await this._assistantRepository.UpdateChangedProperties(data))
                throw new GenericRequestException
                {
                    Title = "Failed to update data",
                    Error =
                        "No changes were made",
                    StatusCode = 400
                };
            
            return Ok();
        }
    }
}