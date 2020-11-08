using System;
using System.Collections.Generic;
using System.IO.IsolatedStorage;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using GreenDoorV1.ViewModels;
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

        public async Task<IEnumerable<Reservation>> GetUserReservations(string userId)
        {
            var user = await Context.Users.Include(u=>u.Reservations).SingleOrDefaultAsync(x=>x.Id.Equals(userId));
            var result = user.Reservations.ToList();
            return result;
        }

        public async Task<bool> BookReservation(string userId, long reservationId)
        {
            var set = Context.Reservations;

            var reservation = await set.SingleOrDefaultAsync(x => x.Id.Equals(reservationId));

            if (reservation != null && reservation.User == null)
            {
                var user = await Context.Users.FindAsync(userId);
                reservation.User = user;
                reservation.IsBooked = true;

                set.Update(reservation);

                await Context.SaveChangesAsync();

                return true;
            }

            return false;
        }

        public async Task<bool> UnbookReservation(string userId, long reservationId)
        {
            var set = Context.Reservations;

            var reservation = await set.SingleOrDefaultAsync(x => x.User.Id.Equals(userId) && x.Id.Equals(reservationId));

            if (reservation == null)
            {
                return false;
            }

            reservation.User = null;
            reservation.IsBooked = false;

            var user = await Context.Users.FindAsync(userId);
            user.Reservations.Remove(reservation);

            set.Update(reservation);

            await Context.SaveChangesAsync();

            return true;
        }

        public async Task<ActionResult<IEnumerable<ReservationListView>>> GetAllBookedReservations()
        {
            var result = await Context.Reservations
                            .Where(r => r.IsBooked && !r.IsDeleted)
                                .Include(x => x.Room)
                                .Include(y => y.User)
                                    .Select(z => new ReservationListView()
                                    {
                                        Id = z.Id,
                                        ReservationDateTime = z.ReservationDateTime,
                                        NumberOfPlayers = z.NumberOfPlayers,
                                        IsBooked = z.IsBooked,
                                        RoomName = z.Room.Name,
                                        UserName = z.User.LastName
                                    })
                                        .ToListAsync();

            //TODO:kellez a null??
            /*if (result.Count == 0)
            {
                return null;
            }*/

            return result;
        }

        public async Task<ActionResult<IEnumerable<ReservationListView>>> GetAllFreeReservationsByRoomId(long roomId)
        {
            var result = await Context.Rooms.Include(x => x.AvailableReservations).SingleOrDefaultAsync(r => r.Id.Equals(roomId));

            var reservations = result.AvailableReservations.Where(s => !s.IsBooked && !s.IsDeleted)
                .Select(z => new ReservationListView()
                {
                    Id = z.Id,
                    ReservationDateTime = z.ReservationDateTime,
                    NumberOfPlayers = z.NumberOfPlayers,
                    IsBooked = z.IsBooked,
                })
                .ToList();

            if (reservations.Count == 0)
            {
                return null;
            }

            return reservations;
        }

        public async Task<ActionResult<IEnumerable<ReservationListView>>> GetAllBookedReservationsByRoomId(long roomId)
        {
            //Room.IsDeleted?????
            var result = await Context.Reservations
                        .Where(r => r.Room.Id.Equals(roomId) && r.IsBooked && !r.IsDeleted)
                            .Include(x => x.Room)
                            .Include(y => y.User)
                            .Select(z => new ReservationListView()
                            {
                                Id = z.Id,
                                ReservationDateTime = z.ReservationDateTime,
                                NumberOfPlayers = z.NumberOfPlayers,
                                IsBooked = z.IsBooked,
                                RoomName = z.Room.Name,
                                UserName = z.User.LastName
                            })
                                .ToListAsync();

            if (result.Count == 0)
            {
                return null;
            }

            return result;
        }

        public async Task<ActionResult<IEnumerable<Reservation>>> AddAvailableRangeReservation(long roomId, int qty, DateTime fromDateTime)
        {
            var ReservationSet = Context.Reservations;

            var room = await Context.Rooms.SingleOrDefaultAsync(x => x.Id.Equals(roomId));

            /*if (fromDateTime - (room.AvailableReservations.OrderByDescending(x => x.ReservationDateTime).Select(x => x.ReservationDateTime).First()) < room.IntervalTime)
            {
                throw new ArgumentOutOfRangeException("Interval time between the new reservations and the last reservation are smaller than it's stated in the Room");
            }
            */
            var newReservations = new List<Reservation>();

            if (room.AvailableReservations == null)
            {
                room.AvailableReservations = new List<Reservation>();
            }

            for (int i = 0; i < qty; i++)
            {
                newReservations.Add(new Reservation
                {
                    ReservationDateTime = fromDateTime + i * room.IntervalTime,
                    Room = room,
                    IsBooked = false,
                    IsDeleted = false,
                });
            }

            await ReservationSet.AddRangeAsync(newReservations);

            await Context.SaveChangesAsync();

            return newReservations;
        }

        //Lehet ide is elég bemenetneka DTO és nem az entity kell
        public async Task<ActionResult<Reservation>> UpdateReservation(long id, Reservation reservation)
        {
            var set = Context.Reservations;

            var original = await set.SingleOrDefaultAsync(item => item.Id.Equals(id));

            if (original == null)
            {
                return null;
            }

            //set.Remove(original);

            reservation.Room = await Context.Rooms.FindAsync(reservation.Room.Id);
            reservation.User = await Context.Users.FindAsync(reservation.User.Id);

            //await set.AddAsync(reservation);

            set.Update(reservation);

            await Context.SaveChangesAsync();

            return reservation;
        }

        public async Task<ActionResult<bool>> DeleteReservation(long id)
        {
            var deletable = await Context.Reservations.SingleOrDefaultAsync(r => r.Id.Equals(id));
            if (deletable == null)
            {
                return false;
            }

            deletable.IsDeleted = true;

            //Context.Remove(deletable);
            Context.Update(deletable);

            await Context.SaveChangesAsync();

            return true;
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
