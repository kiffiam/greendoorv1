using GreenDoorV1.Entities;
using GreenDoorV1.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GreenDoorV1.DTOs
{
    public class ReservationDTO
    {
        public long Id { get; set; }

        [Required]
        [DateAfterNow]
        public DateTime? ReservationDateTime { get; set; }
        public int? NumberOfPlayers { get; set; }
        public string RoomName { get; set; }
        public string UserName { get; set; }
        [DefaultValue(false)]
        public bool IsBooked { get; set; }

        [DefaultValue(false)]
        public bool IsDeleted { get; set; }
    }
}
