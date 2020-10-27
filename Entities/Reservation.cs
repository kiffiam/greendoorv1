using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace GreenDoorV1.Entities
{
    public class Reservation
    {
        public long Id { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
        public DateTime ReservationDate { get; set; }
        public Room Room { get; set; }
        public int NumberOfPlayers { get; set; }

    }
}
