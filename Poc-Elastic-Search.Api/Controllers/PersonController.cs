using Microsoft.AspNetCore.Mvc;
using Nest;
using Poc_Elastic_Search.Api.Contract;
using Poc_Elastic_Search.Api.Model;
using System.Threading.Tasks;

namespace Poc_Elastic_Search.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class PersonController : ControllerBase
    {
        private IPersonService _service;

        public PersonController(IPersonService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            return Ok(await _service.GetAllAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            return Ok(await _service.GetAsync(id));
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync(Person person)
        {
            return Ok(await _service.Save(person));
        }

        [HttpPut]
        public async Task<IActionResult> PutAsync(Person person)
        {
            return Ok(await _service.Save(person));
        }
    }
}
