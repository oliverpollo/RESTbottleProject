using Microsoft.AspNetCore.Mvc;
using RESTbottle.Models;
using RESTbottle.Repos;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860
namespace RESTbottle.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TradingCardsController : ControllerBase
    {
        private ITradingCardsRepo repo;
        public TradingCardsController(ITradingCardsRepo repo)
        {
            this.repo = repo;
        }

        // GET: api/<TradingCardsController>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("{id}")]
        public ActionResult<TradingCards> Get(int id)
        {
            var tradingCard = repo.GetTradingCardById(id);
            if (tradingCard == null)
            {
                return NotFound("Trading card not found with ID: " + id);
            }
            return Ok(tradingCard);
        }


        // GET: api/<BottlesController>
        [HttpGet]
        public IEnumerable<TradingCards> Get()
        {
            return repo.GetAllTradingCards();
        }

        [ProducesResponseType(StatusCodes.Status201Created)]
        [HttpPost]

        public ActionResult<TradingCards> Post([FromBody] TradingCards value)
        {
            repo.AddTradingCard(value);
            return CreatedAtAction(nameof(Get), new { id = value.Id }, value);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpPut("{id}")]
        public ActionResult<TradingCards> Put(int id, [FromBody] TradingCards value)
        {
            TradingCards? updatedTradingCard = repo.UpdateTradingCard(id, value);
            if (updatedTradingCard == null)
            {
                return NotFound("Trading card not found with ID: " + id);
            }
            return Ok(updatedTradingCard);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {

            TradingCards? tradingCard = repo.DeleteByIdTradingCard(id);

            if (tradingCard == null)
            {
                return NotFound("Trading card not found with ID: " + id);
            }
            return Ok(tradingCard); 
        }
    }
}
