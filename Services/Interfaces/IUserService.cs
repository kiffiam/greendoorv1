using GreenDoorV1.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GreenDoorV1.Services.Interfaces
{
    public interface IUserService
    {
        Task<object> Register(RegisterViewModel registerViewModel);
        Task Login(LoginViewModel loginViewModel);
        Task Logout();
    }
}
