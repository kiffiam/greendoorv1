using GreenDoorV1.Entities;
using GreenDoorV1.Helpers;
using GreenDoorV1.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GreenDoorV1.Services
{
    public class AuthService: IAuthService
    {

        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly AppSettings _appSettings;
        //private readonly IConfiguration _configuration;
        ApplicationDbContext _db;

        public AuthService(UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
           // IConfiguration configuration,
            AppSettings appSettings,
            ApplicationDbContext db)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            //_configuration = configuration;
            _appSettings = appSettings;
            _db = db;
        }

        public Task Login()
        {
            throw new NotImplementedException();
        }

        public Task Logout()
        {
            throw new NotImplementedException();
        }

        public Task Register()
        {
            throw new NotImplementedException();
        }
    }
}
