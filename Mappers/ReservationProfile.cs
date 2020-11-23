using AutoMapper;
using GreenDoorV1.ViewModels;
using GreenDoorV1.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GreenDoorV1.Mappers
{
    public class ReservationProfile: Profile
    {
        public ReservationProfile()
        {
            CreateMap<Reservation, ReservationListView>()
                .ForMember(r => r.RoomName, m => m.MapFrom(a => a.Room.Name))
                .ForMember(r => r.UserName, m => m.MapFrom(u => u.User.UserName))
                .ForMember(r => r.UserPhoneNumber, m=>m.MapFrom(up=>up.User.PhoneNumber));

            CreateMap<ReservationListView, Reservation>();
        }
    }
}
