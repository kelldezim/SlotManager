using Microsoft.AspNetCore.Mvc;
using SlotManager.Api.Models;
using SlotManager.Api.Services;

namespace SlotManager.Api.Controllers
{
    [ApiController]
    [Route("reservations")]

    public class ReservationsController : ControllerBase
    {
        private readonly ReservationService _service = new();

        [HttpGet]
        public ActionResult<IEnumerable<Reservation>> Get() => Ok(_service.GetAll());

        [HttpGet("{id:int}")]
        public ActionResult<Reservation> Get(int id)
        {
            var reservation = _service.Get(id);

            if (reservation is null)
            {
                return NotFound();
            }

            return Ok(reservation);
        }

        [HttpPost]
        public ActionResult Post(Reservation reservation)
        {
            var newReservationId = _service.Create(reservation);

            if(newReservationId is null)
            {
                return BadRequest();
            }

            return CreatedAtAction(nameof(Get), new { id = newReservationId }, null);
        }

        [HttpPut("{id:int}")]
        public ActionResult Put(int id, Reservation reservation)
        {
            reservation.Id = id;
            var successed = _service.Update( reservation);

            if (successed)
            {
                return NoContent();
            }

            return NotFound();
        }

        [HttpDelete("{id:int}")]
        public ActionResult Delete(int id)
        {
            var successed = _service.Delete(id);

            if (successed)
            {
                return NoContent();
            }

            return NotFound();
        }
    }
}
