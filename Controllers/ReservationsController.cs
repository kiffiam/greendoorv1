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
using AutoMapper;
using GreenDoorV1.ViewModels;

namespace GreenDoorV1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservationsController : ControllerBase
    {
        private readonly IReservationService _reservationService;
        private readonly IMapper _mapper;

        public ReservationsController(IReservationService reservationService, IMapper mapper)
        {
            _reservationService = reservationService;
            _mapper = mapper;
        }

        //TODO: GetUserBookings.
        [HttpGet("profile/bookings")]
        public async Task<ActionResult> GetUserReservations(string userId)
        {
            var result = await _reservationService.GetUserReservations(userId);
            if (result == null)
            {
                return BadRequest();
            }
            return Ok(_mapper.Map<IEnumerable<ReservationListView>>(result)); ;
        }

        [HttpPut("Book/{reservationId}")]
        public async Task<ActionResult> BookReservation(string userId, long reservationId)
        {
            var result = await _reservationService.BookReservation(userId, reservationId);
            if (!result)
            {
                return BadRequest();
            }
            return Ok(result);
        }

        [HttpPut("Unbook/{reservationId}")]
        public async Task<ActionResult> UnbookReservation(string userId, long reservationId)
        {
            var result = await _reservationService.UnbookReservation(userId, reservationId);
            if (!result)
            {
                return BadRequest("A foglalás törlése nem sikerült!");
            }
            return Ok(result);
        }
    }
}
