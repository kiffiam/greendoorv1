using GreenDoorV1.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
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

      var user = new IdentityRole(ApplicationRoles.User)
      {
        NormalizedName = ApplicationRoles.User.ToUpper()
      };
      var admin = new IdentityRole(ApplicationRoles.Admin)
      {
        NormalizedName = ApplicationRoles.Admin.ToUpper()
      };


      modelBuilder.Entity<IdentityRole>().HasData(admin, user);
    }

    //public DbSet<ApplicationUser> ApplicationUsers { get; set; }
    public DbSet<Reservation> Reservations { get; set; }
    public DbSet<FeedPost> FeedPosts { get; set; }
    public DbSet<Review> Reviews { get; set; }
    public DbSet<Room> Rooms { get; set; }

  }
}
