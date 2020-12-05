using GreenDoorV1.ViewModels;
using GreenDoorV1.Entities;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GreenDoorV1.Services.Interfaces
{
    public interface IRoomService
    {
        Task<IEnumerable<Room>> GetAllRooms();
        Task<Room> GetRoomDetailedById(long? id);
        Task<ActionResult<Room>> AddRoom(Room room);
        Task<ActionResult<Room>> UpdateRoom(long? id, Room room);
        Task<bool> DeleteRoom(long? roomId);

        
    }
}
