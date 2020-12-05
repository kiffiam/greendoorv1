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
using Microsoft.AspNetCore.Identity;

namespace GreenDoorV1.Services
{
    public class ReservationService : IReservationService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        protected ApplicationDbContext Context { get; }

        public ReservationService(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            Context = context;
            _userManager = userManager;
        }

        public async Task<IEnumerable<Reservation>> GetUserReservations(string userId)
        {
            var user = await Context.Users
                        .Include(u=>u.Reservations)
                            .ThenInclude(r=>r.Room)
                        .SingleOrDefaultAsync(x=>x.Id.Equals(userId));

            if (user == null) 
            {
                return null;
            }

            var result = user.Reservations.Where(r=>!r.IsDeleted).ToList();
            return result;
        }

        public async Task<bool> BookReservation(string userId, long reservationId)
        {
            var set = Context.Reservations;

            var reservation = await set.Include(x=>x.User).SingleOrDefaultAsync(x => x.Id.Equals(reservationId));

            if (reservation != null)
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

            var user = await Context.Users.FindAsync(userId);
            var roles = await _userManager.GetRolesAsync(user);

            var reservation = await set.Include(r=>r.User).SingleOrDefaultAsync(x => x.User.Id.Equals(userId) && x.Id.Equals(reservationId));
            

            if (roles.FirstOrDefault() == ApplicationRoles.Admin)
            {
                reservation = await set.Include(r=>r.User).SingleOrDefaultAsync(x=>x.Id.Equals(reservationId));

            }

            if (reservation == null)
            {
                return false;
            }

            user = await Context.Users.FindAsync(reservation.User.Id);
            user.Reservations.Remove(reservation);

            reservation.User = null;
            reservation.IsBooked = false;

            set.Update(reservation);

            await Context.SaveChangesAsync();

            return true;
        }

        public async Task<IEnumerable<Reservation>> GetAllBookedReservations()
        {
            var result = await Context.Reservations
                            .Where(r => r.IsBooked && !r.IsDeleted)
                                .Include(x => x.Room)
                                .Include(y => y.User)
                                        .ToListAsync();

            if (result == null)
            {
                return null;
            }

            return result;
        }

        public async Task<IEnumerable<Reservation>> GetAllFreeReservationsByRoomId(long roomId)
        {
            var result = await Context.Rooms.Include(x => x.AvailableReservations).SingleOrDefaultAsync(r => r.Id.Equals(roomId));

            var reservations = result.AvailableReservations.Where(s => !s.IsBooked && !s.IsDeleted).ToList();

            if (reservations.Count == 0)
            {
                return null;
            }

            return reservations;
        }

        public async Task<IEnumerable<Reservation>> GetAllBookedReservationsByRoomId(long roomId)
        {

            var result = await Context.Reservations
                        .Where(r => r.Room.Id.Equals(roomId) && r.IsBooked && !r.IsDeleted)
                            .Include(x => x.Room)
                            .Include(y => y.User).ToListAsync();

            if (result.Count == 0)
            {
                return null;
            }

            return result;
        }

        public async Task<IEnumerable<Reservation>> AddAvailableRangeReservation(long roomId, int qty, DateTime fromDateTime)
        {
            var ReservationSet = Context.Reservations;

            var room = await Context.Rooms.SingleOrDefaultAsync(x => x.Id.Equals(roomId));

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

        public async Task<Reservation> UpdateReservation(long id, Reservation reservation)
        {
            var set = Context.Reservations;

            var original = await set.SingleOrDefaultAsync(item => item.Id.Equals(id));

            if (original == null)
            {
                return null;
            }

            reservation.Room = await Context.Rooms.FindAsync(reservation.Room.Id);
            reservation.User = await Context.Users.FindAsync(reservation.User.Id);

            set.Update(reservation);

            await Context.SaveChangesAsync();

            return reservation;
        }

        public async Task<bool> DeleteReservation(long id)
        {
            var deletable = await Context.Reservations.SingleOrDefaultAsync(r => r.Id.Equals(id));
            if (deletable == null)
            {
                return false;
            }

            deletable.IsDeleted = true;
            deletable.User = null;

            Context.Update(deletable);

            await Context.SaveChangesAsync();

            return true;
        }

        public async Task<IEnumerable<Reservation>> GetAllReservations()
        {
            var result = await Context.Reservations
                        .Where(r => !r.IsDeleted)
                            .Include(x => x.Room)
                            .Include(y => y.User).ToListAsync();
            return result;
        }

        public async Task<IEnumerable<Reservation>> GetAllReservationsByRoomId(long roomId)
        {
            var result = await Context.Reservations
                        .Where(r => !r.IsDeleted && r.Room.Id.Equals(roomId))
                            .Include(x => x.Room)
                            .Include(y => y.User).ToListAsync();
            return result;
        }
    }
}
