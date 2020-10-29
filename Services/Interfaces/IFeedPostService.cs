using GreenDoorV1.Entities;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GreenDoorV1.Services.Interfaces
{
    interface IFeedPostService
    {
        Task<IEnumerable<FeedPost>> GetAllFeedPost();
        Task<ActionResult> AddFeedPost();
        void DeleteFeedPost();
        void UpdateFeedPost();

    }
}
