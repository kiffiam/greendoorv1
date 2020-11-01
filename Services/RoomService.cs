using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using GreenDoorV1.Entities;
using GreenDoorV1.Services.Interfaces;
using Microsoft.AspNetCore.Http.Connections;
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

        public async Task<ActionResult<Room>> AddRoom(Room room)
        {
            //room.isDeleted = false;
            if (room.AvailableReservations == null)
            {
                room.AvailableReservations = new List<Reservation>();
            }

            await Context.Rooms.AddAsync(room);
           
            await Context.SaveChangesAsync();

            return room;
        }

        public async Task<ActionResult<bool>> DeleteRoom(long? roomId)
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

        public async Task<ActionResult<Room>> GetRoomDetailedById(long? id)
        {

            var room = await Context.Rooms.FirstOrDefaultAsync(r => r.Id.Equals(id));

            if (room == null)
            {
                return null;
            }

            return room;
        }

        public async Task<ActionResult<Room>> UpdateRoom(long? id ,Room room)
        {
            var set = Context.Rooms;

            //var original = await set.Include(r => r.AvailableReservations).SingleOrDefaultAsync(item => item.Id.Equals(id));
            var original = await set.SingleOrDefaultAsync(item => item.Id.Equals(id));

            if (original == null)
            {
                return null;
            }

            var reservations = original.AvailableReservations;

            set.Remove(original);

            room.AvailableReservations = reservations;

            await set.AddAsync(room);

            await Context.SaveChangesAsync();

            return room;

        }
    }
}
