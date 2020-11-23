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

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace GreenDoorV1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewsController : ControllerBase
    {
        private readonly IReviewService _reviewService;
        private readonly IMapper _mapper;

        public ReviewsController(IReviewService reviewService, IMapper mapper)
        {
            _reviewService = reviewService;
            _mapper = mapper;
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
        [HttpGet("RoomReviews/{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetRoomReviews([FromRoute]long? id)
        {
            var result = await _reviewService.GetRoomReviews(id);

            if (result == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<List<ReviewViewModel>>(result));
        }

        // POST api/<ReviewsController>
        [HttpPost]
        //[Authorize(Roles = "User")]
        public async Task<IActionResult> PostReview([FromBody] Review review)
        {
            var result = await _reviewService.AddReview(review);
            return Ok(_mapper.Map<ReviewViewModel>(result));
        }

        // PUT api/<ReviewsController>/5
        /*[HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {

        }*/

        // DELETE api/<ReviewsController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteReview(long? id)
        {
            var result = await _reviewService.DeleteReview(id);
            return Ok(result);
        }
    }
}
