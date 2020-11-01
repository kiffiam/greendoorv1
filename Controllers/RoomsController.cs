using System;
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

namespace GreenDoorV1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoomsController : ControllerBase
    {
        private readonly IRoomService _roomService;

        public RoomsController(IRoomService roomService)
        {
            _roomService = roomService;
        }

        // GET: api/Rooms
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Room>>> GetRooms()
        {
            var result = await _roomService.GetAllRooms();
            return Ok(result);
        }

        // GET: api/Rooms/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Room>> GetRoomDetailed(long id)
        {
            var room = await _roomService.GetRoomDetailedById(id);
            return room;
        }

        // PUT: api/Rooms/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRoom(long id, Room room)
        {
            var result = await _roomService.UpdateRoom(id, room);
            if (result == null)
            {
                return NotFound();
            }
            return Ok();
        }

        // POST: api/Rooms
        [HttpPost]
        public async Task<ActionResult<Room>> PostRoom(Room room)
        {
            await _roomService.AddRoom(room);

            return Ok(room);
        }

        // DELETE: api/Rooms/5
        [HttpDelete("{id}")]
        public IActionResult DeleteRoom(long id)
        {
            var result = _roomService.DeleteRoom(id);
            return Ok(result);
        }
    }
}
