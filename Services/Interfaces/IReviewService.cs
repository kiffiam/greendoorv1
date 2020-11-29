using GreenDoorV1.Entities;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace GreenDoorV1.Services.Interfaces
{
    public interface IReviewService
    {
        //unlogged---
        Task<IEnumerable<Review>> GetAllReviews();
        Task<IEnumerable<Review>> GetRoomReviews(long? roomId);

        //user--
        Task<Review> AddReview(Review review, string userId, long roomId);

        //admin
        //Probably not neceserry, because who deletes reviews
        Task<bool> DeleteReview(long? reviewId);
    }
}
