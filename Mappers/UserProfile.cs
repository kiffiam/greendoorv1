using AutoMapper;
using GreenDoorV1.Entities;
using GreenDoorV1.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GreenDoorV1.Mappers
{
    public class UserProfile: Profile
    {
        public UserProfile()
        {
            CreateMap<ApplicationUser, LoginViewModel>();
            CreateMap<LoginViewModel, ApplicationUser>();
            CreateMap<RegisterViewModel, ApplicationUser>();
        }
    }
}
