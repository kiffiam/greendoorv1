using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GreenDoorV1.Entities;
using GreenDoorV1.Services.Interfaces;

namespace GreenDoorV1.Services
{
    public class ReviewService : IReviewService
    {
        public void AddReview(long roomId)
        {
            throw new NotImplementedException();
        }

        public void DeleteReview(long? reviewId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Review>> GetAllReviews()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Review>> GetRoomReviews(long roomId)
        {
            throw new NotImplementedException();
        }
    }
}
