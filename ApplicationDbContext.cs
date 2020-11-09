using GreenDoorV1.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Xml;

namespace GreenDoorV1
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        //private readonly RoleManager<IdentityRole> _roleManager;

        public ApplicationDbContext(DbContextOptions options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //modelBuilder.Entity<Reservation>().HasKey(x => new { x.Room.Id, x.ReservationDateTime });
        }

        //public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<FeedPost> FeedPosts { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Room> Rooms { get; set; }

        public async void SeedData(RoleManager<IdentityRole> roleManager)
        {
            await roleManager.CreateAsync(new IdentityRole(Entities.Roles.Admin));
            await roleManager.CreateAsync(new IdentityRole(Entities.Roles.User));
        }

    }
}
