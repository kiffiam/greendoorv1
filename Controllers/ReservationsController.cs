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
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Security.Claims;

namespace GreenDoorV1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class ReservationsController : ControllerBase
    {
        private readonly IReservationService _reservationService;
        private readonly IMapper _mapper;
        private readonly UserManager<ApplicationUser> _userManager;

        public ReservationsController(IReservationService reservationService, IMapper mapper, UserManager<ApplicationUser> userManager)
        {
            _reservationService = reservationService;
            _mapper = mapper;
            _userManager = userManager;
        }

        //TODO: GetUserBookings.
        [HttpGet("MyReservations")]
        [Authorize(Roles = "User")]
        public async Task<ActionResult> GetUserReservations()
        {
            var userId = User.FindFirst("id")?.Value;
            var result = await _reservationService.GetUserReservations(userId);
            if (result == null)
            {
                return BadRequest();
            }
            return Ok(_mapper.Map<IEnumerable<ReservationListView>>(result)); ;
        }

        /*[HttpPut("Book/{id}")]
        //[Authorize(Roles = "User")]
        //[AllowAnonymous]
        public async Task<ActionResult> BookReservation([FromRoute] long id, [FromBody] string userId)
        {

            var result = await _reservationService.BookReservation(userId, id);
            if (!result)
            {
                return BadRequest();
            }
            return Ok(result);
        }*/

        /*[HttpPut("Unbook/{id}")]
        //[Authorize(Roles = "User")]
        public async Task<ActionResult> UnbookReservation([FromRoute] long id, [FromBody] string userId)
        {
            var result = await _reservationService.UnbookReservation(userId, id);
            if (!result)
            {
                return BadRequest("A foglalás törlése nem sikerült!");
            }
            return Ok(result);
        }*/

        [HttpPut("Book/{id}")]
        [Authorize(Roles = "User")]
        public async Task<ActionResult> BookReservation([FromRoute] long id)
        {
            var userId = User.FindFirst("id")?.Value;
            var user = await _userManager.GetUserAsync(HttpContext.User);

            var result = await _reservationService.BookReservation(userId, id);
            if (!result)
            {
                return BadRequest();
            }
            return Ok(result);
        }

        [HttpPut("Unbook/{id}")]
        [Authorize(Roles = "User")]
        public async Task<ActionResult> UnbookReservation([FromRoute] long id)
        {
            var userId = User.FindFirst("id")?.Value;
            var result = await _reservationService.UnbookReservation(userId, id);
            if (!result)
            {
                return BadRequest("A foglalás törlése nem sikerült!");
            }
            return Ok(result);
        }
    }
}
