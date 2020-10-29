using GreenDoorV1.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace GreenDoorV1.Entities
{
    public class Reservation
    {
        public long Id { get; set; }
        public ApplicationUser ApplicationUser { get; set; }

        [Required]
        [DateAfterNow]
        public DateTime ReservationDateTime { get; set; }
        public Room Room { get; set; }
        public int? NumberOfPlayers { get; set; }
        public bool IsBooked { get; set; }
        public bool isDeleted { get; set; }

    }
}
