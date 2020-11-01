using GreenDoorV1.Entities;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GreenDoorV1.Services.Interfaces
{
    public interface IFeedPostService
    {
        Task<IEnumerable<FeedPost>> GetAllFeedPost();
        Task<ActionResult<FeedPost>> AddFeedPost(FeedPost feedPost);
        Task<bool> DeleteFeedPost(long? id);
        Task<ActionResult<FeedPost>> UpdateFeedPost(long? id, FeedPost feedPost);

    }
}
