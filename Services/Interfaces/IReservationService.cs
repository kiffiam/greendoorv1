using GreenDoorV1.ViewModels;
using GreenDoorV1.Entities;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GreenDoorV1.Services.Interfaces
{
    public interface IReservationService
    {
        //unlogged--
        Task<IEnumerable<Reservation>> GetAllFreeReservationsByRoomId(long roomId);

        //user--
        Task<IEnumerable<Reservation>> GetUserReservations(string userId);
        Task<bool> BookReservation(string userId, long reservationId);
        Task<bool> UnbookReservation(string userId, long reservationId);


        //admin----
        Task<IEnumerable<Reservation>> GetAllReservations();
        Task<IEnumerable<Reservation>> GetAllBookedReservations();
        Task<IEnumerable<Reservation>> GetAllReservationsByRoomId(long roomId);
        Task<IEnumerable<Reservation>> GetAllBookedReservationsByRoomId(long roomId);
        Task<IEnumerable<Reservation>> AddAvailableRangeReservation(long roomId, int qty, DateTime fromDateTime);
        Task<Reservation> UpdateReservation(long id, Reservation reservation);
        Task<bool> DeleteReservation(long id);
    }
}
