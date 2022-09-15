﻿using Microsoft.AspNetCore.Mvc;
using SlotManager.Api.Commands;
using SlotManager.Api.DTO;
using SlotManager.Api.Entities;
using SlotManager.Api.Services;

namespace SlotManager.Api.Controllers
{
    [ApiController]
    [Route("reservations")]

    public class ReservationsController : ControllerBase
    {
        private readonly ReservationService _service = new();

        [HttpGet]
        public ActionResult<IEnumerable<ReservationDto>> Get() => Ok(_service.GetAllWeekly());

        [HttpGet("{id:guid}")]
        public ActionResult<Reservation> Get(Guid id)
        {
            var reservation = _service.Get(id);

            if (reservation is null)
            {
                return NotFound();
            }

            return Ok(reservation);
        }

        [HttpPost]
        public ActionResult Post(CreateReservation command)
        {
            var newReservationId = _service.Create(command with { ReservationId = Guid.NewGuid()});

            if(newReservationId is null)
            {
                return BadRequest();
            }

            return CreatedAtAction(nameof(Get), new { id = newReservationId }, null);
        }

        [HttpPut("{id:guid}")]
        public ActionResult Put(Guid id, ChangeReservationLicensePlate command)
        {
            if (_service.Update(command with { ReservationId = id}))
            {
                return NoContent();
            }

            return NotFound();
        }

        [HttpDelete("{id:guid}")]
        public ActionResult Delete(Guid id)
        {
            if(_service.Delete(new DeleteReservation(id)))
            {
                return NoContent();
            }

            return NotFound();
        }
    }
}
