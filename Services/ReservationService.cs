using System;
using System.Collections.Generic;
using System.IO.IsolatedStorage;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using GreenDoorV1.DTOs;
using GreenDoorV1.Entities;
using GreenDoorV1.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GreenDoorV1.Services
{
    public class ReservationService : IReservationService
    {
        protected ApplicationDbContext Context { get; }

        public ReservationService(ApplicationDbContext context)
        {
            Context = context;
        }

        public Task<ActionResult<IEnumerable<Reservation>>> GetUserReservations(ApplicationUser user)
        {
            throw new NotImplementedException();
        }

        public async Task<ActionResult<IEnumerable<Reservation>>> GetAllFreeReservationsByRoomId(long roomId)
        {
            var result = await Context.Reservations.Where(x => x.Room.Id.Equals(roomId) && !x.IsBooked && (x.ReservationDateTime > DateTime.Now))
                        .Include(y => y.ApplicationUser)
                            .ToListAsync();

            if (result.Count == 0)
            {
                return null;
            }

            return result;
        }

        public async Task<ActionResult<bool>> BookReservation(string userId, long reservationId)
        {
            var set = Context.Reservations;

            var reservation = await set.SingleOrDefaultAsync(x => x.Id.Equals(reservationId));

            if (reservation != null && reservation.ApplicationUser == null)
            {
                var user = await Context.Users.FindAsync(userId);
                reservation.ApplicationUser = user;
                reservation.IsBooked = true;

                if (user.Reservations == null)
                {
                    user.Reservations = new List<Reservation>();
                }
                user.Reservations.Add(reservation);

                set.Update(reservation);

                await Context.SaveChangesAsync();

                return true;
            }

             return false;
        }

        public async Task<ActionResult<bool>> UnbookReservation(ApplicationUser applicationUser, long reservationId)
        {
            var set = Context.Reservations;

            var reservation = await set.SingleOrDefaultAsync(x => x.ApplicationUser.Id.Equals(applicationUser.Id) && x.Id.Equals(reservationId));

            if (reservation == null)
            {
                return false;
            }

            reservation.ApplicationUser = null;
            reservation.IsBooked = false;

            var user = await Context.Users.FindAsync(applicationUser.Id);
            user.Reservations.Remove(reservation);

            set.Update(reservation);

            await Context.SaveChangesAsync();
            
            return true;
        }

        public async Task<ActionResult<IEnumerable<ReservationDTO>>> GetAllBookedReservations()
        {
            var result = await Context.Reservations
                            .Where(r => r.IsBooked)
                                .Include(x => x.Room)                 
                                .Include(y => y.ApplicationUser)
                                    .Select(z => new ReservationDTO()
                                    { 
                                        Id = z.Id,
                                        ReservationDateTime = z.ReservationDateTime,
                                        NumberOfPlayers = z.NumberOfPlayers,
                                        IsBooked = z.IsBooked,
                                        IsDeleted = z.IsDeleted,
                                        RoomName = z.Room.Name,
                                        UserName = z.ApplicationUser.LastName
                                    })
                                        .ToListAsync();

            //TODO:kellez a null??
            /*if (result.Count == 0)
            {
                return null;
            }*/

            return result;

        }

        public async Task<ActionResult<IEnumerable<ReservationDTO>>> GetAllBookedReservationsByRoomId(long roomId)
        {
            var result = await Context.Reservations
                        .Where(r => r.Room.Id.Equals(roomId) && r.IsBooked)
                            .Include(x => x.Room)
                            .Include(y => y.ApplicationUser)
                            .Select(z => new ReservationDTO()
                            {
                                Id = z.Id,
                                ReservationDateTime = z.ReservationDateTime,
                                NumberOfPlayers = z.NumberOfPlayers,
                                IsBooked = z.IsBooked,
                                IsDeleted = z.IsDeleted,
                                RoomName = z.Room.Name,
                                UserName = z.ApplicationUser.LastName
                            })
                                .ToListAsync();


            if (result.Count == 0)
            {
                return null;
            }

            return result;
        }

        public async Task<ActionResult<Reservation>> AddAvailableRangeReservation(long roomId, int qty, DateTime fromDateTime)
        {
            var ReservationSet = Context.Reservations;

            //var room = await Context.Rooms.Include(x => x.AvailableReservations).SingleOrDefaultAsync(x => x.Id.Equals(roomId));

            var room = await Context.Rooms.SingleOrDefaultAsync(x => x.Id.Equals(roomId));

            var newReservations = new List<Reservation>();

            for (int i = 0; i < qty; i++)
            {
                newReservations.Add(new Reservation
                {
                    ReservationDateTime = fromDateTime + i * room.IntervalTime,
                    Room = room,
                    ApplicationUser = null,
                    IsBooked = false,
                    IsDeleted = false,
                });
                //room.AvailableReservations.Add(newReservations[i]);
            }
            //Context.Rooms.Update(room);
            await ReservationSet.AddRangeAsync(newReservations);

            await Context.SaveChangesAsync();

            return newReservations[0];
        }

        public async Task<ActionResult<Reservation>> UpdateReservation(long id, Reservation reservation)
        {
            var set = Context.Reservations;

            var original = await set.SingleOrDefaultAsync(item => item.Id.Equals(id));

            if (original == null)
            {
                return null;
            }

            set.Remove(original);

            //object cycle -- but it is updated nevertheless
            reservation.Room = await Context.Rooms.FindAsync(reservation.Room.Id);
            reservation.ApplicationUser = await Context.Users.FindAsync(reservation.ApplicationUser.Id);
            
            await set.AddAsync(reservation);

            await Context.SaveChangesAsync();

            return reservation;
        }

        public async Task<ActionResult<bool>> DeleteReservation(long id)
        {
            //Törölni kell a hozzá tarzotó szoba IcollectionReservation-ből a reservationt, meg az applicationUser reservataionből is
            throw new NotImplementedException();
        }

        public async Task<ActionResult<Reservation>> AddReservation(Reservation reservation)
        {
            var result = await Context.Reservations.SingleOrDefaultAsync(x => x.ReservationDateTime.Equals(reservation.ReservationDateTime));
            
            if (result != null)
            {
                return null;
            }

            await Context.Reservations.AddAsync(reservation);

            await Context.SaveChangesAsync();

            return reservation;
        }
    }
}
