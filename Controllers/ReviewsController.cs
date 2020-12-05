using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using GreenDoorV1.ViewModels;
using GreenDoorV1.Entities;
using GreenDoorV1.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;


namespace GreenDoorV1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewsController : ControllerBase
    {
        private readonly IReviewService _reviewService;
        private readonly IMapper _mapper;
        private readonly UserManager<ApplicationUser> _userManager;

        public ReviewsController(IReviewService reviewService, IMapper mapper, UserManager<ApplicationUser> userManager)
        {
            _reviewService = reviewService;
            _mapper = mapper;
            _userManager = userManager;
        }

        // GET: api/<ReviewsController>
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetAllReviews()
        {
            var result = await _reviewService.GetAllReviews();

            return Ok(_mapper.Map<List<ReviewViewModel>>(result));
        }

        // GET api/<ReviewsController>/5
        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetRoomReviews([FromRoute] long? id)
        {
            var result = await _reviewService.GetRoomReviews(id);

            if (result == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<List<ReviewViewModel>>(result));
        }

        // POST api/<ReviewsController>
        [HttpPost("{roomId}")]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> PostReview([FromBody] Review review, [FromRoute] long roomId)
        {
            var userId = User.FindFirst("id")?.Value;
            var result = await _reviewService.AddReview(review, userId, roomId);
            return Ok(_mapper.Map<ReviewViewModel>(result));
        }

        // DELETE api/<ReviewsController>/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteReview(long? id)
        {
            var result = await _reviewService.DeleteReview(id);
            return Ok(result);
        }
    }
}
