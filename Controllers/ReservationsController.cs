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
using GreenDoorV1.DTOs;

namespace GreenDoorV1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservationsController : ControllerBase
    {
        private readonly IReservationService _reservationService;

        public ReservationsController(IReservationService reservationService)
        {
            _reservationService = reservationService;
        }

        //ADMIN
        [HttpGet("booked")]
        public async Task<ActionResult<IEnumerable<ReservationDTO>>> GetAllBookedReservations()
        {
            var result = await _reservationService.GetAllBookedReservations();
            return Ok(result);
        }

        // GET: api/Reservations/5
        [HttpGet("{id}/booked")]
        public async Task<ActionResult<IEnumerable<ReservationDTO>>> GetAllBookedReservationsByRoomId([FromRoute] long id)
        {
            var result = await _reservationService.GetAllBookedReservationsByRoomId(id);

            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        [HttpGet("{id}/free")]
        public async Task<ActionResult<IEnumerable<Reservation>>> GetFreeReservationsByRoomId(long id)
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
        public async Task<ActionResult<Reservation>> PutReservation(long id, Reservation reservation)
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

        [HttpPut("Book/{reservationId}")]
        public async Task<ActionResult<bool>> BookReservation(string userId, long reservationId)
        {
            var result = await _reservationService.BookReservation(userId, reservationId);
            return Ok(result);
        }

        // DELETE: api/Reservations/5
        /*[HttpDelete("{id}")]
        public async Task<ActionResult<Reservation>> DeleteReservation(long id)
        {
            var reservation = await _context.Reservations.FindAsync(id);
            if (reservation == null)
            {
                return NotFound();
            }

            _context.Reservations.Remove(reservation);
            await _context.SaveChangesAsync();

            return reservation;
        }

        private bool ReservationExists(long id)
        {
            return _context.Reservations.Any(e => e.Id == id);
        }*/
    }
}
