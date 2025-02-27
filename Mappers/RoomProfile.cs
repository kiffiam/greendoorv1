﻿using AutoMapper;
using GreenDoorV1.ViewModels;
using GreenDoorV1.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GreenDoorV1.Mappers
{
    public class RoomProfile: Profile
    {
        public RoomProfile()
        {
            CreateMap<Room, RoomListView>();
            CreateMap<Room, RoomDetailedView>();

            CreateMap<RoomDetailedView, Room>();
        }
    }
}
