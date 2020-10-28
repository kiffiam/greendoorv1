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
            /*try
            {*/
                Context.Rooms.Add(room);
                    /*new Room
                    {
                        Id = room.Id,
                        Difficulty = room.Difficulty,
                        RecordTime = room.RecorsdTime,
                        MinTime = room.MinTime,
                        MaxTime = room.MaxTime,
                        IntervalTime = room.IntervalTime,
                        Description = room.Description,
                    });*/
                await Context.SaveChangesAsync();
                return room.Id;
            //}
            /*catch (Exception e) 
            {
                return 
            }
            */
            //throw new NotImplementedException();
        }

        public async Task<bool> DeleteRoom(long? roomId)
        {
            var deletable = Context.Rooms.Find(roomId);            
            
            //TODO: függöségek felszámolása, reservation amik a szobára vonatkoznak

            if (deletable != null)
            {
                Context.Rooms.Remove(deletable);
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

            if (room != null)
            {
                return room;
            }

            return room;
        }

        public Task UpdateRoom()
        {
            throw new NotImplementedException();
        }
    }
}
