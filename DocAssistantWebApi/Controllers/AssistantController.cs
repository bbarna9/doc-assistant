using System.Collections.Generic;
using System.Threading.Tasks;
using DocAssistant_Common.Models;
using DocAssistantWebApi.Database.Repositories;
using DocAssistantWebApi.Errors;
using Microsoft.AspNetCore.Mvc;

namespace DocAssistantWebApi.Controllers
{
    [ApiController]
    public class AssistantController : ControllerBase
    {
        private readonly IRepository<Assistant> _repository;
        private readonly IRepository<Doctor> _doctorRepository;

        public AssistantController(IRepository<Assistant> repository,IRepository<Doctor> doctorRepository)
        {
            this._repository = repository;
            this._doctorRepository = doctorRepository;
        }
    }
}