using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GreenDoorV1.Entities;
using GreenDoorV1.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GreenDoorV1.Services
{
    public class ReviewService : IReviewService
    {
        protected ApplicationDbContext Context { get; }

        public ReviewService(ApplicationDbContext context)
        {
            Context = context;
        }

        public async Task<Review> AddReview(Review review, string userId, long roomId) { 


            review.Room = await Context.Rooms.FindAsync(roomId);

            review.User = await Context.Users.SingleOrDefaultAsync(u => u.Id.Equals(userId));

            await Context.Reviews.AddAsync(review);

            await Context.SaveChangesAsync();

            return review;
        }

        public async Task<bool> DeleteReview(long? reviewId)
        {
            var deletable = await Context.Reviews.FindAsync(reviewId);
            if (deletable == null)
            {
                return false;
            }
            Context.Reviews.Remove(deletable);
            await Context.SaveChangesAsync();
            return true;

        }

        public async Task<IEnumerable<Review>> GetAllReviews()
        {
            var result = await Context.Reviews
                    .Include(r => r.Room)
                    .Include(r => r.User)
                        .ToListAsync();

            return result;
        }

        public async Task<IEnumerable<Review>> GetRoomReviews(long? roomId)
        {
            var roomReviews = await Context.Reviews
                .Include(r => r.Room)
                .Include(r => r.User)
                    .Where(r => r.Room.Id.Equals(roomId))
                        .ToListAsync();

            if (roomReviews.Count == 0)
            {
                return null;
            }

            return roomReviews;
        }
    }
}
