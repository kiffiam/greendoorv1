using GreenDoorV1.Entities;
using GreenDoorV1.Helpers;
using GreenDoorV1.Services.Interfaces;
using GreenDoorV1.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace GreenDoorV1.Services
{
    public class AuthService : IAuthService
    {
        //private readonly RoleManager<ApplicationUser> _roleManager;

        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        //private readonly AppSettings _appSettings;
        private readonly IConfiguration _configuration;
        ApplicationDbContext Context;

        public AuthService(UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IConfiguration configuration,
            ApplicationDbContext db)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
            Context = db;
        }

        public async Task<object> Register(ApplicationUser user, string password)
        {
            var userWithSameEmail = await _userManager.FindByEmailAsync(user.Email);
            if (userWithSameEmail == null)
            {
                var result = await _userManager.CreateAsync(user, password);
                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, ApplicationRoles.User);
                    await _signInManager.SignInAsync(user, false);
                    var roles = await _userManager.GetRolesAsync(user);
                    return await GenerateJwtToken(user.Email, user, roles.FirstOrDefault());
                }
                return $"User Registered with email {user.Email}";
            }
            else
            {
                return $"Email {user.Email } is already registered.";
            }
        }

        public async Task<object> RegisterAdmin(ApplicationUser user, string password)
        {
            var userWithSameEmail = await _userManager.FindByEmailAsync(user.Email);
            if (userWithSameEmail == null)
            {
                var result = await _userManager.CreateAsync(user, password);
                if (result.Succeeded)
                {
                    await _signInManager.SignInAsync(user, false);
                    await _userManager.AddToRoleAsync(user, ApplicationRoles.Admin);
                    var roles = await _userManager.GetRolesAsync(user);
                    return await GenerateJwtToken(user.Email, user, ApplicationRoles.Admin);
                }
                return $"User Registered with username {user.UserName}";
            }
            else
            {
                return $"Email {user.Email } is already registered.";
            }
        }

        public async Task<object> Login(string email, string password)

        {   var user = await Context.Users.SingleOrDefaultAsync(x => x.Email.Equals(email));
            if (user == null)
            {
                return null;
            }
            var result = await _signInManager.PasswordSignInAsync(user.UserName, password, false, false);

            if (!result.Succeeded)
            {
                return null;
            }

            var roles = await _userManager.GetRolesAsync(user);

            return GenerateJwtToken(email, user, roles.FirstOrDefault());
            
        }



        public async Task Logout()
        {
            await _signInManager.SignOutAsync();
        }

        private async Task<object> GenerateJwtToken(string email, ApplicationUser user, string role)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Email, email),
                new Claim(JwtRegisteredClaimNames.FamilyName, user.LastName),
                new Claim(JwtRegisteredClaimNames.GivenName, user.FirstName),
                new Claim("phonenumber", user.PhoneNumber),
                new Claim("username", user.UserName),
                new Claim("id", user.Id),
                new Claim("role", role),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.Now.AddHours(3);

            var token = new JwtSecurityToken(
                _configuration["JWT:ValidIssuer"],
                _configuration["JWT:ValidAudience"],
                claims,
                expires: expires,
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

    }
}
