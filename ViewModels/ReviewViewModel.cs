using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GreenDoorV1.ViewModels
{
    public class ReviewViewModel
    {
        public long Id { get; set; }
        public string UserName { get; set; }
        public string RoomName { get; set; }
        public int Point { get; set; }
        public string ReviewText { get; set; }
    }
}
