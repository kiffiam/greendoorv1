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
        //unlogged--?? kell ez egyáltalán? DetailedRoomView-ba ugyis ott lesz
        Task<ActionResult<IEnumerable<ReservationListView>>> GetAllFreeReservationsByRoomId(long roomId);

        //user--
        Task<IEnumerable<Reservation>> GetUserReservations(string userId);
        Task<bool> BookReservation(string userId, long reservationId);
        Task<bool> UnbookReservation(string userId, long reservationId);


        //admin----

        Task<ActionResult<IEnumerable<ReservationListView>>> GetAllBookedReservations();
        Task<ActionResult<IEnumerable<ReservationListView>>> GetAllBookedReservationsByRoomId(long roomId);
        Task<ActionResult<IEnumerable<Reservation>>> AddAvailableRangeReservation(long roomId, int qty, DateTime fromDateTime);
        Task<ActionResult<Reservation>> AddReservation(Reservation reservation);
        Task<ActionResult<Reservation>> UpdateReservation(long id, Reservation reservation);
        Task<ActionResult<bool>> DeleteReservation(long id);
    }
}
