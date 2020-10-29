using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace GreenDoorV1.Entities
{
    public class ApplicationUser: IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
        public string EmailAddress { get; set; }
        public ICollection<Reservation> Reservations { get; set; }
    }
}
