﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GreenDoorV1;
using GreenDoorV1.Entities;
using GreenDoorV1.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace GreenDoorV1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class FeedPostsController : ControllerBase
    {
        private readonly IFeedPostService _feedPostService;

        public FeedPostsController(IFeedPostService feedPostService)
        {
            _feedPostService = feedPostService;
        }

        // GET: api/FeedPosts
        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<FeedPost>>> GetFeedPosts()
        {
            var result = await _feedPostService.GetAllFeedPost();
            return Ok(result);
        }

        // PUT: api/FeedPosts/5
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> PutFeedPost(long id, FeedPost feedPost)
        {
            var result = await _feedPostService.UpdateFeedPost(id, feedPost);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        // POST: api/FeedPosts
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<FeedPost>> PostFeedPost([FromBody] FeedPost feedPost)
        {
            var result = await _feedPostService.AddFeedPost(feedPost);
            if (result == null)
            {
                return BadRequest();
            }
            return Ok(result);
        }

        // DELETE: api/FeedPosts/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<FeedPost>> DeleteFeedPost([FromRoute] long? id)
        {
            var result = await _feedPostService.DeleteFeedPost(id);
            if (result)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
    }
}
