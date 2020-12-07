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

        public virtual ApplicationUser User { get; set; }

        public virtual Room Room { get; set; }

        [Range(0,5)]
        public int Point { get; set; }

        [MaxLength(1000)]
        public string ReviewText { get; set; }
    }
}
