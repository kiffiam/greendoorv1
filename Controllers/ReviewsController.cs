using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GreenDoorV1.Entities;
using GreenDoorV1.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace GreenDoorV1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewsController : ControllerBase
    {
        private readonly IReviewService _reviewService;

        public ReviewsController(IReviewService reviewService)
        {
            _reviewService = reviewService;
        }

        // GET: api/<ReviewsController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Review>>> GetAllReviews()
        {
            var result = await _reviewService.GetAllReviews();
            return Ok(result);
        }

        // GET api/<ReviewsController>/5
        [HttpGet("RoomReviews/{id}")]
        public async Task<ActionResult<IEnumerable<Review>>> GetRoomRevies([FromRoute]long? id)
        {
            var result = await _reviewService.GetRoomReviews(id);

            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        // POST api/<ReviewsController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Review review)
        {
            var result = await _reviewService.AddReview(review);
            return Ok(result);
        }

        // PUT api/<ReviewsController>/5
        /*[HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {

        }*/

        // DELETE api/<ReviewsController>/5
        /*[HttpDelete("{id}")]
        public void Delete(long? id)
        {

        }*/
    }
}
