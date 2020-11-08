using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using AutoMapper;
using GreenDoorV1.ViewModels;
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
            room.AvailableReservations = new List<Reservation>();

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
                original.IsDeleted = true;
                await Context.SaveChangesAsync();
                return true;
            } else {
                return false;
            }
        }

        //All rooms for list view
        public async Task<IEnumerable<Room>> GetAllRooms()
        {
            var rooms = await Context.Rooms.ToListAsync();

            if (rooms == null)
            {
                return null;
            }

            return rooms;
        }

        //Room with details and free reservation times
        public async Task<Room> GetRoomDetailedById(long? id)
        {
            var room = await Context.Rooms.Include(r => r.AvailableReservations).SingleOrDefaultAsync(r => r.Id.Equals(id));

            //lehet nem csak a szabadakat hanem az összeset kellene
            room.AvailableReservations = room.AvailableReservations.Where(r => !r.IsBooked && !r.IsDeleted).ToList();

            if (room == null)
            {
                return null;
            }
            //var result = Mapper.Map<RoomDetailedView>(room);
            return room;
        }

        //admin
        public async Task<ActionResult<Room>> UpdateRoom(long? id ,Room room)
        {
            var set = Context.Rooms;

            var original = await set.Include(r => r.AvailableReservations).SingleOrDefaultAsync(item => item.Id.Equals(id));
            //var original = await set.SingleOrDefaultAsync(item => item.Id.Equals(id));

            if (original == null)
            {
                return null;
            }

            //var reservations = original.AvailableReservations;



            //set.Remove(original);

            //room.AvailableReservations = reservations;

            //await set.AddAsync(room);

            //DTO kell meg mapper geci te lusta szar
            original.Difficulty = room.Difficulty;
            original.Description = room.Description;
            original.IsDeleted = room.IsDeleted;
            original.IntervalTime = room.IntervalTime;
            original.MinTime = room.MinTime;
            original.MaxTime = room.MaxTime;
            original.RecordTime = room.RecordTime;
            original.Name = room.Name;


            set.Update(original);

            await Context.SaveChangesAsync();

            return room;

        }
    }
}
