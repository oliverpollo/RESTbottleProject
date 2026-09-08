using Microsoft.AspNetCore.Mvc;
using RESTbottle.Models;
using RESTbottle.Repos;
using System.Runtime.CompilerServices;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RESTbottle.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BottlesController : ControllerBase
    {

        private IBottlesRepository repo; 

        public BottlesController(IBottlesRepository repo)
        {
            this.repo = repo;
        }
        // GET: api/<BottlesController>
        [HttpGet]
        public IEnumerable<Bottle> Get()
        {
            return repo.GetAllBottles();
        }

        // GET api/<BottlesController>/5
        [HttpGet("{id}")]
        public Bottle? Get(int id)
        {
            //TODO: Handle null case, return 404 if not found
            return repo.GetBottleById(id);
        }

        // POST api/<BottlesController>
        [HttpPost]
        public void Post([FromBody] Bottle value)
        {
            repo.AddBottle(value);
        }

        // PUT api/<BottlesController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] Bottle value)
        {
            repo.UpdateBottle(id, value);
        }

        // DELETE api/<BottlesController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            repo.DeleteByIdBottle(id);
        }
    }
}
