using GreenDoorV1.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace GreenDoorV1.Services.Interfaces
{
    interface IReviewService
    {
        Task<IEnumerable<Review>> GetAllReviews();
        Task<IEnumerable<Review>> GetRoomReviews(long roomId);
        void AddReview(long roomId);
        void DeleteReview(long? reviewId);
    }
}
