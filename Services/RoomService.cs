using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using GreenDoorV1.Entities;
using GreenDoorV1.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;


namespace GreenDoorV1.Services
{
    public class RoomService : IRoomService
    {

        protected ApplicationDbContext Context { get; }

        public RoomService(ApplicationDbContext context)
        {
            Context = context;
        }

        public async Task<long> AddRoom(Room room)
        {
            var original = await Context.Rooms.SingleOrDefaultAsync(item => item.Id.Equals(room.Id));

            if (original != null)
            {
                Context.Rooms.Remove(original);
            }
            
            room.isDeleted = false;

            await Context.Rooms.AddAsync(room);
            /*await Context.Rooms.AddAsync(new Room
            {
                Id = room.Id,
                Difficulty = room.Difficulty,
                RecordTime = room.RecordTime,
                MinTime = room.MinTime,
                MaxTime = room.MaxTime,
                IntervalTime = room.IntervalTime,
                Description = room.Description
            });*/
           
            await Context.SaveChangesAsync();
            return room.Id;
        }

        public async Task<bool> DeleteRoom(long? roomId)
        {

            var original = await Context.Rooms.SingleAsync(r => r.Id.Equals(roomId));            
            
            //TODO: függöségek felszámolása, reservation amik a szobára vonatkoznak

            if (original != null)
            {
                original.isDeleted = true;
                await Context.SaveChangesAsync();
                return true;
            } else {
                return false;
            }
        }

        public async Task<IEnumerable<Room>> GetAllRooms()
        {
            var rooms = await Context.Rooms.ToListAsync();
            return rooms;
        }

        public async Task<ActionResult<Room>> GetRoomById(long id)
        {
            
                var room = await Context.Rooms.FirstOrDefaultAsync(r => r.Id.Equals(id));
                return room;
            
            
        }

        public Task UpdateRoom()
        {
            throw new NotImplementedException();
        }
    }
}
