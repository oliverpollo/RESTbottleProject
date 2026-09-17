using Microsoft.AspNetCore.Mvc;
using RESTbottle.Models;
using RESTbottle.Repos;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RESTbottle.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CharactersController : ControllerBase
    {
        private ICharacterRepo repo;
        public CharactersController(ICharacterRepo repo)
        {
            this.repo = repo;
        }

        // GET: api/<CharactersController>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("{id}")]
        public ActionResult<Characters> Get(int id)
        {
            var character = repo.GetCharacterById(id);
            if (character == null)
            {
                return NotFound("Character not found with ID: " + id);
            }
            return Ok(character);
        }


        // GET: api/<BottlesController>
        [HttpGet]
        public IEnumerable<Characters> Get()
        {
            return repo.GetAllCharacters();
        }

        [ProducesResponseType(StatusCodes.Status201Created)]
        [HttpPost]

        public ActionResult<Characters> Post([FromBody] Characters value)
        {
            repo.AddCharacter(value);
            return CreatedAtAction(nameof(Get), new { id = value.Id }, value);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpPut("{id}")]
        public ActionResult<Characters> Put(int id, [FromBody] Characters value)
        {
            Characters? updatedCharacter = repo.UpdateCharacter(id, value);
            if (updatedCharacter == null)
            {
                return NotFound("Character not found with ID: " + id);
            }
            return Ok(updatedCharacter);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {

            Characters? character = repo.DeleteByIdCharacter(id);

            if (character == null)
            {
                return NotFound("Character not found with ID: " + id);
            }
            return Ok(character);
        }
    }
}
