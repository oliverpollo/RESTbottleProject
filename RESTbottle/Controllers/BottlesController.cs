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

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet ("{id}")]
        public ActionResult<Bottle> Get([FromRoute] int id)
        { 
            var bottle = repo.GetBottleById( id);
            if (bottle == null)
            {
                return NotFound("Bottle not found with ID: " + id);
            }
            return Ok(bottle);
        }


        // GET: api/<BottlesController>
        [HttpGet]
        public ActionResult<IEnumerable<Bottle>> Getv2([FromQuery] string? nameStartsWith = null, [FromQuery] double? minVolume = null, [FromQuery] string? sortOrder = null)
        {
            IEnumerable<Bottle> bottles = repo.GetV2(nameStartsWith, minVolume, sortOrder);
            if (!bottles.Any())
            {
                return Ok(bottles);
            }   
            return NoContent();
        }

        [ProducesResponseType(StatusCodes.Status201Created)]
        [HttpPost]

        public ActionResult<Bottle> Post([FromBody] Bottle value)
        {
            repo.AddBottle(value);
            return CreatedAtAction(nameof(Get), new { id = value.Id }, value);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpPut("{id}")]
        public ActionResult<Bottle> Put(int id, [FromBody] Bottle value)
        {
            Bottle? updatedBottle = repo.UpdateBottle(id, value);
            if (updatedBottle == null)
            {
                return NotFound("Bottle not found with ID: " + id);
            } 
            return Ok(updatedBottle);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {

            Bottle? bottle = repo.DeleteByIdBottle(id);
            
            if (bottle == null)
            {
                return NotFound("Bottle not found with ID: " + id);
            }            
            return NoContent();
        }


        //Version 1.0 med bottles i stedet for ActionResults

        // GET api/<BottlesController>/5
        //[HttpGet("{id}")]
        //public Bottle? Get(int id)
        //{
        //    //TODO: Handle null case, return 404 if not found
        //    return repo.GetBottleById(id);
        //}


        // POST api/<BottlesController>
        //[HttpPost]
        //public void Post([FromBody] Bottle value)
        //{
        //    repo.AddBottle(value);
        //}

        // PUT api/<BottlesController>/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody] Bottle value)
        //{
        //    repo.UpdateBottle(id, value);
        //}

        // DELETE api/<BottlesController>/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //    repo.DeleteByIdBottle(id);
        //}
    }
}
