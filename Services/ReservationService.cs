using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GreenDoorV1.Entities;
using GreenDoorV1.Services.Interfaces;

namespace GreenDoorV1.Services
{
    public class ReservationService : IReservationService
    {
        public void AddReservation()
        {
            throw new NotImplementedException();
        }

        public void DeleteReservation()
        {
            throw new NotImplementedException();
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
