using GreenDoorV1.Entities;
using GreenDoorV1.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GreenDoorV1.Services
{
    public class FeedPostService : IFeedPostService
    {
     
        public void DeleteFeedPost()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<FeedPost>> GetAllFeedPost()
        {
            throw new NotImplementedException();
        }

        public void UpdateFeedPost()
        {
            throw new NotImplementedException();
        }

        Task<ActionResult> IFeedPostService.AddFeedPost()
        {
            throw new NotImplementedException();
        }
    }
}
