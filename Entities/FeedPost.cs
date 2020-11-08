using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GreenDoorV1.Entities
{
    public class FeedPost
    {
        public long Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string PostText { get; set; }
        public DateTime PostingDate { get; set; }
    }
}
