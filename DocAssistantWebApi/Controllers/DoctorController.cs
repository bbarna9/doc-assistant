using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DocAssistant_Common.Models;
using DocAssistantWebApi.Database.DbModels;
using DocAssistantWebApi.Database.Repositories;
using DocAssistantWebApi.Filters;
using DocAssistantWebApi.Utils;
using Microsoft.AspNetCore.Authorization;

namespace DocAssistantWebApi.Controllers
{
    [ApiController]
    public class DoctorController : ControllerBase
    {

        private readonly IRepository<Database.DbModels.Doctor> _repository;
        
        public DoctorController(IRepository<Database.DbModels.Doctor> repository)
        {
            this._repository = repository;
        }
        
        [Route("/api/doc/create")]
        [Produces("application/json")]
        [HttpPost]
        public async Task<ActionResult> Create()
        {
            await _repository.Save(new Database.DbModels.Doctor
            {
                FirstName = "TestFirstName",
                LastName = "TestLastName",
                Username = "Test",
                Password = SecurityUtils.CreatePasswordHash("testpassword234")
            });
            
            return Ok();
        }
    }
}