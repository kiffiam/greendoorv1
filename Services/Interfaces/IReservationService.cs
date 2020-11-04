using GreenDoorV1.DTOs;
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
        Task<ActionResult<IEnumerable<Reservation>>> GetAllFreeReservationsByRoomId(long roomId);

        //user--
        Task<ActionResult<IEnumerable<Reservation>>> GetUserReservations(ApplicationUser user);
        Task<ActionResult<bool>> BookReservation(ApplicationUser applicationUser, long reservationId);
        Task<ActionResult<bool>> UnbookReservation(ApplicationUser applicationUser, long reservationId);
        

        //admin----

        Task<ActionResult<IEnumerable<ReservationDTO>>> GetAllBookedReservations();
        Task<ActionResult<IEnumerable<Reservation>>> GetAllBookedReservationsByRoomId(long roomId);
        Task<ActionResult<Reservation>> AddAvailableRangeReservation(long roomId, int qty, DateTime fromDateTime);
        Task<ActionResult<Reservation>> AddReservation(Reservation reservation);
        //so the admin can modify the bookings
        Task<ActionResult<Reservation>> UpdateReservation(long id, Reservation reservation);
        Task<ActionResult<bool>> DeleteReservation(long id);
    }
}
