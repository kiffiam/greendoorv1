using GreenDoorV1.Entities;
using GreenDoorV1.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GreenDoorV1.Services
{
    public class UserService : IUserService
    {

        protected ApplicationDbContext Context { get; }
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public UserService(ApplicationDbContext context, SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager)
        {
            Context = context;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task<bool> DeleteUser(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);

            

            if (user == null)
            { 
                return false;
            }

            var user1 = await Context.Users.Include(u => u.Reservations).SingleOrDefaultAsync(u => u.Id.Equals(userId));
            var reservations = user1.Reservations.ToList();

            foreach (Reservation res in reservations)
            {
                res.User = null;
                res.IsBooked = false;
            }

            await _userManager.DeleteAsync(user);

            //Context.Users.Remove(user);
            //await Context.SaveChangesAsync();
            return true;

            //throw new NotImplementedException();
        }

        public IEnumerable<ApplicationUser> GetAllUsers()
        {
            var result = Context.Users.ToList();
            if (result == null)
            {
                return null;
            }
            return result;
        }
    }
}
