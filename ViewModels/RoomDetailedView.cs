using GreenDoorV1.Entities;
using GreenDoorV1.Helpers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using static GreenDoorV1.Helpers.TimespanConverter;

namespace GreenDoorV1.ViewModels
{
    public class RoomDetailedView
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        [Range(0, 5)]
        public int Difficulty { get; set; }

        //Helpers.TimeSpanConverter for NewtonJson
        [JsonConverter(typeof(TimespanConverter))]
        [JsonProperty(TypeNameHandling = TypeNameHandling.All)]
        public TimeSpan MinTime { get; set; }

        [JsonConverter(typeof(TimespanConverter))]
        [JsonProperty(TypeNameHandling = TypeNameHandling.All)]
        public TimeSpan MaxTime { get; set; }

        [JsonConverter(typeof(TimespanConverter))]
        [JsonProperty(TypeNameHandling = TypeNameHandling.All)]
        public TimeSpan RecordTime { get; set; }
        
        public virtual ICollection<ReservationListView> AvailableReservations { get; set; }
        //public List<Picture> Pictures { get; set; }
    }
}
