using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace GreenDoorV1.Entities
{
    public class Room
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        [Range(0, 5)]
        public int Difficulty { get; set; }
        public ICollection<Reservation> AvailableReservations { get; set; }
        public TimeSpan MinTime { get; set; }
        public TimeSpan MaxTime { get; set; }
        public TimeSpan RecordTime { get; set; }
        public TimeSpan IntervalTime { get; set; }
        //public List<Picture> Pictures { get; set; }
    }
}
