using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

        public Task<ActionResult> AddAvailableReservation()
        {
            throw new NotImplementedException();
        }

        public async Task<IActionResult> DeleteReservation(long reservationId)
        {
            var original = await Context.Reservations.SingleAsync(reservation => reservation.Id.Equals(reservationId));

            //original.IsDeleted = true;

            await Context.SaveChangesAsync();

            return 
        }

        public Task<IEnumerable<Reservation>> GetAllReservations()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Reservation>> GetUserReservations(ApplicationUser user)
        {
            throw new NotImplementedException();
        }
    }
}
