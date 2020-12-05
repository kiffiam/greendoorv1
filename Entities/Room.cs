using GreenDoorV1.Helpers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace GreenDoorV1.Entities
{
    public class Room
    {
        public long Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        [Required]
        [MaxLength(1000)]
        public string Description { get; set; }

        [Required]
        [Range(0, 5)]
        public int Difficulty { get; set; }

        //Helpers.TimeSpanConverter for NewtonJson
        [Required]
        [JsonConverter(typeof(TimespanConverter))]
        [JsonProperty(TypeNameHandling = TypeNameHandling.All)]
        public TimeSpan MinTime { get; set; }

        [Required]
        [JsonConverter(typeof(TimespanConverter))]
        [JsonProperty(TypeNameHandling = TypeNameHandling.All)]
        public TimeSpan MaxTime { get; set; }

        [JsonConverter(typeof(TimespanConverter))]
        [JsonProperty(TypeNameHandling = TypeNameHandling.All)]
        public TimeSpan RecordTime { get; set; }

        [Required]
        [JsonConverter(typeof(TimespanConverter))]
        [JsonProperty(TypeNameHandling = TypeNameHandling.All)]
        public TimeSpan IntervalTime { get; set; }

        [DefaultValue(false)]
        public bool IsDeleted { get; set; }
        public virtual ICollection<Reservation> AvailableReservations { get; set; }
    }
}
