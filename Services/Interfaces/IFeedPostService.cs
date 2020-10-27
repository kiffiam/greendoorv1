using GreenDoorV1.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GreenDoorV1.Services.Interfaces
{
    interface IFeedPostService
    {
        Task<IEnumerable<FeedPost>> GetAllFeedPost();
        void AddFeedPost();
        void DeleteFeedPost();
        void UpdateFeedPost();

    }
}
