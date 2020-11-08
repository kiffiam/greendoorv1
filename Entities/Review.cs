using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace GreenDoorV1.Entities
{
    public class Review
    {
        public long Id { get; set; }
        public ApplicationUser User { get; set; }

        //[ForeignKey("Id")]
        //public long UserId { get; set; }
        public Room Room { get; set; }

       // [ForeignKey("Id")]
        //public long RoomId { get; set; }
        //public string RoomName { get; set; }
        [Range(0,5)]
        public int Point { get; set; }

        [MaxLength(1000)]
        public string ReviewText { get; set; }
    }
}
