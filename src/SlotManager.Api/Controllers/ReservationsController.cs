using Microsoft.AspNetCore.Mvc;
using SlotManager.Application.Commands;
using SlotManager.Application.DTO;
using SlotManager.Application.Services;
using SlotManager.Core.Entities;

namespace SlotManager.Api.Controllers
{
    [ApiController]
    [Route("reservations")]

    public class ReservationsController : ControllerBase
    {
        private readonly IReservationService _reservationService;

        public ReservationsController(IReservationService reservationService)
        {
            _reservationService = reservationService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<ReservationDto>> Get() => Ok(_reservationService.GetAllWeekly());

        [HttpGet("{id:guid}")]
        public ActionResult<Reservation> Get(Guid id)
        {
            var reservation = _reservationService.Get(id);

            if (reservation is null)
            {
                return NotFound();
            }

            return Ok(reservation);
        }

        [HttpPost]
        public ActionResult Post(CreateReservation command)
        {
            var newReservationId = _reservationService.Create(command with { ReservationId = Guid.NewGuid()});

            if(newReservationId is null)
            {
                return BadRequest();
            }

            return CreatedAtAction(nameof(Get), new { id = newReservationId }, null);
        }

        [HttpPut("{id:guid}")]
        public ActionResult Put(Guid id, ChangeReservationLicensePlate command)
        {
            if (_reservationService.Update(command with { ReservationId = id}))
            {
                return NoContent();
            }

            return NotFound();
        }

        [HttpDelete("{id:guid}")]
        public ActionResult Delete(Guid id)
        {
            if(_reservationService.Delete(new DeleteReservation(id)))
            {
                return NoContent();
            }

            return NotFound();
        }
    }
}
