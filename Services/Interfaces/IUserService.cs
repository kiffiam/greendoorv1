using GreenDoorV1.Entities;
using GreenDoorV1.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GreenDoorV1.Services.Interfaces
{
    public interface IUserService
    {
        Task<object> Register(ApplicationUser user, string password);
        Task<object> RegisterAdmin(ApplicationUser user, string password);
        Task<object> Login(string email, string password);
        Task Logout();
    }
}
