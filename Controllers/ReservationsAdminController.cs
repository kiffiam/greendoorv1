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
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using AutoMapper;
using Newtonsoft.Json.Linq;
using GreenDoorV1.Helpers;

namespace GreenDoorV1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class ReservationsAdminController : ControllerBase
    {
        private readonly IReservationService _reservationService;
        private readonly IMapper _mapper;

        public ReservationsAdminController(IReservationService reservationService, IMapper mapper)
        {
            _reservationService = reservationService;
            _mapper = mapper;
        }

        //ADMIN
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ReservationListView>>> GetAllReservations()
        {
            var result = await _reservationService.GetAllReservations();
            return Ok(_mapper.Map<IEnumerable<ReservationListView>>(result));
        }

        [HttpGet("AllBooked")]
        public async Task<ActionResult<IEnumerable<ReservationListView>>> GetAllBookedReservations()
        {
            var result = await _reservationService.GetAllBookedReservations();
            return Ok(_mapper.Map<IEnumerable<ReservationListView>>(result));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetAllReservationsByRoomId([FromRoute]long id)
        {
            var result = await _reservationService.GetAllReservationsByRoomId(id);

            if (result == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<IEnumerable<ReservationListView>>(result));
        }

        // GET: api/Reservations/5
        [HttpGet("{id}/booked")]
        public async Task<ActionResult> GetAllBookedReservationsByRoomId([FromRoute] long id)
        {
            var result = await _reservationService.GetAllBookedReservationsByRoomId(id);

            if (result == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<IEnumerable<ReservationListView>>(result));
        }

        [HttpGet("{id}/free")]
        public async Task<ActionResult> GetFreeReservationsByRoomId(long id)
        {
            var result = await _reservationService.GetAllFreeReservationsByRoomId(id);

            if (result == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<IEnumerable<ReservationListView>>(result));
        }

        // PUT: api/Reservations/5
        [HttpPut("{id}")]
        public async Task<ActionResult<Reservation>> PutReservation([FromRoute] long id, [FromBody]Reservation reservation)
        {
            return await _reservationService.UpdateReservation(id, reservation);
        }

        // POST: api/Reservations
        [HttpPost("{roomId}")]
        public async Task<ActionResult<IEnumerable<ReservationListView>>> AddAvailableReservations([FromRoute] long roomId, [FromBody]QtyAndTime qtyAndTime)
        {
            var result = await _reservationService.AddAvailableRangeReservation(roomId, qtyAndTime.Quantity, qtyAndTime.FromDateTime);

            if (result == null)
            {
                return BadRequest();
            }

            return Ok(_mapper.Map<IEnumerable<ReservationListView>>(result));

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
