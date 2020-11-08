using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GreenDoorV1;
using GreenDoorV1.Entities;
using GreenDoorV1.Services.Interfaces;
using GreenDoorV1.ViewModels;

namespace GreenDoorV1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservationsAdminController : ControllerBase
    {
        private readonly IReservationService _reservationService;

        public ReservationsAdminController(IReservationService reservationService)
        {
            _reservationService = reservationService;
        }

        //ADMIN
        [HttpGet("AllBooked")]
        public async Task<ActionResult<IEnumerable<ReservationListView>>> GetAllBookedReservations()
        {
            var result = await _reservationService.GetAllBookedReservations();
            return Ok(result);
        }

        // GET: api/Reservations/5
        [HttpGet("{id}/booked")]
        public async Task<ActionResult<IEnumerable<ReservationListView>>> GetAllBookedReservationsByRoomId([FromRoute] long id)
        {
            var result = await _reservationService.GetAllBookedReservationsByRoomId(id);

            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        [HttpGet("{id}/free")]
        public async Task<ActionResult> GetFreeReservationsByRoomId(long id)
        {
            var result = await _reservationService.GetAllFreeReservationsByRoomId(id);

            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        // PUT: api/Reservations/5
        [HttpPut("{id}")]
        public async Task<ActionResult<Reservation>> PutReservation([FromRoute] long id, [FromBody]Reservation reservation)
        {
            return await _reservationService.UpdateReservation(id, reservation);
        }

        // POST: api/Reservations
        [HttpPost("{roomId}")]
        public async Task<ActionResult<Reservation>> AddAvailableReservations([FromRoute] long roomId, int qty, DateTime fromDateTime)
        {
            var result = await _reservationService.AddAvailableRangeReservation(roomId, qty, fromDateTime);

            if (result == null)
            {
                return BadRequest();
            }

            return Ok(result);

        }

        // DELETE: api/Reservations/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteReservation(long id)
        {
            var result = await _reservationService.DeleteReservation(id);
            return Ok(result);
        }
    }
}
