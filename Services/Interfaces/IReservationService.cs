using GreenDoorV1.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GreenDoorV1.Services.Interfaces
{
    interface IReservationService
    {
        Task<IEnumerable<Reservation>> GetUserReservations(ApplicationUser user);
        Task<IEnumerable<Reservation>> GetAllReservations();
        void AddReservation();
        void DeleteReservation();
    }
}
