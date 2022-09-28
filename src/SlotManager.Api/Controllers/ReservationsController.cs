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
        public async Task<ActionResult<IEnumerable<ReservationDto>>> Get()
        {
            return Ok(await _reservationService.GetAllWeeklyAsync());
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<Reservation>> Get(Guid id)
        {
            var reservation = await _reservationService.GetAsync(id);

            if (reservation is null)
            {
                return NotFound();
            }

            return Ok(reservation);
        }

        [HttpPost("vehicle")]
        public async Task<ActionResult> Post(ReserveParkingSpotForVehicle command)
        {
            var newReservationId = await _reservationService.ReserveForVehicleAsync(command with { ReservationId = Guid.NewGuid()});

            if(newReservationId is null)
            {
                return BadRequest();
            }

            return CreatedAtAction(nameof(Get), new { id = newReservationId }, null);
        }

        [HttpPost("cleaning")]
        public async Task<ActionResult> Post(ReserveParkingSpotForCleaning command)
        {
            await _reservationService.ReserveForCleaningAsync(command);

            return Ok();
        }

            [HttpPut("{id:guid}")]
        public async Task<ActionResult> Put(Guid id, ChangeReservationLicensePlate command)
        {
            if (await _reservationService.ChangeReservationLicencePlateAsync(command with { ReservationId = id}))
            {
                return NoContent();
            }

            return NotFound();
        }

        [HttpDelete("{id:guid}")]
        public async Task<ActionResult> Delete(Guid id)
        {
            if(await _reservationService.DeleteAsync(new DeleteReservation(id)))
            {
                return NoContent();
            }

            return NotFound();
        }
    }
}
