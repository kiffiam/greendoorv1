using GreenDoorV1.Entities;
using GreenDoorV1.Helpers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;


namespace GreenDoorV1.ViewModels
{
    public class RoomListView
    {
        public long Id { get; set; }
        public string Name { get; set; }

        [Range(0, 5)]
        public int Difficulty { get; set; }

        //Helpers.TimeSpanConverter for NewtonJson
        [JsonConverter(typeof(TimespanConverter))]
        [JsonProperty(TypeNameHandling = TypeNameHandling.All)]
        public TimeSpan MinTime { get; set; }

        [JsonConverter(typeof(TimespanConverter))]
        [JsonProperty(TypeNameHandling = TypeNameHandling.All)]
        public TimeSpan MaxTime { get; set; }

    }
}
