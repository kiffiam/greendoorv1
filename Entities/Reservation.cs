using GreenDoorV1.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace GreenDoorV1.Entities
{
    public class Reservation
    {
        public long Id { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }

        [Required]
        [DateAfterNow]
        public DateTime? ReservationDateTime { get; set; }
        public virtual Room Room { get; set; }
        public int? NumberOfPlayers { get; set; }

        [DefaultValue(false)]
        public bool IsBooked { get; set; }

        [DefaultValue(false)]
        public bool IsDeleted { get; set; }

    }
}
