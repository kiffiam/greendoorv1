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
        Task<ActionResult<Room>> GetRoomById(long id);
        Task<long> AddRoom(Room room);
        Task UpdateRoom();
        //TODO: Deleting -> delete reservations or flag deleted
        Task<bool> DeleteRoom(long? roomId);

        
    }
}
