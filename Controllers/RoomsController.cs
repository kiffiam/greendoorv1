﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GreenDoorV1;
using GreenDoorV1.Entities;
using GreenDoorV1.Services;
using GreenDoorV1.Services.Interfaces;
using AutoMapper;
using GreenDoorV1.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace GreenDoorV1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class RoomsController : ControllerBase
    {
        private readonly IRoomService _roomService;
        private readonly IMapper _mapper;

        public RoomsController(IRoomService roomService, IMapper mapper)
        {
            _roomService = roomService;
            _mapper = mapper;
        }

        // GET: api/Rooms
        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<RoomListView>>> GetRooms()
        {
            var result = await _roomService.GetAllRooms();

            if (result == null)
            {
                return NotFound();
            }

            var _result = _mapper.Map<IEnumerable<RoomListView>>(result);

            return Ok(_result);
        }

        // GET: api/Rooms/5
        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<ActionResult> GetRoomDetailed([FromRoute] long id)
        {
            var room = await _roomService.GetRoomDetailedById(id);

            if (room == null)
            {
                return NotFound("Room not found!");
            }

            var result = _mapper.Map<RoomDetailedView>(room);

            return Ok(result);
        }

        // PUT: api/Rooms/5

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> PutRoom([FromRoute] long id, [FromBody] Room room)
        {
            var result = await _roomService.UpdateRoom(id, room);

            if (result == null)
            {
                return BadRequest();
            }

            return Ok(result);
        }

        // POST: api/Rooms
        
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> PostRoom([FromBody] Room room)
        {
            var result = await _roomService.AddRoom(room);

            return Ok(_mapper.Map<RoomDetailedView>(room));
        }

        // DELETE: api/Rooms/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> DeleteRoom(long id)
        {
            var result = await _roomService.DeleteRoom(id);
            if (!result)
            {
                return BadRequest();
            }
            return Ok(result);
        }
    }
}
